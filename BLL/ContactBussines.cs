using Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data.SqlClient;
using System.Data;
using Utils;

namespace BLL
{
    public class ContactBussines : IContactBussines
    {
        private List<Contacto> listContacto;

        public ContactBussines(List<Contacto> lista)
        {
            this.listContacto = lista;
        }

        public ContactBussines()
        {
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

        public int update(Contacto contacto)
        {
            using (DataAccessLayer dal = new DataAccessLayer())
            {
                SqlConnection conexion = dal.AbrirConexion();
                SqlTransaction transaccion = conexion.BeginTransaction();

                int resultado = dal.ActualizarContacto(transaccion, conexion, contacto);
                transaccion.Commit();
                return resultado;
            }
            return 0;
        }

        public int delete(int idContacto)
        {
            using (DataAccessLayer dal = new DataAccessLayer())
            {
                SqlConnection conexion = dal.AbrirConexion();
                SqlTransaction transaccion = conexion.BeginTransaction();

                int resultado = dal.EliminarContacto(transaccion, conexion, idContacto);
                transaccion.Commit();
                return resultado;
            }
            return 0;
        }

        public int insertar(Contacto contacto)
        {
            using (DataAccessLayer dal = new DataAccessLayer())
            {
                SqlConnection conexion = dal.AbrirConexion();
                SqlTransaction transaccion = conexion.BeginTransaction();

                int resultado = dal.CrearContacto(transaccion, conexion, contacto);
                transaccion.Commit();
                return resultado;
            }
            return 0;
        }

        public List<Contacto> ConsultaFiltroContacto(ContactoFiltro contactoFiltro, int pageIndex, int pageSize)
        {
            using (DataAccessLayer dal = new DataAccessLayer())
            {
                SqlConnection conexion = dal.AbrirConexion();

                DataSet ds = dal.QueryContactosFiltros(conexion, contactoFiltro, pageIndex, pageSize);
                return MapDataSetToContacto(ds);
            }

        }

        private List<Contacto> MapDataSetToContacto(DataSet ds)
        {
            List<Contacto> listaContacto = new List<Contacto>();
            if (DataSetHelper.HasRecords(ds))
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    listaContacto.Add(MapDataRowToContacto(row));
                }
                return listaContacto;
            }
            else
            {
                return null;
            }
        }

        private Contacto MapDataRowToContacto(DataRow row)
        {
            return new Contacto
            {
                id = Convert.ToInt32(row["IdContacto"]),
                nombreApellido = Convert.ToString(row["ApellidoNombre"]),
                genero = Convert.ToString(row["Genero"]),
                pais = Convert.ToString(row["NombrePais"]),
                localidad = Convert.ToString(row["Localidad"]),
                contactoInterno = Convert.ToString(row["ContactoInterno"]),
                organizacion = Convert.ToString(row["Organizacion"]),
                area = Convert.ToString(row["NombreArea"]),
                fechaIngreso = Convert.ToDateTime(row["FechaIngreso"]),
                activo = Convert.ToString(row["Activo"]),
                direccion = Convert.ToString(row["Direccion"]),
                telefonoFijo = Convert.ToString(row["TelefonoFijo"]),
                telefonoCelular = Convert.ToString(row["TelefonoCelular"]),
                email = Convert.ToString(row["Email"]),
                cuentaSkype = Convert.ToString(row["CuentaSkype"])
            };
        }
        public int? ActivarPausarContacto(int id_contacto, string activo)
        {
            int? regAfec;
            using (DataAccessLayer dal = new DataAccessLayer())
            {
                var connection = dal.AbrirConexion();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {

                    regAfec = dal.ActivarPausarContacto(transaction, connection, id_contacto, activo);
                    transaction.Commit();

                    return regAfec;

                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return null;
                }
                finally
                {
                    transaction.Dispose();
                }
            }
        }
    }
}
