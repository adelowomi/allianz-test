using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Allianz.Models;
using Allianz.Models.InputModels;
using Allianz.Models.UtilitiModels;
using Allianz.Repositories.Interfaces;
using Allianz.Services.Interfaces;
using Allianz.Utilities;
using AutoMapper;
using LocalPay.Flutterwave;
using LocalPay.Flutterwave.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Allianz.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IBaseRepository<User> _userRepository;
        public readonly IBaseRepository<Application> _applicationRepository;
        public readonly IBaseRepository<Transaction> _transactionRepository;
        public readonly IMapper _mapper;
        private readonly IBaseRepository<BodyType> _bodyTypeRepository;
        private readonly IFlutterwavePayments _flutterwavePayments;
        private readonly AppSettings _appSettings;
        public ApplicationService(IBaseRepository<User> userRepository, IBaseRepository<Application> applicationRepository, IBaseRepository<Transaction> transactionRepository, IMapper mapper, IFlutterwavePayments flutterwavePayments, IOptions<AppSettings> appSettings)
        {
            _userRepository = userRepository;
            _applicationRepository = applicationRepository;
            _transactionRepository = transactionRepository;
            _mapper = mapper;
            _flutterwavePayments = flutterwavePayments;
            _appSettings = appSettings.Value;
        }

        public async Task<StandardResponse<Application>> CreateApplication(ApplicationModel model)
        {
            var user = await _userRepository.Query().Where(x => x.Email == model.User.Email).FirstOrDefaultAsync();
            if (user == null)
            {
                user = _mapper.Map<User>(model.User);
                user = _userRepository.CreateAndReturn(user);
            }

            var bodyType = await _bodyTypeRepository.Query().Where(x => x.Id == model.VehiclePurchase.BodyTypeId).FirstOrDefaultAsync();
            if (bodyType == null)
                return StandardResponse<Application>.NotFound("Body type not found");

            var application = _mapper.Map<Application>(model.VehiclePurchase);
            application.UserId = user.Id;
            application.Amount = bodyType.Amount;
            application.StatusId = (int)Statuses.PENDING;
            application.CreatedAt = DateTime.Now;

            application = _applicationRepository.CreateAndReturn(application);

            return StandardResponse<Application>.Ok(application);
        }

        public async Task<StandardResponse<IEnumerable<Application>>> GetApplications()
        {
            var applications = await _applicationRepository.Query().ToListAsync();
            return StandardResponse<IEnumerable<Application>>.Ok(applications);
        }

        public async Task<StandardResponse<Application>> GetApplication(int id)
        {
            var application = await _applicationRepository.Query().Where(x => x.Id == id).FirstOrDefaultAsync();
            return StandardResponse<Application>.Ok(application);
        }

        public async Task<StandardResponse<string>> InitializePayment(int applicationId)
        {
            var thisApplication = _applicationRepository.Query().FirstOrDefault(x => x.Id == applicationId);
            if (thisApplication == null)
                return StandardResponse<string>.NotFound("Application not found");

            var user = await _userRepository.Query().Where(x => x.Id == thisApplication.UserId).FirstOrDefaultAsync();

            var transaction = new Transaction
            {
                ApplicationId = thisApplication.Id,
                Amount = thisApplication.Amount.ToString(),
                Description = $"Payment for vehicle purchase with registration number {thisApplication.RegisterationNumber}",
                Title = "Payment for land",
                Reference = Guid.NewGuid().ToString(),
                StatusId = (int)Statuses.PENDING,
                Provider = "FLUTTERWAVE",
                Channel = "CARD",
            };

            // everything from here is wrapped in a transaction so everything is rolled back if anything fails
            using var transactionScope = _transactionRepository.BeginTransaction();
            try
            {
                _transactionRepository.Create(transaction);
                var paymentModel = new PaymentPayload
                {
                    amount = transaction.Amount,
                    currency = "NGN",
                    customer = new Customer
                    {
                        email = user.Email,
                        name = $"{user.FirstName} {user.LastName}",
                    },
                    customization = new Customization
                    {
                        title = transaction.Title,
                        description = transaction.Description
                        // Logo = "https://res.cloudinary.com/dqjxh7zki/image/upload/v1623778853/ownland/ownland-logo.png"
                    },
                    tx_ref = transaction.Reference,
                    redirect_url = $"{_appSettings.FrontEndBaseUrl}verify-payment",
                    payment_options = "card"
                };
                var paymentResponse = await _flutterwavePayments.InitiatePayment(paymentModel);
                if (paymentResponse.status != "success")
                    return StandardResponse<string>.Error("Error initializing payment");

                transactionScope.Commit();
                return StandardResponse<string>.Ok("Payment initialized successfully").AddData(paymentResponse.data.link);
            }
            catch (Exception e)
            {
                transactionScope.Rollback();
                return StandardResponse<string>.Error("Error initializing payment");
            }
        }

        public async Task<StandardResponse<Application>> VerifyPayment(string transactionReference, int transactionId)
        {
            var transaction = _transactionRepository.Query().Include(x => x.Application).FirstOrDefault(x => x.Reference == transactionReference);
            if (transaction == null)
                return StandardResponse<Application>.NotFound("Transaction not found");

            var paymentResponse = await _flutterwavePayments.ValidatePayment(transactionId);
            using var transactionScope = _transactionRepository.BeginTransaction();
            if (paymentResponse.status != "success")
            {
                transaction.StatusId = (int)Statuses.FAILED;
                return StandardResponse<Application>.Error("Error verifying payment");
            }
            transaction.StatusId = (int)Statuses.PAID;
            transaction.FlutterwaveId = paymentResponse.data.id;
            _transactionRepository.Update(transaction);

            transaction.Application.StatusId = (int)Statuses.PAID;

            transactionScope.Commit();
            return StandardResponse<Application>.Ok("Payment verified successfully").AddData(transaction.Application);
        }
    }
}