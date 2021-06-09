using PashaInsuranceTest.DbEntities.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace PashaInsuranceTest.DbEntities.Models
{
    public class Spesification : IDbGenericLookup<int>
    {
        public int Id { get; set; }
        [Required] [StringLength(255)] public string Name { get; set; }

        public virtual ICollection<Service> Services { get; set; } = new HashSet<Service>();
    }
}
