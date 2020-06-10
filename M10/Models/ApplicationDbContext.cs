using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace M10.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Lecture> Lecture { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<StudentScores> StudentScores { get; set; }
    }
}
