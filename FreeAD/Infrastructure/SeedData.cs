using FreeAD.Infrastructure.Tasks;
using FreeAD.Models;
using FreeADPortable.Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;

namespace FreeAD.Infrastructure
{
    public class SeedData : IRunAtStartup
    {
        private readonly ApplicationDbContext _context;

        public SeedData(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Execute()
        {
            var role1 = _context.Roles.FirstOrDefault(u => u.Id == "maintenance") ??
                        _context.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole { Id = "maintenance", Name= "maintenance" });
            _context.SaveChanges();
            var userEmail = "arthur.lv6@gmail.com";
            var user = _context.Users.FirstOrDefault(u => u.UserName == userEmail);
            if (user == null)
            {
                if (!(_context.Users.Any(u => u.UserName == userEmail)))
                {
                    var userStore = new UserStore<ApplicationUser>(_context);
                    var userManager = new UserManager<ApplicationUser>(userStore);
                    var userToInsert = new ApplicationUser { UserName = userEmail, Email = userEmail, PhoneNumber = "0210578463" };
                    userManager.Create(userToInsert, "tomhack");
                    _context.SaveChanges();
                }
            }
            //languge
            if( _context.Languages.FirstOrDefault(u => u.Name == "English")==null)
            {
                var lanague = new Language { Name = "English" };
                lanague.Categories.Add(new Category { Name = "Company Recruit", Order = 1 });
                lanague.Categories.Add(new Category { Name = "Company Introduction", Order = 2 });
                lanague.Categories.Add(new Category { Name = "Rooms for Rent", Order = 4 });
                lanague.Categories.Add(new Category { Name = "Products", Order = 5 });
                lanague.Categories.Add(new Category { Name = "Personal introduction", Order = 3 });
                lanague.Categories.Add(new Category { Name = "All",Order=0 });
                _context.Languages.Add(lanague);
            }
            _context.SaveChanges();
        }
    }
}