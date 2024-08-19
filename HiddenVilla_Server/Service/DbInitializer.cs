using DataAccess.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using System;

namespace HiddenVilla_Server.Service
{
    public class DbInitializer : IDbInitializer
    {
        private readonly AppilicationDbContext _db;

        private readonly Microsoft.AspNetCore.Identity.UserManager<IdentityUser> _userManager; //User manager
        private readonly Microsoft.AspNetCore.Identity.RoleManager<IdentityRole> _roleManager; //User Role

        public DbInitializer(AppilicationDbContext db, Microsoft.AspNetCore.Identity.RoleManager<IdentityRole> roleManager,
            Microsoft.AspNetCore.Identity.UserManager<IdentityUser> userManager)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public void Initalize()
        {
            try
            {

            }
            catch (Exception)
            {

            }
        }
    }
}
