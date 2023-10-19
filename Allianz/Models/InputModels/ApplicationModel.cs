using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Allianz.Models.InputModels
{
    public class ApplicationModel
    {
        public UserModel User { get; set; }
        public VehiclePurchaseModel VehiclePurchase { get; set; }
    }
}