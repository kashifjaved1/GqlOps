using GQLFilterSortProjection.EF.Entities;
using Microsoft.EntityFrameworkCore;

namespace GQLFilterSortProjection.EF
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Organization> Organizations { get; set; }
    }
}
