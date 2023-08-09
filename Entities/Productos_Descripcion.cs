using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MARKET_PLACE.Entities
{
    public class Producto_Descripcion
    {
        
        [ForeignKey("Producto")]
        public int ProductoId { get; set; }
        public Producto NomProducto { get; set; }

        [ForeignKey("Genero")]
        public int GeneroId { get; set; }
        public Genero NomGenero { get; set; }
    }
}
