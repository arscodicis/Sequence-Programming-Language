using ITEJA_CustomLanguage.Lexer;
using System;
using System.Collections.Generic;

namespace ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses.Variables.CalculateInteger
{
    /// <summary>
    /// Expression that splits the expression and calculates it.
    /// </summary>
    public class Expression
    {
        private readonly Stack<Token> tokens;
        private readonly List<Term> terms = new List<Term>();
        private Stack<string> operands = new Stack<string>();

        public Expression(Stack<Token> parTokens)
        {
            tokens = new Stack<Token>(parTokens);
            LoadTerms();
        }
        /// <summary>
        /// Loads terms for the later evaluation.
        /// </summary>
        private void LoadTerms()
        {
            Term t = new Term();

            while (tokens.Count != 0)
            {
                if (tokens.Peek().Value.Equals("+") || tokens.Peek().Value.Equals("-"))
                {
                    if (t.Factors.Count != 0)
                    {
                        terms.Add(t);
                    }
                    operands.Push(tokens.Pop().Value);
                    LoadTerms();
                    return;
                }
                if (tokens.Peek().Type == TokenType.NumberCharacters || tokens.Peek().Type == TokenType.Identifier)
                {
                    t.Factors.Push(new Factor(Double.Parse(tokens.Pop().Value)));
                }
                else
                {
                    t.Operators.Add(tokens.Pop().Value);
                }
            }
            if (t.Factors.Count != 0)
            {
                terms.Add(t);
            }
        }
        /// <summary>
        /// Calculates the expression.
        /// </summary>
        /// <returns>the result of the calculation.</returns>
        public double Calculate()
        {
            Stack<double> results = new Stack<double>();
            foreach (var item in terms)
            {
                results.Push(item.Calculate());
            }

            operands = new Stack<string>(operands);
            results = new Stack<double>(results);
            while (results.Count != 1)
            {
                for (int i = 0; i < operands.Count; i++)
                {
                    switch (operands.Pop())
                    {
                        case "+":
                            results.Push(results.Pop() + results.Pop());
                            break;
                        case "-":
                            results.Push(results.Pop() - results.Pop());
                            break;
                    }
                }
            }
            return results.Pop();
        }
    }
}
