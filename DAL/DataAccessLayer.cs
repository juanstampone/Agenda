using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Utils;
using Entidad;

namespace DAL
{
    public class DataAccessLayer : IDisposable

    {
        public SqlConnection connection;

        public DataAccessLayer()
        {
            connection = new SqlConnection
            {
                ConnectionString = Configuracion.GetConnectionString()
            };
        }

        public SqlConnection AbrirConexion()
        {
            try
            {
                connection.Open();
                return connection;
            }
            catch (Exception e)
            {
                //ExceptionPrinter.Print(e);
                return null;
            }
        }

        public DataSet QueryContactosFiltros(SqlConnection conexion, ContactoFiltro cFiltro)
        {
            using (SqlDataAdapter adapter = new SqlDataAdapter())
            {
                adapter.SelectCommand = ConfigSelectCommand(conexion, cFiltro);

                DataSet ds = new DataSet();
                adapter.Fill(ds);
                return ds;
            }
        }

        private SqlCommand ConfigSelectCommand(SqlConnection conexion, ContactoFiltro cFiltro)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandText = "Sp_Contacto_GetByFilter",
                CommandType = CommandType.StoredProcedure,
                Connection = conexion
            };
            cmd.Parameters.AddRange(new SqlParameter[]{
                new SqlParameter() { ParameterName = "@ApellidoNombre", Value = cFiltro.ApellidoNombre, SqlDbType = SqlDbType.VarChar },
                new SqlParameter() { ParameterName = "@IdPais", Value = cFiltro.IdPais, SqlDbType = SqlDbType.Int },
                new SqlParameter() { ParameterName = "@Localidad", Value = cFiltro.Localidad, SqlDbType = SqlDbType.VarChar},
                new SqlParameter() { ParameterName = "@FechaIngDesde", Value = cFiltro.FechaIngresoDesde, SqlDbType = SqlDbType.DateTime},
                new SqlParameter() { ParameterName = "@FechaIngHasta", Value = cFiltro.FechaIngresoHasta, SqlDbType = SqlDbType.DateTime},
                new SqlParameter() { ParameterName = "@ContInterno", Value = cFiltro.ContactoInterno, SqlDbType = SqlDbType.VarChar},
                new SqlParameter() { ParameterName = "@Organizacion", Value = cFiltro.Organizacion, SqlDbType = SqlDbType.VarChar },
                new SqlParameter() { ParameterName = "@IdArea", Value = cFiltro.IdArea, SqlDbType = SqlDbType.Int },
                new SqlParameter() { ParameterName = "@Activo", Value = cFiltro.Activo, SqlDbType = SqlDbType.VarChar }
            });
            return cmd;
        }

        public int EliminarContacto(SqlTransaction transaccion, SqlConnection conexion, int idContacto)
        {
            SqlCommand cmd = new SqlCommand
            {
                Connection = transaccion != null ? transaccion.Connection : conexion,
                Transaction = transaccion,
                CommandType = CommandType.StoredProcedure,
                CommandText = "SP_Eliminar_Contacto"
            };

            cmd = ConfigurarParametrosEliminar(cmd, idContacto);
            int registrosAfectados = cmd.ExecuteNonQuery();
            return registrosAfectados;
        }

        public int CrearContacto(SqlTransaction transaccion, SqlConnection conexion, Contacto contacto)
        {
            SqlCommand cmd = new SqlCommand
            {
                Connection = transaccion != null ? transaccion.Connection : conexion,
                Transaction = transaccion,
                CommandType = CommandType.StoredProcedure,
                CommandText = "SP_Insertar_Contacto"
            };

            cmd = ConfigurarParametros(cmd, contacto);
            int registrosAfectados = cmd.ExecuteNonQuery();
            return registrosAfectados;
        }

        private SqlCommand ConfigurarParametrosEliminar(SqlCommand cmd, int idContacto)
        {
            cmd.Parameters.AddRange(new SqlParameter[]{
                new SqlParameter(){ParameterName = "@IdContacto", Value = idContacto, SqlDbType = SqlDbType.Int}
            });
            return cmd;
        }

        private SqlCommand ConfigurarParametros(SqlCommand cmd, Contacto contacto)
        {
            cmd.Parameters.AddRange(new SqlParameter[]{
                new SqlParameter() { ParameterName = "@ApellidoNombre", Value = contacto.nombreApellido,    SqlDbType = SqlDbType.VarChar },
                new SqlParameter() { ParameterName = "@Genero",      Value = contacto.genero,    SqlDbType = SqlDbType.VarChar },
                new SqlParameter() { ParameterName = "@IdPais",         Value = contacto.IdPais,    SqlDbType = SqlDbType.Int },
                new SqlParameter() { ParameterName = "@Localidad",      Value = contacto.localidad,    SqlDbType = SqlDbType.VarChar },
                new SqlParameter() { ParameterName = "@ContInterno",  Value = contacto.contactoInterno,    SqlDbType = SqlDbType.VarChar},
                new SqlParameter() { ParameterName = "@Organizacion",   Value = contacto.organizacion,    SqlDbType = SqlDbType.VarChar },
                new SqlParameter() { ParameterName = "@IdArea",         Value = contacto.IdArea,    SqlDbType = SqlDbType.Int},
                new SqlParameter() { ParameterName = "@Activo",      Value = contacto.activo,    SqlDbType = SqlDbType.VarChar },
                new SqlParameter() { ParameterName = "@Direccion",      Value = contacto.direccion,    SqlDbType = SqlDbType.VarChar },
                new SqlParameter() { ParameterName = "@TelFijo",       Value = contacto.telefonoFijo,    SqlDbType = SqlDbType.VarChar },
                new SqlParameter() { ParameterName = "@TelCel",        Value = contacto.telefonoCelular,    SqlDbType = SqlDbType.VarChar },
                new SqlParameter() { ParameterName = "@Email",          Value = contacto.email,    SqlDbType = SqlDbType.VarChar },
                new SqlParameter() { ParameterName = "@Skype",          Value = contacto.cuentaSkype,    SqlDbType = SqlDbType.VarChar }
            });
            return cmd;
        }

        public void Dispose()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
                connection.Dispose();
            }
        }
    }
}
