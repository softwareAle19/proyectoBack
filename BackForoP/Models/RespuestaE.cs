namespace BackForoP.Models
{
    public class RespuestaE
    {
        public string texto { get; set; }
        public byte[] Multimedia { get; set; }
        public DateTime fecha { get; set; }
        public int idForo { get; set; }
        public int idUsuario { get; set; }

    }
}
