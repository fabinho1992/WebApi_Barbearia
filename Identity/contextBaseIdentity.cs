using Identity.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity
{
    public class ContextBaseIdentity : IdentityDbContext<Usuario>
    {
        public ContextBaseIdentity(DbContextOptions<ContextBaseIdentity> options) : base(options) { }

        
        
    }
}
