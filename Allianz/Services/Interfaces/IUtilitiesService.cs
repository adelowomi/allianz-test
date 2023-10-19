using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Allianz.Models;
using Allianz.Utilities;

namespace Allianz.Services.Interfaces
{
    public interface IUtilitiesService
    {
        Task<StandardResponse<IEnumerable<VehicleMake>>> GetVehicleMakes();
        Task<StandardResponse<IEnumerable<BodyType>>> GetBodyTypes();
    }
}