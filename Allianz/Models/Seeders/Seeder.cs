using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Allianz.Models.Seeders
{
    public class Seeder
    {
        private readonly AppDbContext _context;
        public Seeder(AppDbContext context)
        {
            _context = context;
        }


        // add vehicle makes and models 
        public void SeedVehicleMakes()
        {
            var makes = new List<VehicleMake>
            {
                new VehicleMake { Name = "Toyota" },
                new VehicleMake { Name = "Honda" }
            };

            _context.VehicleMakes.AddRange(makes);
            _context.SaveChanges();
        }

        public void SeedVehicleModels()
        {
            var models = new List<VehicleModel>
            {
                new VehicleModel { Name = "Camry", VehicleMakeId = 1 },
                new VehicleModel { Name = "Corolla", VehicleMakeId = 1 },
                new VehicleModel { Name = "Accord", VehicleMakeId = 2 },
                new VehicleModel { Name = "Civic", VehicleMakeId = 2 }
            };

            _context.VehicleModels.AddRange(models);
            _context.SaveChanges();
        }

        public void SeedBodyTypes()
        {
            var bodyTypes = new List<BodyType>
            {
                new BodyType { Name = "Car", Amount = 15000 },
                new BodyType { Name = "SUV", Amount = 20000 },
                new BodyType { Name = "Truck", Amount = 100000 },
                new BodyType { Name = "Van", Amount = 20000 }
            };

            _context.BodyTypes.AddRange(bodyTypes);
            _context.SaveChanges();
        }

        public void SeedStatuses()
    {
        foreach (int app in Enum.GetValues(typeof(Statuses)))
        {
            if (!_context.Statuses.Any(sp => sp.Name == Enum.GetName(typeof(Statuses), app)))
            {

                var status = new Status
                {
                    Name = Enum.GetName(typeof(Statuses), app),
                    // Description = Enum.GetName(typeof(Statuses), app)
                };
                _context.Statuses.Add(status);
            }
        }
        _context.SaveChanges();
    }

        public void Seed()
        {
            SeedVehicleMakes();
            SeedVehicleModels();
            SeedBodyTypes();
            SeedStatuses();
        }
    }
}