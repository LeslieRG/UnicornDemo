using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace UnicornDemo.Entities.Models
{
    public partial class UnicornDemoContext : DbContext
    {
        public UnicornDemoContext()
        {
        }

        public UnicornDemoContext(DbContextOptions<UnicornDemoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Contacto> Contacto { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=unicorntic.database.windows.net;Database=UnicornDemoApi;User ID=dominiotic;Password=unicornTic@;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contacto>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.IdUsuario, e.IdContacto })
                    .HasName("PK_Contactos");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.IdContactoNavigation)
                    .WithMany(p => p.ContactoIdContactoNavigation)
                    .HasForeignKey(d => d.IdContacto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contacto");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.ContactoIdUsuarioNavigation)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Usuario");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.Property(e => e.ColorFavorito)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Correo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FechaIngreso).HasColumnType("date");

                entity.Property(e => e.FechaNacimiento).HasColumnType("date");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NombreUsuario)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
