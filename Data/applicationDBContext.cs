using Microsoft.EntityFrameworkCore;
using ST10254164_LukeC_GR2_PROG7311_A2.Models;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Data
{
    public class applicationDBContext : DbContext
    {
        public applicationDBContext(DbContextOptions<applicationDBContext> options)
    : base(options)
        {
        }

        public DbSet<employeeModel> Users { get; set; }
        public DbSet<farmerModel> Farmers { get; set; }
        public DbSet<productModel> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<employeeModel>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<employeeModel>()
                .HasOne(u => u.FarmerProfile)
                .WithOne(f => f.employeeModel)
                .HasForeignKey<farmerModel>(f => f.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<farmerModel>()
                .HasMany(f => f.Products)
                .WithOne(p => p.Farmer)
                .HasForeignKey(p => p.FarmerId)
                .OnDelete(DeleteBehavior.Cascade);

            SeedInitialData(modelBuilder);
        }


        private void SeedInitialData(ModelBuilder modelBuilder)
        {
            var employeeUser = new employeeModel
            {
                Id = 1,
                Username = "employee01",
                PasswordHash = "EmpP@ss123", // Storing plain text
                Role = "Employee"
            };
            modelBuilder.Entity<employeeModel>().HasData(employeeUser);

            var farmerUser1 = new employeeModel
            {
                Id = 2,
                Username = "farmerJohn",
                PasswordHash = "FarmP@ss123",
                Role = "Farmer"
            };
            modelBuilder.Entity<employeeModel>().HasData(farmerUser1);

            var farmerUser2 = new employeeModel
            {
                Id = 3,
                Username = "farmerJane",
                PasswordHash = "FarmP@ss456",
                Role = "Farmer"
            };
            modelBuilder.Entity<employeeModel>().HasData(farmerUser2);

            modelBuilder.Entity<farmerModel>().HasData(
                new farmerModel
                {
                    Id = 1,
                    Name = "John's Sunny Acres",
                    ContactDetails = "john@sunnyacres.com, 555-0101",
                    UserId = farmerUser1.Id
                },
                new farmerModel
                {
                    Id = 2,
                    Name = "Jane's Green Fields",
                    ContactDetails = "jane@greenfields.org, 555-0202",
                    UserId = farmerUser2.Id
                }
            );

            modelBuilder.Entity<productModel>().HasData(
                new productModel
                {
                    Id = 1,
                    Name = "Solar Panel",
                    Category = "Outdoor",
                    ProductionDate = new DateTime(2025, 4, 15),
                    FarmerId = 1,
                    AddedDate = new DateTime(2025, 5, 2)
                },
                new productModel
                {
                    Id = 2,
                    Name = "Free-Range Eggs",
                    Category = "Poultry",
                    ProductionDate = new DateTime(2025, 5, 1),
                    FarmerId = 1,
                    AddedDate = new DateTime(2025, 5, 7)
                },
                new productModel
                {
                    Id = 3,
                    Name = "Artisan Bread",
                    Category = "Bakery",
                    ProductionDate = new DateTime(2025, 5, 5),
                    FarmerId = 2,
                    AddedDate = new DateTime(2025, 5, 10)
                },
                new productModel
                {
                    Id = 4,
                    Name = "Fresh Strawberries",
                    Category = "Fruit",
                    ProductionDate = new DateTime(2025, 5, 3),
                    FarmerId = 2,
                    AddedDate = new DateTime(2025, 5, 9)
                }
            );
        }
    }
}

