using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Allianz.Models
{
    public class Application
    {
        public int Id { get; set; }
        public int VehicleMakeId  { get; set; }
        public int VehicleModelId { get; set; }
        public int BodyTypeId { get; set; }
        public string RegisterationNumber { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}