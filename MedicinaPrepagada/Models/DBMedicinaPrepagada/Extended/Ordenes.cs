using MedicinaPrepagada.Models.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicinaPrepagada.Models.DBMedicinaPrepagada.Source
{
    public partial class Ordenes
    {
        /// <summary>
        /// Metodo que se encarga de las validaciones propias del negocio
        /// </summary>
        /// <returns>Conjunto de reglas no aprobadas</returns>
        public IEnumerable<ReglaValidacion> GetReglasValidacion(Condiciones condicion, int servicios_recibidos)
        {
            int edad = (int)DateTime.Now.Subtract(this.Pacientes.fecha_nacimiento).TotalDays / 360;
            int antiguedad = (int)DateTime.Now.Subtract(this.Pacientes.Titulares.FirstOrDefault() != null ? this.Pacientes.Titulares.FirstOrDefault().fecha_registro : this.Pacientes.Beneficiarios.FirstOrDefault().fecha_registro).TotalDays / 360;
            if (this.Pacientes.Titulares.FirstOrDefault() != null && !this.Pacientes.Titulares.FirstOrDefault().habilitado)
                yield return new ReglaValidacion("El paciente esta inactivo", "id_paciente");
            if (this.Pacientes.Beneficiarios.FirstOrDefault() != null && !this.Pacientes.Beneficiarios.FirstOrDefault().habilitado)
                yield return new ReglaValidacion("El paciente esta inactivo", "id_paciente");
            if (!this.Servicios.habilitado)
                yield return new ReglaValidacion("El servicio esta inactivo", "id_servicio");            
            if (this.Servicios.Condiciones.First().edad_minima < edad || this.Servicios.Condiciones.First().edad_maxima > edad)
                yield return new ReglaValidacion("El usuario no se encuentra en el rango de edad", "id_servicio");            
            if (this.Servicios.Condiciones.First().dias_afiliacion < antiguedad)
                yield return new ReglaValidacion("El usuario no se con la antiguedad requerida", "id_servicio");
            if(servicios_recibidos > condicion.cantidad_maxima_servicios)
                yield return new ReglaValidacion("Cantidad máxima de servicios", "id_servicio");
            yield break;
        }
    }
}