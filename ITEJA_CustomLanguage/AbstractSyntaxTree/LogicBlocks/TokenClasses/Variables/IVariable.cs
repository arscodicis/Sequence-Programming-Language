using System.Text;

namespace ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses.Variables
{
    /// <summary>
    /// Encapsulating interface for all variables.
    /// </summary>
    public interface IVariable
    {
        /// <summary>
        /// Value of the variable.
        /// </summary>
        StringBuilder Value { get; set; }
        /// <summary>
        /// Name of the variable.
        /// </summary>
        StringBuilder Name { get; set; }
    }
}
