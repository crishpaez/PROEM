using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entidades.Medico;
using static Entidades.Paciente;

namespace Logica
{
    public class Medico
    {
        /// <summary>
        /// Agregar un medico
        /// </summary>
        /// <param name="medico">Ingresar medico</param>
        /// <param name="lista">Ingresar la lista de medicos</param>
        public void Agregar(Entidades.Medico medico, List<Entidades.Medico> lista)
        {
            lista.Add(medico);
        }
        /// <summary>
        /// Modificar algún dato del medico
        /// </summary>
        /// <param name="medico">Ingresar un medico</param>
        /// <param name="lista">Ingresar la lista de medicos</param>
        public void Modificar(Entidades.Medico medico, List<Entidades.Medico> lista)
        {
            foreach (var item in lista)
            {
                try
                {
                    if (item.Estado.Equals(item.arrEstado[(int)enumEstadoMedico.Desocupado].ToString()))
                    {
                        if (item.Id == medico.Id)
                        {
                            item.Nombre = medico.Nombre;
                            item.Apellido = medico.Apellido;
                            item.Especialidad = medico.Especialidad;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("\nNo puede modificar un medico que esta ocupado con un paciente");
                }
            }
        }
        /// <summary>
        /// Borrar un medico.
        /// Debe ingresar todos los datos correctamente para poder borrar un medico de la lista
        /// </summary>
        /// <param name="idMedico">Ingrese el id del medico</param>
        /// <param name="lista">Ingrese la lista de medicos</param>
        public void Borrar(int idMedico, List<Entidades.Medico> lista)
        {
            foreach (var item in lista)
            {
                if (item.Id == idMedico && item.Estado == item.arrEstado[(int)enumEstadoMedico.Desocupado])
                {
                    lista.Remove(item);
                }
            }
        }
        /// <summary>
        /// Consultar medico por especialidad
        /// </summary>
        /// <param name="especialidad">Ingresar especialidad</param>
        /// <param name="lista">Ingresar lista de medicos</param>
        /// <returns>Retorna lista filtrada por especialidad</returns>
        public List<Entidades.Medico> TraerMedico(string especialidad, List<Entidades.Medico> lista)
        {
            List<Entidades.Medico> listaMedicos = new List<Entidades.Medico>();
            foreach (var item in lista)
            {
                if (item.Especialidad.ToLower().Equals(especialidad.ToLower()))
                {
                    listaMedicos.Add(item);
                }
            }
            return listaMedicos;
        }
        /// <summary>
        /// Traer medicos por ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lista"></param>
        /// <returns></returns>
        public Entidades.Medico TraerMedicoParaAsignar(int id, List<Entidades.Medico> lista)
        {
            Entidades.Medico medico = new Entidades.Medico();
            foreach (var item in lista)
            {
                if (item.Id.Equals(id))
                {
                    medico.Id = item.Id;
                    medico.Nombre = item.Nombre;
                    medico.Apellido = item.Apellido;
                    medico.Especialidad = item.Especialidad;
                    medico.Estado = item.Estado;
                    medico.CantAtendidos = item.CantAtendidos;
                }
            }
            return medico;
        }
        /// <summary>
        /// Consultar medico por nombre y apellido
        /// </summary>
        /// <param name="nombre">Ingrese el nombre</param>
        /// <param name="apellido">Ingrese el apellido</param>
        /// <param name="lista">Ingrese la lista de medicos</param>
        /// <returns>Retorna lista filtrada por nombre y/o apellido</returns>
        public List<Entidades.Medico> TraerMedico(string nombre, string apellido, List<Entidades.Medico> lista)
        {
            List<Entidades.Medico> listaMedicos = new List<Entidades.Medico>();
            foreach (var item in lista)
            {
                if (item.Nombre.ToLower().Equals(nombre.ToLower()) || item.Apellido.ToLower().Equals(apellido.ToLower()))
                {
                    listaMedicos.Add(item);
                }
            }
            return listaMedicos;
        }
        /// <summary>
        /// Consultar lista de medicos (TODOS)
        /// </summary>
        /// <param name="lista">Ingresar lista de medicos</param>
        /// <returns>Lista de medicos completa</returns>
        public List<Entidades.Medico> TraerMedico(List<Entidades.Medico> lista)
        {
            return lista;
        }
        /// <summary>
        /// Asignar paciente a un medico
        /// </summary>
        /// <param name="dniPaciente">Ingresar dni del paciente</param>
        /// <param name="idMedico">Ingresar id del medico</param>
        /// <param name="listaPaciente">Ingresar lista de pacientes</param>
        /// <param name="listaMedico">Ingresar lista de medicos</param>
        public void AsignarPaciente(int dniPaciente, int idMedico, List<Entidades.Paciente> listaPaciente, List<Entidades.Medico> listaMedico)
        {
            foreach (var itemMedico in listaMedico)
            {
                try
                {
                    if (itemMedico.Estado == "Ocupado" && itemMedico.Id == idMedico)
                    {
                        foreach (var itemPaciente in listaPaciente)
                        {

                            if (itemPaciente.Dni == dniPaciente && itemPaciente.Estado == itemPaciente.arrEstado[(int)enumEstadoPaciente.SinAtencion])
                            {
                                Console.WriteLine("El medico está ocupado!...");
                                Console.WriteLine("Asignando paciente a lista de espera...");
                                itemMedico.Paciente = itemPaciente;
                                itemPaciente.nombreMedico = itemMedico.Nombre;
                                itemPaciente.apellidoMedico = itemMedico.Apellido;
                                itemPaciente.especialidadMedico = itemMedico.Especialidad;
                                itemPaciente.idMedico = itemMedico.Id;
                                Task.Delay(3000);
                                itemPaciente.Estado = itemPaciente.arrEstado[(int)enumEstadoPaciente.EnListaDeEspera];
                                itemPaciente.HoraIngreso = DateTime.Now;
                                Console.WriteLine("Paciente Asignado! (En Lista De Espera)");
                            }
                            else if (itemPaciente.idMedico == itemMedico.Id && itemPaciente.Estado == itemPaciente.arrEstado[(int)enumEstadoPaciente.EnListaDeEspera] && itemMedico.dniPaciente == 1)
                            {
                                itemMedico.Paciente = itemPaciente;
                                itemPaciente.nombreMedico = itemMedico.Nombre;
                                itemPaciente.apellidoMedico = itemMedico.Apellido;
                                itemPaciente.especialidadMedico = itemMedico.Especialidad;
                                itemMedico.dniPaciente = dniPaciente;
                                itemMedico.CantAtendidos++;
                                itemMedico.Estado = itemMedico.arrEstado[(int)enumEstadoMedico.Ocupado];
                                itemPaciente.Estado = itemPaciente.arrEstado[(int)enumEstadoPaciente.EnAtencion];
                                Task.Delay(3000);
                                Console.WriteLine("\nPaciente asignado desde lista de espera! (En Atencion)");
                            }
                            else if (itemPaciente.idMedico == itemMedico.Id && itemPaciente.Estado != itemPaciente.arrEstado[(int)enumEstadoPaciente.EnListaDeEspera] && itemMedico.dniPaciente == 1)
                            {
                                itemMedico.Estado = itemMedico.arrEstado[(int)enumEstadoMedico.Desocupado];
                            }
                        }
                    }
                    if (itemMedico.Estado == "Desocupado" && itemMedico.Id == idMedico)
                    {
                        foreach (var itemPaciente in listaPaciente)
                        {
                            if (itemPaciente.Dni == dniPaciente && itemMedico.Id == idMedico && itemPaciente.Estado == itemPaciente.arrEstado[(int)enumEstadoPaciente.SinAtencion])
                            {
                                itemMedico.Paciente = itemPaciente;
                                itemPaciente.nombreMedico = itemMedico.Nombre;
                                itemPaciente.apellidoMedico = itemMedico.Apellido;
                                itemPaciente.especialidadMedico = itemMedico.Especialidad;
                                Console.WriteLine("Asignando paciente y cambiando el estado del medico...");
                                Task.Delay(3000);
                                itemMedico.dniPaciente = dniPaciente;
                                itemMedico.CantAtendidos++;
                                itemMedico.Estado = itemMedico.arrEstado[(int)enumEstadoMedico.Ocupado].ToString();
                                itemPaciente.Estado = itemPaciente.arrEstado[(int)enumEstadoPaciente.EnAtencion].ToString();
                                Task.Delay(3000);
                                Console.WriteLine("Paciente asignado! (En Atencion)");
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                }
            }

        }
    }
}
