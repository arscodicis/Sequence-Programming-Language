using ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses.BodyStatements;
using ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses.Variables;
using System;
using System.Text;

namespace ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses
{
    /// <summary>
    /// Prints results to the console.
    /// </summary>
    public class RandintStatment : IStatement
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
            Random rnd = new Random();
            int random = rnd.Next(0, Convert.ToInt32(Variable.Value));
            string s = Convert.ToString(random);
            StringBuilder sb = new StringBuilder();
            sb.Append(s);
            Console.WriteLine(sb);
            Variable.Value = sb;
            //Console.WriteLine("Number1: " + Num1 + "\nNumber2: " + Num2);
        }
    }
}
