using Microsoft.EntityFrameworkCore;
using ST10254164_LukeC_GR2_PROG7311_A2.Models;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Data
{
    public class applicationDBContext : DbContext
    {
        //-------------------START OF FILE----------------------------------//
        public applicationDBContext(DbContextOptions<applicationDBContext> options)
    : base(options)
        {

        }
        //establishing the connection to the database and creating the tables
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
                Username = "employee",
                Password = "employee101", // Storing plain text
                Role = "Employee"
            };
            modelBuilder.Entity<employeeModel>().HasData(employeeUser);

            var farmerUser1 = new employeeModel
            {
                Id = 2,
                Username = "Test",
                Password = "testrun",
                Role = "Farmer"
            };
            modelBuilder.Entity<employeeModel>().HasData(farmerUser1);

            var farmerUser2 = new employeeModel //dummy data for logging in as either a farmer or employee
            {
                Id = 3,
                Username = "farmer",
                Password = "123456",
                Role = "Farmer"
            };
            modelBuilder.Entity<employeeModel>().HasData(farmerUser2);

            modelBuilder.Entity<farmerModel>().HasData(
                new farmerModel
                {
                    Id = 1,
                    Name = "Diddly Squat",
                    Email = "DiddlySquat@gmail.com",
                    UserId = farmerUser1.Id
                },
                new farmerModel
                {
                    Id = 2,
                    Name = "Jordon Wine Estate",
                    Email = "JorWine@gmail.com",
                    UserId = farmerUser2.Id
                }
            );

            modelBuilder.Entity<productModel>().HasData(
                new productModel
                {
                    Id = 1,
                    Name = "Solar Panels",
                    Category = "Renewable energy",
                    ProductionDate = new DateTime(2023, 6, 1),
                    FarmerId = 1,
                    AddedDate = new DateTime(2024, 1, 1)
                },
                new productModel
                {
                    Id = 2,
                    Name = "Jam",
                    Category = "sugary food",
                    ProductionDate = new DateTime(2025, 2, 5),
                    FarmerId = 1,
                    AddedDate = new DateTime(2025, 3, 7)
                },
                new productModel
                {
                    Id = 3,
                    Name = "Olives",
                    Category = "Drupe",
                    ProductionDate = new DateTime(2016, 2, 3),
                    FarmerId = 2,
                    AddedDate = new DateTime(2016, 3, 2)
                },
                new productModel
                {
                    Id = 4,
                    Name = "Cara Cara",
                    Category = "Fruit",
                    ProductionDate = new DateTime(2025, 7, 15),
                    FarmerId = 2,
                    AddedDate = new DateTime(2025, 7, 20)
                }
            );
        }
    }
}
//-------------------END OF FILE----------------------------------//
