using System.Collections.Generic;

namespace PagoEfectivo.Api.Domain.Model
{
    public class Estado
    {
        public int Id_Estado { get; set; }
        public string Descripcion { get; set; }
        public IEnumerable<Promocion> Promocion { get; set; }
    }
}