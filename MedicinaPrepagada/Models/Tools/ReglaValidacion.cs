using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicinaPrepagada.Models.Tools
{
    public class ReglaValidacion
    {
        public string mensaje { get; private set; }
        public string propiedad { get; private set; }
        public ReglaValidacion(string mensaje)
        {
            this.mensaje = mensaje;
        }
        public ReglaValidacion(string mensaje, string propiedad)
        {
            this.mensaje = mensaje;
            this.propiedad = propiedad;
        }
    }
}