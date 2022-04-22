using ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses.BodyStatements;
using ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses.Variables;
using NAudio.Wave;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses
{
    /// <summary>
    /// Prints results to the console.
    /// </summary>
    public class ReadFileStatment : IStatement
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
        ///


        public void Execute()
        {
            //Console.WriteLine(Variable.Value);
            try
            {
                string s = Convert.ToString(File.ReadAllText(Convert.ToString(Variable.Value)));
                StringBuilder sb = new StringBuilder();
                _ = sb.Append(s);
                Variable.Value = sb;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while reading file! Please check your path.");
            }
        }

    }




}
