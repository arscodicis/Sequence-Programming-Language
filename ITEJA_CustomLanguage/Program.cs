using EncryptStringSample;
using ITEJA_CustomLanguage.AbstractSyntaxTree;
using ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks;
using ITEJA_CustomLanguage.Lexer;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

                if(args[i] == "--version")
                {
                    Console.WriteLine("Sequence v2.1");
                } else if(args[i] == "--path")
                {
                    Console.WriteLine("");
                }
                else if(args[i] == "--about")
                {
                    Console.WriteLine(@"

                :~!!~^.                
              .:^57^::::.               
              :J!J.                     
              .7YJ7^.                   
               .^!?JJ?7^.               
                   .:!JJ7.              
                      ?77:              
                :::::!5~:.              
                .^~!!!^         
 
");
                    Console.WriteLine("Sequence: a particular order in which related events, movements, or things follow each other");
                    Console.WriteLine("Sequence © 2022 PIRUX, LLC");
                    Console.WriteLine("The vision of Sequence is that programming would be simplified to mere sequence; easy to read, easy to write.");

                }

                else
                {

                    

                    //Console.WriteLine(args[i]);
                    pathToFile = args[i];
                    //Console.WriteLine("Path to file: " + pathToFile);


                    //File.Delete(pathToFile + ".preprocess");
                    File.WriteAllText(pathToFile + ".preprocess", "");
                    File.Delete(pathToFile + ".bin");
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

                        try
                        {
                            string encryptedstring = StringCipher.Encrypt(sourceCode, "BIN");

                            File.WriteAllText(pathToFile.Replace(".seq", "") + ".bin", encryptedstring);
                        } catch(Exception ex)
                        {

                        }

                        

                        try
                        {
                            SyntaxTreeCreator ast = new SyntaxTreeCreator(lexer.GetFoundTokens());

                            MainClass.Run();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }


                        File.Delete(pathToFile + ".preprocess");
                        Console.Write("\n\nPress any key to continue...");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Incorrect file extension.");

                    }


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
