using ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks;
using ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses.BodyStatements;
using ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses.Variables;
using ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses.Variables.CalculateInteger;
using ITEJA_CustomLanguage.Lexer;
using System.Collections.Generic;
using System.IO;

namespace ITEJA_CustomLanguage.AbstractSyntaxTree
{
    /// <summary>
    /// Creates a condition and evaluates it
    /// </summary>
    public class Condition : ICondition
    {
        private Stack<Token> expressionTokens;
        private IBodyStatement parent;
        private string comparisonOperator = "";
        private double leftResult;
        private double rightResult;

        /// <summary>
        /// Parses expression that was given. Splits left and right expressions and operators too.
        /// </summary>
        private void ParseExpression()
        {
            Stack<Token> leftSide = new Stack<Token>();
            Stack<Token> rightSide = new Stack<Token>();
            IList<Token> comparisonOperators = new List<Token>();
            bool isLeftSide = true;
            while (expressionTokens.Count != 0)
            {
                Token newToken = expressionTokens.Pop();
                if (IsComparsionOperatorFound(newToken))
                {
                    comparisonOperators.Add(newToken);
                    isLeftSide = false;
                }
                else if (isLeftSide)
                {
                    if (newToken.Type == TokenType.Identifier)
                    {
                        ChangeTokenData(newToken);
                    }
                    leftSide.Push(newToken);
                }
                else
                {
                    if (newToken.Type == TokenType.Identifier)
                    {
                        ChangeTokenData(newToken);

                    }
                    rightSide.Push(newToken);
                }
            }
            Expression exprLeft = new Expression(leftSide);
            leftResult = exprLeft.Calculate();

            Expression exprRight = new Expression(rightSide);
            rightResult = exprRight.Calculate();

            foreach (var oper in comparisonOperators)
            {
                comparisonOperator += oper.Value.ToString();
            }
        }
        /// <summary>
        /// Replaces an identifier token with a number or string token type so that it can be used for calculations.
        /// </summary>
        /// <param name="newToken">Token that needs to be changed.</param>
        private void ChangeTokenData(Token newToken)
        {
            IVariable existingVariable = MainClass.FindIdentifier(parent, newToken.Value.ToString());
            if (existingVariable is IIntegerVariable)
            {
                newToken.Type = TokenType.NumberCharacters;
            }
            else
            {
                newToken.Type = TokenType.StringCharacters;
            }
            newToken.Value = existingVariable.Value.ToString();
        }
        /// <summary>
        /// Evaluates the expression tokens given.
        /// </summary>
        /// <param name="parExpressionTokens">Stack of all tokens in expression.</param>
        /// <param name="parParent">BodyStatement of the Parent that this statement is nested to.</param>
        /// <returns>true or false depending on the evaluation.</returns>
        public bool IsConditionTrue(Stack<Token> parExpressionTokens, IBodyStatement parParent)
        {
            expressionTokens = new Stack<Token>(parExpressionTokens);
            parent = parParent;
            ParseExpression();
            return GetResultOfCondition();
        }
        /// <summary>
        /// Gives a final result of the comparison.
        /// </summary>
        /// <returns>true or false depending on the expressions.</returns>
        private bool GetResultOfCondition()
        {
            return comparisonOperator switch
            {
                "<" => leftResult < rightResult,
                ">" => leftResult > rightResult,
                "<=" => leftResult <= rightResult,
                ">=" => leftResult >= rightResult,
                "==" => leftResult == rightResult,
                "!=" => leftResult != rightResult,
                _ => throw new InvalidDataException("Invalid operator encountered.")
            };
        }
        /// <summary>
        /// Checks whether it contains operator token
        /// </summary>
        /// <param name="token"></param>
        /// <returns>true or false</returns>
        private bool IsComparsionOperatorFound(Token token)
        {
            return token.Type == TokenType.LessThan || token.Type == TokenType.HigherThan
                || token.Type == TokenType.ExclMark || token.Type == TokenType.Equals;
        }
    }
}
