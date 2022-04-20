using ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses.Variables;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses.BodyStatements.Classes
{
    /// <summary>
    /// Interface for methods that are being used in a program.
    /// </summary>
    public class Method : IMethod
    {
        /// <summary>
        /// Name of the method.
        /// </summary>
        public StringBuilder Name { get; set; } = new StringBuilder();
        /// <summary>
        /// List of all the statements that will be alter executed.
        /// </summary>
        public IList<IStatement> Statements { get; } = new List<IStatement>();
        /// <summary>
        /// List of all local variables.
        /// </summary>
        public IList<IVariable> LocalVariables { get; } = new List<IVariable>();
        /// <summary>
        /// A reference to the parent of this method (will be null in most cases)
        /// </summary>
        public IBodyStatement Parent { get; set; }
        /// <summary>
        /// List of parameters needed to call this method.
        /// </summary>
        public IList<IVariable> ParametersList { get; set; } = new List<IVariable>();
        /// <summary>
        /// Executes all the statements within itself.
        /// </summary>
        public void Execute()
        {
            foreach (var statement in Statements)
            {
                statement.Execute();
            }
        }
        /// <summary>
        /// Assigns parameters from when some statement calls this method.
        /// </summary>
        /// <param name="variablesList"></param>
        public void AssignParameters(IList<IVariable> variablesList)
        {
            if (variablesList.Count != ParametersList.Count)
            {
                throw new ArgumentException("The number of parameters don't match for this method");
            }
            for (int i = 0; i < ParametersList.Count; i++)
            {
                if (variablesList[i].GetType().Equals(ParametersList[i].GetType()))
                {
                    ParametersList[i].Value = variablesList[i].Value;
                }
                else
                {
                    throw new ArgumentException("The parameters don't match.");
                }
            }
        }
    }
}
