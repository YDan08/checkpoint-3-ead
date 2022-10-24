using eadCp3.Models;
using Microsoft.EntityFrameworkCore;

namespace eadCp3.Config
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options): base(options)
        {
           // Database.EnsureCreated();
        }

        public DbSet<Paciente> Paciente { get; set; }
    }
}
