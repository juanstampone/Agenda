using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using Entidad;
using BLL;

namespace Agenda
{
    public partial class AEContacto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarFiltros();
            }
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

            ListaDeGenero.Items.Add(new ListItem("F", "1"));
            ListaDeGenero.Items.Insert(0, new ListItem("M", "0"));


        }
        protected void Guardar(object sender, EventArgs e)
        {

            Contacto contactoNuevo = new Contacto
            {
                nombreApellido = TextBoxNombre.Text,
                genero = ListaDeGenero.SelectedItem.ToString(),
                IdPais = int.Parse(ListaDePaises.SelectedValue),
                localidad = TextBoxLocalidad.Text,
                contactoInterno = DropDownCI.SelectedItem.ToString(),
                organizacion = TextBoxOrganizacion.Text,
                IdArea = int.Parse(DropDownListArea.SelectedValue),
                activo = DropDownListActivo.SelectedItem.ToString(),
                telefonoFijo = TextBoxTF.Text,
                telefonoCelular = TextBoxTC.Text,
                email = TextBoxEmail.Text,
                cuentaSkype = TextBoxCS.Text,
                direccion = TextBoxDireccion.Text
            };
            IContactBussines contactoBussines = new ContactBussines();
            int resultado = contactoBussines.insertar(contactoNuevo);

        }
    }
}