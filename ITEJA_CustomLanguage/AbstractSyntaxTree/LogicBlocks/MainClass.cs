using ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses.BodyStatements;
using ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses.Variables;
using ITEJA_CustomLanguage.Lexer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks
{
    /// <summary>
    /// The main program of this application, interpret
    /// </summary>
    public class MainClass
    {
        /// <summary>
        /// Contains all global variables that were not part of any method.
        /// </summary>
        public static IList<IVariable> GlobalVariables = new List<IVariable>();
        /// <summary>
        /// Contains all methods of the class.
        /// </summary>
        public static IList<IMethod> Methods = new List<IMethod>();

        /// <summary>
        /// The interpret, runs all statements according to their position and executes them.
        /// </summary>
        public static void Run()
        {
            foreach (var method in Methods)
            {
                if (method.Name.Equals("program"))
                {
                    method.Execute();
                    return;
                }
            }
            throw new ArgumentException("The main method could not be found!");
        }

        /// <summary>
        /// Find variable that corresponds to the token value
        /// </summary>
        /// <param name="parent">BodyStatement parent that the statement is nested in.</param>
        /// <param name="identifierName">Value of the token</param>
        /// <returns></returns>
        public static IVariable FindIdentifier(IBodyStatement parent, string identifierName)
        {
            IVariable localVariable = FindInLocalVariables(parent, identifierName);
            IVariable globalVariable = FindInGlobalVariables(identifierName);

            if (localVariable == null && globalVariable == null)
            {
                throw new ArgumentException("The variable doesn't exist in current context.");
            }
            else if (localVariable != null)
            {
                return localVariable;
            }
            else
            {
                return globalVariable;
            }
        }

        /// <summary>
        /// Replaces all identifiers in a given stack
        /// </summary>
        /// <param name="tokens">Stack of tokens that has identifiers</param>
        /// <param name="parent">BodyStatement parent that the statement is nested in.</param>
        /// <returns></returns>
        public static Stack<Token> ReplaceIdentifiersInExpression(Stack<Token> tokens, IBodyStatement parent)
        {
            Stack<Token> tokensExpressionWithoutIdents = new Stack<Token>();
            foreach (var token in tokens)
            {
                if (token.Type == TokenType.Identifier)
                {
                    if (MainClass.FindIdentifier(parent, token.Value) is IIntegerVariable integerIdent)
                    {
                        Token newToken = new Token
                        {
                            Type = TokenType.NumberCharacters,
                            Value = integerIdent.Value.ToString()
                        };
                        tokensExpressionWithoutIdents.Push(newToken);
                    }
                    else if (MainClass.FindIdentifier(parent, token.Value) is IStringVariable stringIdent)
                    {
                        Token newToken = new Token
                        {
                            Type = TokenType.StringCharacters,
                            Value = stringIdent.Value.ToString()
                        };
                        tokensExpressionWithoutIdents.Push(newToken);
                    }
                }
                else
                {
                    tokensExpressionWithoutIdents.Push(token);
                }
            }
            return new Stack<Token>(tokensExpressionWithoutIdents);
        }
        /// <summary>
        /// Linq call to find name of variable in global variables
        /// </summary>
        /// <param name="identifierName">Value of the token</param>
        /// <returns>Returns variable or null if nothing was found.</returns>
        private static IVariable FindInGlobalVariables(string identifierName)
        {
            return GlobalVariables.Select(variable => variable).Where(variable => variable.Name.Equals(identifierName)).FirstOrDefault();
        }

        /// <summary>
        /// Find 
        /// </summary>
        /// <param name="parent">BodyStatement parent that the statement is nested in.</param>
        /// <param name="identifierName">Value of the token</param>
        /// <returns>Returns variable or null if nothing was found.</returns>
        private static IVariable FindInLocalVariables(IBodyStatement parent, string identifierName)
        {
            while (parent != null)
            {
                foreach (var bodyStatementVariable in parent.LocalVariables)
                {
                    if (identifierName.Equals(bodyStatementVariable.Name.ToString()))
                    {
                        return bodyStatementVariable;
                    }
                }
                parent = parent.Parent;
            }
            return null;
        }
    }
}
