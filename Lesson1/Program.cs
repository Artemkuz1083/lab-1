using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Lesson1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите выражение: ");
            string str = Console.ReadLine();
            str = str.Replace(" ",string.Empty);
            var expression = OperationAndNumbers(str);
            var prn = PRN(expression);
            Console.WriteLine(string.Join(" ", prn));

        }

        public static List<object> OperationAndNumbers(string str)
        {
            char[] testOperations = { '+', '-', '*', '/', '(', ')' };
            List<object> expression = new List<object>();
            string num = string.Empty;
            for (int i = 0; i < str.Length; i++)
            {
                if (testOperations.Contains(str[i]))
                {
                    if (num != string.Empty)
                    {
                        expression.Add(num);
                        num = string.Empty;
                    }
                    expression.Add(str[i]);
                }
                else
                {
                    num += str[i];
                }
            }
            expression.Add(num);
            return expression;
        }

        public static List<object> PRN(List<object> expression) 
        {
            Dictionary<object, int> prioretyDictionary = new Dictionary<object, int>
            {
                {'+', 1},
                {'-', 1},
                {'*', 2},
                {'/', 2},
                {'(', 0},
                {')', 5},
            };
            List<object> prn = new List<object>();
            Stack<object> stack = new Stack<object>();
            foreach (var item in expression)
            {
                if (prioretyDictionary.ContainsKey(item))
                {
                    if ((Char)item == ')')
                    {
                        while ((Char)stack.Peek() != '(')
                        {
                            prn.Add(stack.Pop());
                        }
                        stack.Pop();
                    }
                    else if (stack.Count == 0
                        || (Char)item == '('
                        || prioretyDictionary[item] > prioretyDictionary[stack.Peek()])
                    {
                        stack.Push(item);
                    }
                    else if (prioretyDictionary[item] <= prioretyDictionary[stack.Peek()])
                    {
                        while (stack.Count > 0)
                        {
                            prn.Add(stack.Pop());
                        }
                        stack.Push(item);
                    }
                }
                else
                {
                    prn.Add(item);
                }
            }
            while (stack.Count > 0)
            {
                prn.Add(stack.Pop());
            }
            while(prn.Contains(string.Empty))
            {
                prn.Remove(string.Empty);
            }
            return prn;
        }

        public static double Calculate(int op, double first, double second)
        {
            switch (op)
            {
                case '*': return first * second;
                case '/': return first / second;
                case '+': return first + second;
                case '-': return first - second;
                default: return double.NaN;
            }
        }
    }
}
