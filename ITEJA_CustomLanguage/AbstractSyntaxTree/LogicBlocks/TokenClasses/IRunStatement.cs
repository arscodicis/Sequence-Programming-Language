using ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses.Variables;
using System.Collections.Generic;
using System.Text;

namespace ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses
{
    /// <summary>
    /// Runs a method that corresponds to the given name.
    /// </summary>
    public interface IRunStatement : IStatement
    {
        /// <summary>
        /// Name of the method that should be called.
        /// </summary>
        StringBuilder Name { get; set; }
        /// <summary>
        /// Parameters of the method that will be called and then passed on.
        /// </summary>
        IList<IVariable> TokenParametersList { get; set; }
    }
}