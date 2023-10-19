using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Allianz.Models;
using Allianz.Models.InputModels;
using Allianz.Utilities;

namespace Allianz.Services.Interfaces
{
    public interface IApplicationService
    {
        Task<StandardResponse<Application>> CreateApplication(ApplicationModel model);
        Task<StandardResponse<Application>> GetApplication(int id);
        Task<StandardResponse<IEnumerable<Application>>> GetApplications();
        Task<StandardResponse<string>> InitializePayment(int applicationId);
        Task<StandardResponse<Application>> VerifyPayment(string transactionReference, int transactionId);
    }
}