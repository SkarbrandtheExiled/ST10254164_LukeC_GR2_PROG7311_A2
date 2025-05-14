using Microsoft.EntityFrameworkCore;
using ST10254164_LukeC_GR2_PROG7311_A2.Models;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Models
{
    public class applicationDBContext: DbContext
    {
        public applicationDBContext(DbContextOptions<applicationDBContext> options) : base(options)
        {

        }
        public DbSet<farmerModel> farmers { get; set; }
        public DbSet<employeeModel> employees { get; set; }
        public DbSet<productModel> products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the relationship between Product and Farmer
            modelBuilder.Entity<productModel>()
                .HasOne(p => p.Farmer)
                .WithMany(f => f.products)
                .HasForeignKey(p => p.FarmerId)
                .OnDelete(DeleteBehavior.Cascade); // Consider the delete behavior

            base.OnModelCreating(modelBuilder);
        }
    }
}

