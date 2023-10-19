using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Allianz.Models.InputModels
{
    public class VehiclePurchaseModel
    {
        public int VehicleMakeId  { get; set; }
        public int VehicleModelId { get; set; }
        public int BodyTypeId { get; set; }
        public string RegisterationNumber { get; set; }
    }
}