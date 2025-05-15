using Microsoft.EntityFrameworkCore;
using ST10254164_LukeC_GR2_PROG7311_A2.Models;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Data
{
    public class applicationDBContext: DbContext
    {
        public applicationDBContext(DbContextOptions<applicationDBContext> options) : base(options)
        {

        }
        public DbSet<farmerModel> farmers { get; set; }
        public DbSet<employeeModel> employees { get; set; }
        public DbSet<productModel> products { get; set; }
    }
    }

