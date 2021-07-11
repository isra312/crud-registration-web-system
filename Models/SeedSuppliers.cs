using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace crud_registration_web_system.Models
{
    /* 
     *
     *
     *
     */
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MvcPowerPlantContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MvcPowerPlantContext>>()))
            {
                // Look for any supplier.
                if (context.Supplier.Any())
                {
                    return;   // DB has been seeded
                }

                context.Supplier.AddRange(
                    new Supplier
                    {
                        Name = "SOLARIAN"
                    },
                    new Supplier
                    {
                        Name = "FUTURA"
                    },
                    new Supplier
                    {
                        Name = "CENTRAL GERADORA FAZENDA MODELO"
                    },
                    new Supplier
                    {
                        Name = "NOVA MUNDO"
                    },
                    new Supplier
                    {
                        Name = "SOLARE"
                    },
                    new Supplier
                    {
                        Name = "UNISOL"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}