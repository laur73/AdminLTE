using AdminLTE.Models;
using System.Data;
using System.Data.SqlClient;

namespace AdminLTE.Repositorios
{
    public class RepositorioApoyos
    {
        public List<ApoyoViewModel> Listar()
        {
            var lista = new List<ApoyoViewModel>();
            var con = new Conexion();

            using (var conexion = new SqlConnection(con.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ListarApoyos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new ApoyoViewModel()
                        {
                            IdApoyo = Convert.ToInt32(dr["IdApoyo"]),
                            Nombre = dr["Nombre"].ToString(),
                            Descripcion = dr["Descripcion"].ToString()
                        });
                    }
                }
            }
            return lista;
        }

        public ApoyoViewModel ObtenerId(int IdApoyo)
        {
            var apoyo = new ApoyoViewModel();
            var con = new Conexion();

            using (var conexion = new SqlConnection(con.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ObtenerIdApoyo", conexion);
                cmd.Parameters.AddWithValue("IdApoyo", IdApoyo);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        apoyo.IdApoyo = Convert.ToInt32(dr["IdApoyo"]);
                        apoyo.Nombre = dr["Nombre"].ToString();
                        apoyo.Descripcion = dr["Descripcion"].ToString();
                    }
                }
            }

            return apoyo;
        }

        public bool Crear(ApoyoViewModel apoyo)
        {
            bool rpta;

            try
            {
                var con = new Conexion();

                using (var conexion = new SqlConnection(con.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_CrearApoyo", conexion);
                    cmd.Parameters.AddWithValue("Nombre", apoyo.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", apoyo.Descripcion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Ejecutamos el procedimiento almacenado
                    cmd.ExecuteNonQuery();
                }

                rpta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                rpta = false;
            }

            return rpta;
        }

        public bool Editar(ApoyoViewModel apoyo)
        {
            bool rpta;

            try
            {
                var con = new Conexion();

                using (var conexion = new SqlConnection(con.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_EditarApoyo", conexion);
                    cmd.Parameters.AddWithValue("IdApoyo", apoyo.IdApoyo);
                    cmd.Parameters.AddWithValue("Nombre", apoyo.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", apoyo.Descripcion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();
                }

                rpta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                rpta = false;
            }

            return rpta;
        }

        public bool Eliminar(int IdApoyo)
        {
            bool rpta;

            try
            {
                var con = new Conexion();

                using (var conexion = new SqlConnection(con.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_EliminarApoyo", conexion);
                    cmd.Parameters.AddWithValue("IdApoyo", IdApoyo);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();
                }

                rpta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                rpta = false;
            }

            return rpta;
        }

    }
}
