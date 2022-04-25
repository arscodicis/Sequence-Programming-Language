using ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses.BodyStatements;
using ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses.Variables;
using System;
using System.Linq.Expressions;
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
            string s = Console.ReadLine();
            StringBuilder sb = new StringBuilder();
            _ = sb.Append(s);

            try
            {
                   
                if(int.TryParse(Convert.ToString(sb), out int result))
                {
                    StringBuilder sb2 = new StringBuilder();
                    _ = sb2.Append(Convert.ToString(result));
                    Variable.Value = sb2;
                } else
                {
                    //It is a string!!
                    //Console.WriteLine("Cannot assign an integer a string!");
                    
                    Variable.Value = sb;
                }
               

            } catch (Exception ex)
            {
                Console.WriteLine("Issue setting input value.");
            }

            
            //Console.WriteLine("Variable value: " + Variable.Value);

        }
    }
}
