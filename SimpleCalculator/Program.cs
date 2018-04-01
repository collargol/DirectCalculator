using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Globalization;

namespace SimpleCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            // to change language of exceptions' messages ;)
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-us");
            // 

            for (;;)
            {
                try
                {
                    List<string> postfixExpression = GetUserInput();
                    if (postfixExpression == null)
                    {
                        Console.WriteLine("Bye bye!");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Result:");
                        Console.WriteLine(Calculator.PerformCalculation(postfixExpression).ToString());
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception has been caught:");
                    Console.WriteLine(e.Message);
                }
            }
        }

        static List<string> GetUserInput()
        {
            Console.WriteLine("Provide expression to calculate or 'q' letter to quit:");
            string userInput = Console.ReadLine();
            if (userInput.Equals("q") || userInput.Equals("quit"))
            {
                return null;
            }
            return ExpressionManaging.InfixToPostfix(ExpressionManaging.ExpressionToInfix(userInput));
        }
    }
}
