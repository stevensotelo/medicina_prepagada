//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MedicinaPrepagada.Models.DBMedicinaPrepagada.Source
{
    using System;
    using System.Collections.Generic;
    
    public partial class Condiciones
    {
        public int id_condicion { get; set; }
        public int id_servicio { get; set; }
        public int edad_minima { get; set; }
        public int edad_maxima { get; set; }
        public int dias_afiliacion { get; set; }
        public int cantidad_maxima_servicios { get; set; }
        public string genero { get; set; }
    
        public virtual Servicios Servicios { get; set; }
    }
}
