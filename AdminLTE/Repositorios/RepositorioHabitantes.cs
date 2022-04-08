using AdminLTE.Models;
using System.Data;
using System.Data.SqlClient;

namespace AdminLTE.Repositorios
{
    public class RepositorioHabitantes
    {
        public List<HabitanteViewModel> Listar()
        {
            var lista = new List<HabitanteViewModel>();
            var con = new Conexion();

            using (var conexion = new SqlConnection(con.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ListarHabitantes", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new HabitanteViewModel()
                        {
                            IdHabitante = Convert.ToInt32(dr["IdHabitante"]),
                            Nombre = dr["Nombre"].ToString(),
                            ApePat = dr["ApePat"].ToString(),
                            ApeMat = dr["ApeMat"].ToString(),
                            Direccion = dr["Direccion"].ToString(),
                            FechaNac = Convert.ToDateTime(dr["FechaNac"]),
                            Email = dr["Email"].ToString(),
                            Telefono = dr["Telefono"].ToString()
                        });
                    }
                }
            }
            return lista;
        }

        public HabitanteViewModel ObtenerId(int IdHabitante)
        {
            var habitante = new HabitanteViewModel();
            var con = new Conexion();

            using (var conexion = new SqlConnection(con.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ObtenerIdHabitante", conexion);
                cmd.Parameters.AddWithValue("IdHabitante", IdHabitante);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        habitante.IdHabitante = Convert.ToInt32(dr["IdHabitante"]);
                        habitante.Nombre = dr["Nombre"].ToString();
                        habitante.ApePat = dr["ApePat"].ToString();
                        habitante.ApeMat = dr["ApeMat"].ToString();
                        habitante.Direccion = dr["Direccion"].ToString();
                        habitante.FechaNac = Convert.ToDateTime(dr["FechaNac"]);
                        habitante.Email = dr["Email"].ToString();
                        habitante.Telefono = dr["Telefono"].ToString();
                    }
                }
            }

            return habitante;
        }

        public bool Crear(HabitanteViewModel habitante)
        {
            bool rpta;

            try
            {
                var con = new Conexion();

                using (var conexion = new SqlConnection(con.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_CrearHabitante", conexion);
                    cmd.Parameters.AddWithValue("Nombre", habitante.Nombre);
                    cmd.Parameters.AddWithValue("ApePat", habitante.ApePat);
                    cmd.Parameters.AddWithValue("ApeMat", habitante.ApeMat);
                    cmd.Parameters.AddWithValue("Direccion", habitante.Direccion);
                    cmd.Parameters.AddWithValue("FechaNac", habitante.FechaNac);
                    cmd.Parameters.AddWithValue("Email", habitante.Email);
                    cmd.Parameters.AddWithValue("Telefono", habitante.Telefono);
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

        public bool Editar(HabitanteViewModel habitante)
        {
            bool rpta;

            try
            {
                var con = new Conexion();

                using (var conexion = new SqlConnection(con.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_EditarHabitante", conexion);
                    cmd.Parameters.AddWithValue("IdHabitante", habitante.IdHabitante);
                    cmd.Parameters.AddWithValue("Nombre", habitante.Nombre);
                    cmd.Parameters.AddWithValue("ApePat", habitante.ApePat);
                    cmd.Parameters.AddWithValue("ApeMat", habitante.ApeMat);
                    cmd.Parameters.AddWithValue("Direccion", habitante.Direccion);
                    cmd.Parameters.AddWithValue("FechaNac", habitante.FechaNac);
                    cmd.Parameters.AddWithValue("Email", habitante.Email);
                    cmd.Parameters.AddWithValue("Telefono", habitante.Telefono);
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

        public bool Eliminar(int IdHabitante)
        {
            bool rpta;

            try
            {
                var con = new Conexion();

                using (var conexion = new SqlConnection(con.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_EliminarHabitante", conexion);
                    cmd.Parameters.AddWithValue("IdHabitante", IdHabitante);
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
