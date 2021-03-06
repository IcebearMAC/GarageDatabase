﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GarageDatabse.Models
{
    public class ParkingSpot
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int ParkingPlace { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}