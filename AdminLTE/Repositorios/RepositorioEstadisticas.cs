using Dapper;
using Microsoft.Data.SqlClient;

namespace AdminLTE.Repositorios
{
    public interface IRepositorioEstadisticas
    {
        Task<int> ContarApoyos();
        Task<int> ContarApoyosAsignados();
        Task<int> ContarApoyosEntregados();
        Task<int> ContarHabitantes();
    }

    public class RepositorioEstadisticas : IRepositorioEstadisticas
    {
        private readonly string connectionString;

        public RepositorioEstadisticas(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> ContarHabitantes()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.ExecuteScalarAsync<int>(
                "SELECT COUNT(*) FROM Habitantes");
        }

        public async Task<int> ContarApoyos()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.ExecuteScalarAsync<int>(
                "SELECT COUNT(*) FROM Apoyos");
        }

        public async Task<int> ContarApoyosAsignados()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.ExecuteScalarAsync<int>(
                "SELECT COUNT(*) FROM Beneficios WHERE IdEstado = 1");
        }

        public async Task<int> ContarApoyosEntregados()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.ExecuteScalarAsync<int>(
                "SELECT COUNT(*) FROM Beneficios WHERE IdEstado = 2");
        }
    }
}
