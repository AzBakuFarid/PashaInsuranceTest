using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PashaInsuranceTest.DTOs.ViewModels
{
    public class ServiceViewDto
    {
        public int Id { get; set; }
         public string Name { get; set; }
         public string StartsAt { get; set; }
         public string ValidTill { get; set; }
        public string Type { get; set; }
        public List<InnerData<int>> Groups { get; set; }
        public List<InnerData<int>> Spesifications { get; set; }

    }

}
