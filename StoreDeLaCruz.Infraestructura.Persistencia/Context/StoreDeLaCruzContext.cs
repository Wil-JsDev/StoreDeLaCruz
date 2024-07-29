using Microsoft.EntityFrameworkCore;
using StoreDeLaCruz.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDeLaCruz.Infraestructura.Persistencia.Context
{
    public class StoreDeLaCruzContext : DbContext
    {
        public StoreDeLaCruzContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Nota> Notas { get; set; }

        public DbSet<Folder> Folders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Primary Key
            modelBuilder.Entity<Nota>()
                    .HasKey(n => n.NotaId);

            modelBuilder.Entity<Folder>()
                .HasKey(f => f.FolderId);
            #endregion

            #region ForeignKey
            modelBuilder.Entity<Nota>()
                .HasOne<Folder>()
                .WithMany()
                .HasForeignKey(f => f.FolderId);
            #endregion

            #region Nota

            modelBuilder.Entity<Nota>()
                .Property(n => n.PrioridadTarea)
                .IsRequired();
                
            modelBuilder.Entity<Nota>()
                .Property(n => n.Contenido)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Nota>()
                .Property(n => n.Titulo)
                .IsRequired()
                .HasMaxLength(50);
            #endregion

            #region Folder
            modelBuilder.Entity<Folder>()
                .Property(f => f.Name)
                .IsRequired()
                .HasMaxLength(25);
            #endregion

        }
    }
}
