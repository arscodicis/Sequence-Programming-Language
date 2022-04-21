using EncryptStringSample;
using ITEJA_CustomLanguage.AbstractSyntaxTree;
using ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks;
using ITEJA_CustomLanguage.Lexer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace ITEJA_CustomLanguage
{
    class Program
    {
        
        static void Main(string[] args)
        {
            

            string pathToFile = "";

            for (int i = 0; i < args.Length; i++)
            {
                //Console.WriteLine(args[i]);
                pathToFile = args[i];
                //Console.WriteLine("Path to file: " + pathToFile);
                

                //File.Delete(pathToFile + ".preprocess");
                File.WriteAllText(pathToFile + ".preprocess", "");
                //Console.WriteLine("Path to file: (preprocess) " + pathToFile + ".preprocess");

                IEnumerable<string> lines = File.ReadLines(pathToFile);

                if (pathToFile.EndsWith(".seq") || pathToFile.EndsWith(".vpp"))
                {

                    
                    //Console.WriteLine(String.Join(Environment.NewLine, lines)

                    foreach (var line in lines)
                    {
                        //Console.WriteLine(line);

                        //Console.WriteLine(line);

                        if (line.Contains("#!") == true)
                        {
                            
                        }
                        else
                        {

                            //Console.WriteLine(line);
                            File.AppendAllText(pathToFile + ".preprocess", line + "\n");
                            
                        }

                    }

                    //IEnumerable<string> lines = File.ReadLines(pathToFile + ".preproccess");

                    //Console.WriteLine(pathToFile + ".preprocess");

                    Thread.Sleep(2000);

                    string sourceCode = File.ReadAllText(pathToFile + ".preprocess"); // change files
                    //Console.WriteLine("Source code: " + sourceCode);

                   

                    sourceCode = sourceCode.Replace("\r\n", string.Empty).Replace("\t", string.Empty);

                    //Console.WriteLine(sourceCode);

                    ILexerCreator lexer = new LexerCreator(sourceCode);

                    //PrintLexems(lexer);
                    //PrintTokens(lexer);

                    string encryptedstring = StringCipher.Encrypt(sourceCode, "BIN");

                    File.WriteAllText(pathToFile.Replace(".seq", "") + ".bin", encryptedstring);

                    try
                    {
                        SyntaxTreeCreator ast = new SyntaxTreeCreator(lexer.GetFoundTokens());

                        MainClass.Run();
                    } catch(Exception ex)
                    {
                        Console.WriteLine("An internal error occured. Please check your code for errors.");
                    }
                    

                    File.Delete(pathToFile + ".preprocess");
                    Console.Write("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Incorrect file extension.");
                    
                }


            }
        }
        /// <summary>
        /// Prints Lexems to the console.
        /// </summary>
        /// <param name="lexer">Source lexer</param>
        static void PrintLexems(ILexerCreator lexer)
        {
            foreach (var item in lexer.GetFoundLexems())
            {
                Console.WriteLine(item);
            }
        }
        /// <summary>
        /// Prints Tokens to the console
        /// </summary>
        /// <param name="lexer">Source lexer</param>
        static void PrintTokens(ILexerCreator lexer)
        {
            foreach (var item in lexer.GetFoundTokens())
            {
                Console.WriteLine($"{item.Value}\t{item.Type}");
            }
        }
    }
}
