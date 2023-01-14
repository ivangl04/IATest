namespace IATest.Models.Resource
{
    using IATest.Models.Enums;

    public class SolicitudPost
    {
        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Identificacion { get; set; }

        public string Edad { get; set; }

        public AfinidadMagica AfinidadMagica { get; set; }
    }
}
