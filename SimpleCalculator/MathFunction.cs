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
        public string Formula { get; private set; }
        public List<MathVariable> Variables { get; private set; }

        public MathFunction(string inputExpression)
        {
            // Name
            inputExpression = inputExpression.Replace(" ", string.Empty);
            Name = Regex.Match(inputExpression, @"^([^()]*)\(").Groups[1].Value;
            // Variables
            string variables = Regex.Match(inputExpression, @"\(([^()]*)\)").Groups[1].Value;
            string pattern = @"[,]*";
            Regex R = new Regex(pattern);
            variables = R.Replace(variables, @"");
            
            Variables = new List<MathVariable>();
            foreach (string variable in variables.Select(c => c.ToString()).ToList())
            {
                Variables.Add(new MathVariable(variable));
            }
            // Formula
            Formula = Regex.Match(inputExpression, @"(?<==).*$").Value;

            foreach (var i in Variables)
            {
                Console.WriteLine(i.Symbol + "   " + i.Value.ToString());
            }
            Console.WriteLine(Formula);
            Console.WriteLine(Name);
        }
    }
}
