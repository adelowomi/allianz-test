using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Allianz.Models
{
    public class VehicleMake
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public virtual ICollection<VehicleModel> Models { get; set; }
    }
}