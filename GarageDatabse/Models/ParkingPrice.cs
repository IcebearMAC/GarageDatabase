using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GarageDatabse.Models
{
    public class ParkingPrice
    {
        public int ID { get; set; }
        public int ParkingPrices { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}