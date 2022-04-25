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
    public class ClearFileStatment : IStatement { 
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
            try
            {
                File.WriteAllText(Convert.ToString(Variable.Value), "");
            } catch
            {
                Console.WriteLine("Error while clearing file.");
            }
            
        }

    }




}
