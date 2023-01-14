using FluentValidation;
using Microsoft.AspNetCore.JsonPatch;
using IATest.Models.Resource;

namespace IATest.Validator
{
    public class SolicitudPatchValidator : AbstractValidator<JsonPatchDocument<SolicitudPatch>>
    {
        public SolicitudPatchValidator()
        {
            this.RuleFor(sol => sol.Operations[0].value).Must(value => value.ToString() !.ToLower() == "aprobada" || value.ToString() !.ToLower() == "rechazada").WithMessage("'Value' debe ser 'Aprobada' o 'Rechazada'");
            this.RuleFor(sol => sol.Operations[0].path).Must(value => value.ToLower() == "/estado").WithMessage("'Path' debe ser '/Estado'");
        }
    }
}