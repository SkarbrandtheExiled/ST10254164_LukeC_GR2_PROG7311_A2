using Microsoft.EntityFrameworkCore;
using ST10254164_LukeC_GR2_PROG7311_A2.Models;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Models
{
    public class applicationDBContext: DbContext
    {
        public applicationDBContext(DbContextOptions<applicationDBContext> options) : base(options)
        {

        }
        public DbSet<employeeModel> products { get; set; }
        public DbSet<farmerModel> farmers { get; set; }
    }
    }

