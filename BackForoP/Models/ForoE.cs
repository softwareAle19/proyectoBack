namespace BackForoP.Models
{
    public class ForoE : CursoE
    {
        public int  idForo { get; set; }
        public string nombreForo { get; set; }
        public string descripcion { get; set; }
        public byte[] imagenForo { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaCreacion { get; set; }
        public DateTime fechaFin { get; set; }


    }
}
