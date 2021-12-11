using System;
using System.Collections.Generic;
using System.Linq;
using static Entidades.Medico;
using static Entidades.Paciente;
using System.Threading.Tasks;
using System.Threading;
using System.Text.RegularExpressions;

namespace Presentacion
{
    public class Program
    {
        static public List<Entidades.Medico> ListaMedicos = new List<Entidades.Medico>();
        static public List<Entidades.Paciente> ListaPacientes = new List<Entidades.Paciente>();
        static public List<Entidades.Paciente> ListaPacientesAtendidos = new List<Entidades.Paciente>();

        static public Logica.Medico objLogicaMedico = new Logica.Medico();
        static public Logica.Paciente objLogicaPaciente = new Logica.Paciente();

        static void Main(string[] args)
        {
            Medicos(); //Crea la lista de medicos
            Pacientes();//Crea la lista de pacientes

            string strSeleccionarOpcion = "";
            int intSeleccionarOpcion = 0;
            string strSalir = "";

            do
            {
                Bienvenida();
                OpcionesInicioTXT();
                intSeleccionarOpcion = ValidacionIngresoOpcionesSwitch(intSeleccionarOpcion, strSeleccionarOpcion, 5);
                switch (intSeleccionarOpcion)
                {
                    case 1: //Consultar lista de medicos
                        Console.Clear();
                        Console.WriteLine("_________________________________");
                        Console.WriteLine("|    CONSULTAR LISTA MEDICOS    |");
                        Console.WriteLine("|-------------------------------|");
                        OpcionListaMedica();
                        break;
                    case 2: //Consultar lista de pacientes
                        Console.Clear();
                        Console.WriteLine("_________________________________");
                        Console.WriteLine("|   CONSULTAR LISTA PACIENTES   |");
                        Console.WriteLine("|-------------------------------|");
                        OpcionListaPaciente();
                        break;
                    case 3: //Gestion Medicos
                        Console.Clear();
                        Console.WriteLine("_________________________________");
                        Console.WriteLine("|        GESTION MEDICOS        |");
                        Console.WriteLine("|-------------------------------|");
                        GestionMedico();
                        break;
                    case 4: //Gestion Pacientes
                        Console.Clear();
                        Console.WriteLine("_________________________________");
                        Console.WriteLine("|       GESTION PACIENTES       |");
                        Console.WriteLine("|-------------------------------|");
                        GestionPaciente();
                        break;
                    case 5:
                        Console.Clear();
                        Console.WriteLine("Salir");
                        Console.WriteLine("\nPresione una tecla para continuar");
                        Console.ReadKey();
                        strSalir = "salir";
                        break;
                    default:
                        Console.WriteLine("No seleccionaste ninguna opción");
                        break;
                }
                Console.Clear();
            } while (strSalir != "salir");

            // Pausa
            Console.Write("Presione cualquier tecla para finalizar el programa");
            Console.ReadKey();
            Task.Delay(2000);
            Console.Clear();
            Task.Delay(1000);
        }
        public static void OpcionesInicioTXT()
        {
            Console.WriteLine("   Operaciones que puede realizar");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("1 - Consultar lista de medicos");
            Console.WriteLine("2 - Consultar lista de pacientes");
            Console.WriteLine("3 - Gestion Medico");
            Console.WriteLine("4 - Gestion Paciente");
            Console.WriteLine("5 - Salir");
            Console.WriteLine("------------------------------------");
        }
        public static void Bienvenida()
        {
            Console.WriteLine("\t\t\t\t\tBienvenido/a!!                 |");
            Console.WriteLine("\t\t\t\t\tSoftware de gestión de turnos  |");
            Console.WriteLine("------------------------------------------------------------------------");
        }
        public static void OpcionListaMedicaTXT()
        {
            Console.WriteLine("\n\n  Lista de opciones [Consulta lista de medicos]");
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("1 - Consultar por especialidad");
            Console.WriteLine("2 - Consultar por nombre y/o apellido");
            Console.WriteLine("3 - Mostrar todos los medicos");
            Console.WriteLine("4 - Consultar por cantidad de pacientes atendidos");
            Console.WriteLine("5 - Salir");
            Console.WriteLine("-------------------------------------------------");
        }
        public static void OpcionListaPacienteTXT()
        {
            Console.WriteLine("\n\n Lista de opciones [Consulta lista de pacientes]");
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine("1 - Consultar por DNI");
            Console.WriteLine("2 - Consultar por nombre y/o apellido");
            Console.WriteLine("3 - Consultar por edad y/u obra social");
            Console.WriteLine("4 - Mostrar todos los pacientes");
            Console.WriteLine("5 - Consultar cantidad de pacientes atendidos(ListaAtendidos)");
            Console.WriteLine("6 - Consultar lista de espera");
            Console.WriteLine("7 - Salir");
            Console.WriteLine("---------------------------------------------------------------");
        }
        public static void GestionMedicoTXT()
        {
            Console.WriteLine("\n\n     Lista de opciones [Gestion de medicos]     ");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("1 - Asignar un paciente");
            Console.WriteLine("2 - Agregar un medico");
            Console.WriteLine("3 - Modificar un medico");
            Console.WriteLine("4 - Eliminar un medico");
            Console.WriteLine("5 - Salir");
            Console.WriteLine("--------------------------------------------------");
        }
        public static void GestionPacienteTXT()
        {
            Console.WriteLine("\n\n     Lista de opciones [Gestion de pacientes]     ");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("1 - Dar de alta un paciente");
            Console.WriteLine("2 - Agregar un paciente");
            Console.WriteLine("3 - Modificar un paciente");
            Console.WriteLine("4 - Eliminar un paciente");
            Console.WriteLine("5 - Salir");
            Console.WriteLine("--------------------------------------------------");
        }

        public static void OpcionListaMedica()// 1
        {
            int intSeleccionarOpcion = 0;
            string strSeleccionarOpcion = "";
            string especialidad = "";
            string nombre = "";
            string apellido = "";
            do
            {
                OpcionListaMedicaTXT();
                intSeleccionarOpcion = ValidacionIngresoOpcionesSwitch(intSeleccionarOpcion, strSeleccionarOpcion, 5);
                switch (intSeleccionarOpcion)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("________________________________");
                        Console.WriteLine("|             MEDICO           |");
                        Console.WriteLine("|------------------------------|");
                        Console.WriteLine("|  CONSULTAR POR ESPECIALIDAD  |");
                        Console.WriteLine("|------------------------------|");
                        Console.WriteLine("\nLista especialidades:\nOftalmologia\nTraumatologia\nOdontologia\nOtorrinolaringologia\nClinico");
                        Console.Write("\n Especialidad:");
                        especialidad = Console.ReadLine();
                        Console.WriteLine("");
                        Console.WriteLine("Lista de Medicos (filtro por especialidad)");
                        Console.WriteLine("------------------------------------------\n");
                        foreach (var item in objLogicaMedico.TraerMedico(especialidad, ListaMedicos))
                        {
                            Console.WriteLine($"ID :{item.Id}  \n|Nombre :{item.Apellido}, {item.Nombre}  \n|Especialidad :{item.Especialidad}  \n|Estado :{item.Estado}  \n|DNI Paciente :{item.dniPaciente}\n");
                        }
                        Console.WriteLine("\nPresione una tecla para continuar");
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("___________________________________");
                        Console.WriteLine("|              MEDICO             |");
                        Console.WriteLine("|---------------------------------|");
                        Console.WriteLine("| CONSULTAR POR NOMBRE Y APELLIDO |");
                        Console.WriteLine("|---------------------------------|");
                        Console.Write("\nIngresa nombre :");
                        nombre = Console.ReadLine();
                        Console.Write("\nIngresa apellido :");
                        apellido = Console.ReadLine();
                        Console.WriteLine("");
                        Console.WriteLine("Lista de Medicos (filtro por Nombre/Apellido)");
                        Console.WriteLine("---------------------------------------------\n");
                        foreach (var item in objLogicaMedico.TraerMedico(nombre, apellido, ListaMedicos))
                        {
                            Console.WriteLine($"|ID :{item.Id}  \n|Nombre :{item.Apellido}, {item.Nombre}  \n|Especialidad :{item.Especialidad}  \n|Estado :{item.Estado}  \n|DNI Paciente :{item.dniPaciente}\n");
                        }
                        Console.WriteLine("\nPresione una tecla para continuar");
                        Console.ReadKey();
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("________________________________");
                        Console.WriteLine("|             MEDICO           |");
                        Console.WriteLine("|------------------------------|");
                        Console.WriteLine("|     TRAER LISTA COMPLETA     |");
                        Console.WriteLine("|------------------------------|");
                        Console.WriteLine("");
                        foreach (var item in objLogicaMedico.TraerMedico(ListaMedicos))
                        {
                            Console.WriteLine($"|ID :{item.Id}  \n|Nombre :{item.Apellido}, {item.Nombre}  \n|Especialidad :{item.Especialidad}  \n|Estado :{item.Estado}  \n|DNI Paciente :{item.dniPaciente}\n");

                        }
                        Console.WriteLine("\nPresione una tecla para continuar");
                        Console.ReadKey();
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("_________________________________________");
                        Console.WriteLine("|                  MEDICO               |");
                        Console.WriteLine("|---------------------------------------|");
                        Console.WriteLine("|    CANTIDAD DE PACIENTES ATENDIDOS    |");
                        Console.WriteLine("|---------------------------------------|");
                        Console.WriteLine("");
                        foreach (var item in ListaMedicos.OrderByDescending(x => x.CantAtendidos))
                        {
                            Console.WriteLine($"|PacientesAtendidos :[ {item.CantAtendidos} ]  \n|ID :{item.Id}  \n|Nombre :{item.Apellido}, {item.Nombre}  \n|Especialidad :{item.Especialidad}  \n|Estado :{item.Estado}  \n|DNI Paciente :{item.dniPaciente}\n");
                        }
                        Console.WriteLine("\nPresione una tecla para continuar");
                        Console.ReadKey();
                        break;
                    case 5:
                        Console.Clear();
                        Console.WriteLine("Salir");
                        Console.WriteLine("\nPresione una tecla para continuar");
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("Ingresaste datos no validos");
                        Console.WriteLine("\nPresione una tecla para continuar");
                        Console.ReadKey();
                        break;
                }
            } while (intSeleccionarOpcion < 1 || intSeleccionarOpcion > 5);

        }
        public static void OpcionListaPaciente()// 2
        {
            string strSeleccionarOpcion = "";
            int intSeleccionarOpcion = 0;
            int dni = 0;
            int edad = 0;
            string nombre = "";
            string apellido = "";
            string obraSocial = "";
            //Regex regexDNI = new Regex(@"^[0 - 9]{0,8}$");

            do
            {
                OpcionListaPacienteTXT();
                intSeleccionarOpcion = ValidacionIngresoOpcionesSwitch(intSeleccionarOpcion, strSeleccionarOpcion, 7);
                switch (intSeleccionarOpcion)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("___________________________");
                        Console.WriteLine("|        PACIENTE         |");
                        Console.WriteLine("|-------------------------|");
                        Console.WriteLine("|    CONSULTAR POR DNI    |");
                        Console.WriteLine("|-------------------------|");
                        try
                        {
                            Console.Write("\nIngrese DNI :");//ejem: 40321456
                            dni = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("");
                            foreach (var item in objLogicaPaciente.TraerPaciente(dni, ListaPacientes))
                            {
                                if (item.Dni.Equals(dni))
                                {
                                    Console.WriteLine($"|ID :{item.Id}  \n|DNI :{item.Dni}  \n|Nombre :{item.Apellido}, {item.Nombre}  \n|Obra Social :{item.ObraSocial}  \n|Estado :{item.Estado}\n");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            Console.WriteLine("\n[DNI] se admite 'SOLO NÚMEROS'");
                        }
                        Console.WriteLine("\nPresione una tecla para continuar");
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("_____________________________________");
                        Console.WriteLine("|              PACIENTE             |");
                        Console.WriteLine("|-----------------------------------|");
                        Console.WriteLine("|  CONSULTAR POR NOMBRE Y APELLIDO  |");
                        Console.WriteLine("|-----------------------------------|");
                        try
                        {
                            Console.Write("\nIngrese el nombre :");
                            nombre = Console.ReadLine();
                            Console.Write("\nIngrese el apellido :");
                            apellido = Console.ReadLine();
                            Console.WriteLine("");
                            foreach (var item in objLogicaPaciente.TraerPaciente(nombre, apellido, ListaPacientes))
                            {
                                Console.WriteLine($"|ID :{item.Id}  \n|DNI :{item.Dni}  \n|Nombre :{item.Apellido}, {item.Nombre}  \n|Obra Social :{item.ObraSocial}  \n|Estado :{item.Estado}\n");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            Console.WriteLine("\nPaciente no encontrado (Revise si ingresó bien los datos solicitados)");
                        }
                        Console.WriteLine("\nPresione una tecla para continuar");
                        Console.ReadKey();
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("______________________________________");
                        Console.WriteLine("|              PACIENTE              |");
                        Console.WriteLine("|------------------------------------|");
                        Console.WriteLine("|  CONSULTAR POR EDAD Y OBRA SOCIAL  |");
                        Console.WriteLine("|------------------------------------|");
                        try
                        {
                            do
                            {
                                Console.Write("\nIngrese la edad :");
                                edad = Convert.ToInt32(Console.ReadLine());
                            } while (edad > 120 || edad < 1);
                            try
                            {
                                Console.Write("\nTipo de cobertura medica (ObraSocial) :\n1 - NoTiene\n2 - CoberturaBasica\n3 - CoberturaCompleta\n---------------------------------------------\nEn caso de fallo se selecciona predeterminado('NoTiene')\nSelecciona la cobertura medica :");
                                strSeleccionarOpcion = Console.ReadLine();
                                intSeleccionarOpcion = Convert.ToInt32(strSeleccionarOpcion);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                Console.WriteLine("\nIngresó mal los caracteres para consultar obrasocial");
                                Console.WriteLine("Se seleccionará 'NoTiene' de manera predeterminada");
                            }
                            obraSocial = opcionObraSocial(intSeleccionarOpcion, obraSocial);
                            Console.WriteLine("");
                            foreach (var item in objLogicaPaciente.TraerPaciente(edad, obraSocial, ListaPacientes))
                            {
                                Console.WriteLine($"|ID :{item.Id}  \n|DNI :{item.Dni}  \n|Nombre :{item.Apellido}, {item.Nombre}  \n|Edad :{item.Edad}  \n|Obra Social :{item.ObraSocial}  \n|Estado :{item.Estado}\n");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            Console.WriteLine("\nPaciente no encontrado (Revise si ingresó bien los datos solicitados)");
                        }
                        Console.WriteLine("\nPresione una tecla para continuar");
                        Console.ReadKey();
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("______________________________________");
                        Console.WriteLine("|               PACIENTE             |");
                        Console.WriteLine("|------------------------------------|");
                        Console.WriteLine("|        TRAER LISTA COMPLETA        |");
                        Console.WriteLine("|------------------------------------|\n");
                        foreach (var item in objLogicaPaciente.TraerPaciente(ListaPacientes))
                        {
                            Console.WriteLine($"|ID :{item.Id}  \n|DNI :{item.Dni}  \n|Nombre :{item.Apellido}, {item.Nombre}  \n|Obra Social :{item.ObraSocial}  \n|Estado :{item.Estado} \n|Medico :{item.apellidoMedico}, {item.nombreMedico} / {item.especialidadMedico}\n");
                        }
                        Console.WriteLine("\nPresione una tecla para continuar");
                        Console.ReadKey();
                        break;
                    case 5:
                        Console.Clear();
                        Console.WriteLine("(Sino muestra datos puede que la lista se encuentre vacia)");
                        Console.WriteLine("");
                        Console.WriteLine("");
                        Console.WriteLine("______________________________________");
                        Console.WriteLine("|               PACIENTE             |");
                        Console.WriteLine("|------------------------------------|");
                        Console.WriteLine("|    CONSULTAR LISTA DE ATENDIDOS    |");
                        Console.WriteLine("|------------------------------------|\n");
                        foreach (var item in objLogicaPaciente.TraerPaciente(ListaPacientesAtendidos))
                        {
                            Console.WriteLine($"|ID :{item.Id}  \n|DNI :{item.Dni}  \n|Nombre :{item.Apellido}, {item.Nombre}  \n|Obra Social :{item.ObraSocial}  \n|Estado :{item.Estado}  \n|Medico :{item.apellidoMedico}, {item.nombreMedico} / {item.especialidadMedico}\n");
                        }
                        Console.WriteLine("\nPresione una tecla para continuar");
                        Console.ReadKey();
                        break;
                    case 6:
                        Console.Clear();
                        Console.WriteLine("(Sino muestra datos puede que la lista se encuentre vacia)");
                        Console.WriteLine("");
                        Console.WriteLine("");
                        Console.WriteLine("_____________________________________");
                        Console.WriteLine("|              PACIENTE             |");
                        Console.WriteLine("|-----------------------------------|");
                        Console.WriteLine("|     CONSULTAR LISTA DE ESPERA     |");
                        Console.WriteLine("|-----------------------------------|\n");
                        foreach (var item in objLogicaPaciente.TraerListaDeEspera(ListaPacientes).OrderBy(x => x.HoraIngreso))
                        {
                            Console.WriteLine($"|ID :{item.Id}  \n|DNI :{item.Dni}  \n|Nombre :{item.Apellido}, {item.Nombre}  \n|Obra Social :{item.ObraSocial}  \n|Estado :{item.Estado}");
                            Console.WriteLine($"|Consultorio :{item.especialidadMedico}, {item.nombreMedico} {item.apellidoMedico}  \n|Hora ingreso :{item.HoraIngreso.ToString("HH:mm:ss")}\n");
                        }
                        Console.WriteLine("\nPresione una tecla para continuar");
                        Console.ReadKey();
                        break;
                    case 7:
                        Console.Clear();
                        Console.WriteLine("Salir");
                        Console.WriteLine("\nPresione una tecla para continuar");
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("Ingresaste datos no validos");
                        Console.WriteLine("\nPresione una tecla para continuar");
                        Console.ReadKey();
                        break;
                }

            } while (intSeleccionarOpcion < 1 || intSeleccionarOpcion > 7);
        }
        public static void GestionMedico()
        {
            string strSeleccionarOpcion = "";
            int intSeleccionarOpcion = 0;
            int dniPaciente = 0;
            int idMedico = 0;
            string apellido = "";
            string nombre = "";
            string especialidad = "";
            do
            {
                GestionMedicoTXT();
                intSeleccionarOpcion = ValidacionIngresoOpcionesSwitch(intSeleccionarOpcion, strSeleccionarOpcion, 5);
                switch (intSeleccionarOpcion)
                {
                    case 1://Asignar un paciente
                        Console.Clear();
                        Console.WriteLine("___________________________");
                        Console.WriteLine("|     GESTION MEDICO      |");
                        Console.WriteLine("|-------------------------|");
                        Console.WriteLine("|   ASIGNAR UN PACIENTE   |");
                        Console.WriteLine("|-------------------------|");

                        string dniPrueba = "";
                        Console.WriteLine("\n\nLista de pacientes |");
                        Console.WriteLine("-------------------\n");
                        foreach (var item in ListaPacientes)
                        {
                            if (item.Estado == item.arrEstado[(int)enumEstadoPaciente.SinAtencion])
                            {
                                Console.WriteLine($"|DNI :{item.Dni}  \n|Apellido y Nombre :{item.Apellido}, {item.Nombre}\n");
                            }
                        }
                        Console.Write("\nIngrese el DNI del paciente :");
                        dniPrueba = Console.ReadLine();
                        try
                        {
                            dniPaciente = Convert.ToInt32(dniPrueba);
                            Console.WriteLine("\n\nLista de medicos |");
                            Console.WriteLine("------------------\n");
                            foreach (var item in ListaMedicos)
                            {
                                Console.WriteLine($"|ID :{item.Id}  \n|Apellido y Nombre :{item.Apellido}, {item.Nombre}  \n|Especialidad :{item.Especialidad}\n");
                            }
                            Console.Write("\nIngrese el ID del medico :");
                            idMedico = Convert.ToInt32(Console.ReadLine());
                            objLogicaMedico.AsignarPaciente(dniPaciente, idMedico, ListaPacientes, ListaMedicos);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            Console.WriteLine("\nIngresaste un dato no valido o falló la asignación");
                        }
                        Console.WriteLine("\nPresione una tecla para continuar");
                        Console.ReadKey();
                        break;
                    case 2://Agregar un medico
                        Console.Clear();
                        Console.WriteLine("_____________________________");
                        Console.WriteLine("|      GESTION MEDICO       |");
                        Console.WriteLine("|---------------------------|");
                        Console.WriteLine("|     AGREGAR UN MEDICO     |");
                        Console.WriteLine("|---------------------------|");
                        Entidades.Medico nuevoMedico = new Entidades.Medico();
                        try
                        {
                            Console.Write("\nIngresar el nombre :");
                            nombre = Console.ReadLine();
                            Console.Write("\nIngresar el apellido :");
                            apellido = Console.ReadLine();
                            Console.Write("\nIngresar la Especialidad :");
                            especialidad = Console.ReadLine();
                            if (nombre != "" && nombre is not null && apellido != "" && apellido is not null && especialidad != "" && especialidad is not null)
                            {
                                idMedico = ListaMedicos.Count();
                                nuevoMedico.Id = idMedico;
                                nuevoMedico.Nombre = nombre;
                                nuevoMedico.Apellido = apellido;
                                nuevoMedico.Especialidad = especialidad;
                                nuevoMedico.Estado = nuevoMedico.arrEstado[(int)enumEstadoMedico.Desocupado].ToString();

                                objLogicaMedico.Agregar(nuevoMedico, ListaMedicos);
                                Console.WriteLine("Medico agregado!");
                            }
                        }
                        catch (Exception ex)
                        {
                            objLogicaMedico.Borrar(nuevoMedico.Id, ListaMedicos);
                            Console.WriteLine(ex.Message);
                            Console.WriteLine("\nEl medico no pudo agregarse");
                        }
                        Console.WriteLine("\nPresione una tecla para continuar");
                        Console.ReadKey();
                        break;
                    case 3://Modificar un medico
                        Console.Clear();
                        Console.WriteLine("_____________________________");
                        Console.WriteLine("|      GESTION MEDICO       |");
                        Console.WriteLine("|---------------------------|");
                        Console.WriteLine("|    MODIFICAR UN MEDICO    |");
                        Console.WriteLine("|---------------------------|\n");
                        foreach (var item in ListaMedicos)
                        {
                            Console.WriteLine($"|ID :{item.Id}  \n|Nombre completo :{item.Apellido}, {item.Nombre}  \n|Especialidad :{item.Especialidad}\n");
                        }
                        try
                        {
                            Console.Write("\nIngrese el id del medico :");
                            idMedico = Convert.ToInt32(Console.ReadLine());
                            foreach (var medico in ListaMedicos)
                            {
                                if (medico.Id.Equals(idMedico))
                                {
                                    Console.Write("\nIngresar el nombre :");
                                    nombre = Console.ReadLine();
                                    Console.Write("\nIngresar el apellido :");
                                    apellido = Console.ReadLine();
                                    Console.Write("\nIngresar especialidad :");
                                    especialidad = Console.ReadLine();
                                    if (nombre != "" && nombre is not null && apellido != "" && apellido is not null && especialidad != "" && especialidad is not null)
                                    {
                                        medico.Especialidad = especialidad;
                                        medico.Nombre = nombre;
                                        medico.Apellido = apellido;
                                        objLogicaMedico.Modificar(medico, ListaMedicos);
                                        Console.WriteLine("Modificando medico...");

                                        Task.Delay(3000);
                                        Console.Clear();
                                        Task.Delay(1000);
                                        Console.WriteLine("Medico Modificado\n");
                                        foreach (var item in ListaMedicos)
                                        {
                                            if (item.Id == idMedico)
                                            {
                                                Console.WriteLine($"[Actualizado]\n|ID :{item.Id}  \n|Nombre Completo :{item.Apellido},{item.Nombre}  \n|Especialidad :{item.Especialidad}\n");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            Console.WriteLine("\n[DNI] se admite 'SOLO NÚMEROS'");
                        }
                        Console.WriteLine("\nPresione una tecla para continuar");
                        Console.ReadKey();
                        break;
                    case 4://Borrar un medico
                        Console.Clear();
                        Console.WriteLine("____________________________");
                        Console.WriteLine("|      GESTION MEDICO      |");
                        Console.WriteLine("|--------------------------|");
                        Console.WriteLine("|     BORRAR UN MEDICO     |");
                        Console.WriteLine("|--------------------------|\n");
                        foreach (var item in ListaMedicos)
                        {
                            Console.WriteLine($"|ID :{item.Id}  \n|Nombre completo :{item.Apellido}, {item.Nombre}  \n|Especialidad :{item.Especialidad}\n");
                        }
                        do
                        {
                            try
                            {
                                Console.Write("\nIngresar el id del medico :");
                                idMedico = Convert.ToInt32(Console.ReadLine());
                            }
                            catch (Exception ex)
                            {
                                idMedico = ListaMedicos.Count + 1;
                                Console.WriteLine(ex.Message);
                                Console.WriteLine("Error");
                            }
                        } while (idMedico < 0 || idMedico > ListaMedicos.Count - 1);
                        try
                        {
                            objLogicaMedico.Borrar(idMedico, ListaMedicos);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message + "\nMedico eliminado");
                        }
                        Console.WriteLine("\nPresione una tecla para continuar");
                        Console.ReadKey();
                        break;
                    case 5:
                        Console.Clear();
                        Console.WriteLine("Salir");
                        Console.WriteLine("\nPresione una tecla para continuar");
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("Ingresaste datos no validos");
                        Console.WriteLine("\nPresione una tecla para continuar");
                        Console.ReadKey();
                        break;
                }
            } while (intSeleccionarOpcion < 1 || intSeleccionarOpcion > 5);
        }

        public static void GestionPaciente()
        {
            string strSeleccionarOpcion = "";
            int intSeleccionarOpcion = 0;
            string nombre = "";
            string apellido = "";
            string obraSocial = "";
            string estado = "";

            DateTime fechaNac = new DateTime();
            int dni = 0;
            int idPaciente = 1;
            int dniPredeterminado = 00000001;

            do
            {
                GestionPacienteTXT();
                intSeleccionarOpcion = ValidacionIngresoOpcionesSwitch(intSeleccionarOpcion, strSeleccionarOpcion, 5);
                switch (intSeleccionarOpcion)
                {
                    case 1:// Dar de alta paciente
                        Console.Clear();
                        Console.WriteLine("____________________________");
                        Console.WriteLine("|     GESTION PACIENTE     |");
                        Console.WriteLine("|--------------------------|");
                        Console.WriteLine("|       ALTA PACIENTE      |");
                        Console.WriteLine("|--------------------------|\n");
                        foreach (var item in ListaPacientes)
                        {
                            if (item.Estado == item.arrEstado[(int)enumEstadoPaciente.EnAtencion])
                            {
                                Console.WriteLine($"|ID :{item.Id}  \n|DNI :{item.Dni}  \n|Nombre Completo :{item.Apellido}, {item.Nombre}\n");
                            }
                        }
                        try
                        {
                            Console.Write("\nIngrese DNI (solo números) :");//ejem: 40321456
                            dni = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        estado = SeleccionEstado(estado);
                        objLogicaPaciente.AltaPaciente(ListaMedicos, ListaPacientes, ListaPacientesAtendidos, estado, dni);
                        foreach (var itemMedico in ListaMedicos)
                        {
                            if (itemMedico.dniPaciente == 1)
                            {
                                foreach (var itemPaciente in ListaPacientes)
                                {
                                    if (itemPaciente.idMedico == itemMedico.Id && itemPaciente.Estado == itemPaciente.arrEstado[(int)enumEstadoPaciente.EnListaDeEspera])
                                    {
                                        int dniPaciente = itemPaciente.Dni;
                                        int idMedico = itemMedico.Id;
                                        objLogicaMedico.AsignarPaciente(dniPaciente, idMedico, ListaPacientes, ListaMedicos);
                                    }
                                }
                            }
                        }
                        Console.WriteLine("\nPresione una tecla para continuar");
                        Console.ReadKey();
                        break;
                    case 2:// Agregar un paciente
                        Console.Clear();
                        Console.WriteLine("____________________________");
                        Console.WriteLine("|     GESTION PACIENTE     |");
                        Console.WriteLine("|--------------------------|");
                        Console.WriteLine("|     AGREGAR PACIENTE     |");
                        Console.WriteLine("|--------------------------|");
                        Entidades.Paciente nuevoPaciente = new Entidades.Paciente();
                        try
                        {
                            try
                            {
                                Console.Write("\nIngrese DNI (solo números) :");
                                dni = Convert.ToInt32(Console.ReadLine());
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                dniPredeterminado++;
                                dni = dniPredeterminado;
                                Console.WriteLine($"DNI predeterminado :{dni}");
                            }
                            Console.Write("\nIngresar el nombre :");
                            nombre = Console.ReadLine();
                            Console.Write("\nIngresar el apellido :");
                            apellido = Console.ReadLine();
                            try
                            {
                                Console.WriteLine("\nTipo de cobertura medica (ObraSocial) :\n1 - NoTiene\n2 - CoberturaBasica\n3 - CoberturaCompleta\n---------------------------------------------\nEn caso de fallo se selecciona predeterminado)\nSelecciona la cobertura medica :");
                                strSeleccionarOpcion = Console.ReadLine();
                                intSeleccionarOpcion = Convert.ToInt32(strSeleccionarOpcion);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                Console.WriteLine("\nIngresó mal los caracteres para asignar la obrasocial");
                                Console.WriteLine("Se seleccionará 'NoTiene' de manera predeterminada");
                            }
                            try
                            {
                                Console.Write("\nIngresar la fecha de nacimiento (MM/dd/YYYY)");
                                fechaNac = Convert.ToDateTime(Console.ReadLine());
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                Console.WriteLine("\nEl formato que ingresaste no es el correcto (Fecha nacimiento)");
                                Console.WriteLine("Fecha predeterminada '05 / 25 / 1990'");
                                fechaNac = Convert.ToDateTime("05/25/1990");
                            }
                            if (nombre != "" && nombre is not null && apellido != "" && apellido is not null)
                            {
                                nuevoPaciente.Id = ListaPacientes.Count;
                                nuevoPaciente.Dni = dni;
                                nuevoPaciente.Nombre = nombre;
                                nuevoPaciente.Apellido = apellido;
                                nuevoPaciente.FechaNacimiento = fechaNac;
                                nuevoPaciente.Estado = nuevoPaciente.arrEstado[(int)enumEstadoPaciente.SinAtencion].ToString();
                                nuevoPaciente.ObraSocial = opcionObraSocial(intSeleccionarOpcion, obraSocial);
                                nuevoPaciente.Edad = Convert.ToInt32(CalcularEdad(Convert.ToDateTime(nuevoPaciente.FechaNacimiento)));
                                objLogicaPaciente.Agregar(nuevoPaciente, ListaPacientes);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            Console.WriteLine("\nEl paciente no pudo agregarse");
                        }
                        Console.WriteLine("\nPresione una tecla para continuar");
                        Console.ReadKey();
                        break;
                    case 3:// Modificar un paciente
                        Console.Clear();
                        Console.WriteLine("____________________________");
                        Console.WriteLine("|     GESTION PACIENTE     |");
                        Console.WriteLine("|--------------------------|");
                        Console.WriteLine("|    MODIFICAR PACIENTE    |");
                        Console.WriteLine("|--------------------------|\n");
                        foreach (var item in ListaPacientes)
                        {
                            Console.WriteLine($"|ID :{item.Id}  \n|DNI :{item.Dni}  \n|Nombre Completo :{item.Apellido}, {item.Nombre}\n");
                        }
                        try
                        {
                            Console.Write("\nIngrese el id del paciente :");
                            idPaciente = Convert.ToInt32(Console.ReadLine());
                            foreach (var paciente in ListaPacientes)
                            {
                                if (paciente.Id.Equals(idPaciente))
                                {
                                    Console.Write("\nIngrese DNI (solo números) :");
                                    dni = Convert.ToInt32(Console.ReadLine());
                                    Console.Write("\nIngresar el nombre :");
                                    nombre = Console.ReadLine();
                                    Console.Write("\nIngresar el apellido :");
                                    apellido = Console.ReadLine();
                                    try
                                    {
                                        Console.WriteLine("\nTipo de cobertura medica (ObraSocial) :\n1 - NoTiene\n2 - CoberturaBasica\n3 - CoberturaCompleta\n---------------------------------------------\nEn caso de fallo se selecciona predeterminado)\nSelecciona la cobertura medica :");
                                        strSeleccionarOpcion = Console.ReadLine();
                                        intSeleccionarOpcion = Convert.ToInt32(strSeleccionarOpcion);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                        Console.WriteLine("\nIngresó mal los caracteres para asignar la obrasocial");
                                        Console.WriteLine("Se seleccionará 'NoTiene' de manera predeterminada");
                                    }
                                    if (nombre != "" && nombre is not null && apellido != "" && apellido is not null)
                                    {
                                        paciente.Dni = dni;
                                        paciente.Nombre = nombre;
                                        paciente.Apellido = apellido;
                                        paciente.ObraSocial = opcionObraSocial(intSeleccionarOpcion, obraSocial);
                                        objLogicaPaciente.Modificar(paciente, ListaPacientes);
                                        Console.WriteLine("Modificando paciente...");
                                    }
                                }
                                Task.Delay(3000);
                                Console.Clear();
                                Task.Delay(1000);
                                if (nombre != "" && nombre is not null && apellido != "" && apellido is not null)
                                {
                                    Console.WriteLine("Paciente Modificado\n");
                                    foreach (var item in ListaPacientes)
                                    {
                                        if (item.Id == idPaciente)
                                        {
                                            Console.WriteLine($"[Actualizado]\n|ID :{item.Id}  \n|DNI :{item.Dni}  \n|Nombre Completo {item.Apellido},{item.Nombre}  \n|Obra Social :{item.ObraSocial}\n");
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            Console.WriteLine("\n[DNI] se admite 'SOLO NÚMEROS'");
                        }
                        Console.WriteLine("\nPresione una tecla para continuar");
                        Console.ReadKey();
                        break;
                    case 4:// Eliminar un paciente
                        Console.Clear();
                        Console.WriteLine("______________________________");
                        Console.WriteLine("|      GESTION PACIENTE      |");
                        Console.WriteLine("|----------------------------|");
                        Console.WriteLine("|      ELIMINAR PACIENTE     |");
                        Console.WriteLine("|----------------------------|\n");
                        foreach (var item in ListaPacientes)
                        {
                            Console.WriteLine($"|ID :{item.Id}  \n|DNI :{item.Dni}  \n|Nombre Completo :{item.Apellido}, {item.Nombre}\n");
                        }
                        do
                        {
                            try
                            {
                                Console.Write("\nIngresar el id del paciente :");
                                idPaciente = Convert.ToInt32(Console.ReadLine());
                            }
                            catch (Exception ex)
                            {
                                idPaciente = ListaPacientes.Count + 1;
                                Console.WriteLine(ex.Message);
                                Task.Delay(5000);
                                Console.WriteLine("\nerror");
                            }
                        } while (idPaciente < 0 || idPaciente > ListaPacientes.Count - 1);
                        try
                        {
                            objLogicaPaciente.Borrar(idPaciente, ListaPacientes);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message + "\nPaciente eliminado");
                        }
                        Console.WriteLine("\nPresione una tecla para continuar");
                        Console.ReadKey();
                        break;
                    case 5:
                        Console.Clear();
                        Console.WriteLine("Salir");
                        Console.WriteLine("\nPresione una tecla para continuar");
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("Ingresaste datos no validos");
                        Console.WriteLine("\nPresione una tecla para continuar");
                        Console.ReadKey();
                        break;
                }
            } while (intSeleccionarOpcion < 1 || intSeleccionarOpcion > 5);
        }
        public static int ValidacionIngresoOpcionesSwitch(int intSeleccionarOpcion, string strSeleccionarOpcion, int limiteOpcion)
        {
            do
            {
                Console.Write("\nIngrese la opcion :");
                strSeleccionarOpcion = Console.ReadLine();
                try
                {
                    intSeleccionarOpcion = Convert.ToInt32(strSeleccionarOpcion);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    intSeleccionarOpcion = 0;
                    Console.WriteLine("\nIngresa datos validos");
                }
            } while (intSeleccionarOpcion < 1 || intSeleccionarOpcion > limiteOpcion);
            return intSeleccionarOpcion;
        }
        public static string opcionObraSocial(int intSeleccionarOpcion, string obraSocial)
        {
            switch (intSeleccionarOpcion)
            {
                case 1:
                    obraSocial = "NoTiene";
                    break;
                case 2:
                    obraSocial = "CoberturaBasica";
                    break;
                case 3:
                    obraSocial = "CoberturaCompleta";
                    break;
                default:
                    obraSocial = "NoTiene";
                    break;
            }
            return obraSocial;
        }
        public static string SeleccionEstado(string estadoPaciente)
        {
            Console.WriteLine("\n\n");
            Console.WriteLine("\nTipos de estado del paciente(AltaPaciente) :");
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("1 - 'Atendido'");
            Console.WriteLine("2 - 'Alta'");
            Console.WriteLine("3 - 'Recetado'");
            Console.WriteLine("4 - 'Derivacion'");
            Console.WriteLine("5 - 'Internacion'");
            Console.WriteLine("6 - 'Tratamiento'");
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Seleccion prederterminada - 'Atendido'");

            Entidades.Paciente paciente = new Entidades.Paciente();
            int intEstadoPaciente = 0;
            try
            {
                do
                {
                    Console.Write("\nSelecciona la opcion :");
                    intEstadoPaciente = Convert.ToInt32(Console.ReadLine());
                } while (intEstadoPaciente < 1 && intEstadoPaciente > 6);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("\nSeleccionado 'Atendido'");
                intEstadoPaciente = 1;
            }
            switch (intEstadoPaciente)
            {
                case 1:
                    estadoPaciente = paciente.arrEstado[(int)enumEstadoPaciente.Atendido];
                    break;
                case 2:
                    estadoPaciente = paciente.arrEstado[(int)enumEstadoPaciente.Alta];
                    break;
                case 3:
                    estadoPaciente = paciente.arrEstado[(int)enumEstadoPaciente.Recetado];
                    break;
                case 4:
                    estadoPaciente = paciente.arrEstado[(int)enumEstadoPaciente.Derivacion];
                    break;
                case 5:
                    estadoPaciente = paciente.arrEstado[(int)enumEstadoPaciente.Internacion];
                    break;
                case 6:
                    estadoPaciente = paciente.arrEstado[(int)enumEstadoPaciente.Tratamiento];
                    break;
                default:
                    estadoPaciente = paciente.arrEstado[(int)enumEstadoPaciente.Atendido];
                    break;
            }
            return estadoPaciente;
        }
        public static void Medicos()
        {
            /*Médicos__________________________________________________*/
            Entidades.Medico objEntidadMedico = new Entidades.Medico();
            objEntidadMedico.Id = 0;
            objEntidadMedico.Apellido = "Borges";
            objEntidadMedico.Nombre = "Ramiro";
            objEntidadMedico.Especialidad = "Oftalmologia";
            objEntidadMedico.Estado = objEntidadMedico.arrEstado[(int)Entidades.Medico.enumEstadoMedico.Desocupado].ToString();
            objEntidadMedico.CantAtendidos = 0;

            Entidades.Medico objEntidadMedico1 = new Entidades.Medico();
            objEntidadMedico1.Id = 1;
            objEntidadMedico1.Apellido = "Padua";
            objEntidadMedico1.Nombre = "Jorge";
            objEntidadMedico1.Especialidad = "Traumatologia";
            objEntidadMedico1.Estado = objEntidadMedico1.arrEstado[(int)Entidades.Medico.enumEstadoMedico.Desocupado].ToString();
            objEntidadMedico1.CantAtendidos = 0;


            Entidades.Medico objEntidadMedico2 = new Entidades.Medico();
            objEntidadMedico2.Id = 2;
            objEntidadMedico2.Apellido = "Ramirez";
            objEntidadMedico2.Nombre = "Juan";
            objEntidadMedico2.Especialidad = "Odontologia";
            objEntidadMedico2.Estado = objEntidadMedico2.arrEstado[(int)Entidades.Medico.enumEstadoMedico.Desocupado].ToString();
            objEntidadMedico2.CantAtendidos = 0;

            Entidades.Medico objEntidadMedico3 = new Entidades.Medico();
            objEntidadMedico3.Id = 3;
            objEntidadMedico3.Apellido = "Pedraza";
            objEntidadMedico3.Nombre = "Ana";
            objEntidadMedico3.Especialidad = "Otorrinolaringologia";
            objEntidadMedico3.Estado = objEntidadMedico3.arrEstado[(int)Entidades.Medico.enumEstadoMedico.Desocupado].ToString();
            objEntidadMedico3.CantAtendidos = 0;

            Entidades.Medico objEntidadMedico4 = new Entidades.Medico();
            objEntidadMedico4.Id = 4;
            objEntidadMedico4.Apellido = "Burgos";
            objEntidadMedico4.Nombre = "Maria";
            objEntidadMedico4.Especialidad = "Clinico";
            objEntidadMedico4.Estado = objEntidadMedico4.arrEstado[(int)Entidades.Medico.enumEstadoMedico.Desocupado].ToString();
            objEntidadMedico4.CantAtendidos = 0;

            Entidades.Medico objEntidadMedico5 = new Entidades.Medico();
            objEntidadMedico5.Id = 5;
            objEntidadMedico5.Apellido = "Mamani";
            objEntidadMedico5.Nombre = "Antonella";
            objEntidadMedico5.Especialidad = "Clinico";
            objEntidadMedico5.Estado = objEntidadMedico5.arrEstado[(int)Entidades.Medico.enumEstadoMedico.Desocupado].ToString();
            objEntidadMedico5.CantAtendidos = 0;
            /*__________________________________________________________*/
            /*Agregar médicos___________________________________________*/
            objLogicaMedico.Agregar(objEntidadMedico, ListaMedicos);
            objLogicaMedico.Agregar(objEntidadMedico1, ListaMedicos);
            objLogicaMedico.Agregar(objEntidadMedico2, ListaMedicos);
            objLogicaMedico.Agregar(objEntidadMedico3, ListaMedicos);
            objLogicaMedico.Agregar(objEntidadMedico4, ListaMedicos);
            objLogicaMedico.Agregar(objEntidadMedico5, ListaMedicos);
        }
        static string CalcularEdad(DateTime FechaNacimiento)
        {
            DateTime FechaActual = DateTime.Now;
            int anio = FechaNacimiento.Year;
            int mes = FechaNacimiento.Month;
            int dia = FechaNacimiento.Day;

            int anioBisiesto = 0;

            for (int i = 0; i < FechaActual.Year; i++)
            {
                if (DateTime.IsLeapYear(anio))
                {
                    ++anioBisiesto;
                }
            }
            TimeSpan timeSpan = FechaActual.Subtract(FechaNacimiento);
            dia = timeSpan.Days - anioBisiesto;
            int resto = 0;

            anio = Math.DivRem(dia, 365, out resto);
            mes = Math.DivRem(resto, 365, out resto);
            dia = resto;
            return anio.ToString();
        }
        public static void Pacientes()
        {
            /*Pacientes_________________________________________________*/
            Entidades.Paciente objEntidadPaciente = new Entidades.Paciente();
            objEntidadPaciente.Id = 0;
            objEntidadPaciente.Dni = 30123456;
            objEntidadPaciente.Nombre = "Gustavo";
            objEntidadPaciente.Apellido = "Llempen";
            objEntidadPaciente.ObraSocial = "NoTiene";
            objEntidadPaciente.FechaNacimiento = Convert.ToDateTime("12/12/1990");
            objEntidadPaciente.Edad = Convert.ToInt32(CalcularEdad(Convert.ToDateTime(objEntidadPaciente.FechaNacimiento)));
            objEntidadPaciente.Estado = objEntidadPaciente.arrEstado[(int)enumEstadoPaciente.SinAtencion].ToString();
            objEntidadPaciente.HoraIngreso = Convert.ToDateTime("18:30");

            Entidades.Paciente objEntidadPaciente1 = new Entidades.Paciente();
            objEntidadPaciente1.Id = 1;
            objEntidadPaciente1.Dni = 31234567;
            objEntidadPaciente1.Nombre = "Gustavo";
            objEntidadPaciente1.Apellido = "Lopez";
            objEntidadPaciente1.ObraSocial = "CoberturaBasica";
            objEntidadPaciente1.FechaNacimiento = Convert.ToDateTime("12/11/1993");
            objEntidadPaciente1.Edad = Convert.ToInt32(CalcularEdad(Convert.ToDateTime(objEntidadPaciente1.FechaNacimiento)));
            objEntidadPaciente1.Estado = objEntidadPaciente1.arrEstado[(int)enumEstadoPaciente.SinAtencion].ToString();
            objEntidadPaciente1.HoraIngreso = Convert.ToDateTime("18:32");

            Entidades.Paciente objEntidadPaciente2 = new Entidades.Paciente();
            objEntidadPaciente2.Id = 2;
            objEntidadPaciente2.Dni = 40321456;
            objEntidadPaciente2.Nombre = "Carlos";
            objEntidadPaciente2.Apellido = "Paz";
            objEntidadPaciente2.ObraSocial = "CoberturaBasica";
            objEntidadPaciente2.FechaNacimiento = Convert.ToDateTime("12/12/1990");
            objEntidadPaciente2.Edad = Convert.ToInt32(CalcularEdad(Convert.ToDateTime(objEntidadPaciente2.FechaNacimiento)));
            objEntidadPaciente2.Estado = objEntidadPaciente2.arrEstado[(int)enumEstadoPaciente.SinAtencion].ToString();
            objEntidadPaciente2.HoraIngreso = Convert.ToDateTime("18:15");

            Entidades.Paciente objEntidadPaciente3 = new Entidades.Paciente();
            objEntidadPaciente3.Id = 3;
            objEntidadPaciente3.Dni = 34012023;
            objEntidadPaciente3.Nombre = "Liliana";
            objEntidadPaciente3.Apellido = "Perez";
            objEntidadPaciente3.ObraSocial = "CoberturaCompleta";
            objEntidadPaciente3.FechaNacimiento = Convert.ToDateTime("12/12/1987");
            objEntidadPaciente3.Edad = Convert.ToInt32(CalcularEdad(Convert.ToDateTime(objEntidadPaciente3.FechaNacimiento)));
            objEntidadPaciente3.Estado = objEntidadPaciente3.arrEstado[(int)enumEstadoPaciente.SinAtencion].ToString();
            objEntidadPaciente3.HoraIngreso = Convert.ToDateTime("18:45");

            Entidades.Paciente objEntidadPaciente4 = new Entidades.Paciente();
            objEntidadPaciente4.Id = 4;
            objEntidadPaciente4.Dni = 31123450;
            objEntidadPaciente4.Nombre = "Carolina";
            objEntidadPaciente4.Apellido = "Alfonso";
            objEntidadPaciente4.ObraSocial = "CoberturaCompleta";
            objEntidadPaciente4.FechaNacimiento = Convert.ToDateTime("10/28/1996");
            objEntidadPaciente4.Edad = Convert.ToInt32(CalcularEdad(Convert.ToDateTime(objEntidadPaciente4.FechaNacimiento)));
            objEntidadPaciente4.Estado = objEntidadPaciente4.arrEstado[(int)enumEstadoPaciente.SinAtencion].ToString();
            objEntidadPaciente4.HoraIngreso = Convert.ToDateTime("19:00");

            Entidades.Paciente objEntidadPaciente5 = new Entidades.Paciente();
            objEntidadPaciente5.Id = 5;
            objEntidadPaciente5.Dni = 35456321;
            objEntidadPaciente5.Nombre = "Paula";
            objEntidadPaciente5.Apellido = "Gregoratti";
            objEntidadPaciente5.ObraSocial = "NoTiene";
            objEntidadPaciente5.FechaNacimiento = Convert.ToDateTime("09/28/1988");
            objEntidadPaciente5.Edad = Convert.ToInt32(CalcularEdad(Convert.ToDateTime(objEntidadPaciente5.FechaNacimiento)));
            objEntidadPaciente5.Estado = objEntidadPaciente5.arrEstado[(int)enumEstadoPaciente.SinAtencion].ToString();
            objEntidadPaciente5.HoraIngreso = Convert.ToDateTime("19:10");

            Entidades.Paciente objEntidadPaciente6 = new Entidades.Paciente();
            objEntidadPaciente6.Id = 6;
            objEntidadPaciente6.Dni = 37012406;
            objEntidadPaciente6.Nombre = "Juan";
            objEntidadPaciente6.Apellido = "Albarracin";
            objEntidadPaciente6.ObraSocial = "CoberturaCompleta";
            objEntidadPaciente6.FechaNacimiento = Convert.ToDateTime("05/25/1985");
            objEntidadPaciente6.Edad = Convert.ToInt32(CalcularEdad(Convert.ToDateTime(objEntidadPaciente6.FechaNacimiento)));
            objEntidadPaciente6.Estado = objEntidadPaciente6.arrEstado[(int)enumEstadoPaciente.SinAtencion].ToString();
            objEntidadPaciente6.HoraIngreso = Convert.ToDateTime("19:15");

            Entidades.Paciente objEntidadPaciente7 = new Entidades.Paciente();
            objEntidadPaciente7.Id = 7;
            objEntidadPaciente7.Dni = 36789898;
            objEntidadPaciente7.Nombre = "Maria";
            objEntidadPaciente7.Apellido = "Mamani";
            objEntidadPaciente7.ObraSocial = "CoberturaBasica";
            objEntidadPaciente7.FechaNacimiento = Convert.ToDateTime("06/04/1991");
            objEntidadPaciente7.Edad = Convert.ToInt32(CalcularEdad(Convert.ToDateTime(objEntidadPaciente7.FechaNacimiento)));
            objEntidadPaciente7.Estado = objEntidadPaciente7.arrEstado[(int)enumEstadoPaciente.SinAtencion].ToString();
            objEntidadPaciente7.HoraIngreso = Convert.ToDateTime("19:30");

            Entidades.Paciente objEntidadPaciente8 = new Entidades.Paciente();
            objEntidadPaciente8.Id = 8;
            objEntidadPaciente8.Dni = 39999888;
            objEntidadPaciente8.Nombre = "Rosalia";
            objEntidadPaciente8.Apellido = "Show";
            objEntidadPaciente8.ObraSocial = "CoberturaBasica";
            objEntidadPaciente8.FechaNacimiento = Convert.ToDateTime("10/10/1992");
            objEntidadPaciente8.Edad = Convert.ToInt32(CalcularEdad(Convert.ToDateTime(objEntidadPaciente8.FechaNacimiento)));
            objEntidadPaciente8.Estado = objEntidadPaciente8.arrEstado[(int)enumEstadoPaciente.SinAtencion].ToString();
            objEntidadPaciente8.HoraIngreso = Convert.ToDateTime("19:45");

            Entidades.Paciente objEntidadPaciente9 = new Entidades.Paciente();
            objEntidadPaciente9.Id = 9;
            objEntidadPaciente9.Dni = 34456275;
            objEntidadPaciente9.Nombre = "Fabian";
            objEntidadPaciente9.Apellido = "Show";
            objEntidadPaciente9.ObraSocial = "CoberturaCompleta";
            objEntidadPaciente9.FechaNacimiento = Convert.ToDateTime("12/12/1975");
            objEntidadPaciente9.Edad = Convert.ToInt32(CalcularEdad(Convert.ToDateTime(objEntidadPaciente9.FechaNacimiento)));
            objEntidadPaciente9.Estado = objEntidadPaciente9.arrEstado[(int)enumEstadoPaciente.SinAtencion].ToString();
            objEntidadPaciente9.HoraIngreso = Convert.ToDateTime("20:00");

            Entidades.Paciente objEntidadPaciente10 = new Entidades.Paciente();
            objEntidadPaciente10.Id = 10;
            objEntidadPaciente10.Dni = 39012345;
            objEntidadPaciente10.Nombre = "Ramon";
            objEntidadPaciente10.Apellido = "Valdez";
            objEntidadPaciente10.ObraSocial = "NoTiene";
            objEntidadPaciente10.FechaNacimiento = Convert.ToDateTime("06/06/1954");
            objEntidadPaciente10.Edad = Convert.ToInt32(CalcularEdad(Convert.ToDateTime(objEntidadPaciente10.FechaNacimiento)));
            objEntidadPaciente10.Estado = objEntidadPaciente10.arrEstado[(int)enumEstadoPaciente.SinAtencion].ToString();
            objEntidadPaciente10.HoraIngreso = Convert.ToDateTime("20:10");
            /*Agregar Pacientes_________________________________________*/
            objLogicaPaciente.Agregar(objEntidadPaciente, ListaPacientes);
            objLogicaPaciente.Agregar(objEntidadPaciente1, ListaPacientes);
            objLogicaPaciente.Agregar(objEntidadPaciente2, ListaPacientes);
            objLogicaPaciente.Agregar(objEntidadPaciente3, ListaPacientes);
            objLogicaPaciente.Agregar(objEntidadPaciente4, ListaPacientes);
            objLogicaPaciente.Agregar(objEntidadPaciente5, ListaPacientes);
            objLogicaPaciente.Agregar(objEntidadPaciente6, ListaPacientes);
            objLogicaPaciente.Agregar(objEntidadPaciente7, ListaPacientes);
            objLogicaPaciente.Agregar(objEntidadPaciente8, ListaPacientes);
            objLogicaPaciente.Agregar(objEntidadPaciente9, ListaPacientes);
            objLogicaPaciente.Agregar(objEntidadPaciente10, ListaPacientes);
            /*__________________________________________________________*/
        }
    }
}
