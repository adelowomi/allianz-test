using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Allianz.Models
{
    public class BodyType
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Amount { get; set; }
    }
}