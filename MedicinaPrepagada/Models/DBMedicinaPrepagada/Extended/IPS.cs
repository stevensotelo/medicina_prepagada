using MedicinaPrepagada.Models.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicinaPrepagada.Models.DBMedicinaPrepagada.Source
{
    public partial class IPS
    {
        public bool isValidated { get { return GetReglasValidacion().Count() == 0; } }

         /// <summary>
         /// Metodo que se encarga de las validaciones propias del negocio
         /// </summary>
         /// <returns>Conjunto de reglas no aprobadas</returns>
         public IEnumerable<ReglaValidacion> GetReglasValidacion()
         {
             if (string.IsNullOrEmpty(nombre))
                 yield return new ReglaValidacion("El nombre esta vacío","nombre");
             if (!string.IsNullOrEmpty(nombre) && nombre.Length > 50)
                 yield return new ReglaValidacion("El nombre es demasiado largo", "nombre");
             if (string.IsNullOrEmpty(nit))
                 yield return new ReglaValidacion("El nit esta vacío", "nit");
             if (!string.IsNullOrEmpty(nombre) && nit.Length > 10)
                 yield return new ReglaValidacion("El nit es demasiado largo", "nit");
             if (string.IsNullOrEmpty(nombre_contacto))
                 yield return new ReglaValidacion("El nombre de contacto esta vacío", "nombre_contacto");
             if (!string.IsNullOrEmpty(nombre_contacto) && nombre_contacto.Length > 25)
                 yield return new ReglaValidacion("El nombre de contacto es demasiado largo", "nombre_contacto");
             if (string.IsNullOrEmpty(apellidos_contacto))
                 yield return new ReglaValidacion("El apellido de contacto esta vacío", "apellidos_contacto");
             if (!string.IsNullOrEmpty(apellidos_contacto) && apellidos_contacto.Length > 25)
                 yield return new ReglaValidacion("El apellido de contacto es demasiado largo", "apellidos_contacto");
             if (string.IsNullOrEmpty(telefono))
                 yield return new ReglaValidacion("El teléfono esta vacío", "telefono");
             if (!string.IsNullOrEmpty(telefono) && (telefono.Length< 7 || telefono.Length> 10))
                 yield return new ReglaValidacion("El teléfono es demasiado largo", "telefono");
             if(transferencia && string.IsNullOrEmpty(numero_cuenta))
                 yield return new ReglaValidacion("El número de cuenta es obligatorio cuando se usa transferencia", "numero_cuenta");
             if (!transferencia && !string.IsNullOrEmpty(numero_cuenta))
                 yield return new ReglaValidacion("El número de cuenta no es necesario cuando no se paga por transferencia", "numero_cuenta");
             yield break;
        }     
    
    }
}