using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACHE.Model {
	public class WebUser {
		public string Nombre { get; set; }
		public string Apellido { get; set; }
		public bool EsAdmin { get; set; }
		public string Email { get; set; }
		public int IDUsuario { get; set; }
		public string TipoUsuario { get; set; }

		public WebUser(string email, string nombre, string apellido, int idUsuario, bool esAdmin, string tipoUsuario) {
			this.Email = email;
            this.Nombre = nombre;
            this.Apellido = apellido;
			this.IDUsuario = idUsuario;
			this.EsAdmin = esAdmin;
			this.TipoUsuario = tipoUsuario;
		}
	}
}