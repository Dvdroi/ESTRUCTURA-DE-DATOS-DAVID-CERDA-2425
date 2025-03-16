using System;
using System.Collections.Generic;

namespace CatalogoRevistasModificado
{
    class Program
    {
        static void Main(string[] args)
        {
            // Crear la clase Catalogo
            Catalogo miCatalogo = new Catalogo();
            
            // Añadir las 10 revistas al catálogo
            miCatalogo.AgregarRevista("National Geographic");
            miCatalogo.AgregarRevista("Time");
            miCatalogo.AgregarRevista("Vogue");
            miCatalogo.AgregarRevista("Scientific American");
            miCatalogo.AgregarRevista("The New Yorker");
            miCatalogo.AgregarRevista("Wired");
            miCatalogo.AgregarRevista("Forbes");
            miCatalogo.AgregarRevista("Sports Illustrated");
            miCatalogo.AgregarRevista("Cosmopolitan");
            miCatalogo.AgregarRevista("Rolling Stone");
            
            // Ejecutar el menú principal
            MostrarMenuPrincipal(miCatalogo);
        }
        
        static void MostrarMenuPrincipal(Catalogo catalogo)
        {
            int opcion = 0;
            
            do
            {
                Console.Clear();
                Console.WriteLine("***************************************");
                Console.WriteLine("*      SISTEMA CATÁLOGO REVISTAS      *");
                Console.WriteLine("***************************************");
                Console.WriteLine("1. Buscar revista (método recursivo)");
                Console.WriteLine("2. Buscar revista (método iterativo)");
                Console.WriteLine("3. Ver todas las revistas");
                Console.WriteLine("4. Salir");
                Console.WriteLine("***************************************");
                Console.Write("Ingrese su opción: ");
                
                try
                {
                    opcion = Convert.ToInt32(Console.ReadLine());
                    
                    switch(opcion)
                    {
                        case 1:
                            RealizarBusqueda(catalogo, true);
                            break;
                            
                        case 2:
                            RealizarBusqueda(catalogo, false);
                            break;
                            
                        case 3:
                            MostrarRevistas(catalogo);
                            break;
                            
                        case 4:
                            Console.WriteLine("Gracias por utilizar el sistema.");
                            break;
                            
                        default:
                            Console.WriteLine("Opción no válida. Intente nuevamente.");
                            break;
                    }
                    
                    if (opcion != 4)
                    {
                        Console.WriteLine("\nPresione Enter para continuar...");
                        Console.ReadLine();
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error: Debe ingresar un número.");
                    Console.WriteLine("\nPresione Enter para continuar...");
                    Console.ReadLine();
                }
                
            } while (opcion != 4);
        }
        
        static void RealizarBusqueda(Catalogo catalogo, bool usarRecursivo)
        {
            Console.WriteLine("\n--- BÚSQUEDA DE REVISTAS ---");
            Console.Write("Ingrese el título a buscar: ");
            string titulo = Console.ReadLine();
            
            string metodo = usarRecursivo ? "recursivo" : "iterativo";
            Console.WriteLine($"\nRealizando búsqueda {metodo}...");
            
            bool resultado = usarRecursivo ? 
                catalogo.BuscarRecursivo(titulo) : 
                catalogo.BuscarIterativo(titulo);
            
            Console.WriteLine($"\nResultado: {(resultado ? "Encontrado" : "No encontrado")}");
        }
        
        static void MostrarRevistas(Catalogo catalogo)
        {
            Console.WriteLine("\n--- LISTADO DE REVISTAS ---");
            catalogo.MostrarRevistas();
        }
    }
    
    class Catalogo
    {
        private List<string> revistas;
        
        public Catalogo()
        {
            revistas = new List<string>();
        }
        
        public void AgregarRevista(string titulo)
        {
            revistas.Add(titulo);
            // Mantener la lista ordenada para la búsqueda binaria
            revistas.Sort();
        }
        
        public bool BuscarRecursivo(string titulo)
        {
            return BusquedaBinariaRecursiva(titulo, 0, revistas.Count - 1);
        }
        
        private bool BusquedaBinariaRecursiva(string titulo, int inicio, int fin)
        {
            // Caso base: no se encontró el elemento
            if (inicio > fin)
                return false;
                
            // Encontrar el elemento medio
            int medio = (inicio + fin) / 2;
            
            // Comparar (ignorando mayúsculas/minúsculas)
            int comparacion = string.Compare(revistas[medio], titulo, StringComparison.OrdinalIgnoreCase);
            
            // Verificar si se encontró
            if (comparacion == 0)
                return true;
            // Buscar en la mitad izquierda
            else if (comparacion > 0)
                return BusquedaBinariaRecursiva(titulo, inicio, medio - 1);
            // Buscar en la mitad derecha
            else
                return BusquedaBinariaRecursiva(titulo, medio + 1, fin);
        }
        
        public bool BuscarIterativo(string titulo)
        {
            int inicio = 0;
            int fin = revistas.Count - 1;
            
            while (inicio <= fin)
            {
                int medio = (inicio + fin) / 2;
                int comparacion = string.Compare(revistas[medio], titulo, StringComparison.OrdinalIgnoreCase);
                
                if (comparacion == 0)
                    return true;
                else if (comparacion > 0)
                    fin = medio - 1;
                else
                    inicio = medio + 1;
            }
            
            return false;
        }
        
        public void MostrarRevistas()
        {
            if (revistas.Count == 0)
            {
                Console.WriteLine("El catálogo está vacío.");
                return;
            }
            
            for (int i = 0; i < revistas.Count; i++)
            {
                Console.WriteLine($"{i+1}. {revistas[i]}");
            }
        }
    }
}
