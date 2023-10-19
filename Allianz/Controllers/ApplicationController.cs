using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Allianz.Models;
using Allianz.Models.InputModels;
using Allianz.Services.Interfaces;
using Allianz.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Allianz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : StandardControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }


        [HttpPost("CreateApplication")]
        public async Task<ActionResult<StandardResponse<Application>>> CreateApplication(ApplicationModel model)
        {
            var application = await _applicationService.CreateApplication(model);
            return Ok(application);
        }

        [HttpGet("GetApplication")]
        public async Task<ActionResult<StandardResponse<Application>>> GetApplication(int id)
        {
            var application = await _applicationService.GetApplication(id);
            return Ok(application);
        }


        [HttpGet("GetApplications")]
        public async Task<ActionResult<StandardResponse<IEnumerable<Application>>>> GetApplications()
        {
            var applications = await _applicationService.GetApplications();
            return Ok(applications);
        }

        [HttpGet("InitializePayment")]
        public async Task<ActionResult<StandardResponse<string>>> InitializePayment(int applicationId)
        {
            var paymentUrl = await _applicationService.InitializePayment(applicationId);
            return Ok(paymentUrl);
        }

        [HttpGet("VerifyPayment/{transactionId}/{reference}")]
        public async Task<ActionResult<StandardResponse<string>>> VerifyPayment(int transactionId, string reference)
        {
            var paymentUrl = await _applicationService.VerifyPayment(reference, transactionId);
            return Ok(paymentUrl);
        }
    }
}