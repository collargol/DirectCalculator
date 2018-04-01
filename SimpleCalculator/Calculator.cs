using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCalculator
{
    static class Calculator
    {
        static Calculator() { }

        public static double PerformCalculation(List<string> postfixExpression)
        {
            Heap<double> calcHeap = new Heap<double>();
            try
            {
                foreach (string elem in postfixExpression)
                {

                    if (ExpressionManaging.IsMathOperator(elem))
                    {
                        double secondOperand = calcHeap.Pop();
                        double firstOperand = calcHeap.Pop();
                        double result;
                        switch (elem)
                        {
                            case "+":
                                result = firstOperand + secondOperand;
                                break;
                            case "-":
                                result = firstOperand - secondOperand;
                                break;
                            case "*":
                                result = firstOperand * secondOperand;
                                break;
                            case "/":
                                result = firstOperand / secondOperand;
                                break;
                            case "^":
                                result = Math.Pow(firstOperand, secondOperand);
                                break;
                            default:
                                throw new ArgumentException("Wrong operator readed");
                        }
                        calcHeap.Push(result);
                    }
                    else
                    {
                        calcHeap.Push(Convert.ToDouble(elem));
                    }
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            try
            {
                if (calcHeap.Amount != 1)
                {
                    throw new ApplicationException("Wrong output data in method!");
                }
                else
                {
                    return calcHeap.Pop();
                }
            }
            catch
            {
                throw;
            }

        }
    }
}
