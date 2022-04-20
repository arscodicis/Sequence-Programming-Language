using ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses.BodyStatements;

namespace ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses
{
    /// <summary>
    /// Main Statement that is being 
    /// used in all of the classes
    /// </summary>
    public interface IStatement
    {
        /// <summary>
        /// Executes all statements within itself
        /// </summary>
        void Execute();
        /// <summary>
        /// Parent of the statement for local variables
        /// </summary>
        IBodyStatement Parent { get; set; }
    }
}
