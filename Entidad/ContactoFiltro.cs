using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class ContactoFiltro
    {
        public String ApellidoNombre { get; set; }
        public int IdPais { get; set; }
        public String Localidad { get; set; }
        public DateTime FechaIngresoDesde { get; set; }
        public DateTime FechaIngresoHasta { get; set; }
        public String ContactoInterno { get; set; }
        public String Organizacion { get; set; }
        public int IdArea { get; set; }
        public String Activo { get; set; }

        
    }
}
