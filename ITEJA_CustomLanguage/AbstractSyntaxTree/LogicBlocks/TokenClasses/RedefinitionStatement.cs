using ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses.BodyStatements;
using ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses.Variables;
using ITEJA_CustomLanguage.Lexer;
using System.Linq;
using System.Collections.Generic;
using System;

namespace ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses
{
    /// <summary>
    /// Changes a value of the already existing variable
    /// </summary>
    class RedefinitionStatement : IRedefinitionStatement
    {
        /// <summary>
        /// BodyStatement parent of this statement.
        /// </summary>
        public IBodyStatement Parent { get; set; }
        /// <summary>
        /// Value of the token that will be used to
        /// find the variable
        /// </summary>
        public Token IdentifierToken { get; set; }
        /// <summary>
        /// Expression that will be evaluated and calculated
        /// to the final value
        /// </summary>
        public Stack<Token> TokensExpression { get; set; } = new Stack<Token>();
        /// <summary>
        /// Executes a redefinition and changes the value of the variable.
        /// </summary>
        public void Execute()
        {
            IVariable variable = MainClass.FindIdentifier(Parent, IdentifierToken.Value);
            if (variable is IIntegerVariable integerVariable)
            {
                if (TokensExpression.Any(x=>x.Type == TokenType.StringCharacters))
                {
                    throw new InvalidOperationException("String cannot be put into integer datatype!");
                }
                integerVariable.TokensExpression = MainClass.ReplaceIdentifiersInExpression(TokensExpression, Parent);
                integerVariable.Calculate();
            }
            else if (variable is IStringVariable stringVariable)
            {
                if (TokensExpression.Any(x=>x.Type == TokenType.NumberCharacters))
                {
                    throw new InvalidOperationException("Integer cannot be put into string datatype!");
                }
                UpdateStringVariableDefinition(stringVariable);
            }
        }
        /// <summary>
        /// Updates string variable
        /// </summary>
        /// <param name="stringVariable">String variable that should be updated.</param>
        private void UpdateStringVariableDefinition(IStringVariable stringVariable)
        {
            stringVariable.Value.Clear();
            var replacedExpressions = MainClass.ReplaceIdentifiersInExpression(TokensExpression, Parent);
            foreach (var str in replacedExpressions)
            {
                if (str.Type != TokenType.Plus)
                {
                    stringVariable.Value.Append(str.Value);
                }
            }
        }
    }
}
