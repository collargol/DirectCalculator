using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCalculator
{
    static class ExpressionManaging
    {
        public struct MathOperator
        {
            static int PriorityIncrement;
            public string Type { get; private set; }
            public int Priority { get; private set; }

            static MathOperator()
            {
                PriorityIncrement = 3;
            }

            public MathOperator(string operatorType, int bracketsMultiplier)
            {
                if (operatorType.Equals("+") || operatorType.Equals("-"))
                {
                    Priority = 0 + PriorityIncrement * bracketsMultiplier;
                    Type = operatorType;
                }
                else if (operatorType.Equals("*") || operatorType.Equals("/"))
                {
                    Priority = 1 + PriorityIncrement * bracketsMultiplier;
                    Type = operatorType;
                }
                else if (operatorType.Equals("^"))
                {
                    Priority = 2 + PriorityIncrement * bracketsMultiplier;
                    Type = operatorType;
                }
                else
                {
                    throw new ArgumentException("Unknown operator appeared.");
                }
            }
        }

        static ExpressionManaging() { }

        public static List<string> ExpressionToInfix(string inputExpression)
        {
            inputExpression = inputExpression.Replace(" ", string.Empty);
            List<string> expression = inputExpression.Select(c => c.ToString()).ToList();
            List<string> separatedExpression = new List<string>();
            int currentPosition = 0;
            for (int i = 0; i < expression.Count; i++)
            {
                if (IsMathOperator(expression[i]) || IsBracket(expression[i]))
                {
                    separatedExpression.Add(expression[i]);
                    if (IsMathOperator(expression[i - 1]) || IsBracket(expression[i - 1]))
                    {
                        currentPosition++;
                    }
                    else
                    {
                        currentPosition += 2;
                    }
                }
                else
                {
                    if (currentPosition == separatedExpression.Count)
                    {
                        separatedExpression.Add(expression[i]);
                    }
                    else
                    {
                        separatedExpression[currentPosition] += expression[i];
                    }
                }
            }
            return separatedExpression;
        }

        public static List<string> InfixToPostfix(List<string> infixExpression)
        {
            List<string> postfixExpression = new List<string>();
            Heap<MathOperator> signHeap = new Heap<MathOperator>();
            int openBrackets = 0;
            for (int i = 0; i < infixExpression.Count; i++)
            {
                if (IsMathOperator(infixExpression[i]))
                {
                    MathOperator inputOperator = new MathOperator(infixExpression[i], openBrackets);
                    if (signHeap.Amount == 0)
                    {
                        signHeap.Push(inputOperator);
                    }
                    else
                    {
                        while (signHeap.Amount != 0)
                        {
                            MathOperator tempOperator = signHeap.Pop();
                            if (tempOperator.Priority <= inputOperator.Priority)
                            {
                                signHeap.Push(tempOperator);
                                signHeap.Push(inputOperator);
                                break;
                            }
                            else
                            {
                                postfixExpression.Add(tempOperator.Type);
                                if (signHeap.Amount == 0)
                                {
                                    signHeap.Push(inputOperator);
                                    break;
                                }
                            }
                        }
                    }
                }
                else if (IsBracket(infixExpression[i]))
                {
                    if (infixExpression[i].Equals("("))
                    {
                        openBrackets++;
                    }
                    else
                    {
                        openBrackets--;
                    }
                }
                else
                {
                    postfixExpression.Add(infixExpression[i]);
                }
            }
            while (signHeap.Amount != 0)
            {
                postfixExpression.Add(signHeap.Pop().Type);
            }
            return postfixExpression;
        }

        public static bool IsMathOperator(string elem)
        {
            return (elem.Equals("+") ||
                elem.Equals("-") ||
                elem.Equals("*") ||
                elem.Equals("/") ||
                elem.Equals("^"));
        }

        public static bool IsBracket(string elem)
        {
            return (elem.Equals("(") || elem.Equals(")"));
        }

        public static void PrintExpression(List<string> expression)
        {
            string fullExpression = "";
            foreach (string item in expression)
            {
                fullExpression += (item + " ");
            }
            Console.WriteLine(fullExpression);
        }
    }
}
