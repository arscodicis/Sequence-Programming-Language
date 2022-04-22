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


            }
            catch (Exception ex)
            {
                try
                {
                    using (var audioFile = new AudioFileReader(Convert.ToString(Variable.Value)))
                    using (var outputDevice = new WaveOutEvent())
                    {
                        outputDevice.Init(audioFile);
                        outputDevice.Play();
                        while (outputDevice.PlaybackState == PlaybackState.Playing)
                        {
                            Thread.Sleep(1000);
                        }
                    }

                }
                catch (Exception ex2)
                {
                    Console.WriteLine();
                }
            }
        }

    }




}
