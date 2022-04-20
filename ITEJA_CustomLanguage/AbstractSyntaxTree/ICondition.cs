using ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses.BodyStatements;
using ITEJA_CustomLanguage.Lexer;
using System.Collections.Generic;

namespace ITEJA_CustomLanguage.AbstractSyntaxTree
{
    /// <summary>
    /// Creates a condition and evaluates it
    /// </summary>
    public interface ICondition
    {
        /// <summary>
        /// The expression sent to the metho will be evaluated
        /// </summary>
        /// <param name="parExpressionTokens"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        bool IsConditionTrue(Stack<Token> parExpressionTokens, IBodyStatement parent);
    }
}
