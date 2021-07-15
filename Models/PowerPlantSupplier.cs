using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace crud_registration_web_system.Models
{
    public class PowerPlantSupplier
    {
        public int Id{get; set;}
        public List<PowerPlant> PowerPlants { get; set; }
        public SelectList Suppliers{ get; set; }

        [Required]
        [Display(Name = "UC da Usina")]
        public string ConsumerUnit { get; set; }

        [Required]
        [Display(Name = "Fornecedor")]
        public string Supplier { get; set; }

        [Display(Name = "Ativo")]
        public bool Active { get; set; }
    }
}