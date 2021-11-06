using System;
using System.Linq;
using PagoEfectivo.Api.Domain.Const;

namespace PagoEfectivo.Api.Domain.Commands.PromocionCommand
{
    public class CrearPromocionCommand : PromocionCommand
    {
        public CrearPromocionCommand(string nombre, string email) : base(nombre, email)
        {
            Id_Promocion = Guid.NewGuid();
            Id_Estado = EstadoConst.GENERADO;
        }
        public Guid Id_Promocion { get; set; }
        public string Codigo { get; set; }
        public int Id_Estado { get; set; }
        public override bool IsValid()
        {
            ValidationResult = new CrearPromocionValidation().Validate(this);
            return ValidationResult.IsValid;
        }
      
    }
    public class CrearPromocionValidation : PromocionValidation<CrearPromocionCommand>
    {
        public CrearPromocionValidation()
        {
            ValidarNombre();
            ValidarEmail();
        }
    }
}