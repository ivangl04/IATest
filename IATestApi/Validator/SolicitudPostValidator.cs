namespace IATest.Validator
{
    using FluentValidation;
    using IATest.Models.Resource;

    public class SolicitudPostValidator : AbstractValidator<SolicitudPost>
    {
        public SolicitudPostValidator()
        {
            this.RuleFor(sol => sol.Nombre).NotEmpty().MaximumLength(20).Matches("^[a-zA-Z]*$").WithSeverity(Severity.Warning);
            this.RuleFor(sol => sol.Apellido).NotEmpty().MaximumLength(20).Matches("^[a-zA-Z]*$").WithSeverity(Severity.Warning);
            this.RuleFor(sol => sol.Identificacion).NotEmpty().MaximumLength(10).Matches("^[a-zA-Z0-9]*$").WithSeverity(Severity.Warning);
            this.RuleFor(sol => sol.Edad).NotEmpty().MaximumLength(2).Matches("^[0-9]*$").WithSeverity(Severity.Warning);
            this.RuleFor(sol => sol.AfinidadMagica).NotEmpty().IsInEnum().WithSeverity(Severity.Warning);
        }

    }
}
