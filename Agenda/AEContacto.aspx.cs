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
                CargarModo((String)Application["Modo"]);
            }
        }

        private void CargarModo(string modo)
        {
            if (modo.Equals("CONSULTAR"))
            {
                CargarDatosContacto();
                CamposDisable();
            }
            else if (modo.Equals("EDITAR"))
            {
                CargarDatosContacto();
                SetCambios();
            }
        }

        private void CamposDisable()
        {
            TextBoxNombre.Enabled = false;
            ListaDeGenero.Enabled = false;
            ListaDePaises.Enabled = false;
            TextBoxLocalidad.Enabled = false;
            DropDownCI.Enabled = false;
            TextBoxOrganizacion.Enabled = false;
            DropDownListArea.Enabled = false;
            DropDownListActivo.Enabled = false;
            TextBoxDireccion.Enabled = false;
            TextBoxTF.Enabled = false;
            TextBoxTC.Enabled = false;
            TextBoxEmail.Enabled = false;
            TextBoxCS.Enabled = false;
            ButtonGuardar.Visible = false;
        }

        private void CargarDatosContacto()
        {
            Contacto contacto = (Contacto)Application["ContactoSeleccionado"];

            TextBoxNombre.Text = contacto.nombreApellido;
            ListaDeGenero.SelectedIndex = ListaDeGenero.Items.IndexOf(ListaDeGenero.Items.FindByText(contacto.genero)); 
            ListaDePaises.SelectedIndex = ListaDePaises.Items.IndexOf(ListaDePaises.Items.FindByText(contacto.pais));
            TextBoxLocalidad.Text = contacto.localidad.Equals("&nbsp;") ? "" : contacto.localidad;
            DropDownCI.SelectedIndex = DropDownCI.Items.IndexOf(DropDownCI.Items.FindByText(contacto.contactoInterno)); 
            TextBoxOrganizacion.Text = contacto.organizacion.Equals("&nbsp;") ? "" : contacto.organizacion;
            DropDownListArea.SelectedIndex = DropDownListArea.Items.IndexOf(DropDownListArea.Items.FindByText(contacto.area));
            DropDownListActivo.SelectedIndex = DropDownListActivo.Items.IndexOf(DropDownListActivo.Items.FindByText(contacto.activo)); 
            TextBoxDireccion.Text = contacto.direccion.Equals("&nbsp;") ? "" : contacto.direccion;
            TextBoxTF.Text = contacto.telefonoFijo.Equals("&nbsp;") ? "" : contacto.telefonoFijo;
            TextBoxTC.Text = contacto.telefonoCelular.Equals("&nbsp;") ? "" : contacto.telefonoCelular;
            TextBoxEmail.Text = contacto.email.Equals("&nbsp;") ? "" : contacto.email;
            TextBoxCS.Text = contacto.cuentaSkype.Equals("&nbsp;") ? "" : contacto.cuentaSkype;

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

            SqlCommand consultaArea = new SqlCommand("select * from Area", con);
            SqlDataAdapter daArea = new SqlDataAdapter(consultaArea);
            DataSet dsArea = new DataSet();
            daArea.Fill(dsArea);
            DropDownListArea.DataTextField = dsArea.Tables[0].Columns["NombreArea"].ToString();
            DropDownListArea.DataValueField = dsArea.Tables[0].Columns["IdArea"].ToString();
            DropDownListArea.DataSource = dsArea.Tables[0];
            DropDownListArea.DataBind();

            DropDownListActivo.Items.Add(new ListItem("SI", "1"));
            DropDownListActivo.Items.Add(new ListItem("NO", "2"));



            DropDownCI.Items.Add(new ListItem("SI", "1"));
            DropDownCI.Items.Add(new ListItem("NO", "2"));


            ListaDeGenero.Items.Add(new ListItem("F", "1"));
            ListaDeGenero.Items.Insert(0, new ListItem("M", "0"));


        }
        protected void Guardar(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                //btnDelete.OnClientClick = "if(!confirm('" + delteMessage + "')) return false;";
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
                    int resultado;
                    if (((String)Application["Modo"]).Equals("ALTA")) { 
                        
                        resultado = contactoBussines.insertar(contactoNuevo);
                    }
                    else
                    {
                        contactoNuevo.id = ((Contacto)Application["ContactoSeleccionado"]).id;
                        resultado = contactoBussines.update(contactoNuevo);
                    }
                
                Response.Redirect("Default.aspx");
            }

        }

        protected void EmailValidador(object source, ServerValidateEventArgs email)
        {
            if (TextBoxEmail.Text.Contains('@'))
            {
                email.IsValid = true;
            }
            else
            {
                string mensaje = "El email ingresado no es valido";
                MostrarMensajeAlerta(mensaje);
                
                email.IsValid = false;
            }
        }

        private void MostrarMensajeAlerta(string mensaje)
        {
           // System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "alert('alert to be displayed');", true);
           // ClientScript.RegisterOnSubmitStatement(this.GetType(), "alert", "alert('" + mensaje + "');");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('"+mensaje+"')", true);
        }
        private void MostrarMensajeConfirm(string mensaje)
        {
           // ScriptManager.RegisterOnSubmitStatement(this.GetType(), "alert", "return confirm('" + mensaje +"');");
        }

        protected void ValidarTelSkype(object source, ServerValidateEventArgs input)
        {
            if (TextBoxTC.Text.Equals("") && TextBoxTF.Text.Equals("") && TextBoxCS.Text.Equals(""))
            {
                input.IsValid = false;
                string mensaje = "Al menos un campo de los siguientes debe estar completo: TELEFONO FIJO/CELULAR/SKYPE";
                MostrarMensajeAlerta(mensaje);
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Al menos un campo de los siguientes debe estar completo: TELEFONO FIJO/CELULAR/SKYPE')", true);

            }
            else
            {
                input.IsValid = true;
            }
        }

        private void SetCambios()
        {
            if (DropDownCI.SelectedValue.Equals("1"))
            {
                TextBoxOrganizacion.Text = "";
                TextBoxOrganizacion.Enabled = false;
                DropDownListArea.Enabled = true;

            }
            else
            {
                TextBoxOrganizacion.Enabled = true;
                DropDownListArea.Enabled = false;
            }
        }

        protected void CambioContactoInterno(object sender, EventArgs e)
        {
            SetCambios();
           
        }

        protected void VolverAinicio(Object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }

}