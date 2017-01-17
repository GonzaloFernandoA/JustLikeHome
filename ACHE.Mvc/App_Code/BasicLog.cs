using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;

namespace ACHE.Model {
    public class BasicLog
    {
		public static void AppendToFile(string fileName, string msj, string detalle) {
			var texto = string.Format("Fecha: {0} \r\nError: {1}\r\nDetalle: {2}\r\n^^-------------------------------------------------------------------^^\r\n", DateTime.Now, msj, detalle);
			File.AppendAllText(fileName, texto, Encoding.GetEncoding(1252));
		}
	}

}
