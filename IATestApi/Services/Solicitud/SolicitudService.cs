namespace IATest.Services.Solicitud
{
    using AutoMapper;
    using IATest.Repository.Solicitud;

    public class SolicitudService : ISolicitudService
    {
        private readonly ISolicitudRepository solicitudRepository;
        private readonly IMapper mapper;

        public SolicitudService(ISolicitudRepository solicitudRepository, IMapper mapper)
        {
            this.solicitudRepository = solicitudRepository;
            this.mapper = mapper;
        }

        public async Task<Models.Internal.Solicitud> ActualizarSolicitud(Models.Internal.Solicitud solicitud)
        {
            var solicitudMapped = this.mapper.Map<Models.Data.Solicitud>(solicitud);
            var solicitudUpdated = await this.solicitudRepository.ActualizarSolicitud(solicitudMapped);
            return this.mapper.Map<Models.Internal.Solicitud>(solicitudUpdated);
        }

        public async Task<Models.Internal.Solicitud> CargarSolicitud(Models.Internal.Solicitud solicitud, bool estado)
        {
            var random = new Random();
            var solicitudMapped = this.mapper.Map<Models.Data.Solicitud>(solicitud);
            if (estado == true)
            {
                solicitudMapped.Estado = Models.Enums.EstadoSolicitud.Aprobada;
                solicitudMapped.Grimorio = (Models.Enums.GrimorioType)random.Next(1, 5);
                var solicitudCreated = await this.solicitudRepository.CargarSolicitud(solicitudMapped);
                return this.mapper.Map<Models.Internal.Solicitud>(solicitudCreated);
            }
            else
            {
                solicitudMapped.Estado = Models.Enums.EstadoSolicitud.Rechazada;
                var solicitudRechazada = await this.solicitudRepository.CargarSolicitud(solicitudMapped);
                return this.mapper.Map<Models.Internal.Solicitud>(solicitudRechazada);
            }
        }

        public async Task<Models.Internal.Solicitud> ConsultarSolicitud(Guid id)
        {
            var solicitudResult = await this.solicitudRepository.ConsultarSolicitud(id);
            return this.mapper.Map<Models.Internal.Solicitud>(solicitudResult);
        }

        public async Task<List<Models.Internal.Solicitud>> ConsultarSolicitudes()
        {
            var solicitudesResult = await this.solicitudRepository.ConsultarSolicitudes();
            return this.mapper.Map<List<Models.Data.Solicitud>, List<Models.Internal.Solicitud>>(solicitudesResult);
        }

        public Task<bool> EliminarSolicitud(Guid id)
        {
            return this.solicitudRepository.EliminarSolicitud(id);
        }
    }
}
