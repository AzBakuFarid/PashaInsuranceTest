using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PashaInsuranceTest.DbEntities.Models
{
    public class AppUser : IdentityUser
    {
        public int? GroupId { get; set; }
        public virtual Group Group { get; set; }

        [NotMapped] public bool IsClient => GroupId.HasValue;  
    }
}
