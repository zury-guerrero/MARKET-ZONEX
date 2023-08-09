using MARKET_PLACE.Context;
using MARKET_PLACE.Entities;
using Microsoft.EntityFrameworkCore;
using Renci.SshNet.Messages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MARKET_PLACE.Services
{
    public class ProductoServices
    {
        #region Agregar Producto
        public void AddProducto(Producto request)
        {
            try
            {
                if (request != null)
                {
                    using (var _Context = new ApplicationDbContext())
                    {
                        Producto productos = new Producto();
                        productos.Nombre = request.Nombre;
                        productos.Precio = request.Precio;
                        productos.Cantidad = request.Cantidad;
                        productos.FkGenero = request.FkGenero;
                        productos.ImagenPath = request.ImagenPath; // Agregar ImagenPath

                        _Context.Add(productos);
                        _Context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Error: " + ex.Message);
            }
        }
        #endregion

        #region Eliminar Producto
        public void deleteProducto(int id)
        {
            try
            {
                using (var _context = new ApplicationDbContext())
                {
                    Producto existingProducto = _context.Productos.FirstOrDefault(u => u.PkProducto == id);
                    if (existingProducto != null)
                    {
                        _context.Productos.Remove(existingProducto);
                        _context.SaveChanges();
                        MessageBox.Show("Producto eliminado");
                    }
                }


            }
            catch (Exception ex)
            {

                throw new Exception("Succedio un error " + ex.Message);
            }
        }
        #endregion

        #region Editar Producto

        public void UpdateProducto(Producto request)
        {
            try
            {
                using (var _context = new ApplicationDbContext())
                {

                    Producto producto = _context.Productos.Find(request.PkProducto);
                    if (producto != null)
                    {

                        producto.Nombre = request.Nombre;
                        producto.Precio = request.Precio;
                        producto.Cantidad = request.Cantidad;
                        producto.FkGenero = request.FkGenero;

                        //_context.Update(usuario);
                        _context.Entry(producto).State = EntityState.Modified;
                        _context.SaveChanges();
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }
        #endregion

        #region Lista Generos
        public List<Genero> GetGeneros()
        {
            try
            {
                using (var _context = new ApplicationDbContext())
                {
                    List<Genero> genero = _context.Generos.ToList();

                    return genero;
                }

            }
            catch (Exception ex)
            {

                throw new Exception("Succedio un error" + ex.Message);
            }
        }
        #endregion

        #region Lista Productos
        public Producto GetProductoPorID(int id)
        {
            try
            {
                using (var _Context = new ApplicationDbContext())
                {
                    Producto producto = _Context.Productos.Include(x => x.Generos).FirstOrDefault(p => p.PkProducto == id);


                    return producto;
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Error: " + ex.Message);
            }
        }
        public List<Producto> GetProducto()
        {
            try
            {
                using (var _Context = new ApplicationDbContext())
                {
                    List<Producto> producto = new List<Producto>();
                    producto = _Context.Productos.AsNoTracking().Include(x => x.Generos).ToList();
                    if (producto.Count > 0)
                    {
                        return producto;
                    }
                    return producto;
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Error: " + ex.Message);
            }
        }

        public bool DescontarCantidadProducto(int id_producto, int cantidad)
        {
            try
            {
                using (var _Context = new ApplicationDbContext())
                {
                    var producto = _Context.Productos.Where(x => x.PkProducto == id_producto).FirstOrDefault();



                    producto.Cantidad = producto.Cantidad - cantidad;

                    _Context.Productos.Update(producto);

                    _Context.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
                return false;
            }
        }
        #endregion
    }
}
