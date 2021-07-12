using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace crud_registration_web_system.Models
{
    public class PowerPlantSupplier
    {
        public List<PowerPlant> PowerPlants { get; set; }
        public SelectList Suppliers{ get; set; }
        public SelectList ActivationStatuses{ get; set; }
        public string SupplierName { get; set; }
        public string SearchString { get; set; }
        public string ActivationStatus { get; set; }
    }
}