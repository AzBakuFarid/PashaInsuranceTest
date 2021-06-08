using PashaInsuranceTest.DbEntities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PashaInsuranceTest.DbEntities.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartsAt { get; set; }
        public DateTime ValidTill { get; set; }
        public ServiceTypeEnum Type { get; set; } // bir az duz qataq

        public virtual ICollection<Spesification> Spesifications { get; set; } = new HashSet<Spesification>();
        public virtual ICollection<Group> Groups { get; set; } = new HashSet<Group>();

    }
}
