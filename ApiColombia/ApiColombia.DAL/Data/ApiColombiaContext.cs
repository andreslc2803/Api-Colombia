using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApiColombia.Entities.Entities;

namespace ApiColombia.DAL.Data
{
    /// <summary>
    /// Contexto de Entity Framework Core para la base de datos de ApiColombia.
    /// Define los DbSet para las entidades Region y User y configura sus propiedades mediante Fluent API.
    /// </summary>
    public class ApiColombiaContext : DbContext
    {
        public DbSet<Region> Region { get; set; }
        public DbSet<User> User { get; set; }

        public ApiColombiaContext(DbContextOptions<ApiColombiaContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Configuración de las entidades mediante Fluent API
        /// Se definen llaves primarias, propiedades requeridas y longitudes máximas
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Region>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Id)
                      .ValueGeneratedOnAdd();

                entity.Property(x => x.Name)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(x => x.Description)
                      .HasMaxLength(500);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Username)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(x => x.PasswordHash)
                      .IsRequired()
                      .HasMaxLength(500);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
