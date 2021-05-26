using Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ContactBussines : IContactBussines
    {
        private List<Contacto> listContacto;

        public ContactBussines(List<Contacto> lista)
        {
            this.listContacto = lista;
        }
        public Contacto getContactosById(int id)
        {
            return this.listContacto.Single(p => p.id == id); ;
        }

        public List<Contacto> getListContactoByFilter(String nombreapellido)
        {
            if (!string.IsNullOrEmpty(nombreapellido))
            {
                return this.listContacto.FindAll(p => p.nombreApellido.Contains(nombreapellido));
            }
            else
            {
                return this.listContacto.OrderBy(p => p.id).ToList();
            }
        }

        public void update(Contacto contacto)
        {
            this.listContacto.Remove(contacto);
            this.listContacto.Add(contacto);
        }
        
        public void delete(Contacto contacto)
        {
            Contacto contactoDelete = this.listContacto.Find(p => p.id.Equals(contacto.id));
            if (contactoDelete != null)
            {
                this.listContacto.Remove(contactoDelete);
            }
        }

        public Contacto insertar(Contacto contacto)
        {
            int max = this.listContacto.OrderByDescending(x => x.id).First().id;
            contacto.id = (max + 1);
            this.listContacto.Add(contacto);
            return contacto;
        }



    }
}
