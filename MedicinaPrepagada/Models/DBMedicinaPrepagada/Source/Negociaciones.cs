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
    
    public partial class Negociaciones
    {
        public int id_negociacion { get; set; }
        public int id_servicio { get; set; }
        public int id_ips { get; set; }
        public double valor { get; set; }
        public bool habilitado { get; set; }
    
        public virtual IPS IPS { get; set; }
        public virtual Servicios Servicios { get; set; }
    }
}
