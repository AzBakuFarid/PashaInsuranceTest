using PashaInsuranceTest.DbEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PashaInsuranceTest.Helpers
{
    public class MockDataWrapper
    {
        public List<Group> Groups { get; set; }
        public List<Service> Services { get; set; }
        public List<Spesification> Spesifications { get; set; }
        public Dictionary<string, string[]> ServiceSpesifications { get; set; }
        public Dictionary<string, string[]> GroupServices { get; set; }
    }
}
