using ITEJA_CustomLanguage.Lexer;
using System.Collections.Generic;

namespace ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses
{
    /// <summary>
    /// Changes a value of the already existing variable
    /// </summary>
    public interface IRedefinitionStatement : IStatement
    {
        /// <summary>
        /// Value of the token that will be used to
        /// find the variable
        /// </summary>
        public Token IdentifierToken { get; set; }
        /// <summary>
        /// Expression that will be evaluated and calculated
        /// to the final value
        /// </summary>
        public Stack<Token> TokensExpression { get; set; }
    }
}
