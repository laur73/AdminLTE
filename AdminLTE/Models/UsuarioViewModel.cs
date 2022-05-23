namespace AdminLTE.Models
{
    public class UsuarioViewModel
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string ApePat { get; set; }
        public string ApeMat { get; set; }
        public string Email { get; set; }
        public string EmailNormalizado { get; set; }
        public string PasswordHash { get; set; }
    }
}
