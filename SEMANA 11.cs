using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    // Diccionarios para las traducciones
    static Dictionary<string, string> espToEng = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
    static Dictionary<string, string> engToEsp = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

    static void Main(string[] args)
    {
        // Inicializar los diccionarios con palabras predefinidas
        InicializarDiccionarios();
        
        bool salir = false;
        
        while (!salir)
        {
            MostrarMenu();
            string opcion = Console.ReadLine();
            
            switch (opcion)
            {
                case "1":
                    TraducirFrase();
                    break;
                case "2":
                    AgregarPalabra();
                    break;
                case "0":
                    salir = true;
                    Console.WriteLine("¡Gracias por usar el traductor básico!");
                    break;
                default:
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                    break;
            }
            
            Console.WriteLine("\nPresione Enter para continuar...");
            Console.ReadLine();
            Console.Clear();
        }
    }
    
    static void InicializarDiccionarios()
    {
        // Inicialización con las palabras proporcionadas
        AgregarPalabraADiccionarios("tiempo", "time");
        AgregarPalabraADiccionarios("persona", "person");
        AgregarPalabraADiccionarios("año", "year");
        AgregarPalabraADiccionarios("camino", "way");
        AgregarPalabraADiccionarios("forma", "way");
        AgregarPalabraADiccionarios("día", "day");
        AgregarPalabraADiccionarios("cosa", "thing");
        AgregarPalabraADiccionarios("hombre", "man");
        AgregarPalabraADiccionarios("mundo", "world");
        AgregarPalabraADiccionarios("vida", "life");
        AgregarPalabraADiccionarios("mano", "hand");
        AgregarPalabraADiccionarios("parte", "part");
        AgregarPalabraADiccionarios("niño", "child");
        AgregarPalabraADiccionarios("niña", "child");
        AgregarPalabraADiccionarios("ojo", "eye");
        AgregarPalabraADiccionarios("mujer", "woman");
        AgregarPalabraADiccionarios("lugar", "place");
        AgregarPalabraADiccionarios("trabajo", "work");
        AgregarPalabraADiccionarios("semana", "week");
        AgregarPalabraADiccionarios("caso", "case");
        AgregarPalabraADiccionarios("punto", "point");
        AgregarPalabraADiccionarios("tema", "point");
        AgregarPalabraADiccionarios("gobierno", "government");
        AgregarPalabraADiccionarios("empresa", "company");
        AgregarPalabraADiccionarios("compañía", "company");
    }
    
    static void AgregarPalabraADiccionarios(string palabraEsp, string palabraEng)
    {
        // Agregamos en minúscula para facilitar las comparaciones
        espToEng[palabraEsp.ToLower()] = palabraEng.ToLower();
        engToEsp[palabraEng.ToLower()] = palabraEsp.ToLower();
    }
    
    static void MostrarMenu()
    {
        Console.WriteLine("MENU");
        Console.WriteLine("=======================================================");
        Console.WriteLine("1. Traducir una frase");
        Console.WriteLine("2. Ingresar más palabras al diccionario");
        Console.WriteLine("0. Salir");
        Console.Write("Seleccione una opción: ");
    }
    
    static void TraducirFrase()
    {
        Console.Write("Ingrese la frase: ");
        string frase = Console.ReadLine();
        
        // Determinar el idioma de entrada (español o inglés)
        bool esEspanol = DetectarIdioma(frase);
        
        string fraseTraducida = TraducirPalabras(frase, esEspanol);
        
        Console.WriteLine($"Su frase traducida es: {fraseTraducida}");
    }
    
    static bool DetectarIdioma(string frase)
    {
        // Dividir la frase en palabras
        string[] palabras = frase.Split(' ', ',', '.', ':', ';', '!', '?');
        
        int contadorEsp = 0;
        int contadorEng = 0;
        
        foreach (string palabra in palabras)
        {
            string palabraLimpia = LimpiarPalabra(palabra);
            if (string.IsNullOrEmpty(palabraLimpia)) continue;
            
            if (espToEng.ContainsKey(palabraLimpia))
                contadorEsp++;
            
            if (engToEsp.ContainsKey(palabraLimpia))
                contadorEng++;
        }
        
        // Si hay más coincidencias en el diccionario español-inglés, asumimos que es español
        return contadorEsp >= contadorEng;
    }
    
    static string TraducirPalabras(string frase, bool esEspanol)
    {
        // Dividir la frase mientras preservamos los separadores
        List<string> elementos = new List<string>();
        string palabraActual = "";
        
        foreach (char c in frase)
        {
            if (char.IsLetterOrDigit(c) || c == 'á' || c == 'é' || c == 'í' || c == 'ó' || c == 'ú' || c == 'ñ' || c == 'ü')
            {
                palabraActual += c;
            }
            else
            {
                if (!string.IsNullOrEmpty(palabraActual))
                {
                    elementos.Add(palabraActual);
                    palabraActual = "";
                }
                elementos.Add(c.ToString());
            }
        }
        
        if (!string.IsNullOrEmpty(palabraActual))
        {
            elementos.Add(palabraActual);
        }
        
        // Traducir cada palabra si está en el diccionario
        for (int i = 0; i < elementos.Count; i++)
        {
            string elemento = elementos[i];
            
            // Solo intentamos traducir elementos que son palabras
            if (elemento.Length > 0 && char.IsLetterOrDigit(elemento[0]))
            {
                string palabraLimpia = LimpiarPalabra(elemento);
                
                // Si la palabra existe en el diccionario, la traducimos
                if (esEspanol && espToEng.ContainsKey(palabraLimpia))
                {
                    elementos[i] = MantenerCapitalizacion(elemento, espToEng[palabraLimpia]);
                }
                else if (!esEspanol && engToEsp.ContainsKey(palabraLimpia))
                {
                    elementos[i] = MantenerCapitalizacion(elemento, engToEsp[palabraLimpia]);
                }
            }
        }
        
        // Unir todos los elementos nuevamente
        return string.Concat(elementos);
    }
    
    static string LimpiarPalabra(string palabra)
    {
        // Convertir a minúsculas y eliminar caracteres especiales
        return palabra.ToLower().Trim();
    }
    
    static string MantenerCapitalizacion(string original, string traduccion)
    {
        // Mantener la capitalización de la palabra original en la traducción
        if (string.IsNullOrEmpty(original) || string.IsNullOrEmpty(traduccion))
            return traduccion;
            
        if (char.IsUpper(original[0]))
            return char.ToUpper(traduccion[0]) + traduccion.Substring(1);
            
        return traduccion;
    }
    
    static void AgregarPalabra()
    {
        Console.Write("Ingrese la palabra en español: ");
        string palabraEsp = Console.ReadLine().Trim();
        
        Console.Write("Ingrese la traducción en inglés: ");
        string palabraEng = Console.ReadLine().Trim();
        
        if (string.IsNullOrEmpty(palabraEsp) || string.IsNullOrEmpty(palabraEng))
        {
            Console.WriteLine("Error: Las palabras no pueden estar vacías.");
            return;
        }
        
        AgregarPalabraADiccionarios(palabraEsp, palabraEng);
        Console.WriteLine($"¡Palabra agregada correctamente! {palabraEsp} = {palabraEng}");
    }
}
