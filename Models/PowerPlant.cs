using System;
using System.ComponentModel.DataAnnotations;

namespace crud_registration_web_system.Models
{
    public class PowerPlant
    {
        public int Id { get; set; }
        public string ConsumerUnit { get; set; }
        public string Supplier { get; set; }
        public bool Active{get;set;} = true;
   }
}