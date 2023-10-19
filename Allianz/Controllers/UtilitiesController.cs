using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Allianz.Models;
using Allianz.Services.Interfaces;
using Allianz.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Allianz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilitiesController : ControllerBase
    {
        private readonly IUtilitiesService _utilitiesService;

        public UtilitiesController(IUtilitiesService utilitiesService)
        {
            _utilitiesService = utilitiesService;
        }

        [HttpGet("GetVehicleMakes")]
        public async Task<ActionResult<StandardResponse<VehicleMake>>> GetVehicleMakes()
        {
            var vehicleMakes = await _utilitiesService.GetVehicleMakes();
            return Ok(vehicleMakes);
        }

        [HttpGet("GetBodyTypes")]
        public async Task<ActionResult<StandardResponse<BodyType>>> GetBodyTypes()
        {
            var bodyTypes = await _utilitiesService.GetBodyTypes();
            return Ok(bodyTypes);
        }
    }
}