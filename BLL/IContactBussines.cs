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
        int insertar(Contacto contacto);
        int update(Contacto contacto);
        int delete(int idContacto);
        List<Contacto> ConsultaFiltroContacto(ContactoFiltro contactoFiltro,int pageIndex, int pageSize);

    }
}
