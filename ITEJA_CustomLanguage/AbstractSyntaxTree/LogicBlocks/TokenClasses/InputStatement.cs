using ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses.BodyStatements;
using ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses.Variables;
using System;
using System.Text;

namespace ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses
{
    /// <summary>
    /// Prints results to the console.
    /// </summary>
    public class InputStatment : IStatement
    {
        /// <summary>
        /// Variable that will be printed to the console
        /// </summary>
        public IVariable Variable { get; set; }
        /// <summary>
        /// Bodystatement parent of this statement.
        /// </summary>
        public IBodyStatement Parent { get; set; }
        /// <summary>
        /// Executes a println
        /// </summary>
        public void Execute()
        {
            Console.Write("> ");
            string s = Console.ReadLine();
            StringBuilder sb = new StringBuilder();
            sb.Append(s);
            Variable.Value = sb;
            //Console.WriteLine("Variable value: " + Variable.Value);

        }
    }
}
