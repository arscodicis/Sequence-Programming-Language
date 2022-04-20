using ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses.Variables;
using System.Collections.Generic;
using System.Text;

namespace ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses.BodyStatements
{
    /// <summary>
    /// Interface for methods that are being used in a program.
    /// </summary>
    public interface IMethod : IBodyStatement
    {
        /// <summary>
        /// Name of the method
        /// </summary>
        StringBuilder Name { get; set; }
        /// <summary>
        /// Parameters of this method
        /// </summary>
        IList<IVariable> ParametersList { get; set; }
        /// <summary>
        /// Assigns parameters when the method is called from somewhere by run statement
        /// </summary>
        /// <param name="parametersList"></param>
        void AssignParameters(IList<IVariable> parametersList);
    }
}
