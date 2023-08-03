using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Models.Identity
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole(string role) : base(role)
        {

        }
        public ApplicationRole() : base()
        {

        }
    }
}
