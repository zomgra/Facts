using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Models.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string GetFullName()
        {
            return string.Concat(FirstName, " ", LastName);
        }
    }
}
