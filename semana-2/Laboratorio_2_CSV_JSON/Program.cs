using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class Program
{
    static void Main(string[] args)
    {
        string csv = "estudiantes.csv";
        string json = "estudiantes.json";

        if (!File.Exists(csv))
        {
            Console.WriteLine($"El archivo {csv} no existe.");
            return;
        }

        string[] lineas = File.ReadAllLines(csv);
        List<Estudiante> listaEstudiantes = new List<Estudiante>();

        for (int i = 1; i < lineas.Length; i++)
        {
            string linea = lineas[i];
            if (string.IsNullOrWhiteSpace(linea)) continue;

            string[] datos = linea.Split(',');

            if (datos.Length >= 3)
            {
                Estudiante estudiante = new Estudiante
                {
                    Id = int.Parse(datos[0].Trim()),
                    Nombre = datos[1].Trim(),
                    Carrera = datos[2].Trim()
                };

                listaEstudiantes.Add(estudiante);
            }
        }

        foreach (var est in listaEstudiantes)
        {
            Console.WriteLine($"{est.Id} - {est.Nombre} - {est.Carrera}");
        }

        var opcionesJson = new JsonSerializerOptions { WriteIndented = true };
        string jsonResultado = JsonSerializer.Serialize(listaEstudiantes, opcionesJson);

        File.WriteAllText(json, jsonResultado);

        Console.WriteLine("Archivo estudiantes.json creado correctamente.");
    }
}