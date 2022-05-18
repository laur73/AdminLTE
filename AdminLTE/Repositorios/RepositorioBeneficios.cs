using AdminLTE.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace AdminLTE.Repositorios
{
    //Interfaz que va a contener los metodos
    public interface IRepositorioBeneficios
    {
        Task Actualizar(BeneficioCreacionViewModel beneficio);
        Task Crear(BeneficioViewModel beneficio);
        Task Eliminar(int IdBeneficio);
        Task<bool> Existe(int idHabitante, int idApoyo);
        Task<IEnumerable<BeneficioViewModel>> Listar();
        Task<BeneficioViewModel> ObtenerId(int IdBeneficio);
    }

    public class RepositorioBeneficios : IRepositorioBeneficios
    {
        //Declaramos la variable de cadena de conexion de solo lectura
        private readonly string connectionString;

        //Constructor para poder acceder al las propiedades del IConfiguration
        public RepositorioBeneficios(IConfiguration configuration)
        {
            //Cadena de conexion
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        //Async await para programacion asincrona: métodos asincronos

        //Método para Listar los Beneficios
        public async Task<IEnumerable<BeneficioViewModel>> Listar()
        {
            //Abrimos la cadena de conexión
            using var connection = new SqlConnection(connectionString);
            //Usamos QueryAsync pporque permite realizar un Select
            return await connection.QueryAsync<BeneficioViewModel>(@"Beneficios_Listar", commandType: System.Data.CommandType.StoredProcedure);
        }

        //Método para saber si ya existe un Beneficio

        public async Task<bool> Existe(int IdHabitante, int IdApoyo)
        {
            using var connection = new SqlConnection(connectionString);
            //El FirstOrDefault quiere decir que va a traer lo que encuentre primero o un valor por defecto por eso traemos un int
            var existe = await connection.QueryFirstOrDefaultAsync<int>(
                @"SELECT 1 FROM Beneficios WHERE IdHabitante = @IdHabitante AND IdApoyo = @IdApoyo", new { IdHabitante, IdApoyo });
            return existe == 1;
        }

        //Método para Insertar un Beneficio
        public async Task Crear(BeneficioViewModel beneficio)
        {
            //Abrimos la cadena de conexión
            using var connection = new SqlConnection(connectionString);

            //QuerySingleAsync quiere decir que nos va a traer un solo resultado
            //El Scope Identity trae el Id del registro recién creado (esta dentro del sp)
            var id = await connection.QuerySingleAsync<int>(@"Beneficios_Crear",
                new
                {
                    beneficio.IdHabitante,
                    beneficio.IdApoyo,
                    beneficio.Cantidad,
                    beneficio.IdEstado,
                    beneficio.Fecha
                },
                commandType: System.Data.CommandType.StoredProcedure);


            beneficio.IdBeneficio = id;
        }

        //Método para Obtener el Id de los Habitantes para poder Actualizar o Eliminar
        public async Task<BeneficioViewModel> ObtenerId(int IdBeneficio)
        {
            //Abrimos la cadena de conexión
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<BeneficioViewModel>(
                @"Beneficios_ObtenerId", new { IdBeneficio }, commandType: System.Data.CommandType.StoredProcedure);
        }

        //Método para Actualizar un Beneficio
        public async Task Actualizar(BeneficioCreacionViewModel beneficio)
        {
            //Abrimos la cadena de conexión
            using var connection = new SqlConnection(connectionString);
            //ExecuteAsync quiere decir que solo vamos a realizar una consulta y no retorna nada
            await connection.ExecuteAsync(@"UPDATE Beneficios SET IdHabitante = @IdHabitante, IdApoyo = @IdApoyo,
                                            Cantidad = @Cantidad, IdEstado =  @IdEstado, Fecha = @Fecha
                                            WHERE IdBeneficio = @IdBeneficio", beneficio);
        }

        //Método para Eliminar un Beneficio
        public async Task Eliminar(int IdBeneficio)
        {
            //Abrimos la cadena de conexión
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"Beneficios_Eliminar", new { IdBeneficio }, commandType: System.Data.CommandType.StoredProcedure);
        }

    }
}
