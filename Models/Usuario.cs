namespace Biblioteca.Models
{
    public class Usuario
    {

        public int Id { get; set; }
        public string Login { get; set; }
        public string NomeUsuario { get; set; }
        public string SenhaUsuario { get; set; }
        public int Tipo { get; set; }
        public static int ADMIN = 284;
        public static int USER = 742;
    }
}