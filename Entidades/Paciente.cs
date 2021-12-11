using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Paciente : Persona
    {
        public string ObraSocial { get; set; }
        public DateTime HoraIngreso { get; set; }
        public enum enumEstadoPaciente
        {
            SinAtencion,
            EnListaDeEspera,
            EnAtencion,
            Atendido,
            Alta,
            Recetado,
            Derivacion,
            Internacion,
            Tratamiento
        }
        public virtual Entidades.Medico Medico { get; set; }

        public string[] arrEstado = new string[] { "SinAtencion", "EnListaDeEspera", "EnAtencion", "Atendido", "Alta", "Recetado", "Derivado", "Internacion", "Tratamiento" };
        public int Edad { get; set; }
        public int idMedico { get; set; }
        public string nombreMedico { get; set; }
        public string apellidoMedico { get; set; }
        public string especialidadMedico { get; set; }
    }
}
