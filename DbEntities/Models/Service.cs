using PashaInsuranceTest.DbEntities.Enums;
using PashaInsuranceTest.DbEntities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PashaInsuranceTest.DbEntities.Models
{
    public class Service : IDbGenericLookup<int>
    {
        public int Id { get; set; }
        [Required] [StringLength(255)] public string Name { get; set; }
        [Required] public DateTime StartsAt { get; set; }
        [Required] public DateTime ValidTill { get; set; }
        public ServiceTypeEnum Type { get; set; } // bir az duz qataq

        public virtual ICollection<Spesification> Spesifications { get; set; } = new HashSet<Spesification>();
        public virtual ICollection<Group> Groups { get; set; } = new HashSet<Group>();

    }
}
