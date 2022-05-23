using Dapper;
using AdminLTE.Models;
using Microsoft.Data.SqlClient;

namespace AdminLTE.Repositorios
{
    public interface IRepositorioUsuarios
    {
        Task<UsuarioViewModel> BuscarUsuarioPorEmail(string emailNormalizado);
        Task<int> CrearUsuario(UsuarioViewModel usuario);
    }

    public class RepositorioUsuarios : IRepositorioUsuarios
    {
        private readonly string connectionString;

        public RepositorioUsuarios(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> CrearUsuario(UsuarioViewModel usuario)
        {
            using var connection = new SqlConnection(connectionString);
            var usuarioId = await connection.QuerySingleAsync<int>(
                                                            @"INSERT INTO Usuarios (Nombre, ApePat, ApeMat, Email, EmailNormalizado, PasswordHash)
                                                            VALUES (@Nombre, @ApePat, @ApeMat, @Email, @EmailNormalizado, @PasswordHash);
                                                            SELECT SCOPE_IDENTITY();", usuario);
            return usuarioId;
        }

        public async Task<UsuarioViewModel> BuscarUsuarioPorEmail(string emailNormalizado)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QuerySingleOrDefaultAsync<UsuarioViewModel>(@"SELECT * FROM Usuarios WHERE EmailNormalizado = @emailNormalizado",
                                                                        new { emailNormalizado });
        }
    }
}