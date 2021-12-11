using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Medico : Persona
    {
        public enum enumEstadoMedico
        {
            Ocupado,
            Desocupado
        }
        public string Especialidad { get; set; }
        
        public string[] arrEstado = new string[] { "Ocupado", "Desocupado" };
        public virtual Entidades.Paciente Paciente { get; set; }
        public int dniPaciente { get; set; }
        public int CantAtendidos { get; set; }
    }
}
