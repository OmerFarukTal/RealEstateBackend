using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RealEstate.Api.Entities;

namespace RealEstate.Api.Context
{
    public class RealEstateContext : IdentityDbContext<IdentityUser>
    {
        public RealEstateContext(DbContextOptions<RealEstateContext> options) : base(options) { 
        }

        public DbSet<Currencies> Currencies { get; set; }
        public DbSet<Properties> Properties { get; set; }
        public DbSet<PropertyStatuses> PropertyStatuses { get; set; }
        public DbSet<PropertyTypes> PropertyTypes { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Translations> Translations { get; set; }
        public DbSet<Users> Users { get; set; }



    }
}
