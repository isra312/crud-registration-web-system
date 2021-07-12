using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace crud_registration_web_system.Models
{
    public class PowerPlant
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Fornecedor")]
        public string Supplier { get; set; }
        
        [Required]
        [Display(Name = "UC da Usina")]
        public string ConsumerUnit { get; set; }
        
        [Required]
        [Display(Name = "Ativo")]
        public bool Active{get;set;} = true;
   }
}