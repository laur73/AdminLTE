using AdminLTE.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace AdminLTE.Repositorios
{
    //Interfaz que va a contener los metodos
    public interface IRepositorioApoyos
    {
        Task Actualizar(ApoyoViewModel apoyo);
        Task Eliminar(int IdApoyo);
        Task Crear(ApoyoViewModel apoyo);
        Task<bool> Existe(string nombre);
        Task<IEnumerable<ApoyoViewModel>> Listar();
        Task<ApoyoViewModel> ObtenerId(int id);
    }

    //Heredamos la interfaz
    public class RepositorioApoyos : IRepositorioApoyos
    {
        //Declaramos la variable de cadena de conexion de solo lectura
        private readonly string connectionString;

        //Constructor para poder acceder al las propiedades del IConfiguration
        public RepositorioApoyos(IConfiguration configuration)
        {
            //Cadena de conexion
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        //Async await para programacion asincrona: metodos asincronos

        //Método para Listar los Apoyos
        public async Task<IEnumerable<ApoyoViewModel>> Listar()
        {
            //Abrimos la cadena de conexión
            using var connection = new SqlConnection(connectionString);
            //Usamos QueryAsync pporque permite realizar un Select
            return await connection.QueryAsync<ApoyoViewModel>(@"Apoyos_Listar", commandType: System.Data.CommandType.StoredProcedure);
        }

        //Método para saber si ya existe un Apoyo
        public async Task<bool> Existe(string nombre)
        {
            using var connection = new SqlConnection(connectionString);
            //El FirstOrDefaul quiere decir que va a traer lo que encuentre primero o un valor por defecto por eso traemos un int
            var existe = await connection.QueryFirstOrDefaultAsync<int>($@"SELECT 1 FROM Apoyos WHERE Nombre = @Nombre", new { nombre });
            return existe == 1;
        }

        //Método para Insertar un Apoyo
        public async Task Crear(ApoyoViewModel apoyo)
        {
            //Abrimos la cadena de conexión
            using var connection = new SqlConnection(connectionString);

            //QuerySingleAsync quiere decir que nos va a traer un solo resultado
            //El Scope Identity trae el Id del registro recién creado (esta dentro del sp)
            var id = await connection.QuerySingleAsync<int>(@"Apoyos_Crear",
                new
                {
                    apoyo.Nombre,
                    apoyo.Descripcion,
                    apoyo.Cantidad
                },
                commandType: System.Data.CommandType.StoredProcedure);
            apoyo.IdApoyo = id;
        }

        //Método para Obtener el Id de los Apoyos para poder Actualizar o Eliminar
        public async Task<ApoyoViewModel> ObtenerId(int IdApoyo)
        {
            //Abrimos la cadena de conexión
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<ApoyoViewModel>(
                @"Apoyos_ObtenerId", new { IdApoyo }, commandType: System.Data.CommandType.StoredProcedure);
        }

        //Método para Actualizar un Apoyo
        public async Task Actualizar(ApoyoViewModel apoyo)
        {
            //Abrimos la cadena de conexión
            using var connection = new SqlConnection(connectionString);
            //ExecuteAsync quiere decir que solo vamos a realizar una consulta y no retorna nada
            await connection.ExecuteAsync(@"Apoyos_Actualizar", apoyo, commandType: System.Data.CommandType.StoredProcedure);
        }

        //Método para Eliminar un Apoyo
        public async Task Eliminar(int IdApoyo)
        {
            //Abrimos la cadena de conexión
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"Apoyos_Eliminar", new { IdApoyo }, commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}
