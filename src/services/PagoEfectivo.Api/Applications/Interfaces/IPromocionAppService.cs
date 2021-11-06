using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using PagoEfectivo.Api.Applications.DTO.Request.PromocionDTOs;
using PagoEfectivo.Api.Applications.DTO.Response.PromocionDTOs;

namespace PagoEfectivo.Api.Applications.Interfaces{
    public interface IPromocionAppService{
        Task<(ValidationResult, CrearPromocionResponseDTO)> Crear(CrearPromocionRequestDTO model);
        Task<ValidationResult> Canjear(CanjearPromocionRequestDTO model);
        Task<IEnumerable<PromocionResponseDTO>> Listar();
    }
}