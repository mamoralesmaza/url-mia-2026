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
            // Solicitud del nombre completo del usuario
            Console.Write("Usuario: ");
            string usuario = Console.ReadLine()?.Trim();

            // Solicitud de la ruta del archivo de texto
            Console.Write("Archivo: ");
            string rutaArchivo = Console.ReadLine()?.Trim('"');

            // Validación de existencia del archivo
            if (!File.Exists(rutaArchivo))
            {
                Console.WriteLine("\n[Error] El archivo especificado no existe. Asegúrese de que la ruta sea correcta.");
                return;
            }

            // ==========================================
            // PROCESO
            // ==========================================
            // Lectura del archivo y conteos mediante iteración básica de caracteres
            int lineas = 0;
            int palabras = 0;
            int caracteres = 0;
            bool enPalabra = false;

            // Se utiliza StreamReader para leer el archivo carácter por carácter
            using (StreamReader reader = new StreamReader(rutaArchivo))
            {
                int c;
                while ((c = reader.Read()) != -1)
                {
                    char caracterActual = (char)c;
                    caracteres++;

                    // Conteo de líneas por salto de línea
                    if (caracterActual == '\n')
                    {
                        lineas++;
                    }

                    // Conteo de palabras validando espacios y separadores
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

            // Conteo de la última línea si el archivo no está vacío y no termina en salto de línea
            if (caracteres > 0 && lineas == 0)
            {
                lineas = 1;
            }
            else if (caracteres > 0 && !File.ReadAllText(rutaArchivo).EndsWith("\n"))
            {
                lineas++;
            }

            // ==========================================
            // SALIDAS
            // ==========================================
            // 1. Impresión de los resultados en consola
            Console.WriteLine($"El archivo contiene: {lineas} líneas, {palabras} palabras, {caracteres} caracteres.");

            // 2. Definición del directorio y nombre del archivo CSV
            string directorioSalida = @"C:\Parcial_I_Martin_Morales";
            
            // Garantizar que la carpeta exista
            if (!Directory.Exists(directorioSalida))
            {
                Directory.CreateDirectory(directorioSalida);
            }

            // Generar el nombre con formato <Nombre_Apellido>
            string nombreFormateado = usuario.Replace(" ", "_");
            string rutaCsv = Path.Combine(directorioSalida, $"resultados_{nombreFormateado}.csv");

            // Formato de salida para el CSV: <Nombre_Apellido>,Lineas,Palabras,Caracteres
            string lineaCsv = $"{nombreFormateado},{lineas},{palabras},{caracteres}";
            File.WriteAllLines(rutaCsv, new string[] { lineaCsv });

            Console.WriteLine($"Resultados guardados en {rutaCsv}");
        }
    }
}