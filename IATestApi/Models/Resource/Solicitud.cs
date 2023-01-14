namespace IATest.Models.Resource
{
    using IATest.Models.Enums;

    public class Solicitud
    {
        public Guid IdSolicitud { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Identificacion { get; set; }

        public string Edad { get; set; }

        public EstadoSolicitud Estado { get; set; }

        public AfinidadMagica AfinidadMagica { get; set; }

        public GrimorioType Grimorio { get; set; }
    }
}
