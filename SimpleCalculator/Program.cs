using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            List<MathFunction> Functions = new List<MathFunction>();

            for (;;)
            {
                try
                {
                    Console.WriteLine();
                    Console.WriteLine("Type specific letter to perform action:");
                    Console.WriteLine("f - to define new function");
                    Console.WriteLine("c - to give arguments to calculate existing function");
                    Console.WriteLine("x - to calculate direct given expression");
                    Console.WriteLine("h - to get help");
                    Console.WriteLine("q - to quit");
                    string userChoice = Console.ReadLine();
                    if (userChoice.Equals("q"))
                    {
                        Console.WriteLine("Bye bye!");
                        break;
                    }
                    else if (userChoice.Equals("f"))
                    {
                        Console.WriteLine("Provide function's pattern below:");
                        string inputExpression = Console.ReadLine();
                        if (inputExpression.Equals("") || !inputExpression.Contains("="))
                        {
                            Console.WriteLine("Given funtion is incomplete.");
                            continue;
                        }
                        string functionName = Regex.Match(inputExpression, @"^([^()]*)\(").Groups[1].Value;
                        MathFunction functionToRemove = Functions.SingleOrDefault(f => f.Name.Equals(functionName));
                        if (functionToRemove != null)
                        {
                            Console.WriteLine("Function with specified name already existed. Do you want to replace it with your function? y/n");
                            string choice = Console.ReadLine();
                            if (choice.Equals("y"))
                            {
                                Functions.Remove(functionToRemove);
                            }
                            else if (choice.Equals("n"))
                            {
                                continue;
                            }
                            else
                            {
                                Console.WriteLine("Unknown command, function not created.");
                                continue;
                            }
                        }
                        Functions.Add(new MathFunction(inputExpression));
                    }
                    else if (userChoice.Equals("c"))
                    {
                        Console.WriteLine("Provide function's symbol with variables' values below:");
                        string inputExpression = Console.ReadLine();
                        string functionName = Regex.Match(inputExpression, @"^([^()]*)\(").Groups[1].Value;
                        MathFunction functionToCompute = Functions.SingleOrDefault(f => f.Name.Equals(functionName));
                        if (functionToCompute != null)
                        {
                            inputExpression = inputExpression.Replace(" ", string.Empty);
                            string variablesSection = Regex.Match(inputExpression, @"\(([^()]*)\)").Groups[1].Value;
                            List<double> variablesValues = new List<double>();
                            string tempVariable = "";
                            foreach (string v in variablesSection.Select(c => c.ToString()).ToList())
                            {
                                if (v.Equals(","))
                                {
                                    if (!tempVariable.Equals(""))
                                    {
                                        variablesValues.Add(Convert.ToDouble(tempVariable));
                                        tempVariable = "";
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                else
                                {
                                    tempVariable += v;
                                }
                            }
                            if (!tempVariable.Equals(""))
                            {
                                variablesValues.Add(Convert.ToDouble(tempVariable));
                            }
                            double result = functionToCompute.ComputeFunction(variablesValues);
                            Console.WriteLine(result.ToString());
                        }
                        else
                        {
                            Console.WriteLine("This function is not defined.");
                        }
                    }
                    else if (userChoice.Equals("x"))
                    {
                        Console.WriteLine("Provide expression to calculate:");
                        string inputExpression = Console.ReadLine();
                        List<string> postfixExpression = ExpressionManaging.InfixToPostfix(ExpressionManaging.ExpressionToInfix(inputExpression));
                        Console.WriteLine(Calculator.PerformCalculation(postfixExpression).ToString());
                    }
                    else if (userChoice.Equals("h"))
                    {
                        Console.WriteLine("*****************************************************************");
                        Console.WriteLine("Functions should be declared as below:");
                        Console.WriteLine("f(x,y,z) = x^3 + 2*x*y - 3*(y/z)^2 + z^2");
                        Console.WriteLine("Direct expressions should be given like this:");
                        Console.WriteLine("4.25 * (5.68/(2.1+3.44) - 89.2)^4.2");
                        Console.WriteLine("Spaces are non-compulsory - you can use them wherever you want.");
                        Console.WriteLine("*****************************************************************");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("Incorrect choice, try again.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception has been caught:");
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
