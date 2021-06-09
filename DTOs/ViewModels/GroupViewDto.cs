using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PashaInsuranceTest.DTOs.ViewModels
{
    public class GroupViewDto
    {
        public int Id { get; set; }
         public string Name { get; set; }
        public decimal Amount { get; set; }
        public List<InnerData<int>> Services { get; set; } 
        public List<InnerData<string>> Clients { get; set; }  

    }
}
