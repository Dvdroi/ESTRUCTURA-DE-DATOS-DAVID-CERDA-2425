using System;
using System.Collections.Generic;

public class BalanceChecker
{
    public static bool IsBalanced(string expression)
    {
        // Pila para almacenar los símbolos de apertura
        Stack<char> stack = new Stack<char>();

        // Diccionario para mapear símbolos de cierre con sus correspondientes aperturas
        Dictionary<char, char> brackets = new Dictionary<char, char>
        {
            { ')', '(' },
            { ']', '[' },
            { '}', '{' }
        };

        foreach (char c in expression)
        {
            // Si es un símbolo de apertura, lo agregamos a la pila
            if (c == '(' || c == '[' || c == '{')
            {
                stack.Push(c);
            }
            // Si es un símbolo de cierre
            else if (c == ')' || c == ']' || c == '}')
            {
                // Si la pila está vacía, significa que hay un cierre sin apertura
                if (stack.Count == 0)
                {
                    return false;
                }

                // Obtenemos el último símbolo de apertura
                char lastOpening = stack.Pop();

                // Verificamos si corresponde con el símbolo de cierre actual
                if (lastOpening != brackets[c])
                {
                    return false;
                }
            }
        }

        // La expresión está balanceada si la pila queda vacía
        return stack.Count == 0;
    }

    public static void Main()
    {
        string expression = "{7+(8*5)-[(9-7)+(4+1)]}";
        bool isBalanced = IsBalanced(expression);
        
        Console.WriteLine($"Expresión: {expression}");
        Console.WriteLine($"Resultado: {(isBalanced ? "Formula balanceada" : "Formula no balanceada")}");

        // Casos de prueba adicionales
        string[] testCases = {
            "((()))",           // Balanceada
            "{[()]}",           // Balanceada
            "{[(])}",           // No balanceada
            "((())",            // No balanceada
            "{7+[8*(5-3)])}",   // No balanceada
        };

        foreach (string test in testCases)
        {
            Console.WriteLine($"\nExpresión: {test}");
            Console.WriteLine($"Resultado: {(IsBalanced(test) ? "Formula balanceada" : "Formula no balanceada")}");
        }
    }
}
