using Microsoft.AspNetCore.Identity;

namespace AmdarisInternship.Domain.Auth
{
    public class Role : IdentityRole<int>
    {
        public Role(string name) : base(name)
        {

        }
    }
}
