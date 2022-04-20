using ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses.Variables.CalculateInteger;
using ITEJA_CustomLanguage.Lexer;
using System.Collections.Generic;
using System.Text;

namespace ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses.Variables
{
    /// <summary>
    /// Integer variable that is used to carry numeric values.
    /// </summary>
    public class IntegerVariable : IIntegerVariable
    {
        /// <summary>
        /// Final value of the integer variable.
        /// </summary>
        public StringBuilder Value { get; set; } = new StringBuilder();
        /// <summary>
        /// Name of the integer variable.
        /// </summary>
        public StringBuilder Name { get; set; } = new StringBuilder();
        /// <summary>
        /// The expression of tokens that will be calculated
        /// and then put as a final value.
        /// </summary>
        public Stack<Token> TokensExpression { get; set; } = new Stack<Token>();
        /// <summary>
        /// Calcultates the expression and puts it as a final value.
        /// </summary>
        public void Calculate()
        {
            if (TokensExpression.Count != 0)
            {
                Expression expr = new Expression(TokensExpression);
                Value.Clear();
                Value.Append(expr.Calculate().ToString());
            }
        }
    }
}
