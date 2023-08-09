using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MARKET_PLACE.Entities
{
    public class Genero
    {
        //public Genero()
        //{
        //    ProductosGenero = new List<Producto_Descripcion>();
        //}
        [Key]
        public int PkGenero { get; set; }
        public string NombreG { get; set; }
        //public List<Producto> Productos { get; set; }
        //public ICollection<Producto_Descripcion> ProductosGenero { get; set; }
    }
}
