using CarWash.Models.Cars;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CarWash.Models
{
    public class CarWashContext : DbContext
    {
        public DbSet<CarInfo> CarInfos { get; set; }
    }
}