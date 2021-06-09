using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PashaInsuranceTest.DTOs.ViewModels
{
    public class InnerData<TKey>
    {
        public TKey Id { get; set; }
        public string Name { get; set; }
    }
}
