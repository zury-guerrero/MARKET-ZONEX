using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MARKET_PLACE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MARKET_PLACE.Context
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {

            options.UseMySQL("server= localhost; database= MarketZonezx; user= root; password=zury");


        }

        public DbSet <Usuario> Usuarios { get; set; }
        public DbSet <Rol> Roles { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Genero> Generos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Producto_Descripcion>()
            //    .HasKey(ep => new { ep.ProductoId, ep.GeneroId }); // Especificar la clave primaria compuesta de la relación

            //modelBuilder.Entity<Producto_Descripcion>()
            //    .HasOne(ep => ep.NomGenero)
            //    .WithMany(e => e.ProductosGenero)
            //    .HasForeignKey(ep => ep.GeneroId);

            //modelBuilder.Entity<Producto_Descripcion>()
            //    .HasOne(ep => ep.NomProducto)
            //    .WithMany(p => p.ProductosGenero)
            //    .HasForeignKey(ep => ep.ProductoId);

            // Agregar Usuarios
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    PkUsuario = 1,
                    Nombre = "Diego",
                    UserName = "Admin1",
                    Password = "1234",
                    FkRol = 1
                },
                new Usuario
                {
                    PkUsuario = 2,
                    Nombre = "Maximiliano",
                    UserName = "User1",
                    Password = "1212",
                    FkRol = 2
                },
                new Usuario
                {
                    PkUsuario = 3,
                    Nombre = "Adamaris",
                    UserName = "User2",
                    Password = "pato",
                    FkRol = 3
                }

                 );

            // Agregar Roles
            modelBuilder.Entity<Rol>().HasData(

                new Rol
                {
                    PkRol = 1,
                    Nombre = "Administrador"
                },
                new Rol
                {
                    PkRol = 2,
                    Nombre = "Vendedor"
                },
                new Rol
                {
                    PkRol = 3,
                    Nombre = "Comprador"
                }
                );

            // Agregar Generos
            modelBuilder.Entity<Genero>().HasData(

                new Genero
                {
                    PkGenero = 1,
                    NombreG = "Electrónicos"
                },
                new Genero
                {
                    PkGenero = 2,
                    NombreG = "Moda"
                },
                new Genero
                {
                    PkGenero = 3,
                    NombreG = "Belleza y cuidado personal"
                },
                new Genero
                {
                    PkGenero = 4,
                    NombreG = "Deportes"
                }
                );

            //// Agregar Productos
            //modelBuilder.Entity<Producto>().HasData(
            //    new Producto
            //    {
            //        PkProducto = 3,
            //        Nombre = "Maquillaje",
            //        Precio = 1850.00,
            //        Cantidad = 20,
            //        FkGenero = 3

            //    },
            //    new Producto
            //    {
            //        PkProducto = 2,
            //        Nombre = "Playera",
            //        Precio = 649.99,
            //        Cantidad = 12,
            //        FkGenero = 2

            //    },
            //    new Producto
            //    {
            //        PkProducto = 1,
            //        Nombre = "Telefono",
            //        Precio = 8999.99,
            //        Cantidad = 5,
            //        FkGenero = 1
            //    },
            //    new Producto
            //    {
            //        PkProducto = 4,
            //        Nombre = "Balon",
            //        Precio = 500.00,
            //        Cantidad = 15,
            //        FkGenero = 4
            //    }
            //); 

            // Relaciones entre productos y géneros
            //modelBuilder.Entity<Producto_Descripcion>().HasData(
                
            //    new Producto_Descripcion { ProductoId = 1, GeneroId = 1 }, // Producto "Telefono" relacionado con Genero "Electrónicos"
            //    new Producto_Descripcion { ProductoId = 2, GeneroId = 2 }, // Producto "Playera" relacionado con Genero "Moda"
            //    new Producto_Descripcion { ProductoId = 3, GeneroId = 3 }, // Producto "Maquillaje" relacionado con Genero "Belleza y cuidado personal"
            //    new Producto_Descripcion { ProductoId = 4, GeneroId = 4 }  // Producto "Balon" relacionado con Genero "Moda"
                                                                           
            
        }

    }
}
