using Common;
using DataAccess.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

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
                if(_db.Database.GetPendingMigrations().Count() > 0)//if Count >0 it means There are pending migrations
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception)
            {

            }
            if (_db.Roles.Any(x => x.Name == SD.Role_Admin)) return;
            _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();

            _userManager.CreateAsync(new IdentityUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
            }, "Admin123*").GetAwaiter().GetResult();

            //We retrieve that user and assign it to admin
            IdentityUser user = _db.Users.FirstOrDefault(u => u.Email == "admin@gmail.com");
            _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();

        }
    }

}






