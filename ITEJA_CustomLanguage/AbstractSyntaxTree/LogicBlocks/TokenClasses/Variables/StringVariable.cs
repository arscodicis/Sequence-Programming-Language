using System.Text;

namespace ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses.Variables
{
    /// <summary>
    /// String variable that contains its name and value.
    /// </summary>
    class StringVariable : IStringVariable
    {
        /// <summary>
        /// Value of the variable.
        /// </summary>
        public StringBuilder Value { get; set; } = new StringBuilder();
        /// <summary>
        /// Name of the variable.
        /// </summary>
        public StringBuilder Name { get; set; } = new StringBuilder();
    }
}
