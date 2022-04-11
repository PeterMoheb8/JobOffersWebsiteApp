using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using WebApplication1.Models;

[assembly: OwinStartupAttribute(typeof(WebApplication1.Startup))]
namespace WebApplication1
{
    public partial class Startup
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateDefaultRolesAndUsers();
        }

        public void CreateDefaultRolesAndUsers()
        {

            var rolManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            IdentityRole role= new IdentityRole();

            if (!rolManager.RoleExists("Admins"))
            {
                role.Name = "Admins";
                rolManager.Create(role);
                ApplicationUser user = new ApplicationUser();
                user.UserName = "Peter";
                user.Email = "petermoheb72@gmail.com";
                var Check = userManager.Create(user, "PeterMoheb123@");
                if (Check.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admins");
                }

            }

        }
    }
}
