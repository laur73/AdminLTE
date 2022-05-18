using AdminLTE.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace AdminLTE.Repositorios
{
    //Interfaz que va a contener los metodos
    public interface IRepositorioHabitantes
    {
        Task Actualizar(HabitanteViewModel habitante);
        Task Crear(HabitanteViewModel habitante);
        Task Eliminar(int IdHabitante);
        Task<bool> Existe(string nombre, string apepat, string apemat);
        Task<IEnumerable<HabitanteViewModel>> Listar();
        Task<HabitanteViewModel> ObtenerId(int IdHabitante);
    }

    //Heredamos de la interfaz
    public class RepositorioHabitantes : IRepositorioHabitantes
    {
        //Declaramos la variable de cadena de conexion de solo lectura
        private readonly string connectionString;

        //Constructor para poder acceder al las propiedades del IConfiguration
        public RepositorioHabitantes(IConfiguration configuration)
        {
            //Cadena de conexion
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        //Async await para programacion asincrona: metodos asincronos

        //Método para Listar los Habitantes
        public async Task<IEnumerable<HabitanteViewModel>> Listar()
        {
            //Abrimos la cadena de conexión
            using var connection = new SqlConnection(connectionString);
            //Usamos QueryAsync pporque permite realizar un Select
            return await connection.QueryAsync<HabitanteViewModel>(@"Habitantes_Listar", commandType: System.Data.CommandType.StoredProcedure);
        }

        //Método para saber si ya existe un Habitante
        public async Task<bool> Existe(string nombre, string apepat, string apemat)
        {
            using var connection = new SqlConnection(connectionString);
            //El FirstOrDefaul quiere decir que va a traer lo que encuentre primero o un valor por defecto por eso traemos un int
            var existe = await connection.QueryFirstOrDefaultAsync<int>(
                @"SELECT 1 FROM Habitantes WHERE Nombre = @Nombre AND ApePat = @ApePat AND ApeMat = @ApeMat", new { nombre, apepat, apemat });
            return existe == 1;
        }

        //Método para Insertar un Habitante
        public async Task Crear(HabitanteViewModel habitante)
        {
            //Abrimos la cadena de conexión
            using var connection = new SqlConnection(connectionString);

            //QuerySingleAsync quiere decir que nos va a traer un solo resultado
            //El Scope Identity trae el Id del registro recién creado (esta dentro del sp)
            var id = await connection.QuerySingleAsync<int>(@"Habitantes_Crear",
                new
                {
                    habitante.Nombre,
                    habitante.ApePat,
                    habitante.ApeMat,
                    habitante.Sexo,
                    habitante.Direccion,
                    habitante.FechaNac,
                    habitante.Telefono
                },
                commandType: System.Data.CommandType.StoredProcedure);
            habitante.IdHabitante = id;
        }

        //Método para Obtener el Id de los Habitantes para poder Actualizar o Eliminar
        public async Task<HabitanteViewModel> ObtenerId(int IdHabitante)
        {
            //Abrimos la cadena de conexión
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<HabitanteViewModel>(
                @"Habitantes_ObtenerId", new { IdHabitante }, commandType: System.Data.CommandType.StoredProcedure);
        }

        //Método para Actualizar un Habitante
        public async Task Actualizar(HabitanteViewModel habitante)
        {
            //Abrimos la cadena de conexión
            using var connection = new SqlConnection(connectionString);
            //ExecuteAsync quiere decir que solo vamos a realizar una consulta y no retorna nada
            await connection.ExecuteAsync(@"Habitantes_Actualizar", habitante, commandType: System.Data.CommandType.StoredProcedure);
        }

        //Método para Eliminar un Apoyo
        public async Task Eliminar(int IdHabitante)
        {
            //Abrimos la cadena de conexión
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"Habitantes_Eliminar", new { IdHabitante }, commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}
