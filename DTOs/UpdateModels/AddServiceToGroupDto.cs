using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PashaInsuranceTest.DTOs.UpdateModels
{
    public class AddServiceToGroupDto
    {
        public int Service { get; set; }
        public int Group { get; set; }
    }
}
