using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HailStorm.Core.Models
{
    public class HailItem : Entity
    {
        public string Location { get; set; }
        public string State { get; set; }
        public string Date { get; set; }
        public string Magnitude { get; set; }
        public string StateName { get; set; }
    }
}
