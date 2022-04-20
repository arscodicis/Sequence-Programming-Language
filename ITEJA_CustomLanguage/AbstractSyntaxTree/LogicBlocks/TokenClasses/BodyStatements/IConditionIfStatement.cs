using ITEJA_CustomLanguage.Lexer;
using System.Collections.Generic;

namespace ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses.BodyStatements
{
    /// <summary>
    /// Interface for condition statements in the program.
    /// </summary>
    public interface IConditionIfStatement : IBodyStatement
    {
        /// <summary>
        /// Contains another condition in itself that will
        /// be used as else (can be null)
        /// </summary>
        IConditionIfStatement ElseCondition { get; set; }
        /// <summary>
        /// Tokens of expressions that will be used to determine
        /// whether the condition is true or false
        /// </summary>
        Stack<Token> ExpressionTokens { get; set; }
    }
}
