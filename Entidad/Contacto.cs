using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Contacto
    {
        public int id { get; set; }
        public String nombreApellido { get; set; }
        public String genero { get; set; }
        public String pais { get; set; }
        public String localidad { get; set; }
        public String organizacion { get; set; }
        public String direccion { get; set; }
        public String contactoInterno { get; set; }
        public String area { get; set; }
        public int IdArea { get; set; }
        public int IdPais { get; set; }
        public DateTime fechaIngreso { get; set; }
        public String activo { get; set; }
        public String telefonoFijo { get; set; }
        public String telefonoCelular { get; set; }
        public String email { get; set; }
        public String cuentaSkype { get; set; }


    }
}
