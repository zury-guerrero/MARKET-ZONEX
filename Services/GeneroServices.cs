// Importaciones necesarias
using MARKET_PLACE.Context;
using MARKET_PLACE.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace MARKET_PLACE.Services
{
    public class GeneroServices
    {
        // Método para agregar un género
        public void AgregarGenero(Genero request)
        {
            try
            {
                using (var _Context = new ApplicationDbContext())
                {
                    // Crear un objeto Genero y asignarle el nombre del género solicitado
                    Genero genero = new Genero();
                    genero.NombreG = request.NombreG;

                    // Agregar el género a la base de datos y guardar cambios
                    _Context.Generos.Add(genero);
                    _Context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                throw new Exception("Error al agregar el género: " + ex.Message);
            }
        }

        // Método para eliminar un género
        public void EliminarGenero(int id)
        {
            try
            {
                using (var _context = new ApplicationDbContext())
                {
                    // Encontrar el género existente por su ID
                    Genero generoExistente = _context.Generos.FirstOrDefault(u => u.PkGenero == id);
                    if (generoExistente != null)
                    {
                        // Remover y guardar cambios
                        _context.Generos.Remove(generoExistente);
                        _context.SaveChanges();
                        MessageBox.Show("Género eliminado correctamente");
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                throw new Exception("Error al eliminar el género: " + ex.Message);
            }
        }

        // Método para actualizar un género
        public void ActualizarGenero(Genero request)
        {
            try
            {
                using (var _context = new ApplicationDbContext())
                {
                    // Encontrar el género existente por su ID
                    Genero genero = _context.Generos.Find(request.PkGenero);
                    if (genero != null)
                    {
                        // Actualizar el nombre del género y guardar cambios
                        genero.NombreG = request.NombreG;
                        _context.Entry(genero).State = EntityState.Modified;
                        _context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                throw new Exception("Error al actualizar el género: " + ex.Message);
            }
        }

        // Método para obtener la lista de géneros
        public List<Genero> ObtenerGeneros()
        {
            try
            {
                using (var _context = new ApplicationDbContext())
                {
                    // Obtener la lista de géneros desde la base de datos
                    List<Genero> generos = _context.Generos.ToList();
                    return generos;
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                throw new Exception("Error al obtener la lista de géneros: " + ex.Message);
            }
        }
    }
}