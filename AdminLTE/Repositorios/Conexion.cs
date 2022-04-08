namespace AdminLTE.Repositorios
{
    public class Conexion
    {
        private string cadenaSQL = String.Empty;

        public Conexion()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.Development.json").Build();
            cadenaSQL = builder.GetConnectionString("CadenaSQL");
        }

        public string getCadenaSQL()
        {
            return cadenaSQL;
        }
    }
}
