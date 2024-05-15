﻿// <auto-generated />
using System;
using APIBLOG.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace APIBLOG.Migrations
{
    [DbContext(typeof(ApiblogContext))]
    [Migration("20240515194604_Categoria-Rol-Etiqueta")]
    partial class CategoriaRolEtiqueta
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("APIBLOG.Models.Categoria", b =>
                {
                    b.Property<int>("IdCategoria")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCategoria"));

                    b.Property<string>("Descripcion")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime");

                    b.Property<string>("Nombre")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("IdCategoria")
                        .HasName("PK__CATEGORI__DDBEFBF9A4E0BB96");

                    b.ToTable("CATEGORIA", (string)null);
                });

            modelBuilder.Entity("APIBLOG.Models.Comentario", b =>
                {
                    b.Property<int>("IdComentario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdComentario"));

                    b.Property<bool?>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Contenido")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("FechaCreacion")
                        .HasColumnType("datetime");

                    b.Property<int?>("IdPost")
                        .HasColumnType("int");

                    b.Property<Guid?>("IdUsuario")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("IdComentario")
                        .HasName("PK__COMENTAR__DDBEFBF9A4E0BB96");

                    b.HasIndex("IdPost");

                    b.HasIndex("IdUsuario");

                    b.ToTable("COMENTARIO", (string)null);
                });

            modelBuilder.Entity("APIBLOG.Models.Etiqueta", b =>
                {
                    b.Property<int>("IdEtiqueta")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdEtiqueta"));

                    b.Property<string>("Nombre")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("IdEtiqueta")
                        .HasName("PK__ETIQUETA__A3C02A1086117A8A");

                    b.ToTable("ETIQUETA", (string)null);
                });

            modelBuilder.Entity("APIBLOG.Models.HistorialRefreshToken", b =>
                {
                    b.Property<int>("IdHistorialToken")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdHistorialToken"));

                    b.Property<bool?>("EsActivo")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bit")
                        .HasComputedColumnSql("(case when [FechaExpiracion]<getdate() then CONVERT([bit],(0)) else CONVERT([bit],(1)) end)", false);

                    b.Property<DateTime?>("FechaCreacion")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("FechaExpiracion")
                        .HasColumnType("datetime");

                    b.Property<Guid?>("IdUsuario")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdHistorialToken")
                        .HasName("PK__HISTORIA__03DC48A56DB735F0");

                    b.HasIndex("IdUsuario");

                    b.ToTable("HISTORIAL_REFRESH_TOKEN", (string)null);
                });

            modelBuilder.Entity("APIBLOG.Models.Post", b =>
                {
                    b.Property<int>("IdPost")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPost"));

                    b.Property<bool?>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Contenido")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("FechaActualizacion")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("FechaCreacion")
                        .HasColumnType("datetime");

                    b.Property<int>("IdCategoria")
                        .HasColumnType("int");

                    b.Property<Guid?>("IdUsuario")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Imagen")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Titulo")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("IdPost")
                        .HasName("PK__POST__F8DCBD4D44C192C8");

                    b.HasIndex("IdCategoria");

                    b.HasIndex("IdUsuario");

                    b.ToTable("POST", (string)null);
                });

            modelBuilder.Entity("APIBLOG.Models.Usuario", b =>
                {
                    b.Property<Guid>("IdUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("(newid())");

                    b.Property<bool?>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Contraseña")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("CorreoElectronico")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("FechaRegistro")
                        .HasColumnType("datetime");

                    b.Property<string>("NombreUsuario")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Usuario");

                    b.Property<string>("Rol")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("IdUsuario")
                        .HasName("PK__USUARIO__5B65BF978CD2A107");

                    b.ToTable("USUARIO", (string)null);
                });

            modelBuilder.Entity("PostEtiqueta", b =>
                {
                    b.Property<int>("IdEtiqueta")
                        .HasColumnType("int");

                    b.Property<int>("IdPost")
                        .HasColumnType("int");

                    b.HasKey("IdEtiqueta", "IdPost")
                        .HasName("PK__POST_ETI__6C4DE1C4DCBE7F07");

                    b.HasIndex("IdPost");

                    b.ToTable("POST_ETIQUETA", (string)null);
                });

            modelBuilder.Entity("APIBLOG.Models.Comentario", b =>
                {
                    b.HasOne("APIBLOG.Models.Post", "IdPostNavigation")
                        .WithMany("Comentarios")
                        .HasForeignKey("IdPost")
                        .HasConstraintName("FK__COMENTARI__IdPos__656C112C");

                    b.HasOne("APIBLOG.Models.Usuario", "IdUsuarioNavigation")
                        .WithMany("Comentarios")
                        .HasForeignKey("IdUsuario")
                        .HasConstraintName("FK__COMENTARI__IdUsu__6477ECF3");

                    b.Navigation("IdPostNavigation");

                    b.Navigation("IdUsuarioNavigation");
                });

            modelBuilder.Entity("APIBLOG.Models.HistorialRefreshToken", b =>
                {
                    b.HasOne("APIBLOG.Models.Usuario", "IdUsuarioNavigation")
                        .WithMany("HistorialRefreshTokens")
                        .HasForeignKey("IdUsuario")
                        .HasConstraintName("FK__HISTORIAL__IdUsu__6FE99F9F");

                    b.Navigation("IdUsuarioNavigation");
                });

            modelBuilder.Entity("APIBLOG.Models.Post", b =>
                {
                    b.HasOne("APIBLOG.Models.Categoria", "IdCategoriaNavigation")
                        .WithMany("Posts")
                        .HasForeignKey("IdCategoria")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__POST__IdCategoria__619B8050");

                    b.HasOne("APIBLOG.Models.Usuario", "IdUsuarioNavigation")
                        .WithMany("Posts")
                        .HasForeignKey("IdUsuario")
                        .HasConstraintName("FK__POST__IdUsuario__619B8048");

                    b.Navigation("IdCategoriaNavigation");

                    b.Navigation("IdUsuarioNavigation");
                });

            modelBuilder.Entity("PostEtiqueta", b =>
                {
                    b.HasOne("APIBLOG.Models.Etiqueta", null)
                        .WithMany()
                        .HasForeignKey("IdEtiqueta")
                        .IsRequired()
                        .HasConstraintName("FK__POST_ETI__IdEti__68487DD7");

                    b.HasOne("APIBLOG.Models.Post", null)
                        .WithMany()
                        .HasForeignKey("IdPost")
                        .IsRequired()
                        .HasConstraintName("FK__POST_ETI__IdPos__693CA210");
                });

            modelBuilder.Entity("APIBLOG.Models.Categoria", b =>
                {
                    b.Navigation("Posts");
                });

            modelBuilder.Entity("APIBLOG.Models.Post", b =>
                {
                    b.Navigation("Comentarios");
                });

            modelBuilder.Entity("APIBLOG.Models.Usuario", b =>
                {
                    b.Navigation("Comentarios");

                    b.Navigation("HistorialRefreshTokens");

                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}