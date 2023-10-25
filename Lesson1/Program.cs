using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
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
            OperationAndNumbers(str);
        }

        public static void OperationAndNumbers(string str)
        {
            string[] arrayNum = str.Split(new char[] {'+', '-', '*', '/', '(', ')'},StringSplitOptions.RemoveEmptyEntries);
            List<double> numbers = new List<double>();
            List<Char> operation = new List<Char>();
            char[] testOperations = {'+', '-', '*', '/', '(', ')'};
            foreach(string num in arrayNum)
            {
                numbers.Add(Convert.ToDouble(num));
            }
            for (int i = 0; i < str.Length; i++)
            {
                if (testOperations.Contains(str[i]))
                {
                    operation.Add(str[i]);
                }
            }
            Console.WriteLine($"список чисел: {string.Join(" ", numbers)}");
            Console.WriteLine($"список операций: {string.Join(" ", operation)}");
            Operation(numbers, operation);
        }

        public static void Operation(List<double> num, List<Char> oper)
        {
            var priorety = Priorety(oper);
            while (oper.Contains('(') && oper.Contains(')'))
            {
                oper.Remove('(');
                oper.Remove(')');
            }

            while (num.Count != 1)
            {
                int ind = priorety.IndexOf(priorety.Max());
                priorety.RemoveAt(ind);

                var op = oper[ind];
                var first = num[ind];
                var second = num[ind + 1];
                num.RemoveAt(ind);
                num.RemoveAt(ind);
                oper.RemoveAt(ind);

                var result = Calculate(op, first, second);
                num.Insert(ind, result);
            }
            Console.WriteLine($"Значение выражения: {string.Join(" ", num)}");
        }

        public static List<int> Priorety(List<char> oper)
        {
            List<int> prioretyList = new List<int>();
            int prioret = 1;
            foreach (var ch in oper)
            {
                if(ch == '(')
                {
                    prioret += 2;
                    continue;
                }
                else if (ch == ')')
                {
                    prioret -= 2;
                    continue;
                }
                if(ch == '*' ||  ch == '/')
                {
                    prioretyList.Add(prioret + 1);
                    continue;
                }
                prioretyList.Add(prioret);
            }
            return prioretyList;
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
