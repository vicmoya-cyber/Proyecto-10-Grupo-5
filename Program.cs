using System;
using System.IO;

// 1. ESTRUCTURAS (RECORDS)

public record Ciudadano(
    string Id,
    string NombreCompleto,
    string CI,
    string ZonaBarrio,
    string Telefono,
    DateTime FechaRegistro
);

public record Propuesta(
    string NumeroPropuesta,
    string Titulo,
    string Descripcion,
    string Categoria,
    string AutorId,
    DateTime FechaPresentacion,
    string Estado,
    int VotosFavor,
    int VotosContra,
    DateTime? FechaResolucion
);

public record ConsultaPublica(
    string CodigoConsulta,
    string Tema,
    DateTime FechaInicio,
    DateTime FechaFin,
    int TotalParticipantes,
    string Resultado
);

namespace VozComunitaria
{
    class Program
    {

        // 2. VARIABLES GLOBALES Y ARREGLOS

        static Ciudadano[] ciudadanos = new Ciudadano[50];
        static int contadorCiudadanos = 0;

        static Propuesta[] propuestas = new Propuesta[30];
        static int contadorPropuestas = 0;

        static string[,] matrizVotacion = new string[30, 10];

        static string rutaCiudadanos = "ciudadanos.csv";
        static string rutaPropuestas = "propuestas.csv";
        static string rutaLog = "propuestas.log";

        // 3. MÉTODO PRINCIPAL (MENÚ)

        static void Main(string[] args)
        {
            bool continuar = true;
            while (continuar)
            {
                Console.Clear();
                Console.WriteLine("=== SISTEMA VOZ-COMUNITARIA - UPDS ===");
                Console.WriteLine("1. Registrar Ciudadano");
                Console.WriteLine("2. Presentar Propuesta");
                Console.WriteLine("3. Emitir Voto");
                Console.WriteLine("4. Generar Acta (.txt)");
                Console.WriteLine("5. Salir y Guardar");
                Console.Write("\nSeleccione una opción: ");

                string opcion = Console.ReadLine();
                switch (opcion)
                {
                    case "1":
                        RegistrarCiudadano();
                        GuardarDatosCSV();
                        break;
                    case "2":
                        PresentarPropuesta();
                        GuardarDatosCSV();
                        break;
                    case "3":
                        EmitirVoto();
                        break;
                    case "4":
                        Console.Write("Ingrese el índice de la propuesta (0, 1, 2...): ");
                        if (int.TryParse(Console.ReadLine(), out int idx) && idx < contadorPropuestas)
                        {
                            GenerarActa(idx);
                        }
                        else
                        {
                            Console.WriteLine("Índice no válido.");
                            Console.ReadKey();
                        }
                        break;
                    case "5":
                        Console.WriteLine("Guardando datos finales...");
                        GuardarDatosCSV();
                        continuar = false;
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // 4. FUNCIONALIDADES DEL SISTEMA

        static void RegistrarCiudadano()
        {
            if (contadorCiudadanos >= 50)
            {
                Console.WriteLine("Límite de ciudadanos alcanzado.");
                Console.ReadKey();
                return;
            }

            Console.Write("Ingrese CI: ");
            string ci = Console.ReadLine();

            for (int i = 0; i < contadorCiudadanos; i++)
            {
                if (ciudadanos[i].CI == ci)
                {
                    Console.WriteLine("Error: El CI ya está registrado.");
                    Console.ReadKey();
                    return;
                }
            }

            Console.Write("Nombre Completo: ");
            string nombre = Console.ReadLine();
            Console.Write("Zona/Barrio: ");
            string zona = Console.ReadLine();
            Console.Write("Teléfono: ");
            string telf = Console.ReadLine();

            string id = "CID" + (contadorCiudadanos + 1);
            ciudadanos[contadorCiudadanos] = new Ciudadano(id, nombre, ci, zona, telf, DateTime.Now);
            contadorCiudadanos++;

            Auditar("Registro de ciudadano: " + ci);
            Console.WriteLine("Ciudadano registrado exitosamente.");
            Console.ReadKey();
        }

        static void PresentarPropuesta()
        {
            if (contadorPropuestas >= 30)
            {
                Console.WriteLine("Límite de propuestas alcanzado.");
                Console.ReadKey();
                return;
            }

            Console.Write("Título: ");
            string titulo = Console.ReadLine();

            string desc;
            do
            {
                Console.Write("Descripción (mínimo 20 caracteres): ");
                desc = Console.ReadLine();
            } while (desc.Length < 20);

            string num = "PROP" + (contadorPropuestas + 1);
            propuestas[contadorPropuestas] = new Propuesta(num, titulo, desc, "General", "ID001", DateTime.Now, "Pendiente", 0, 0, null);
            contadorPropuestas++;

            Auditar("Nueva propuesta creada: " + num);
            Console.WriteLine("Propuesta registrada.");
            Console.ReadKey();
        }

        static void EmitirVoto()
        {
            Console.Write("Ingrese su CI para votar: ");
            string ci = Console.ReadLine();

            int indiceCiudadano = -1;
            for (int i = 0; i < contadorCiudadanos; i++)
            {
                if (ciudadanos[i].CI == ci)
                {
                    indiceCiudadano = i;
                    break;
                }
            }

            if (indiceCiudadano == -1)
            {
                Console.WriteLine("Error: Ciudadano no registrado.");
                Console.ReadKey();
                return;
            }

            Console.Write("Ingrese número de propuesta (ej. PROP1): ");
            string numProp = Console.ReadLine().ToUpper();
            int indiceProp = -1;
            for (int i = 0; i < contadorPropuestas; i++)
            {
                if (propuestas[i].NumeroPropuesta == numProp)
                {
                    indiceProp = i;
                    break;
                }
            }

            if (indiceProp == -1)
            {
                Console.WriteLine("Error: Propuesta no encontrada.");
                Console.ReadKey();
                return;
            }

            Console.Write("¿Voto a Favor (F) o en Contra (C)?: ");
            string tipoVoto = Console.ReadLine().ToUpper();

            if (tipoVoto == "F")
            {
                propuestas[indiceProp] = propuestas[indiceProp] with { VotosFavor = propuestas[indiceProp].VotosFavor + 1 };
                matrizVotacion[indiceProp, 0] = ciudadanos[indiceCiudadano].ZonaBarrio;
                Console.WriteLine("Voto a favor registrado.");
            }
            else if (tipoVoto == "C")
            {
                propuestas[indiceProp] = propuestas[indiceProp] with { VotosContra = propuestas[indiceProp].VotosContra + 1 };
                Console.WriteLine("Voto en contra registrado.");
            }

            Auditar($"Voto emitido por {ci} en {numProp}");
            GuardarDatosCSV();
            Console.ReadKey();
        }

        // 5. PERSISTENCIA Y ARCHIVOS

        static void GuardarDatosCSV()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(rutaCiudadanos))
                {
                    sw.WriteLine("Id,Nombre,CI,Zona,Telefono,Fecha");
                    for (int i = 0; i < contadorCiudadanos; i++)
                    {
                        var c = ciudadanos[i];
                        sw.WriteLine($"{c.Id},{c.NombreCompleto},{c.CI},{c.ZonaBarrio},{c.Telefono},{c.FechaRegistro}");
                    }
                }

                using (StreamWriter sw = new StreamWriter(rutaPropuestas))
                {
                    sw.WriteLine("Numero,Titulo,Descripcion,Categoria,AutorId,Estado,VotosFavor,VotosContra");
                    for (int i = 0; i < contadorPropuestas; i++)
                    {
                        var p = propuestas[i];
                        sw.WriteLine($"{p.NumeroPropuesta},{p.Titulo},{p.Descripcion},{p.Categoria},{p.AutorId},{p.Estado},{p.VotosFavor},{p.VotosContra}");
                    }
                }
                Auditar("Datos guardados en archivos CSV");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al guardar CSV: " + ex.Message);
            }
        }

        static void GenerarActa(int indicePropuesta)
        {
            var p = propuestas[indicePropuesta];
            string nombreArchivo = $"actas/Acta_{p.NumeroPropuesta}.txt";

            try
            {
                if (!Directory.Exists("actas")) Directory.CreateDirectory("actas");

                using (StreamWriter sw = new StreamWriter(nombreArchivo))
                {

                    sw.WriteLine("       ACTA DE RESOLUCIÓN CIUDADANA     ");

                    sw.WriteLine($"Propuesta Nro: {p.NumeroPropuesta}");
                    sw.WriteLine($"Título: {p.Titulo}");
                    sw.WriteLine($"Estado Final: {p.Estado}");
                    sw.WriteLine($"Votos a Favor: {p.VotosFavor}");
                    sw.WriteLine($"Votos en Contra: {p.VotosContra}");
 
                    sw.WriteLine($"Fecha de Emisión: {DateTime.Now}");

                }
                Auditar($"Acta generada para la propuesta {p.NumeroPropuesta}");
                Console.WriteLine($"Acta creada exitosamente en: {nombreArchivo}");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al generar el acta: " + ex.Message);
                Console.ReadKey();
            }
        }

        static void Auditar(string accion)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(rutaLog, true))
                {
                    sw.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {accion}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al escribir en el log: " + ex.Message);
            }
        }
    }
}