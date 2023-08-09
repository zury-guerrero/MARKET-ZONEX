using MARKET_PLACE.Context;
using MARKET_PLACE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MARKET_PLACE.Services
{
    public class carritofuncion
    {
        public void vaciador(int ID)
        {
            try
            {
                using (var _context = new ApplicationDbContext())
                {
                    Producto productos = _context.Productos.Find(ID);
                    if (productos != null)
                    {
                        _context.Productos.Remove(productos);
                        _context.SaveChanges();

                        MessageBox.Show("Se elimino correctamente producto");
                    }
                }


            }
            catch (Exception ex)
            {

                throw new Exception("Succedio un error " + ex.Message);
            }


        }
        public void pagar()
        {
           
                        {
                            
                            MessageBox.Show("Pago de producto exitoso");
                        }
        }
    }
}
