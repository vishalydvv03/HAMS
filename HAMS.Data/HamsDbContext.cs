using HAMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.Data
{
    public class HamsDbContext : DbContext 
    {
        public HamsDbContext(DbContextOptions<HamsDbContext> options) : base(options) 
        { 

        }

        protected override void ConfigureConventions(ModelConfigurationBuilder cb)
        {
            cb.Properties<Enum>().HaveConversion<string>();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Department> Departements { get; set; }
    }
}
