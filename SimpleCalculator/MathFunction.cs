using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SimpleCalculator
{
    class MathFunction
    {
        public struct MathVariable
        {
            public string Symbol { get; private set; }
            public double Value { get; set; }

            public MathVariable(string symbol)
            {
                Symbol = symbol;
                Value = 0.0;
            }
        }

        public string Name { get; private set; }
        public List<string> Formula { get; private set; }
        public List<MathVariable> Variables { get; private set; }

        public MathFunction(string inputExpression)
        {
            // Name
            inputExpression = inputExpression.Replace(" ", string.Empty);
            Name = Regex.Match(inputExpression, @"^([^()]*)\(").Groups[1].Value;
            // Variables
            Variables = GetVariablesList(inputExpression);
            // Formula
            string rawFormula = Regex.Match(inputExpression, @"(?<==).*$").Value;
            List<string> infixFormula = ExpressionManaging.ExpressionToInfix(rawFormula);
            Formula = ExpressionManaging.InfixToPostfix(infixFormula);
        }

        public double ComputeFunction(List<double> variablesValues)
        {
            if (variablesValues.Count > Variables.Count)
            {
                throw new ArgumentException("Too much arguments.");
            }
            else if (variablesValues.Count < Variables.Count)
            {
                throw new ArgumentException("Not enough arguments.");
            }
            else
            {
                List<string> tempFormula = Formula;
                for (int i = 0; i < variablesValues.Count; i++)
                {
                    List<int> indexes = Enumerable.Range(0, tempFormula.Count).Where(j => tempFormula[j].Equals(Variables[i].Symbol)).ToList();
                    foreach (int ind in indexes)
                    {
                        tempFormula[ind] = variablesValues[i].ToString();
                    }
                }
                return Calculator.PerformCalculation(tempFormula);
            }
        }

        static List<MathVariable> GetVariablesList(string inputExpression)
        {
            string variablesSection = Regex.Match(inputExpression, @"\(([^()]*)\)").Groups[1].Value;
            string pattern = @"[,]*";
            Regex R = new Regex(pattern);
            variablesSection = R.Replace(variablesSection, @" ");
            List<MathVariable> variables = new List<MathVariable>();
            string temp = "";
            foreach (string v in variablesSection.Select(c => c.ToString()).ToList())
            {
                if (v.Equals(" "))
                {
                    if (!temp.Equals(""))
                    {
                        variables.Add(new MathVariable(temp));
                        temp = "";
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    temp += v;
                }
            }
            if (!temp.Equals(""))
            {
                variables.Add(new MathVariable(temp));
            }
            return variables;
        }

    }
}
