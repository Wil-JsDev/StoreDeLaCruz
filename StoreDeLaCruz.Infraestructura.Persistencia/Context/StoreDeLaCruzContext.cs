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
        public StoreDeLaCruzContext(DbContextOptions<StoreDeLaCruzContext> options) : base(options)
        {
        }

        #region Models
        public DbSet<Nota> Notas { get; set; }

        public DbSet<Folder> Folders { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Primary Key
            modelBuilder.Entity<Nota>()
                    .HasKey(n => n.NotaId);

            modelBuilder.Entity<Folder>()
                .HasKey(f => f.FolderId);
            #endregion

            #region ForeignKey
            modelBuilder.Entity<Folder>()
                .HasMany(f => f.Notas)
                .WithOne(f => f.Folder)
                .HasForeignKey(f => f.FolderId)
                .IsRequired()
                .HasConstraintName("FK_FolderId");
            
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
