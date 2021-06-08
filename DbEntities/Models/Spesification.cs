using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PashaInsuranceTest.DbEntities.Models
{
    public class Spesification
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Service> Services { get; set; } = new HashSet<Service>();
    }
}
