using EnvironmentCrime.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnvironmentCrime.Models
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

        public DbSet<Errand> Errands { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<ErrandStatus> ErrandStatuses { get; set; }

        public DbSet<Sequence> Sequences { get; set; }

        public DbSet<Picture> Pictures { get; set; }

        public DbSet<Sample> Samples { get; set; }

       

    }
}
