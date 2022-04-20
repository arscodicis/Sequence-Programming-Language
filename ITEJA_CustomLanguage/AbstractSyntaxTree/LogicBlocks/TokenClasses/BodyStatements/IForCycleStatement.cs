using ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses.Variables;
using ITEJA_CustomLanguage.Lexer;
using System.Collections.Generic;

namespace ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses.BodyStatements
{
    /// <summary>
    /// Interface for forcycle statements in the program
    /// </summary>
    public interface IForCycleStatement : IBodyStatement
    {
        /// <summary>
        /// Keeps a variable in itself that monitors how many
        /// loops have already been done.
        /// </summary>
        IIntegerVariable InnerCounterVariable { get; set; }
        /// <summary>
        /// Keeps a variable that has the maximum allowed
        /// number of loops.
        /// </summary>
        IIntegerVariable MaximumAllowedCounter { get; set; }
        /// <summary>
        /// List of comparison operators that will be used for evaluating.
        /// </summary>
        IList<Token> ComparisonOperators { get; set; }
        /// <summary>
        /// If the loop has i++ then it's incremental, 
        /// otherwise it's decremental.
        /// </summary>
        bool IsIncremental { get; set; }
    }
}
