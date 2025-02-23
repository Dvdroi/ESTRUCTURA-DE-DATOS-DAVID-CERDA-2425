using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    public class Ciudadano
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
    }

    static void Main()
    {
        // Crear conjuntos de datos
        HashSet<Ciudadano> todosLosCiudadanos = GenerarCiudadanos(500);
        HashSet<Ciudadano> vacunadosPfizer = GenerarVacunados(todosLosCiudadanos, 75);
        HashSet<Ciudadano> vacunadosAstrazeneca = GenerarVacunados(
            todosLosCiudadanos.Except(vacunadosPfizer).ToHashSet(), 75);

        // 1. Ciudadanos no vacunados
        var noVacunados = todosLosCiudadanos
            .Except(vacunadosPfizer)
            .Except(vacunadosAstrazeneca)
            .ToHashSet();

        // 2. Ciudadanos con ambas vacunas (intersección)
        var dosVacunas = vacunadosPfizer
            .Intersect(vacunadosAstrazeneca)
            .ToHashSet();

        // 3. Ciudadanos solo con Pfizer
        var soloPfizer = vacunadosPfizer
            .Except(vacunadosAstrazeneca)
            .ToHashSet();

        // 4. Ciudadanos solo con Astrazeneca
        var soloAstrazeneca = vacunadosAstrazeneca
            .Except(vacunadosPfizer)
            .ToHashSet();

        // Mostrar resultados
        Console.WriteLine($"Total de ciudadanos: {todosLosCiudadanos.Count}");
        Console.WriteLine($"No vacunados: {noVacunados.Count}");
        MostrarListado("Ciudadanos no vacunados:", noVacunados);

        Console.WriteLine($"\nVacunados con ambas: {dosVacunas.Count}");
        MostrarListado("Ciudadanos con ambas vacunas:", dosVacunas);

        Console.WriteLine($"\nSolo Pfizer: {soloPfizer.Count}");
        MostrarListado("Ciudadanos solo con Pfizer:", soloPfizer);

        Console.WriteLine($"\nSolo Astrazeneca: {soloAstrazeneca.Count}");
        MostrarListado("Ciudadanos solo con Astrazeneca:", soloAstrazeneca);
    }

    static HashSet<Ciudadano> GenerarCiudadanos(int cantidad)
    {
        var ciudadanos = new HashSet<Ciudadano>();
        var nombres = new[] { "Juan", "María", "Pedro", "Ana", "Luis", "Carlos", "Sofia", "Laura" };
        var apellidos = new[] { "García", "Martínez", "López", "Rodríguez", "González", "Pérez" };

        for (int i = 0; i < cantidad; i++)
        {
            var random = new Random();
            ciudadanos.Add(new Ciudadano
            {
                Id = i + 1,
                Nombre = nombres[random.Next(nombres.Length)],
                Apellido = apellidos[random.Next(apellidos.Length)]
            });
        }

        return ciudadanos;
    }

    static HashSet<Ciudadano> GenerarVacunados(HashSet<Ciudadano> poblacion, int cantidad)
    {
        return poblacion.OrderBy(x => Guid.NewGuid()).Take(cantidad).ToHashSet();
    }

    static void MostrarListado(string titulo, HashSet<Ciudadano> ciudadanos)
    {
        Console.WriteLine($"\n{titulo}");
        foreach (var ciudadano in ciudadanos.Take(5)) // Mostrar solo los primeros 5 como ejemplo
        {
            Console.WriteLine($"ID: {ciudadano.Id}, Nombre: {ciudadano.Nombre} {ciudadano.Apellido}");
        }
        if (ciudadanos.Count > 5)
        {
            Console.WriteLine($"... y {ciudadanos.Count - 5} más");
        }
    }
}