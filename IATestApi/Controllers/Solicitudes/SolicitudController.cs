namespace IATest.Controllers.Solicitudes
{
    using System;
    using AutoMapper;
    using FluentValidation;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.Mvc;
    using IATest.Models.Resource;
    using IATest.Services.Solicitud;
    using IATest.Models.Enums;

    [ApiController]
    [Route("api/solicitud")]
    public class SolicitudController : Controller
    {
        private readonly ISolicitudService solicitudService;
        private readonly IMapper mapper;
        private readonly IValidator<SolicitudPost> validatorPost;
        private readonly IValidator<Solicitud> validatorPut;
        private readonly IValidator<JsonPatchDocument<SolicitudPatch>> validatorPatch;

        public SolicitudController(
            ISolicitudService solicitudService,
            IMapper mapper,
            IValidator<SolicitudPost> validatorPost,
            IValidator<JsonPatchDocument<SolicitudPatch>> validatorPatch,
            IValidator<Solicitud> validatorPut)
        {
            this.solicitudService = solicitudService;
            this.mapper = mapper;
            this.validatorPost = validatorPost;
            this.validatorPatch = validatorPatch;
            this.validatorPut = validatorPut;
        }

        [HttpPost(Name = nameof(PostSolicitud))]
        [ProducesResponseType(typeof(Solicitud), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Solicitud), StatusCodes.Status400BadRequest)]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> PostSolicitud([FromBody] SolicitudPost solicitudPost)
        {
            var validatorResult = this.validatorPost.Validate(solicitudPost);
            if (solicitudPost == null)
            {
                return this.BadRequest();
            }

            var solicitudMapped = this.mapper.Map<Models.Internal.Solicitud>(solicitudPost);

            if (!validatorResult.IsValid)
            {
                var solicitudRechazada = await this.solicitudService.CargarSolicitud(solicitudMapped, false);
                return this.BadRequest(this.mapper.Map<Solicitud>(solicitudRechazada));
            }

            var solicitudCreated = await this.solicitudService.CargarSolicitud(solicitudMapped, true);
            return this.Created(nameof(this.PostSolicitud), this.mapper.Map<Solicitud>(solicitudCreated));
        }

        [HttpPut(Name = nameof(PutSolicitud))]
        [ProducesResponseType(typeof(Solicitud), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> PutSolicitud([FromBody] Solicitud solicitud)
        {
            var validatorResult = this.validatorPut.Validate(solicitud);
            if (!validatorResult.IsValid)
            {
                return this.BadRequest(validatorResult.Errors);
            }

            var internalSolicitud = await this.solicitudService.ConsultarSolicitud(solicitud.IdSolicitud);

            if (internalSolicitud == null)
            {
                return this.NotFound();
            }

            var solicitudMapped = this.mapper.Map<Models.Internal.Solicitud>(solicitud);
            var solicitudUpdated = await this.solicitudService.ActualizarSolicitud(solicitudMapped);
            return this.Ok(this.mapper.Map<Solicitud>(solicitudUpdated));
        }

        [HttpPatch("{id}", Name = nameof(PatchSolicitud))]
        [ProducesResponseType(typeof(Solicitud), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Consumes("application/json-patch+json")]
        [Produces("application/json")]
        public async Task<IActionResult> PatchSolicitud(Guid id, [FromBody] JsonPatchDocument<SolicitudPatch> solicitudPatch)
        {
            var validatorResult = this.validatorPatch.Validate(solicitudPatch);
            if (!validatorResult.IsValid)
            {
                return this.BadRequest(validatorResult.Errors);
            }

            var internalSolicitud = await this.solicitudService.ConsultarSolicitud(id);

            if (internalSolicitud == null)
            {
                return this.NotFound();
            }

            var baseSolicitudPatch = this.mapper.Map<SolicitudPatch>(internalSolicitud);
            baseSolicitudPatch.Estado = (EstadoSolicitud)Enum.Parse(typeof(EstadoSolicitud), solicitudPatch.Operations[0].value.ToString());

            try
            {
                solicitudPatch.ApplyTo(baseSolicitudPatch, this.ModelState);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, e);
            }

            var solicitudMapped = this.mapper.Map<Models.Internal.Solicitud>(baseSolicitudPatch);
            solicitudMapped.IdSolicitud = id;
            var solicitudUpdated = await this.solicitudService.ActualizarSolicitud(solicitudMapped);
            return this.Ok(this.mapper.Map<Solicitud>(solicitudUpdated));
        }

        [HttpGet(Name= nameof(GetSolicitudes))]
        [ProducesResponseType(typeof(SolicitudResponseCollection), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<IActionResult> GetSolicitudes()
        {
            var solicitudes = await this.solicitudService.ConsultarSolicitudes();
            if (solicitudes == null)
            {
                return this.NotFound();
            }

            return this.Ok(this.mapper.Map<SolicitudResponseCollection>(solicitudes));
        }

        [HttpGet("grimorio/{id}")]
        [ProducesResponseType(typeof(GrimorioAsignado), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<IActionResult> GetGrimorioById(Guid id)
        {
            var solicitud = await this.solicitudService.ConsultarSolicitud(id);
            if (solicitud == null)
            {
                return this.NotFound();
            }

            return this.Ok(this.mapper.Map<Models.Resource.GrimorioAsignado>(solicitud));
        }

        [HttpDelete("{id}", Name = nameof(DeleteSolicitud))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteSolicitud(Guid id)
        {
            var internalSolicitud = await this.solicitudService.ConsultarSolicitud(id);
            if (internalSolicitud == null)
            {
                return this.NotFound();
            }

            var result = await this.solicitudService.EliminarSolicitud(id);

            if (result)
            {
                return this.Ok();
            }
            else
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
