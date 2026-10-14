using System;
using System.Collections.Generic;
using System.Linq;

namespace BibliotecaApp_Evaluacion
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Sistema de Gestión de Biblioteca - UNAB");

            // Fase 1: Modelado e inicialización de datos
            var autores = new List<Autor>
            {
                new Autor { Id = 1, Nombre = "Gabriel", Apellido = "García Márquez" },
                new Autor { Id = 2, Nombre = "Isabel", Apellido = "Allende" },
                new Autor { Id = 3, Nombre = "Jorge Luis", Apellido = "Borges" },
                new Autor { Id = 4, Nombre = "Laura", Apellido = "Restrepo" },
                new Autor { Id = 5, Nombre = "Mario", Apellido = "Vargas Llosa" }
            };

            var libros = new List<Libro>
            {
                new Libro { Id = 1, Codigo = "L-001", Titulo = "Cien años de soledad", Genero = "Realismo mágico", PrecioAlquiler = 3.5m, CopiasDisponibles = 4, Estado = "Disponible", AutorId = 1 },
                new Libro { Id = 2, Codigo = "L-002", Titulo = "La casa de los espíritus", Genero = "Realismo", PrecioAlquiler = 2.8m, CopiasDisponibles = 2, Estado = "Disponible", AutorId = 2 },
                new Libro { Id = 3, Codigo = "L-003", Titulo = "Ficciones", Genero = "Cuento", PrecioAlquiler = 2.0m, CopiasDisponibles = 0, Estado = "No disponible", AutorId = 3 },
                new Libro { Id = 4, Codigo = "L-004", Titulo = "Delirio", Genero = "Novela", PrecioAlquiler = 2.5m, CopiasDisponibles = 1, Estado = "Disponible", AutorId = 4 },
                new Libro { Id = 5, Codigo = "L-005", Titulo = "La ciudad y los perros", Genero = "Novela", PrecioAlquiler = 3.0m, CopiasDisponibles = 3, Estado = "Disponible", AutorId = 5 }
            };

            // Fase 2: Gestión de colecciones y simulación de préstamos
            // Simularemos una serie de solicitudes de préstamo por Id de libro
            var solicitudesPrestamo = new List<int> { 1, 1, 3, 4, 1, 2, 5, 3, 4, 1 };
            foreach (var solicitud in solicitudesPrestamo)
            {
                var libro = libros.FirstOrDefault(l => l.Id == solicitud);
                if (libro == null) continue;

                if (libro.CopiasDisponibles > 0)
                {
                    libro.CopiasDisponibles--;
                    libro.VecesPrestado++;
                    libro.Estado = libro.CopiasDisponibles > 0 ? "Disponible" : "No disponible";
                }
                else
                {
                    // Si no hay copias disponibles no se realiza el préstamo
                }
            }

            Console.WriteLine("\nEstado de inventario tras simulación de préstamos:");
            foreach (var libro in libros)
            {
                Console.WriteLine(libro);
            }

            // Reporte de frecuencia de uso
            var rankingFrecuencia = libros.OrderByDescending(l => l.VecesPrestado);
            Console.WriteLine("\nReporte - Libros por frecuencia de préstamo:");
            foreach (var l in rankingFrecuencia)
            {
                Console.WriteLine($"{l.Titulo} | Veces prestado: {l.VecesPrestado}");
            }

            // Fase 3 - Ejercicio 1: Query Syntax -> libros disponibles ordenados por PrecioAlquiler asc
            var librosDisponiblesQuery =
                from l in libros
                where l.Estado == "Disponible"
                orderby l.PrecioAlquiler
                select l;

            Console.WriteLine("\nLibros disponibles (Query Syntax) ordenados por PrecioAlquiler:");
            foreach (var libro in librosDisponiblesQuery)
            {
                Console.WriteLine(libro);
            }

            // Fase 3 - Ejercicio 2: Join entre libros y autores
            var consultaJoin =
                from l in libros
                join a in autores on l.AutorId equals a.Id
                select new { Titulo = l.Titulo, Autor = $"{a.Nombre} {a.Apellido}", l.Genero };

            Console.WriteLine("\nListado (Join) - Título | Autor | Género:");
            foreach (var item in consultaJoin)
            {
                Console.WriteLine($"{item.Titulo} | {item.Autor} | {item.Genero}");
            }

            // Fase 4: Method Syntax - reescritura de la consulta de disponibles
            var librosDisponiblesMethod = libros
                .Where(l => l.Estado == "Disponible")
                .OrderBy(l => l.PrecioAlquiler)
                .ToList();

            Console.WriteLine("\nLibros disponibles (Method Syntax) ordenados por PrecioAlquiler:");
            librosDisponiblesMethod.ForEach(l => Console.WriteLine(l));

            // Agregaciones y reportes estadísticos
            var valorTotalAlquileres = libros.Sum(l => l.PrecioAlquiler);
            var promedioExistencias = libros.Average(l => l.CopiasDisponibles);
            var existeAgotado = libros.Any(l => l.CopiasDisponibles == 0);

            Console.WriteLine($"\nValor total de alquileres posibles: {valorTotalAlquileres:C}");
            Console.WriteLine($"Promedio de existencias (copias disponibles): {promedioExistencias:F2}");
            Console.WriteLine($"¿Existe algún libro con stock agotado? {(existeAgotado ? "Sí" : "No")}");

            Console.WriteLine("\nPresione cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}
