﻿// <auto-generated />
using System;
using MARKET_PLACE.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MARKET_PLACE.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("MARKET_PLACE.Entities.Genero", b =>
                {
                    b.Property<int>("PkGenero")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("NombreG")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("PkGenero");

                    b.ToTable("Generos");

                    b.HasData(
                        new
                        {
                            PkGenero = 1,
                            NombreG = "Electrónicos"
                        },
                        new
                        {
                            PkGenero = 2,
                            NombreG = "Moda"
                        },
                        new
                        {
                            PkGenero = 3,
                            NombreG = "Belleza y cuidado personal"
                        },
                        new
                        {
                            PkGenero = 4,
                            NombreG = "Deportes"
                        });
                });

            modelBuilder.Entity("MARKET_PLACE.Entities.Producto", b =>
                {
                    b.Property<int>("PkProducto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<int?>("FkGenero")
                        .HasColumnType("int");

                    b.Property<string>("ImagenPath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Precio")
                        .HasColumnType("double");

                    b.HasKey("PkProducto");

                    b.HasIndex("FkGenero");

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("MARKET_PLACE.Entities.Rol", b =>
                {
                    b.Property<int>("PkRol")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("PkRol");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            PkRol = 1,
                            Nombre = "Administrador"
                        },
                        new
                        {
                            PkRol = 2,
                            Nombre = "Vendedor"
                        },
                        new
                        {
                            PkRol = 3,
                            Nombre = "Comprador"
                        });
                });

            modelBuilder.Entity("MARKET_PLACE.Entities.Usuario", b =>
                {
                    b.Property<int>("PkUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("FkRol")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("PkUsuario");

                    b.HasIndex("FkRol");

                    b.ToTable("Usuarios");

                    b.HasData(
                        new
                        {
                            PkUsuario = 1,
                            FkRol = 1,
                            Nombre = "Diego",
                            Password = "1234",
                            UserName = "Admin1"
                        },
                        new
                        {
                            PkUsuario = 2,
                            FkRol = 2,
                            Nombre = "Maximiliano",
                            Password = "1212",
                            UserName = "User1"
                        },
                        new
                        {
                            PkUsuario = 3,
                            FkRol = 3,
                            Nombre = "Adamaris",
                            Password = "pato",
                            UserName = "User2"
                        });
                });

            modelBuilder.Entity("MARKET_PLACE.Entities.Producto", b =>
                {
                    b.HasOne("MARKET_PLACE.Entities.Genero", "Generos")
                        .WithMany()
                        .HasForeignKey("FkGenero");

                    b.Navigation("Generos");
                });

            modelBuilder.Entity("MARKET_PLACE.Entities.Usuario", b =>
                {
                    b.HasOne("MARKET_PLACE.Entities.Rol", "Roles")
                        .WithMany()
                        .HasForeignKey("FkRol");

                    b.Navigation("Roles");
                });
#pragma warning restore 612, 618
        }
    }
}
