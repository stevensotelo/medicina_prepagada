using MedicinaPrepagada.Models.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicinaPrepagada.Models.DBMedicinaPrepagada.Source
{
    public partial class Ordenes
    {
        public bool isValid { get { return GetReglasValidacion().Count() == 0; } }

        /// <summary>
        /// Metodo que se encarga de las validaciones propias del negocio
        /// </summary>
        /// <returns>Conjunto de reglas no aprobadas</returns>
        public IEnumerable<ReglaValidacion> GetReglasValidacion()
        {
            if (this.Pacientes.Titulares.FirstOrDefault() != null && !this.Pacientes.Titulares.FirstOrDefault().habilitado)
                yield return new ReglaValidacion("El paciente esta inactivo", "id_paciente");
            if (this.Pacientes.Beneficiarios.FirstOrDefault() != null && !this.Pacientes.Beneficiarios.FirstOrDefault().habilitado)
                yield return new ReglaValidacion("El paciente esta inactivo", "id_paciente");
            if(!this.Servicios.habilitado)
                yield return new ReglaValidacion("El servicio esta inactivo", "id_servicio");
            int dias = (int)DateTime.Now.Subtract(this.Pacientes.fecha_nacimiento).TotalDays / 360;
            if (this.Servicios.Condiciones.First().edad_minima < dias || this.Servicios.Condiciones.First().edad_maxima > dias)
                yield return new ReglaValidacion("El usuario no se encuentra en el rango de edad", "id_servicio");
            dias = (int)DateTime.Now.Subtract(this.Pacientes.Titulares.FirstOrDefault() != null ? this.Pacientes.Titulares.FirstOrDefault().fecha_registro : this.Pacientes.Beneficiarios.FirstOrDefault().fecha_registro).TotalDays / 360;
            if (this.Servicios.Condiciones.First().dias_afiliacion < dias)
                yield return new ReglaValidacion("El usuario no se con la antiguedad requerida", "id_servicio");

            yield break;
        }
    }
}