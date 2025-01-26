using System;
using System.Collections.Generic;

public class TorresHanoi
{
    private Stack<int>[] torres;
    private int numMovimientos;

    public TorresHanoi(int numDiscos)
    {
        // Inicializar las tres torres como pilas
        torres = new Stack<int>[3];
        for (int i = 0; i < 3; i++)
        {
            torres[i] = new Stack<int>();
        }

        // Colocar los discos iniciales en la primera torre
        for (int i = numDiscos; i >= 1; i--)
        {
            torres[0].Push(i);
        }

        numMovimientos = 0;
    }

    public void Resolver()
    {
        int numDiscos = torres[0].Count;
        Console.WriteLine($"Resolviendo Torres de Hanoi con {numDiscos} discos:\n");
        MostrarEstado();
        MoverDiscos(numDiscos, 0, 2, 1);
        Console.WriteLine($"\nSolución completada en {numMovimientos} movimientos!");
    }

    private void MoverDiscos(int n, int origen, int destino, int auxiliar)
    {
        if (n > 0)
        {
            // Mover n-1 discos a la torre auxiliar
            MoverDiscos(n - 1, origen, auxiliar, destino);

            // Mover el disco más grande a la torre destino
            int disco = torres[origen].Pop();
            torres[destino].Push(disco);
            numMovimientos++;

            Console.WriteLine($"\nMovimiento {numMovimientos}: Mover disco {disco} de Torre {origen + 1} a Torre {destino + 1}");
            MostrarEstado();

            // Mover los n-1 discos de la torre auxiliar a la torre destino
            MoverDiscos(n - 1, auxiliar, destino, origen);
        }
    }

    private void MostrarEstado()
    {
        // Crear copias temporales de las pilas para no modificar las originales
        Stack<int>[] torresTemp = new Stack<int>[3];
        for (int i = 0; i < 3; i++)
        {
            torresTemp[i] = new Stack<int>(torres[i].Reverse());
        }

        // Encontrar la altura máxima de las torres
        int maxAltura = Math.Max(torres[0].Count,
                        Math.Max(torres[1].Count, torres[2].Count));

        // Mostrar las torres
        for (int nivel = maxAltura - 1; nivel >= 0; nivel--)
        {
            for (int torre = 0; torre < 3; torre++)
            {
                if (torresTemp[torre].Count > nivel)
                {
                    Console.Write($"[{torresTemp[torre].Pop()}]\t");
                }
                else
                {
                    Console.Write("| |\t");
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine("=================");
        Console.WriteLine("T1\tT2\tT3");
    }

    public static void Main()
    {
        Console.WriteLine("Torres de Hanoi - Solución con Pilas");
        Console.Write("\nIngrese el número de discos: ");
        int numDiscos = int.Parse(Console.ReadLine());

        TorresHanoi hanoi = new TorresHanoi(numDiscos);
        hanoi.Resolver();
    }
}
