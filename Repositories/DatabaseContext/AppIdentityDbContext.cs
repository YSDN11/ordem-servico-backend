using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ordem_servico_backend.Models;

namespace ordem_servico_backend.Repositories.DatabaseContext
{
    public class AppIdentityDbContext
        : IdentityDbContext<AppUser, IdentityRole<int>, int>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
            : base(options)
        {
        }
    }
}
