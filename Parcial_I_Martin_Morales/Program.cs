using System;
using System.IO;

namespace Parcial_I_Martin_Morales
{
    class Program
    {
        static void Main(string[] args)
        {
            // ==========================================
            // ENTRADAS
            // ==========================================
            Console.Write("Usuario: ");
            string usuario = Console.ReadLine()?.Trim() ?? "";

            Console.Write("Archivo: ");
            string rutaArchivo = Console.ReadLine()?.Trim('"') ?? "";

            if (string.IsNullOrWhiteSpace(rutaArchivo) || !File.Exists(rutaArchivo))
            {
                Console.WriteLine("\n[Error] El archivo especificado no existe. Asegúrese de que la ruta sea correcta.");
                return;
            }

            // ==========================================
            // PROCESO
            // ==========================================
            int lineas = 0;
            int palabras = 0;
            int caracteres = 0;
            bool enPalabra = false;

            using (StreamReader reader = new StreamReader(rutaArchivo))
            {
                int c;
                while ((c = reader.Read()) != -1)
                {
                    char caracterActual = (char)c;
                    caracteres++;

                    if (caracterActual == '\n')
                    {
                        lineas++;
                    }

                    if (char.IsWhiteSpace(caracterActual))
                    {
                        enPalabra = false;
                    }
                    else if (!enPalabra)
                    {
                        enPalabra = true;
                        palabras++;
                    }
                }
            }

            string contenidoCompleto = File.ReadAllText(rutaArchivo);
            if (caracteres > 0 && lineas == 0)
            {
                lineas = 1;
            }
            else if (caracteres > 0 && !contenidoCompleto.EndsWith("\n"))
            {
                lineas++;
            }

            // ==========================================
            // SALIDAS
            // ==========================================
            Console.WriteLine($"El archivo contiene: {lineas} líneas, {palabras} palabras, {caracteres} caracteres.");

            string directorioSalida = @"C:\Parcial_I_Martin_Morales";
            
            if (!Directory.Exists(directorioSalida))
            {
                Directory.CreateDirectory(directorioSalida);
            }

            string nombreFormateado = usuario.Replace(" ", "_");
            string rutaCsv = Path.Combine(directorioSalida, $"resultados_{nombreFormateado}.csv");

            string lineaCsv = $"{nombreFormateado},{lineas},{palabras},{caracteres}";
            File.WriteAllLines(rutaCsv, new string[] { lineaCsv });

            Console.WriteLine($"Resultados guardados en {rutaCsv}");
        }
    }
}
