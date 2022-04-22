using ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses.BodyStatements;
using ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses.Variables;
using NAudio.Wave;
using System;
using System.Diagnostics;
using System.Threading;

namespace ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses
{
    /// <summary>
    /// Prints results to the console.
    /// </summary>
    public class AudioStatment : IStatement
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
                Process proc = new System.Diagnostics.Process();
                proc.StartInfo.FileName = "/bin/bash";
                proc.StartInfo.Arguments = "-c \" " + "afplay " + Convert.ToString(Variable.Value) + " \""; 
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.Start();

                while (!proc.StandardOutput.EndOfStream)
                {
                    //Console.WriteLine(proc.StandardOutput.ReadLine());
                }


            } catch (Exception ex)
            {
                try
                {
                    using (var waveOut = new WaveOutEvent())
                    using (var wavReader = new WaveFileReader(Convert.ToString(Variable.Value)))
                    {
                        waveOut.Init(wavReader);
                        waveOut.Play();
                    }

                }
                catch (Exception ex2)
                {
                    Console.WriteLine("Error playing sound file. Make sure path is right!");
                }
            }
        }

    }

    

    
}
