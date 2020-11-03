using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PS.Template.Domain.Entities;

namespace PS.Template.AccessData.Context
{
    public partial class DbAutenticacionContext : DbContext
    {
        public DbAutenticacionContext()
        {
        }

        public DbAutenticacionContext(DbContextOptions<DbAutenticacionContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cuenta> Cuenta { get; set; }
        public virtual DbSet<Estado> Estado { get; set; }
        public virtual DbSet<TipoCuenta> TipoCuenta { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source= DESKTOP-Q70Q68R\\SQLEXPRESS; Initial Catalog= DbAutenticacion; user=Lean; password=123123; Integrated Security= true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cuenta>(entity =>
            {
                entity.HasKey(e => e.IdCuenta)
                    .HasName("PK__Cuenta__D41FD70632A42B05");

                entity.Property(e => e.Contraseña).IsRequired();

                entity.Property(e => e.FecAlta).HasColumnType("datetime");

                entity.Property(e => e.Mail)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.Cuenta)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cuenta_Estado");

                entity.HasOne(d => d.IdTipoCuentaNavigation)
                    .WithMany(p => p.Cuenta)
                    .HasForeignKey(d => d.IdTipoCuenta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cuenta_TipoCuenta");
            });

            modelBuilder.Entity<Estado>(entity =>
            {
                entity.HasKey(e => e.IdEstado)
                    .HasName("PK__Estado__FBB0EDC12929C298");

                entity.Property(e => e.DescEstado)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.HasData(new Estado
                {
                    IdEstado=1,
                    DescEstado="Alta"
                });
                entity.HasData(new Estado
                {
                    IdEstado = 2,
                    DescEstado = "Baja"
                });
            });

            modelBuilder.Entity<TipoCuenta>(entity =>
            {
                entity.HasKey(e => e.IdTipoCuenta)
                    .HasName("PK__TipoCuen__9CCA28505748AB74");

                entity.Property(e => e.DescTipCuenta)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.HasData(new TipoCuenta
                {
                    IdTipoCuenta = 1,
                    DescTipCuenta = "Usuario"
                });
                entity.HasData(new TipoCuenta
                {
                    IdTipoCuenta = 2,
                    DescTipCuenta = "Admin"
                });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
