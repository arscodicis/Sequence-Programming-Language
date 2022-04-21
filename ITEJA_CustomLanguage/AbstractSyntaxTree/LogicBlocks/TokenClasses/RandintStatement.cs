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
            //.WriteLine(Variable.Value);
            int recievedVariable = 0;
            if(int.TryParse(Convert.ToString(Variable.Value), out int result))
            {
                recievedVariable = result;
            } else
            {
                Console.WriteLine("Error: Can't create random with a string!");
            }

            Random rnd = new Random();
            int random = rnd.Next(0, recievedVariable);
            string s = Convert.ToString(random);
            StringBuilder sb = new StringBuilder();
            sb.Append(s);
            //Console.WriteLine(sb);
            try
            {
                Variable.Value = sb;
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            
            //Console.WriteLine("Number1: " + Num1 + "\nNumber2: " + Num2);
        }
    }
}
