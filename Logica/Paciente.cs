using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using static Entidades.Paciente;
using static Entidades.Medico;

namespace Logica
{
    public class Paciente
    {
        static public Logica.Paciente objLogicaPaciente = new Logica.Paciente();
        
        /// <summary>
        /// Agregar un paciente
        /// </summary>
        /// <param name="paciente">Ingresar el Paciente</param>
        /// <param name="lista">Ingresar la lista de Pacientes</param>
        public void Agregar(Entidades.Paciente paciente, List<Entidades.Paciente> lista)
        {
            lista.Add(paciente);
        }
        /// <summary>
        /// Modificar los datos de un paciente
        /// </summary>
        /// <param name="paciente">Ingresar el del Paciente | DNI - NOMBRE - APELLIDO - OBRASOCIAL</param>
        /// <param name="lista">Ingresar la Lista de pacientes</param>
        public void Modificar(Entidades.Paciente paciente, List<Entidades.Paciente> lista)
        {
            foreach (var item in lista)
            {
                try
                {
                    if (item.Id.Equals(paciente.Id) && item.Estado.Equals(item.arrEstado[(int)enumEstadoPaciente.SinAtencion].ToString()))
                    {
                        item.Dni = paciente.Dni;
                        item.Nombre = paciente.Nombre;
                        item.Apellido = paciente.Apellido;
                        item.ObraSocial = paciente.ObraSocial;
                    }
                    else
                    {
                        Console.WriteLine("No puede modificar un paciente que esta siendo atendido");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        /// <summary>
        /// Borrar un paciente de la lista.
        /// Solo se puede borrar pacientes 'SinAtencion'
        /// </summary>
        /// <param name="id">Ingresar ID del paciente</param>
        /// <param name="lista">Ingresar la lista de donde queire borrar el paciente</param>
        public void Borrar(int id, List<Entidades.Paciente> lista)
        {
            foreach (var item in lista)
            {
                if (item.Id == id && item.Estado == item.arrEstado[(int)enumEstadoPaciente.SinAtencion])
                {
                    lista.Remove(item);
                }
            }
        }
        /// <summary>
        /// Dar de alta un paciente y pasarlo a la lista de atendidos
        /// </summary>
        /// <param name="listaMedicos">Ingresar la lista de medicos</param>
        /// <param name="listaPacientes">Ingresar la lista de pacientes</param>
        /// <param name="listaAtendidos">Ingresar la lista de atendidos</param>
        /// <param name="estadoPaciente">Ingresar el estado del paciente</param>
        /// <param name="dni">Ingresar el dni del paciente</param>
        public void AltaPaciente(List<Entidades.Medico> listaMedicos, List<Entidades.Paciente> listaPacientes, List<Entidades.Paciente> listaAtendidos, string estadoPaciente, int dni)
        {
            foreach (var itemPaciente in listaPacientes)
            {
                if (itemPaciente.Dni == dni && itemPaciente.Estado == itemPaciente.arrEstado[(int)enumEstadoPaciente.EnAtencion])
                {
                    itemPaciente.Estado = estadoPaciente;
                    listaAtendidos.Add(itemPaciente);
                    Task.Delay(10000);
                    Console.WriteLine("\n\n\t\t Demora 2 segundos para que el paciente sea cambiado de lista");
                    Task.Delay(10000);
                    foreach (var medico in listaMedicos)
                    {
                        if (itemPaciente.idMedico == medico.Id)
                        {
                            medico.dniPaciente = 1;
                            Console.WriteLine("cambió el estado del médico");
                        }
                    }
                    try
                    {
                        objLogicaPaciente.Borrar(itemPaciente.Id, listaPacientes);
                        //listaPacientes.Remove(itemPaciente);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message + "\n* Proceso exitoso * ");                        
                    }
                }
            }
        }
        /// <summary>
        /// Consultar paciente por DNI
        /// </summary>
        /// <param name="dni">Ingresar dni</param>
        /// <param name="lista">Ingresar lista de pacientes</param>
        /// <returns>Lista de pacientes filtrada por dni</returns>
        public List<Entidades.Paciente> TraerPaciente(int dni, List<Entidades.Paciente> lista)//dni 
        {
            List<Entidades.Paciente> listaPacientes = new List<Entidades.Paciente>();
            foreach (var item in lista)
            {
                if (item.Dni.Equals(dni))
                {
                    listaPacientes.Add(item);
                }
            }
            return listaPacientes;
        }
        public Entidades.Paciente TraerPacienteParaAsignar(int dni, List<Entidades.Paciente> lista)//dni 
        {
            Entidades.Paciente paciente = new Entidades.Paciente();
            foreach (var item in lista)
            {
                if (item.Dni.Equals(dni))
                {
                    paciente.Id = item.Id;
                    paciente.Dni = item.Dni;
                    paciente.Nombre = item.Nombre;
                    paciente.Apellido = item.Apellido;
                    paciente.FechaNacimiento = item.FechaNacimiento;
                    paciente.HoraIngreso = item.HoraIngreso;
                    paciente.Edad = item.Edad;
                    paciente.ObraSocial = item.ObraSocial;
                    paciente.Estado = item.Estado;
                }
            }
            return paciente;
        }
        /// <summary>
        /// Consultar paciente por nombre y apellido
        /// </summary>
        /// <param name="nombre">Ingresar nombre</param>
        /// <param name="apellido">Ingresar apellido</param>
        /// <param name="lista">Ingresar lista de pacientes</param>
        /// <returns>Lista de pacientes filtrada por nombre y apellido</returns>
        public List<Entidades.Paciente> TraerPaciente(string nombre, string apellido, List<Entidades.Paciente> lista)//nombre, apellido
        {
            List<Entidades.Paciente> listaPacientes = new List<Entidades.Paciente>();
            foreach (var item in lista)
            {
                if (item.Nombre.ToLower().Equals(nombre.ToLower()) || item.Apellido.ToLower().Equals(apellido.ToLower()))
                {
                    listaPacientes.Add(item);
                }
            }
            return listaPacientes;
        }
        /// <summary>
        /// Consultar paciente por edad y obra social
        /// </summary>
        /// <param name="edad">Ingresar edad</param>
        /// <param name="obraSocial">Ingresar obra social</param>
        /// <param name="lista">Ingresar lista de pacientes</param>
        /// <returns>Lista de pacientes filtrada por edad y obra social</returns>
        public List<Entidades.Paciente> TraerPaciente(int edad, string obraSocial, List<Entidades.Paciente> lista)//edad del paciente y obra social
        {
            List<Entidades.Paciente> listaPacientes = new List<Entidades.Paciente>();
            foreach (var item in lista)
            {
                if (item.Edad.Equals(edad) || item.ObraSocial.ToLower().Equals(obraSocial.ToLower()))
                {
                    listaPacientes.Add(item);
                }
            }
            return listaPacientes;
        }
        /// <summary>
        /// Consultar lista de pacientes (TODOS)
        /// </summary>
        /// <param name="lista">Ingresar lista</param>
        /// <returns>Lista completa de pacientes</returns>
        public List<Entidades.Paciente> TraerPaciente(List<Entidades.Paciente> lista)
        {
            return lista;
        }
        /// <summary>
        /// Consultar pacientes en lista de espera
        /// </summary>
        /// <param name="lista">Ingresar lista de pacientes</param>
        /// <returns>pacientes en lista de espera</returns>
        public List<Entidades.Paciente> TraerListaDeEspera(List<Entidades.Paciente> lista)
        {
            List<Entidades.Paciente> listaEspera = new List<Entidades.Paciente>();
            foreach (var item in lista)
            {
                if (item.Estado == item.arrEstado[(int)enumEstadoPaciente.EnListaDeEspera])
                {
                    listaEspera.Add(item);
                }
            }
            return listaEspera;
        }
    }
}
