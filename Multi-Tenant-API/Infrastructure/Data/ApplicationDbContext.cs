using Microsoft.EntityFrameworkCore;
using Multi_Tenant_API.Domain;

namespace Multi_Tenant_API.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}

        public DbSet<Tenant> Tenant { get; set; }
        public DbSet<EntityA> EntityA { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
        
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tenant>(e => { 
            
             e.HasKey(e=>e.Id);
             e.Property(e=>e.Id).ValueGeneratedOnAdd();

             e.Property(e=>e.Name).IsRequired().HasMaxLength(128);

             e.Property(e => e.ApiKey).IsRequired().HasMaxLength(128);
             
             e.HasMany(s=>s.EntityA).WithOne(s=>s.Tenant).HasForeignKey(s=>s.TenantId);

            });

            modelBuilder.Entity<EntityA>(e => {

                e.HasKey(e => e.Id);
                e.Property(e => e.Id).ValueGeneratedOnAdd();

                e.Property(e => e.Name).IsRequired().HasMaxLength(128);

               

            });


        }
    }
}
