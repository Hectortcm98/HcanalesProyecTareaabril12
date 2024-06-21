using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DL
{
    public class AppDbContext : DbContext
    {
        
        public AppDbContext()
        {

        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public virtual DbSet<Tarea> Tareas { get; set; }

        public virtual DbSet<Estado> Estados { get; set; }

        
    }
}
