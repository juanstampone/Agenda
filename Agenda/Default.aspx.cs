using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Entidad;
using DAL;
using System.Data.SqlClient;
using System.Data;

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
            if (!IsPostBack)
            {
                CargarFiltros();
            }

       
        }

        public void Consultar(Object sender, EventArgs e) 
        {
            //Validar
            ContactoFiltro CFiltro = new ContactoFiltro
            {
                ApellidoNombre = TextBoxNombre.Text.Equals("") ? null : TextBoxNombre.Text,
                FechaIngresoDesde = Convert.ToDateTime(TextBoxFID.Text),
                FechaIngresoHasta = Convert.ToDateTime(TextBoxFIH.Text),
                Activo = DropDownListActivo.SelectedItem.Text,
                IdArea = int.Parse(DropDownListArea.SelectedValue),
                ContactoInterno = DropDownCI.SelectedItem.Text,
                IdPais = int.Parse(ListaDePaises.SelectedValue),
                Localidad = TextBoxLocalidad.Text.Equals("") ? null : TextBoxLocalidad.Text,
                Organizacion = TextBoxOrganizacion.Text.Equals("") ? null : TextBoxOrganizacion.Text
            };

            IContactBussines contactoBussines = new ContactBussines();
            List<Contacto> contactos = contactoBussines.ConsultaFiltroContacto(CFiltro);
            GridViewConsulta.DataSource = contactos;
            GridViewConsulta.DataBind();


        }


        public void CargarFiltros()
        {
            DataAccessLayer conexion = new DataAccessLayer();

            SqlConnection con = conexion.AbrirConexion();
            SqlCommand consulta = new SqlCommand("select * from Pais", con);
            SqlDataAdapter da = new SqlDataAdapter(consulta);
            DataSet ds = new DataSet();
            da.Fill(ds);
            ListaDePaises.DataTextField = ds.Tables[0].Columns["NombrePais"].ToString();
            ListaDePaises.DataValueField = ds.Tables[0].Columns["IdPais"].ToString();
            ListaDePaises.DataSource = ds.Tables[0];
            ListaDePaises.DataBind();
            ListaDePaises.Items.Insert(0, new ListItem("TODOS", "0"));

            SqlCommand consultaArea = new SqlCommand("select * from Area", con);
            SqlDataAdapter daArea = new SqlDataAdapter(consultaArea);
            DataSet dsArea = new DataSet();
            daArea.Fill(dsArea);
            DropDownListArea.DataTextField = dsArea.Tables[0].Columns["NombreArea"].ToString();
            DropDownListArea.DataValueField = dsArea.Tables[0].Columns["IdArea"].ToString();
            DropDownListArea.DataSource = dsArea.Tables[0];
            DropDownListArea.DataBind();
            DropDownListArea.Items.Insert(0, new ListItem("TODAS", "0"));

            DropDownListActivo.Items.Add(new ListItem("SI", "1"));
            DropDownListActivo.Items.Add(new ListItem("NO", "2"));

            DropDownListActivo.Items.Insert(0, new ListItem("TODOS", "0"));


            DropDownCI.Items.Add(new ListItem("SI", "1"));
            DropDownCI.Items.Add(new ListItem("NO", "2"));

            DropDownCI.Items.Insert(0, new ListItem("TODOS", "0"));


        }

        protected void AltaContacto(Object sender, EventArgs e)
        {
            Response.Redirect("AEContacto.aspx", false);
        }
        protected void EditarContacto(Object sender, EventArgs e)
        {
            ImageButton boton = (ImageButton)sender;
            GridViewRow row = (GridViewRow)boton.DataItemContainer;
            Contacto contacto = new Contacto
            {
                id = int.Parse(row.Cells[0].Text),
                nombreApellido = row.Cells[1].Text,
                genero = row.Cells[2].Text,
                pais = row.Cells[3].Text,
                localidad = row.Cells[4].Text,
                organizacion = row.Cells[5].Text,
                direccion = row.Cells[6].Text,
                contactoInterno = row.Cells[7].Text
            }

            Cache['Contacto'] = 
            Application['Disparador'] = 'Editar'
            Response.Redirect("AEContacto.aspx", false);
        }

        protected void EliminarContacto(Object sender, EventArgs e)
        {
            ImageButton boton = (ImageButton)sender;
            GridViewRow row = (GridViewRow)boton.DataItemContainer;
            int idContacto = int.Parse(row.Cells[0].Text);

            IContactBussines contactoBussines = new ContactBussines();
            int resultado = contactoBussines.delete(idContacto);
        }
        public void LimpiarCampos(Object sender, EventArgs e)
        {

        }
    }
}