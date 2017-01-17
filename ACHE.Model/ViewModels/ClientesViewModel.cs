using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACHE.Model {
    public class ClientesViewModel {
        public int IDCliente { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Pais { get; set; }
        public string NroDocumento { get; set; }
        public string Activo { get; set; }
    }
}
