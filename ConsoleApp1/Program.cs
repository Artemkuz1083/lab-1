using System;
using System.Collections;
using System.Collections.Generic;


class Program
{
    public static void Main()
    {
        GetAnswer(GetLists(Console.ReadLine()));
    }

    public static ArrayList GetLists(string expression)
    {
        expression = expression.Replace(" ", string.Empty);

        List<string> numbers = new List<string>();
        List<char> signs = new List<char>();
        string toBuildNumber = "";

        foreach (char symbol in expression)
        {
            if (Char.IsDigit(symbol))
            {
                toBuildNumber += symbol;
            }
            else
            {
                numbers.Add(toBuildNumber);
                toBuildNumber = "";
                signs.Add(symbol);
            }
        }
        numbers.Add(toBuildNumber);

        Console.Write("Your numbers: ");
        foreach (string number in numbers) { Console.Write($"{number} "); }
        Console.WriteLine();
        Console.Write("Your signs: ");
        foreach (char sign in signs) { Console.Write($"{sign} "); }
        Console.WriteLine();

        ArrayList lists = new ArrayList
        {
            numbers,
            signs
        };
        return lists;
    }

    public static void GetAnswer(ArrayList lists)
    {
        List<string> numbers = (List<string>)lists[0];
        List<char> signs = (List<char>)lists[1];

        while (numbers.Count != 1)
        {
            for (int i = 0; i < signs.Count; i++)
            {
                double number = 0;

                if (signs[i] == '/' | signs[i] == '*')
                {

                    if (signs[i] == '/')
                    {
                        number = double.Parse(numbers[i]) / double.Parse(numbers[i + 1]);
                    }

                    else
                    {
                        number = double.Parse(numbers[i]) * double.Parse(numbers[i + 1]);
                    }

                    numbers.RemoveAt(i + 1);
                    numbers.RemoveAt(i);
                    numbers.Insert(i, $"{number}");
                    signs.RemoveAt(i);
                }

                else if (!(signs.Contains('/') || signs.Contains('*')))
                {

                    if (signs[i] == '+')
                    {
                        number = double.Parse(numbers[i]) + double.Parse(numbers[i + 1]);
                    }

                    else if (signs[i] == '-')
                    {
                        number = double.Parse(numbers[i]) - double.Parse(numbers[i + 1]);
                    }

                    numbers.RemoveAt(i + 1);
                    numbers.RemoveAt(i);
                    numbers.Insert(i, $"{number}");
                    signs.RemoveAt(i);
                }
            }

        }

        Console.Write("Answer is ");
        foreach (string number in numbers)
        {
            Console.Write($"{number} ");
        }
    }
}