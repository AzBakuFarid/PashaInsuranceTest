using PashaInsuranceTest.DbEntities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PashaInsuranceTest.DbEntities.Models
{
    public class Group : IDbGenericLookup<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; } // bir az da bura duz qataq

        public virtual ICollection<Service> Services { get; set; } = new HashSet<Service>();
        public virtual ICollection<AppUser> Clients { get; set; } = new HashSet<AppUser>();

    }
}
