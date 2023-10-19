using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Allianz.Models;
using Allianz.Repositories.Interfaces;
using Allianz.Services.Interfaces;
using Allianz.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Allianz.Services
{
    public class UtilitiesServices : IUtilitiesService
    {
        private readonly IBaseRepository<VehicleMake> _vehicleMakeRepository;
        private readonly IBaseRepository<BodyType> _bodyTypeRepository;

        public UtilitiesServices(IBaseRepository<VehicleMake> vehicleMakeRepository, IBaseRepository<BodyType> bodyTypeRepository)
        {
            _vehicleMakeRepository = vehicleMakeRepository;
            _bodyTypeRepository = bodyTypeRepository;
        }

        public async Task<StandardResponse<IEnumerable<VehicleMake>>> GetVehicleMakes()
        {
            var vehicleMakes = await _vehicleMakeRepository.Query().Include(x => x.Models).AsNoTrackingWithIdentityResolution().ToListAsync();
            return StandardResponse<IEnumerable<VehicleMake>>.Ok(vehicleMakes);
        }

        public async Task<StandardResponse<IEnumerable<BodyType>>> GetBodyTypes()
        {
            var bodyTypes = await _bodyTypeRepository.Query().ToListAsync();
            return StandardResponse<IEnumerable<BodyType>>.Ok(bodyTypes);
        }

    }
}