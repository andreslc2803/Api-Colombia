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
    public class ApiColombiaContext : DbContext
    {
        public DbSet<Region> Regions { get; set; }

        public ApiColombiaContext(DbContextOptions<ApiColombiaContext> options)
            : base(options)
        {
        }

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

            base.OnModelCreating(modelBuilder);
        }
    }
}
