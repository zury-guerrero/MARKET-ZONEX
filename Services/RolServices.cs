using MARKET_PLACE.Context;
using MARKET_PLACE.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MARKET_PLACE.Services
{
    public class RolServices
    {
        public void AddRol(Rol request)
        {
            try
            {
                using (var _Context = new ApplicationDbContext())
                {
                    Rol roles = new Rol();
                    
                    roles.Nombre = request.Nombre;

                    _Context.Roles.Add(roles);
                    _Context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Error: " + ex.Message);
            }
        }
        #region Eliminar Roles
        public void deleteRol(int id)
        {
            try
            {
                using (var _context = new ApplicationDbContext())
                {
                    Rol existingRol = _context.Roles.FirstOrDefault(u => u.PkRol == id);
                    if (existingRol != null)
                    {
                        _context.Roles.Remove(existingRol);
                        _context.SaveChanges();
                        MessageBox.Show("Se elimino correctamente");
                    }
                }


            }
            catch (Exception ex)
            {

                throw new Exception("Succedio un error " + ex.Message);
            }
        }
        #endregion

        #region Editar Rol

        public void UpdateRol(Rol request)
        {
            try
            {
                using (var _context = new ApplicationDbContext())
                {

                    Rol roles = _context.Roles.Find(request.PkRol);
                    if (roles != null)
                    {
                        roles.Nombre = request.Nombre;

                        //_context.Update(usuario);
                        _context.Entry(roles).State = EntityState.Modified;
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

        #region Lista Roles
        public List<Rol> GetRols()
        {
            try
            {
                using (var _context = new ApplicationDbContext())
                {
                    List<Rol> roles = new List<Rol>();
                    roles = _context.Roles.ToList();

                    return roles;
                }

            }
            catch (Exception ex)
            {

                throw new Exception("Succedio un error" + ex.Message);
            }
        }
        #endregion

        #region Lista Usuarios
        //public List<Rol> GetRoles()
        //{
        //    try
        //    {
        //        using (var _Context = new ApplicationDbContext())
        //        {
        //            List<Rol> Roles = new List<Rol>();
        //            Roles = _Context.Roles.ToList();
        //            if (Roles.Count > 0)
        //            {
        //                return Roles;
        //            }
        //            return Roles;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        throw new Exception("Error: " + ex.Message);
        //    }
        //}
        #endregion



    }
}
