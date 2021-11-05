using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace PagoEfectivo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MainController : ControllerBase
    {
        private ICollection<string> Errores = new List<string>();
        protected IActionResult ProcesarRespuesta(object response = null)
        {
            if (IsValid())
                return Ok(response);
            else
                return BadRequest(new { Errores = Errores });

        }
        protected IActionResult ProcesarRespuesta(ValidationResult result, object response = null)
        {
            AgregarErrores(result);
            return ProcesarRespuesta(response);
        }

        private void AgregarErrores(ValidationResult result)
        {
            if (!result.IsValid)
            {
                Errores = result.Errors.Select(x => x.ErrorMessage).ToList();
            }
        }
        private bool IsValid()
        {
            return !Errores.Any();
        }
    }
}