using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACHE.Model {
    public class ContactosViewModel {
        public int IDContacto { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Asunto { get; set; }
        public string Mensaje { get; set; }
        public int? IDPropiedad { get; set; }
    }
}
