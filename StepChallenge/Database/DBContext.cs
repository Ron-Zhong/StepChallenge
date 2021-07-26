using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepChallenge.Database
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options)
          : base(options)
        { 
        }

        public DbSet<StepPoint> StepPoints { get; set; }
        public DbSet<UserStepPoint> UserStepPoints { get; set; }
    }
}
