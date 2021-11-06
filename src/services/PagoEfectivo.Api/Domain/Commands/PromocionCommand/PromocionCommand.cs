using FluentValidation;

namespace PagoEfectivo.Api.Domain.Commands.PromocionCommand
{
    public abstract class PromocionCommand : Command
    {
        public PromocionCommand(string nombre, string email)
        {
            Nombre = nombre;
            Email = email;
        }
        public string Nombre { get; set; }
        public string Email { get; set; }
    }
    public class PromocionValidation<TEntity> : AbstractValidator<TEntity>
        where TEntity : PromocionCommand
    {
        protected void ValidarNombre()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es requerido");
        }
        protected void ValidarEmail()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El nombre es requerido")
                .EmailAddress().WithMessage("El email no es valido");
        }
    }
}