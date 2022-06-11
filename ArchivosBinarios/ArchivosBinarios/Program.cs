using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ArchivosBinarios
{
    class Program
    {
        class ArchivoBinarioEmpleados
        {
            //declaracion de flujo
            BinaryWriter bw = null; //flujo salida - estritura de datos
            BinaryReader br = null; //flujo entrada - lectura de datos

            //campos de la clase
            string Nombre, Direccion;
            long Telefono;
            int NumEmp, DiasTrabajados;
            float SalarioDiario;

            public void CrearArchivo(string Archivo)
            {
                //variable local metodo
                char resp;
                try
                {
                    //creacion del flujo para escribir datos al archivo 
                    bw = new BinaryWriter(new FileStream(Archivo, FileMode.Create, FileAccess.Write));

                    //captura de datos 
                    do
                    {
                        Console.Clear();
                        Console.Write("Numero del empleado: "); NumEmp = Int32.Parse(Console.ReadLine());
                        Console.Write("Nombre del Empleado: "); Nombre = Console.ReadLine();
                        Console.Write("Direccion del empleado: "); Direccion = Console.ReadLine();
                        Console.Write("Telefono del empleado: "); Telefono = Int64.Parse(Console.ReadLine());
                        Console.Write("DIas trabajados del empleado: ");
                        DiasTrabajados = Int32.Parse(Console.ReadLine());
                        Console.Write("Salario diario del empleado: ");

                        SalarioDiario = Single.Parse(Console.ReadLine());
                        //escribe los datos del archivo 
                        bw.Write(NumEmp);
                        bw.Write(Nombre);
                        bw.Write(Direccion);
                        bw.Write(Telefono);
                        bw.Write(DiasTrabajados);
                        bw.Write(SalarioDiario);

                        Console.Write("\n\nDesea Almacenar otro Registro (s/n)?");
                        resp = char.Parse(Console.ReadLine());

                    } while ((resp == 's') || (resp == 'S'));
                } catch (IOException e)
                {
                    Console.WriteLine("\nError : " + e.Message);
                    Console.WriteLine("\nRuta : " + e.StackTrace);
                }
                finally
                {
                    if (bw != null) bw.Close();//cierra el flujo - Escritura
                    Console.Write("\nPresione ENTER para terminar la escritura de datos y regresa al menu.");
                    Console.ReadKey();
                }

            }
            public void MostrarArchivo(string Archivo)
            {
                try
                {
                    //verifica si existe el archivo 
                    if (File.Exists(Archivo))
                    {
                        //creacion flujo para leer datos del archivo 
                        br = new BinaryReader(new FileStream(Archivo, FileMode.Open, FileAccess.Read));

                        //despliegue de datos en pantalla 
                        Console.Clear();
                        do
                        {
                            //lectura de registros mientras no llegue a EndofFile
                            NumEmp = br.ReadInt32();
                            Nombre = br.ReadString();
                            Direccion = br.ReadString();
                            Telefono = br.ReadInt64();
                            DiasTrabajados = br.ReadInt32();
                            SalarioDiario = br.ReadSingle();

                            //Muestra los datos
                            Console.WriteLine("Numero del empleado: " + NumEmp);
                            Console.WriteLine("Nombre del Empleado: " + Nombre);
                            Console.WriteLine("Direccion del empleado: " + Direccion);
                            Console.WriteLine("Telefono del empleado: " + Telefono);
                            Console.WriteLine("DIas trabajados del empleado: " + DiasTrabajados);
                            Console.WriteLine("Salario diario del empleado: (0:C) " + SalarioDiario);
                            Console.WriteLine("Sueldo total del empleado: (0:C) ", (DiasTrabajados * SalarioDiario));
                            Console.WriteLine("\n");

                        } while (true);

                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("\n\nEl archivo " + Archivo + "No existe en el Disco!!");
                        Console.Write("\nPresione ENTER para continuar....");
                        Console.ReadKey();
                    }
                }
                catch (EndOfStreamException)
                {
                    Console.WriteLine("\n\nFin del Listado del Empleado");
                    Console.WriteLine("\nPresione ENTER para continuar...");
                    Console.ReadKey();
                }
                finally
                {
                    if (br != null) br.Close(); //cierra flujo
                    Console.Write("\nPresione <enter> para terminar la Lectura de Datos y regresar al Menu.");
                    Console.ReadKey();
                }
            }
        }
        static void Main(string[] args)
        {
            //declaración variables auxiliares
            string Arch = null;
            int opcion;
            //creación del objeto
            ArchivoBinarioEmpleados A1 = new ArchivoBinarioEmpleados();
            //Menu de Opciones
            
            do
            {
                Console.Clear();
                Console.WriteLine("\n*** ARCHIVO BINARIO EMPLEADOS***");
                Console.WriteLine("1.- Creación de un Archivo.");
                Console.WriteLine("2.- Lectura de un Archivo.");
                Console.WriteLine("3.- Salida del Programa.");
                Console.Write("\nQue opción deseas: ");
                opcion = Int16.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        //bloque de escritura
                        try
                        {
                            //captura nombre archivo
                            Console.Write("\nAlimenta el Nombre del Archivo a Crear: "); Arch = Console.ReadLine();
                            
                              //verifica si esxiste el archivo
                            char resp = 's';
                            if (File.Exists(Arch))
                            {
                                Console.Write("\nEl Archivo Existe!!, Deseas Sobreescribirlo(s / n) ? ");
                                

                                resp = Char.Parse(Console.ReadLine());
                            }
                            if ((resp == 's') || (resp == 'S'))
                                A1.CrearArchivo(Arch);
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine("\nError : " + e.Message);
                            Console.WriteLine("\nRuta : " + e.StackTrace);
                        }
                        break;
                    case 2:
                        //bloque de lectura
                        try
                        {
                            //captura nombre archivo
                            Console.Write("\nAlimenta el Nombre del Archivo que deseas Leer: "); Arch = Console.ReadLine();
                            A1.MostrarArchivo(Arch);
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine("\nError : " + e.Message);
                            Console.WriteLine("\nRuta : " + e.StackTrace);
                        }
                        break;
                    case 3:
                        Console.Write("\nPresione <enter> para Salir del Programa.");
                        

                        Console.ReadKey();
                        break;
                    default:
                        Console.Write("\nEsa Opción No Existe!!, Presione < enter > para Continuar...");
                        Console.ReadKey();
                        break;
                }
            } while (opción != 3);
        }
            
        
    }
}
