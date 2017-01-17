using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Mvc;

namespace ACHE.Models {
    public class ReservaFrontViewModel {
        public int IDPropiedad { get; set; }

        public string Nombre { get; set; }

        [Display(Name = "Fecha de Ingreso")]
        [Required(ErrorMessage = "Debe ingresar la fecha de ingreso")]
        [Column(TypeName = "DateTime2")]
        public DateTime FechaIngreso { get; set; }

        //[Display(Name = "Fecha de Egreso")]
        //[Required(ErrorMessage = "Debe ingresar la fecha de egreso")]
        //[Column(TypeName = "DateTime2")]
        //public DateTime FechaEgreso { get; set; }

        [Display(Name = "Cant. Meses")]
        [Required(ErrorMessage = "Debe ingresar la cantidad de meses")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad de meses debe ser mayor a 0")]
        public int CantMeses { get; set; }

        [Display(Name = "Cant. Huespedes")]
        [Required(ErrorMessage = "Debe ingresar la cantidad de huespedes")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad de huespedes debe ser mayor a 0")]
        public int CantHuespedes { get; set; }

        public string Observaciones { get; set; }
        public ClienteFrontViewModel Cliente { get; set; }
        public DetalleViewModel Propiedad { get; set; }
    }

    public class ClienteFrontViewModel {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Debe ingresar su nombre")]
        public string Nombre { get; set; }

        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "Debe ingresar su apellido")]
        public string Apellido { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Debe ingresar su email")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Debe ingresar un email válido")]  
        public string Email { get; set; }

        [Display(Name = "Teléfono")]
        [Required(ErrorMessage = "Debe ingresar su teléfono")]
        public string Telefono { get; set; }

        [Display(Name = "Nro Documento")]
        [Required(ErrorMessage = "Debe ingresar su número de documento")]
        public string NroDocumento { get; set; }

        [Display(Name = "País")]
        [Required(ErrorMessage = "Debe ingresar su país")]
        public string Pais { get; set; }
    }
}