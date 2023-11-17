using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using BASE_RFID_TMS.Models;

namespace BASE_RFID_TMS
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class cClsCrudAccount
    {
        // public string Id { get; set; }
        public string Email { get; set; }
        //public string EmailConfirmed { get; set; }
        public string Password { get; set; }
        //public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        //public string PhoneNumberConfirmed { get; set; }
        //public string TwoFactorEnabled { get; set; }
        //public string LockoutEndDateUtc { get; set; }
        //public string LockoutEnabled { get; set; }
        //public string AccessFailedCount { get; set; }
        //public string UserName { get; set; }
        public string UserId { get; set; }
        // public string PasswordSalt { get; set; }
        //public string IsActive { get; set; }
        //public string CreateDate { get; set; }
        //public string IsDelete { get; set; }
        //public string DeleteDate { get; set; }
        //public string api_key { get; set; }
        public string Name { get; set; }
        //public string Phone { get; set; }
        //public string JobPosition { get; set; }
        //public string client_key { get; set; }
        //public string cust_id { get; set; }
        //public string cust_key_id { get; set; }
        public string IS_RS { get; set; }
    }
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
        internal Task<IdentityResult> CreateAsync(cClsCrudAccount user2, string password)
        {
            throw new NotImplementedException();
        }
    }
}
