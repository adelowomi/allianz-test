using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Allianz.Models
{
    public class VehicleModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int VehicleMakeId { get; set; }
        // [ForeignKey(nameof(VehicleMakeId))]
        // public VehicleMake VehicleMake { get; set; }
    }
}