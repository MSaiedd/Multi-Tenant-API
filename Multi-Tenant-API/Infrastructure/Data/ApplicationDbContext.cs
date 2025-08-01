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


            modelBuilder.Entity<Tenant>().HasData(
          new Tenant
          {
              Id = 1,
              Name = "Tenant A",
              ApiKey = "APIKEY_TENANT_A"
          },
          new Tenant
          {
              Id = 2,
              Name = "Tenant B",
              ApiKey = "APIKEY_TENANT_B"
          }
      );

            // Seed EntityA
            modelBuilder.Entity<EntityA>().HasData(
                new EntityA
                {
                    Id = 1,
                    Name = "Entity A1",
                    TenantId = 1
                },
                new EntityA
                {
                    Id = 2,
                    Name = "Entity A2",
                    TenantId = 1
                },
                new EntityA
                {
                    Id = 3,
                    Name = "Entity A3",
                    TenantId = 2
                }
            );

        }
    }
}
