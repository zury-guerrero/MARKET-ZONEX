using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MARKET_PLACE.Entities
{
    public class Rol
    {
        [Key]
        public int PkRol { get; set; }
        public string Nombre { get; set; }

    }
}
