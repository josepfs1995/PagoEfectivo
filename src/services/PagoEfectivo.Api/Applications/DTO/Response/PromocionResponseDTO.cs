using Mapster;

namespace PagoEfectivo.Api.Applications.DTO.Response.PromocionDTOs
{
    public class PromocionResponseDTO
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        [AdaptMember("Estado_Descripcion")]
        public string Estado { get; set; }
    }
}