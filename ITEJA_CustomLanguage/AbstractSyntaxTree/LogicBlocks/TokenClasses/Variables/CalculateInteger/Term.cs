using System.Collections.Generic;

namespace ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses.Variables.CalculateInteger
{
    /// <summary>
    /// Term that has multiple factors and appends them with operators.
    /// </summary>
    public class Term
    {
        public Stack<Factor> Factors = new Stack<Factor>();
        public List<string> Operators = new List<string>();

        /// <summary>
        /// Calculates the factors between eachother.
        /// </summary>
        /// <returns>The result of the two terms.</returns>
        public double Calculate()
        {
            double result;

            Factors = new Stack<Factor>(Factors);
            while (Factors.Count != 1)
            {
                for (int j = 0; j < Operators.Count; j++)
                {
                    switch (Operators[j])
                    {
                        case "*":
                            result = Factors.Pop().Value * Factors.Pop().Value;
                            Factors.Push(new Factor(result));
                            break;
                        case "/":
                            result = Factors.Pop().Value / Factors.Pop().Value;
                            Factors.Push(new Factor(result));
                            break;
                    }
                }
            }
            return Factors.Pop().Value;
        }
    }
}
