using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidad;

namespace BLL
{
    public interface IContactBussines
    {
        Contacto getContactosById(int id);
        List<Contacto> getListContactoByFilter(String nombreApellido);
        Contacto insertar(Contacto contacto);
        void update(Contacto contacto);
        void delete(Contacto contacto);
    }
}
