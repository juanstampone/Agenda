using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Entidad;

namespace Agenda
{
    public partial class _Default : Page
    {
        private void print(List<Contacto> listado)
        {
            foreach (Contacto example in listado)
            {
                Response.Write(string.Concat("Id: ", example.id.ToString(), " Nombre y Apellido: ", example.nombreApellido));
                Response.Write("<BR/>");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            IContactBussines business = new ContactBussines((List<Contacto>)Application["listaContacto"]);

            Response.Write("Imprimo en pantalla el listado de registros:");
            Response.Write("<BR/>");
            print(business.getListContactoByFilter(""));
            Response.Write("--------------------------------------");
            Response.Write("<BR/>");


            Response.Write("Obtengo el contacto 1 en pantalla:");
            Response.Write("<BR/>");
            Contacto contacto = business.getContactosById(1);
            Response.Write(string.Concat("Id: ", contacto.id.ToString(), " Nombre y Apellido: ", contacto.nombreApellido));
            Response.Write("<BR/>");
            Response.Write("--------------------------------------");
            Response.Write("<BR/>");


            Response.Write("Actualizo el registro obtenido, lo recupero y lo imprimo en pantalla:");
            Response.Write("<BR/>");
            contacto.nombreApellido = "Juan Manuel Stampone";
            business.update(contacto);
            Contacto contactoUpdate = business.getContactosById(1);
            Response.Write(string.Concat("Id: ", contactoUpdate.id.ToString(), " Nombre y Apellido: ", contactoUpdate.nombreApellido));
            Response.Write("<BR/>");
            Response.Write("--------------------------------------");
            Response.Write("<BR/>");


            Response.Write("Elimino el registro con id=2:");
            Response.Write("<BR/>");
            business.delete(new Contacto() { id = 2 });
            Response.Write("--------------------------------------");
            Response.Write("<BR/>");



            Response.Write("Inserto un nuevo registro y lo imprimo en pantalla:");
            Response.Write("<BR/>");
            Contacto contactoInsert = business.insertar(new Contacto { id =3, nombreApellido = "Manuel Gomez" });
            Response.Write(string.Concat("Id: ", contactoInsert.id.ToString(), " Nombre y Apellido: ", contactoInsert.nombreApellido));
            Response.Write("<BR/>");
            Response.Write("--------------------------------------");
            Response.Write("<BR/>");



            Response.Write("Imprimo en pantalla el listado de registros:");
            Response.Write("<BR/>");
            print(business.getListContactoByFilter(""));
            Response.Write("--------------------------------------");
            Response.Write("<BR/>");


            Response.Write("Imprimo en pantalla el listado de registros filtrados con el nombre \"Manuel\":");
            Response.Write("<BR/>");
            print(business.getListContactoByFilter("Manuel"));

        }
    }
}