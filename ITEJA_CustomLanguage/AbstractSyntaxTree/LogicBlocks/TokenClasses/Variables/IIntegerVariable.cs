using ITEJA_CustomLanguage.Lexer;
using System.Collections.Generic;

namespace ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses.Variables
{
    /// <summary>
    /// Integer variable that is used to carry numeric values.
    /// </summary>
    public interface IIntegerVariable : IVariable
    {
        /// <summary>
        /// The expression of tokens that will be calculated
        /// and then put as a final value.
        /// </summary>
        Stack<Token> TokensExpression { get; set; }
        /// <summary>
        /// The method calculates the expression and saves the value to property
        /// </summary>
        void Calculate();
    }
}
