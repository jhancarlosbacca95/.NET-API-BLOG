using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace APIBLOG.Models;

public partial class ApiblogContext : DbContext
{
    public ApiblogContext()
    {
    }

    public ApiblogContext(DbContextOptions<ApiblogContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Etiqueta> Etiquetas { get; set; }

    public virtual DbSet<Comentario> Comentarios { get; set; }

    public virtual DbSet<HistorialRefreshToken> HistorialRefreshTokens { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Etiqueta>(entity =>
        {
            entity.HasKey(e => e.IdEtiqueta).HasName("PK__ETIQUETA__A3C02A1086117A8A");

            entity.ToTable("ETIQUETA");

            entity.Property(e => e.Nombre).HasMaxLength(50);

            entity.HasMany(d => d.IdPosts).WithMany(p => p.IdEtiqueta)
                .UsingEntity<Dictionary<string, object>>(
                    "PostEtiqueta",
                    r => r.HasOne<Post>().WithMany()
                        .HasForeignKey("IdPost")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__POST_ETI__IdPos__693CA210"),
                    l => l.HasOne<Etiqueta>().WithMany()
                        .HasForeignKey("IdEtiqueta")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__POST_ETI__IdEti__68487DD7"),
                    j =>
                    {
                        j.HasKey("IdEtiqueta", "IdPost").HasName("PK__POST_ETI__6C4DE1C4DCBE7F07");
                        j.ToTable("POST_ETIQUETA");
                    });
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__CATEGORI__DDBEFBF9A4E0BB96");
            entity.ToTable("CATEGORIA");

            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e=>e.Descripcion).HasMaxLength(150);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__ROL__DDBEFBF9A4E0BB74");
            entity.ToTable("ROL");

            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<Comentario>(entity =>
        {
            entity.HasKey(e => e.IdComentario).HasName("PK__COMENTAR__DDBEFBF9A4E0BB96");

            entity.ToTable("COMENTARIO");

            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

            entity.HasOne(d => d.IdPostNavigation).WithMany(p => p.Comentarios)
                .HasForeignKey(d => d.IdPost)
                .HasConstraintName("FK__COMENTARI__IdPos__656C112C");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Comentarios)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__COMENTARI__IdUsu__6477ECF3");
        });

        modelBuilder.Entity<HistorialRefreshToken>(entity =>
        {
            entity.HasKey(e => e.IdHistorialToken).HasName("PK__HISTORIA__03DC48A56DB735F0");

            entity.ToTable("HISTORIAL_REFRESH_TOKEN");

            entity.Property(e => e.EsActivo).HasComputedColumnSql("(case when [FechaExpiracion]<getdate() then CONVERT([bit],(0)) else CONVERT([bit],(1)) end)", false);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaExpiracion).HasColumnType("datetime");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.HistorialRefreshTokens)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__HISTORIAL__IdUsu__6FE99F9F");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.IdPost).HasName("PK__POST__F8DCBD4D44C192C8");

            entity.ToTable("POST");

            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.Titulo).HasMaxLength(100);

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Posts)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__POST__IdUsuario__619B8048");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Posts)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK__POST__IdCategoria__619B8050");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__USUARIO__5B65BF978CD2A107");

            entity.ToTable("USUARIO");

            entity.Property(e => e.IdUsuario).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Contraseña).HasMaxLength(20);
            entity.Property(e => e.CorreoElectronico).HasMaxLength(100);
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(50)
                .HasColumnName("Usuario");
            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__USUARIO__IdRol__619B8048");

        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
