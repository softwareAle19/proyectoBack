namespace BackForoP.Models
{
    public class UsuarioE
    {
        public string documento { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string estado { get; set; }
        public byte[] imagen { get; set; }
        public string rol { get; set; }
    }
}
