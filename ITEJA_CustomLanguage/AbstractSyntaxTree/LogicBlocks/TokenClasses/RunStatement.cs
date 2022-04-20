using ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses.BodyStatements;
using ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses.Variables;
using System.Collections.Generic;
using System.Text;

namespace ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses
{
    /// <summary>
    /// Runs a method that corresponds to the given name.
    /// </summary>
    class RunStatement : IRunStatement
    {
        /// <summary>
        /// Name of the method that should be called.
        /// </summary>
        public StringBuilder Name { get; set; } = new StringBuilder();
        /// <summary>
        /// Parameters of the method that will be called and then passed on.
        /// </summary>
        public IList<IVariable> TokenParametersList { get; set; } = new List<IVariable>();
        /// <summary>
        /// Body Statement parent of this statement.
        /// </summary>
        public IBodyStatement Parent { get; set; }

        /// <summary>
        /// Executes a method.
        /// </summary>
        public void Execute()
        {
            foreach (var method in MainClass.Methods)
            {
                if (method.Name.Equals(Name))
                {
                    method.AssignParameters(TokenParametersList);
                    method.Execute();
                }
            }
        }
    }
}
