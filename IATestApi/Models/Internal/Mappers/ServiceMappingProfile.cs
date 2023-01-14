namespace IATest.Services.Mappers
{
    using AutoMapper;

    public class ServiceMappingProfile : Profile
    {
        public ServiceMappingProfile()
        {
            this.CreateMap<Models.Internal.Solicitud, Models.Data.Solicitud>().ReverseMap();
        }
    }
}
