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
    public class UsuarioServices
    {
        #region Agregar Usuario
        public void AddUsuario(Usuario request)
        {
            try
            {
                if (request != null)
                {
                    using (var _Context =    new ApplicationDbContext())
                    {
                        Usuario usuario = new Usuario();
                        usuario.Nombre = request.Nombre;
                        usuario.UserName = request.UserName;
                        usuario.Password = request.Password;
                        usuario.FkRol = request.FkRol;

                        _Context.Add(usuario);
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

        #region Eliminar Usuario
        public void deleteUser(int id)
        {
            try
            {
                using (var _context = new ApplicationDbContext())
                {
                    Usuario existingUser = _context.Usuarios.FirstOrDefault(u => u.PkUsuario == id);
                    if (existingUser != null)
                    {
                        _context.Usuarios.Remove(existingUser);
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

        #region Editar Usuario

        public void UpdateUser(Usuario request)
        {
            try
            {
                using (var _context = new ApplicationDbContext())
                {

                    Usuario usuario = _context.Usuarios.Find(request.PkUsuario);
                    if (usuario != null)
                    {
                        usuario.Nombre = request.Nombre;
                        usuario.UserName = request.UserName;
                        usuario.Password = request.Password;
                        usuario.FkRol = request.FkRol;

                        //_context.Update(usuario);
                        _context.Entry(usuario).State = EntityState.Modified;
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
                    List<Rol> roles = _context.Roles.ToList();
                    
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
        public List<Usuario> GetUsuarios()
        {
            try
            {
                using (var _Context = new ApplicationDbContext())
                {
                    List<Usuario> usuarios = new List<Usuario>();
                    usuarios = _Context.Usuarios.Include(x => x.Roles).ToList();
                    if (usuarios.Count > 0)
                    {
                        return usuarios;
                    }
                    return usuarios;
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Error: " + ex.Message);
            }
        }
        #endregion

        #region Login
        public Usuario Login(string User, string Password)
        {
			try
			{
				using (var _Context = new ApplicationDbContext())
				{
                    var response = _Context.Usuarios.Include(Y => Y.Roles).FirstOrDefault(x => x.UserName == User && x.Password == Password);

                    return response;
                }
            }
			catch (Exception ex)
			{
				
				throw new Exception("Error: " + ex.Message);
			}
        }
        #endregion
    }
}
