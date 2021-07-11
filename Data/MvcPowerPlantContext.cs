using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using crud_registration_web_system.Models;

    public class MvcPowerPlantContext : DbContext
    {
        public MvcPowerPlantContext (DbContextOptions<MvcPowerPlantContext> options)
            : base(options)
        {
        }

        public DbSet<crud_registration_web_system.Models.PowerPlant> PowerPlant { get; set; }
        public DbSet<crud_registration_web_system.Models.Supplier> Supplier { get; set; }
    }
