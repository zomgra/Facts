using IdentityModel;
using IdentityServer.Data.Interfaces;
using IdentityServer.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IdentityServer.Data
{
    public class DbInitiliazer : IDbInitilizer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        private const string _adminPassword = "123qwe";
        private const string _customerPassword = "123qwe";

        public DbInitiliazer(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Initialize()
        {
            if ((await _roleManager.FindByNameAsync(IdentityConfiguration.Admin)) != null)
                return;
            
            await _roleManager.CreateAsync(new ApplicationRole(IdentityConfiguration.Admin));
            await _roleManager.CreateAsync(new ApplicationRole(IdentityConfiguration.Customer));

            ApplicationUser admin = new ApplicationUser()
            {
                UserName = "admin",
                Email = "admin@dev.com",
                EmailConfirmed = true,
                FirstName = "Mykyta",
                LastName = "Serdiuk",
            };
            await _userManager.CreateAsync(admin, _adminPassword);
            await _userManager.AddToRoleAsync(admin, IdentityConfiguration.Admin);
            
            var t = await _userManager.AddClaimsAsync(admin, new List<Claim>()
            {
                new Claim(JwtClaimTypes.Name, admin.GetFullName()),
                new Claim(JwtClaimTypes.Email, admin.Email),
                new Claim(JwtClaimTypes.GivenName, admin.FirstName),
                new Claim(JwtClaimTypes.FamilyName, admin.LastName),
                new Claim(JwtClaimTypes.Role, IdentityConfiguration.Admin),
            });

            ApplicationUser customer = new ApplicationUser()
            {
                UserName = "customer",
                Email = "customer@dev.com",
                EmailConfirmed = true,
                FirstName = "Ben",
                LastName = "Ten",
            };
            await _userManager.CreateAsync(customer, _customerPassword);
            await _userManager.AddToRoleAsync(customer, IdentityConfiguration.Customer);

            var t2 = await _userManager.AddClaimsAsync(customer, new List<Claim>()
            {
                new Claim(JwtClaimTypes.Name, customer.GetFullName()),
                new Claim(JwtClaimTypes.Email, customer.Email),
                new Claim(JwtClaimTypes.GivenName, customer.FirstName),
                new Claim(JwtClaimTypes.FamilyName, customer.LastName),
                new Claim(JwtClaimTypes.Role, IdentityConfiguration.Customer),
            });
        }
    }
}
