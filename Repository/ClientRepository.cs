using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PashaInsuranceTest.DbEntities.Models;
using PashaInsuranceTest.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PashaInsuranceTest.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly UserManager<AppUser> _userManager;

        public ClientRepository(UserManager<AppUser> userManager)
        {
            this._userManager = userManager;
        }
        public IdentityResult Create(AppUser userData, string password) 
        {
            if (userData == null) throw new ArgumentNullException(nameof(userData));
            var result = _userManager.CreateAsync(userData, password).Result;

            return result;
        }
        public IdentityResult Update(AppUser userData)
        {
            if (userData == null) throw new ArgumentNullException(nameof(userData));
            var result = _userManager.UpdateAsync(userData).Result;

            return result;
        }
        public List<AppUser> ListForGroup(int groupId) {
            if (groupId == 0) return new List<AppUser>(); // optimizasiya
            return _userManager.Users.Where(w => w.IsClient && w.GroupId == groupId).ToList();
        }
        public List<AppUser> ListByIds(IEnumerable<string> idList)
        {
            if (idList == null || idList.Count() == 0) return new List<AppUser>(); // optimizasiya
            return _userManager.Users.Where(w => w.IsClient && idList.Contains(w.Id)).ToList();
        }
        public List<AppUser> List() {
            return _userManager.Users.Include(i => i.Group).Where(w => w.IsClient).ToList();
        }
        public AppUser Find(string id)
        {
            if (string.IsNullOrEmpty(id?.Trim())) return null; // optimizasiya
            return _userManager.Users.Include(i => i.Group).FirstOrDefault(w => w.Id.Equals(id));
        }
        public IdentityResult Delete(AppUser user)
        {
            if (user.IsClient)
            {
                return _userManager.DeleteAsync(user).Result;
            }
            return IdentityResult.Failed(new IdentityError { Description = "User that is not client could not be deleted via this service" });
        }
    }
    ///////////////////////////////////////////////////////////////////////////////// 
    public interface IClientRepository
    {
        IdentityResult Create(AppUser userData, string password);
        IdentityResult Update(AppUser userData);
        IdentityResult Delete(AppUser user);
        List<AppUser> ListForGroup(int groupId);
        List<AppUser> ListByIds(IEnumerable<string> idList);
        List<AppUser> List();
        AppUser Find(string id);
    }

}
