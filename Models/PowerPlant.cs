using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace crud_registration_web_system.Models
{
    public class PowerPlant
    {
        public int Id { get; set; }

        [Display(Name = "Fornecedor")]
        public string Supplier { get; set; }
        // public IEnumerable<SelectListItem> Suppliers { get; set; }
        
        [Display(Name = "UC da Usina")]
        public string ConsumerUnit { get; set; }
        

        [Display(Name = "Ativo")]
        public bool Active{get;set;} = true;
   }
}