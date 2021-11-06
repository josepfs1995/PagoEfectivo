using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PagoEfectivo.Api.Applications.DTO.Request.PromocionDTOs;
using PagoEfectivo.Api.Applications.DTO.Response.PromocionDTOs;
using PagoEfectivo.Api.Applications.Interfaces;

namespace PagoEfectivo.Api.Controllers
{
    public class PromocionController : MainController
    {
        private readonly IPromocionAppService _promocionAppService;

        public PromocionController(IPromocionAppService promocionAppService)
        {
            _promocionAppService = promocionAppService;
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PromocionResponseDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var response = await _promocionAppService.Listar();
            return ProcesarRespuesta(response);
        }
        [HttpPost("canjear")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CanjearPost(CanjearPromocionRequestDTO request)
        {
            var response = await _promocionAppService.Canjear(request);
            return ProcesarRespuesta(response);
        }
        [HttpPost]
        [ProducesResponseType(typeof(CrearPromocionResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(CrearPromocionRequestDTO request)
        {
            var (validationResult, response) = await _promocionAppService.Crear(request);
            return ProcesarRespuesta(validationResult, response);
        }
    }
}