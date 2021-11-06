using System;

namespace PagoEfectivo.Api.Domain.Model{
    public class Promocion{
        public Guid Id_Promocion { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public int Id_Estado { get; set; }
        public Estado Estado { get; set; }
    }
}