using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PashaInsuranceTest.DTOs.UpdateModels
{
    public class AddClientToGroupDto
    {
        public string Client { get; set; }
        public int Group { get; set; }
    }
}
