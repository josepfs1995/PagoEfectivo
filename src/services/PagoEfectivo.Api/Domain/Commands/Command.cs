using FluentValidation.Results;
using MediatR;

namespace PagoEfectivo.Api.Domain.Commands
{
    public abstract class Command: IRequest<ValidationResult>
    {
        public ValidationResult ValidationResult { get; set; }
        public abstract bool IsValid();
    }
}