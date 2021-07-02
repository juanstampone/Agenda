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
        int PageSize = 5;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarFechas();
                CargarFiltros();
                ExisteBusqueda((bool)Application["FiltroExiste"] );
            }

       
        }

        private void ExisteBusqueda(bool existe)
        {
            if (existe)
            {
                ContactoFiltro contactoFiltro = new ContactoFiltro();
                contactoFiltro = (ContactoFiltro)Application["FiltroBusqueda"];

                TextBoxNombre.Text = contactoFiltro.ApellidoNombre == "&nbsp;" ? "" : contactoFiltro.ApellidoNombre;
                ListaDePaises.SelectedValue = contactoFiltro.IdPais.ToString();
                TextBoxLocalidad.Text = contactoFiltro.Localidad == "&nbsp;" ? "" : contactoFiltro.Localidad;
                DropDownCI.SelectedValue = contactoFiltro.ContactoInterno.ToString();
                TextBoxOrganizacion.Text = contactoFiltro.Organizacion == "&nbsp;" ? "" : contactoFiltro.Organizacion;
                DropDownListArea.SelectedValue = contactoFiltro.IdArea.ToString();
                DropDownListActivo.SelectedValue = contactoFiltro.Activo.ToString();

                //fechas 
                String newDateDesde = contactoFiltro.FechaIngresoDesde.ToString("dd/MM/yyyy");
                String newDateHasta = contactoFiltro.FechaIngresoHasta.ToString("dd/MM/yyyy");

                TextBoxFID.Text = newDateDesde;
                TextBoxFIH.Text = newDateHasta;

                ConsultarContacto(contactoFiltro.paginadoPropiedades.PageIndex, PageSize);
            }
        }

        private void CargarFechas()
        {
            TextBoxFIH.Text = DateTime.Now.ToString("dd/MM/yyyy");
            TextBoxFID.Text = DateTime.Now.AddDays(-30).ToString("dd/MM/yyyy");
        }

        public void ValidarFechas(Object source, ServerValidateEventArgs Fechas)
        {
            DateTime fechaDesde = Convert.ToDateTime(TextBoxFID.Text);
            DateTime fechaHasta = Convert.ToDateTime(TextBoxFIH.Text);
            int resultado = DateTime.Compare(fechaDesde, fechaHasta);
            if (resultado > 0)
            {
                Fechas.IsValid = false;
            }
            else
            {
                Fechas.IsValid = true;
            }
            
        }
        public void Consultar(Object sender, EventArgs e) 
        {
            Page.Validate();
            if (Page.IsValid)
            {
                ConsultarContacto(1, PageSize);
                
            }


        }

        private void ConsultarContacto(int pageIndex, int pageSize)
        {
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
                Organizacion = TextBoxOrganizacion.Text.Equals("") ? null : TextBoxOrganizacion.Text,
                paginadoPropiedades = new PaginadoPropiedades
                {
                    PageIndex = pageIndex,
                    PageSize = PageSize
                }
            };
          
            Application["FiltroBusqueda"] = CFiltro;
            Application["FiltroExiste"] = true;
            IContactBussines contactoBussines = new ContactBussines();
            List<Contacto> contactos = contactoBussines.ConsultaFiltroContacto(CFiltro,pageIndex,pageSize);
            GridViewConsulta.DataSource = contactos;
            GridViewConsulta.DataBind();
            foreach (GridViewRow row in GridViewConsulta.Rows)
            {
                if (row.Cells[9].Text.Equals("SI"))
                {
                    ImageButton columnImagen = (ImageButton)row.FindControl("BtnActivar");
                    columnImagen.ImageUrl = "/Images/anular.png";
                }
            }
            this.PopulatePager(CFiltro.paginadoPropiedades.RecordsCount, pageIndex);
        }

        private void PopulatePager(int recordCount, int currentPage)
        {
            double dblPageCount = (double)((decimal)recordCount / (PageSize));
            int pageCount = (int)Math.Ceiling(dblPageCount);
            List<ListItem> pages = new List<ListItem>();
            if (pageCount > 0)
            {
                pages.Add(new ListItem("Primero >> ", "1", currentPage > 1));
                for (int i = 1; i <= pageCount; i++)
                {
                    pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                }
                pages.Add(new ListItem(" << Ultimo", pageCount.ToString(), currentPage < pageCount));
            }
            rptPager.DataSource = pages;
            rptPager.DataBind();
        }

        protected void lnkbtn_PageIndexChanged(object sender, EventArgs e)
        {
            int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
            ConsultarContacto(pageIndex, PageSize);
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
            Application["modo"] = "ALTA";
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
                contactoInterno = row.Cells[5].Text,
                organizacion = row.Cells[6].Text,
                area = row.Cells[7].Text,
                activo = row.Cells[9].Text,
                direccion = row.Cells[10].Text,
                telefonoFijo = row.Cells[11].Text,
                telefonoCelular = row.Cells[12].Text,
                email = row.Cells[13].Text,
                cuentaSkype = row.Cells[14].Text
            };

            Application["ContactoSeleccionado"] = contacto;
            Application["modo"] = "EDITAR";
            Response.Redirect("AEContacto.aspx", false);
        }

        protected void EliminarContacto(Object sender, EventArgs e)
        {
            ImageButton boton = (ImageButton)sender;
            GridViewRow row = (GridViewRow)boton.DataItemContainer;
            int idContacto = int.Parse(row.Cells[0].Text);

            string confirmarValor = Request.Form["confirm_value"];
            if (confirmarValor == "Si")
            {
                IContactBussines contactoBussines = new ContactBussines();
                int resultado = contactoBussines.delete(idContacto);

                ContactoFiltro contactoFiltro = (ContactoFiltro)Application["FiltroBusqueda"];
                ConsultarContacto(contactoFiltro.paginadoPropiedades.PageIndex, PageSize);
            }
        }
        public void LimpiarCampos(Object sender, EventArgs e)
        {
            TextBoxNombre.Text = "";
            TextBoxLocalidad.Text = "";
            CargarFechas();
            ListaDePaises.SelectedValue = "0";
            DropDownCI.SelectedValue = "0";
            TextBoxOrganizacion.Text = "";
            TextBoxOrganizacion.Enabled = true;
            DropDownListArea.SelectedValue = "0";
            DropDownListArea.Enabled = false;
            DropDownListActivo.SelectedValue = "0";
        }

        protected void CambioContactoInterno(object sender, EventArgs e)
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

        protected void ActivarContacto(object sender, ImageClickEventArgs e)
        {
            ImageButton boton = (ImageButton)sender;
            GridViewRow row = (GridViewRow)boton.DataItemContainer;

            //setContactoElegido(row);
            int idContacto = int.Parse(row.Cells[0].Text);
            string activo = row.Cells[9].Text;

            string confirmarValor = Request.Form["confirm_value"];
            if (confirmarValor == "Si")
            {
                ContactBussines contactoBussines = new ContactBussines();
                int? resultado = contactoBussines.ActivarPausarContacto(idContacto, activo);
                ContactoFiltro contactoFiltro = (ContactoFiltro)Application["FiltroBusqueda"];
                ConsultarContacto(contactoFiltro.paginadoPropiedades.PageIndex, PageSize);
            }
        }

        protected void ConsultarContactoDetalle(object sender, ImageClickEventArgs e)
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
                contactoInterno = row.Cells[5].Text,
                organizacion = row.Cells[6].Text,
                area = row.Cells[7].Text,
                activo = row.Cells[9].Text,
                direccion = row.Cells[10].Text,
                telefonoFijo = row.Cells[11].Text,
                telefonoCelular = row.Cells[12].Text,
                email = row.Cells[13].Text,
                cuentaSkype = row.Cells[14].Text

               

            };
            Application["ContactoSeleccionado"] = contacto;
            Application["Modo"] = "CONSULTAR";
            Response.Redirect("AEContacto.aspx", false);
        }
    }
}