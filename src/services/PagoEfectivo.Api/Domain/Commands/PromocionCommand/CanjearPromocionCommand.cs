using FluentValidation;
namespace PagoEfectivo.Api.Domain.Commands.PromocionCommand
{
    public class CanjearPromocionCommand : Command
    {
        public CanjearPromocionCommand(string codigo)
        {
            Codigo = codigo;
        }
        public string Codigo { get; set; }
        public override bool IsValid()
        {
            ValidationResult = new CanjearPromocionValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
    public class CanjearPromocionValidation : AbstractValidator<CanjearPromocionCommand>
    {
        public CanjearPromocionValidation()
        {
            ValidarCodigo();
        }
        private void ValidarCodigo()
        {
            RuleFor(x => x.Codigo)
                .NotEmpty().WithMessage("El Codigo es requerido");
        }
    }
}