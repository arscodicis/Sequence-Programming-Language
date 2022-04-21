using ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks;
using ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses;
using ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses.BodyStatements;
using ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses.BodyStatements.Classes;
using ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses.Variables;
using ITEJA_CustomLanguage.Lexer;
using System;
using System.Collections.Generic;

namespace ITEJA_CustomLanguage.AbstractSyntaxTree
{
    /// <summary>
    /// AST creates a syntax tree, parses tokens to their own
    /// classes and fills them up with data needed
    /// </summary>
    public class SyntaxTreeCreator
    {
        private readonly Stack<Token> tokenStack;
        private readonly Stack<IBodyStatement> parentBodyStatements = new Stack<IBodyStatement>();
        public SyntaxTreeCreator(IEnumerable<Token> tokens)
        {
            tokenStack = new Stack<Token>(new Stack<Token>(tokens));
            CreateSyntaxTree();
        }
        /// <summary>
        /// Parses tokens from the tokenStack according to the tokentype
        /// </summary>
        private void CreateSyntaxTree()
        {
            while (tokenStack.Count != 0)
            {
                Token newToken = tokenStack.Pop();
                if (newToken.Type == TokenType.IntegerDataType)
                {
                    IIntegerVariable integerVariable = CreateIntegerVariable();
                    DetermineGlobalOrLocalVariable(integerVariable);
                }
                else if (newToken.Type == TokenType.StringDataType)
                {
                    IStringVariable stringVariable = CreateStringVariable();
                    DetermineGlobalOrLocalVariable(stringVariable);
                }
                else if (IsMethodFound(newToken))
                {
                    CreateMethod(newToken);
                }
                else if (newToken.Type == TokenType.PrintLn)
                {
                    CreatePrintStatement();
                }
                else if (newToken.Type == TokenType.Identifier)
                {
                    CreateRedefinitionStatement(newToken);
                }
                else if (newToken.Type == TokenType.If)
                {
                    CreateConditionStatement();
                }
                else if (newToken.Type == TokenType.Run)
                {
                    CreateRunMethodStatement();
                }
                else if (IsElseStatementFound(newToken))
                {
                    CreateElseStatement();
                }
                else if (IsEndOfBodyStatement(newToken))
                {
                    parentBodyStatements.Pop();
                }
                else if (newToken.Type == TokenType.Forcycle)
                {
                    CreateForCycleStatement();
                }
                else if (newToken.Type == TokenType.TestSyntax)
                {
                    TestSyntax();
                }
                else if (newToken.Type == TokenType.Input)
                {
                    InputSyntax();
                }
                else if (newToken.Type == TokenType.Print)
                {
                    CreatePrintNoNewLineStatement();
                }
            }
        }

        /// <summary>
        /// Creates a InputSyntax
        /// </summary>
        private void InputSyntax()
        {
            InputStatment inputStatment = new InputStatment();
            //CheckAndRemoveLeftParenthesis();
            CheckAndRemoveEqualsSign();
            if (tokenStack.Peek().Type == TokenType.Identifier)
            {
                inputStatment.Variable = MainClass.FindIdentifier(parentBodyStatements.Peek(), tokenStack.Pop().Value);
            }
           

            parentBodyStatements.Peek().Statements.Add(inputStatment);
            //Console.WriteLine("TestSyntax!");
        }

        /// <summary>
        /// Creates a TestSyntax
        /// </summary>
        private void TestSyntax()
        {
            TestSyntaxStatement testSyntaxStatement = new TestSyntaxStatement();
            //CheckAndRemoveLeftParenthesis();
            parentBodyStatements.Peek().Statements.Add(testSyntaxStatement);
            //Console.WriteLine("TestSyntax!");
        }
        /// <summary>
        /// Creates ForCycle Statement
        /// </summary>
        private void CreateForCycleStatement()
        {
            IForCycleStatement forCycle = new ForCycleStatement();
            FillConditionParametersCycle(forCycle);
            forCycle.Parent = parentBodyStatements.Peek();
            parentBodyStatements.Peek().Statements.Add(forCycle);
            parentBodyStatements.Push(forCycle);
        }
        /// <summary>
        /// Retrieves condition for the forcycle to end
        /// which is inner counter, max allowed counter and 
        /// if it is decremental or incremental loop.
        /// </summary>
        /// <param name="forCycle">Loop that will be given parameters.</param>
        private void FillConditionParametersCycle(IForCycleStatement forCycle)
        {
            CheckAndRemoveLeftParenthesis();
            CheckAndRemoveDataTypeInForCycle();

            FindInnerCounterValue(forCycle);
            FindComparisonOperators(forCycle);
            FindMaxCounterValue(forCycle);
            FindIncrementOrDecrement(forCycle);
        }
        /// <summary>
        /// Retrieves wheter the loop is incremental or decremental
        /// according to whether it's variable++ or variable--.
        /// </summary>
        /// <param name="forCycle">Loop that will be given parameters.</param>
        private void FindIncrementOrDecrement(IForCycleStatement forCycle)
        {
            Token newToken;
            while ((newToken = tokenStack.Pop()).Type != TokenType.RightParenthesis)
            {
                if (newToken.Type == TokenType.Plus && tokenStack.Peek().Type == TokenType.Plus)
                {
                    forCycle.IsIncremental = true;
                    tokenStack.Pop();
                    return;
                }
                if (newToken.Type == TokenType.Minus && tokenStack.Peek().Type == TokenType.Minus)
                {
                    forCycle.IsIncremental = false;
                    tokenStack.Pop();
                    return;
                }
            }
            throw new ArgumentException("Invalid increment or decrement of value, make sure forcycle syntax is correct.");
        }
        /// <summary>
        /// Retrieves maximum allowed of loops.
        /// </summary>
        /// <param name="forCycle">Loop that will be given the parameters.</param>
        private void FindMaxCounterValue(IForCycleStatement forCycle)
        {
            Token newToken;
            IIntegerVariable maxCounter = new IntegerVariable();

            while ((newToken = tokenStack.Pop()).Type != TokenType.Comma)
            {
                maxCounter.TokensExpression.Push(newToken);
            }
            maxCounter.Calculate();
            forCycle.MaximumAllowedCounter = maxCounter;
        }
        /// <summary>
        /// Retrieves comparison operators from the condtion in the loop.
        /// </summary>
        /// <param name="forCycle">Loop that will be given the parameters.</param>
        private void FindComparisonOperators(IForCycleStatement forCycle)
        {
            Token newToken;
            while ((newToken = tokenStack.Peek()).Type != TokenType.NumberCharacters)
            {
                if (IsComparisonOperator(newToken))
                {
                    forCycle.ComparisonOperators.Add(newToken);
                }
                tokenStack.Pop();
            }
        }
        /// <summary>
        /// Retrieves inner counter value that will be incremented
        /// after each loop
        /// </summary>
        /// <param name="forCycle">Loop that will be given the parameters.</param>
        private void FindInnerCounterValue(IForCycleStatement forCycle)
        {
            IIntegerVariable innerCounter = new IntegerVariable();

            innerCounter.Name.Append(tokenStack.Pop().Value);

            CheckAndRemoveEqualsSign();
            Token newToken;
            while ((newToken = tokenStack.Pop()).Type != TokenType.Comma)
            {
                innerCounter.TokensExpression.Push(newToken);
            }
            innerCounter.Calculate();
            forCycle.InnerCounterVariable = innerCounter;
            forCycle.LocalVariables.Add(innerCounter);
        }
        /// <summary>
        /// Removes the needed "integer" before the variable declaration.
        /// </summary>
        private void CheckAndRemoveDataTypeInForCycle()
        {
            if (tokenStack.Peek().Type != TokenType.IntegerDataType)
            {
                throw new ArgumentException("Missing data type or wrong data type in forcycle declaration.");
            }
            tokenStack.Pop();
        }
        /// <summary>
        /// Checks whether the TokenType is comparison operator.
        /// </summary>
        /// <param name="newToken">Token that will be checked.</param>
        /// <returns>true or false depending on the type.</returns>
        private bool IsComparisonOperator(Token newToken)
        {
            return newToken.Type == TokenType.LessThan || newToken.Type == TokenType.HigherThan
                || newToken.Type == TokenType.Equals || newToken.Type == TokenType.ExclMark;
        }
        /// <summary>
        /// Checks whether a body statement reached an end by looking
        /// for '}' token type
        /// </summary>
        /// <param name="newToken">Token that will be checked.</param>
        /// <returns>true or false dpeending on the type.</returns>
        private bool IsEndOfBodyStatement(Token newToken)
        {
            return newToken.Type == TokenType.RightBracket;
        }
        /// <summary>
        /// Creates else statement taht will be connected
        /// to the initial if statement
        /// </summary>
        private void CreateElseStatement()
        {
            IConditionIfStatement elseCondition = new ConditionIfStatement();
            IConditionIfStatement ifCondition = (IConditionIfStatement)parentBodyStatements.Pop();
            ifCondition.ElseCondition = elseCondition;
            elseCondition.Parent = parentBodyStatements.Peek();
            parentBodyStatements.Push(elseCondition);
        }
        /// <summary>
        /// Checks whether the last insterted body statement was condition,
        /// whether the current token type is RightBracket and if the next one 
        /// is Else TokenType
        /// </summary>
        /// <param name="newToken">Token that will be checked.</param>
        /// <returns>true or false depending on type.</returns>
        private bool IsElseStatementFound(Token newToken)
        {
            if (parentBodyStatements.Peek() is IConditionIfStatement)
            {
                if (newToken.Type == TokenType.RightBracket)
                {
                    if (tokenStack.Peek().Type == TokenType.Else)
                    {
                        tokenStack.Pop();
                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// Creates a method call statement.
        /// </summary>
        private void CreateRunMethodStatement()
        {
            IRunStatement runStatement = new RunStatement();
            if (tokenStack.Peek().Type == TokenType.Identifier)
            {
                runStatement.Name.Append(tokenStack.Pop().Value);
            }
            else
            {
                throw new ArgumentException("Identifier for method name missing!");
            }
            CheckAndRemoveLeftParenthesis();
            CreateInputParameters(runStatement);
            parentBodyStatements.Peek().Statements.Add(runStatement);
        }
        /// <summary>
        /// Retrieves all input parameters for the future method call.
        /// </summary>
        /// <param name="runStatement">Run statement that will be given the parameters.</param>
        private void CreateInputParameters(IRunStatement runStatement)
        {
            Token newToken;
            while ((newToken = tokenStack.Peek()).Type != TokenType.Semicolon)
            {
                if (newToken.Type == TokenType.Comma)
                {
                    tokenStack.Pop();
                }
                else if (newToken.Type == TokenType.NumberCharacters)
                {
                    runStatement.TokenParametersList.Add(CreateIntegerVariable());
                }
                else if (newToken.Type == TokenType.StringCharacters)
                {
                    runStatement.TokenParametersList.Add(CreateStringVariable());
                }
                else if (newToken.Type == TokenType.Identifier)
                {
                    runStatement.TokenParametersList.Add(MainClass.FindIdentifier(parentBodyStatements.Peek(), newToken.Value));
                    tokenStack.Pop();
                }
                else
                {
                    tokenStack.Pop();
                }
            }
        }
        /// <summary>
        /// Creates a condition statement.
        /// </summary>
        private void CreateConditionStatement()
        {
            IConditionIfStatement conditionStatement = new ConditionIfStatement();
            CreateConditionTokens(conditionStatement);
            conditionStatement.Parent = parentBodyStatements.Peek();
            parentBodyStatements.Peek().Statements.Add(conditionStatement);
            parentBodyStatements.Push(conditionStatement);
        }
        /// <summary>
        /// Creates a stack of all tokens that are in the expression and later evaluates
        /// </summary>
        /// <param name="condition">Condition that will be given the parameters.</param>
        private void CreateConditionTokens(IConditionIfStatement condition)
        {
            CheckAndRemoveLeftParenthesis();
            Token newToken;
            while ((newToken = tokenStack.Pop()).Type != TokenType.RightParenthesis)
            {
                condition.ExpressionTokens.Push(newToken);
            }
        }
        /// <summary>
        /// Checks whether the following token is of type left parenthesis,
        /// if so, it will delete it from the stack, if not, the exception is thrown
        /// </summary>
        private void CheckAndRemoveLeftParenthesis()
        {
            if (tokenStack.Peek().Type == TokenType.LeftParenthesis)
            {
                tokenStack.Pop();
            }
            else
            {
                throw new ArgumentException("Left parenthesis is missing");
            }
        }
        /// <summary>
        /// Creates a print line statement.
        /// </summary>
        private void CreatePrintStatement()
        {
            CheckAndRemoveLeftParenthesis();
            PrintLnStatement printStatement = new PrintLnStatement
            {
                Parent = parentBodyStatements.Peek()
            };
            if (tokenStack.Peek().Type == TokenType.StringCharacters)
            {
                printStatement.Variable = CreateStringVariable();
            }
            else if (tokenStack.Peek().Type == TokenType.NumberCharacters)
            {
                printStatement.Variable = CreateIntegerVariable();
            }
            else if (tokenStack.Peek().Type == TokenType.Identifier)
            {
                printStatement.Variable = MainClass.FindIdentifier(parentBodyStatements.Peek(), tokenStack.Pop().Value);
            }
            parentBodyStatements.Peek().Statements.Add(printStatement);
        }

        /// <summary>
        /// Creates a print line statement.
        /// </summary>
        private void CreatePrintNoNewLineStatement()
        {
            CheckAndRemoveLeftParenthesis();
            PrintStatement printStatement = new PrintStatement
            {
                Parent = parentBodyStatements.Peek()
            };
            if (tokenStack.Peek().Type == TokenType.StringCharacters)
            {
                printStatement.Variable = CreateStringVariable();
            }
            else if (tokenStack.Peek().Type == TokenType.NumberCharacters)
            {
                printStatement.Variable = CreateIntegerVariable();
            }
            else if (tokenStack.Peek().Type == TokenType.Identifier)
            {
                printStatement.Variable = MainClass.FindIdentifier(parentBodyStatements.Peek(), tokenStack.Pop().Value);
            }
            parentBodyStatements.Peek().Statements.Add(printStatement);
        }
        /// <summary>
        /// Creates a redefinition statement.
        /// </summary>
        /// <param name="newToken">Token that will be used for finding
        /// the target variable.</param>
        private void CreateRedefinitionStatement(Token newToken)
        {
            IRedefinitionStatement redefStatement = new RedefinitionStatement
            {
                IdentifierToken = newToken,
                Parent = parentBodyStatements.Peek()
            };
            CheckAndRemoveEqualsSign();
            RedefinitionReadTokens(redefStatement);
            parentBodyStatements.Peek().Statements.Add(redefStatement);
        }
        /// <summary>
        /// Retrieves the expresion that should be used for creating a new value
        /// for variable.
        /// </summary>
        /// <param name="redefStatement">Redefinition statement that will be
        /// given the parameters.</param>
        private void RedefinitionReadTokens(IRedefinitionStatement redefStatement)
        {
            Token expressionToken;
            while ((expressionToken = tokenStack.Pop()).Type != TokenType.Semicolon)
            {
                redefStatement.TokensExpression.Push(expressionToken);
            }
        }
        /// <summary>
        /// Creates a method and adds them to the MainClass list of all methods.
        /// </summary>
        /// <param name="newToken">Token that will be used to create method.</param>
        private void CreateMethod(Token newToken)
        {
            IMethod newMethod = new Method();
            newMethod.Name.Append(newToken.Value);

            CreateMethodParameters(newMethod);
            parentBodyStatements.Push(newMethod);
            MainClass.Methods.Add(newMethod);
        }
        /// <summary>
        /// Retrieves the method parameters that will be used for calling this method.
        /// Also puts them to the local variable list.
        /// </summary>
        /// <param name="newMethod">Method that will be given the parameters.</param>
        private void CreateMethodParameters(IMethod newMethod)
        {
            Token newToken;
            while ((newToken = tokenStack.Pop()).Type != TokenType.LeftBracket)
            {
                if (newToken.Type == TokenType.StringDataType)
                {
                    IStringVariable stringVariable = CreateStringVariable();
                    newMethod.ParametersList.Add(stringVariable);
                    newMethod.LocalVariables.Add(stringVariable);
                }
                else if (newToken.Type == TokenType.IntegerDataType)
                {
                    IIntegerVariable integerVariable = CreateIntegerVariable();
                    newMethod.ParametersList.Add(integerVariable);
                    newMethod.LocalVariables.Add(integerVariable);
                }
            }
        }
        /// <summary>
        /// Checks whether the method was found, basically if the token 
        /// is identifier and next one is left parenthesis
        /// </summary>
        /// <param name="token">The token that will be checked.</param>
        /// <returns>true or false depending on the type of token.</returns>
        private bool IsMethodFound(Token token)
        {
            return (token.Type == TokenType.Identifier || token.Type == TokenType.Program) && tokenStack.Peek().Type == TokenType.LeftParenthesis;
        }
        /// <summary>
        /// Creates a string variable
        /// </summary>
        /// <returns>The string variable created.</returns>
        private IStringVariable CreateStringVariable()
        {
            IStringVariable stringVariable = new StringVariable();
            if (tokenStack.Peek().Type == TokenType.Identifier)
            {
                stringVariable.Name.Append(tokenStack.Pop().Value);
            }
            CreateVariableDefinition(stringVariable);

            return stringVariable;
        }
        /// <summary>
        /// Creates an integer variable.
        /// </summary>
        /// <returns>The integer variable created.</returns>
        private IIntegerVariable CreateIntegerVariable()
        {
            IIntegerVariable integerVariable = new IntegerVariable();
            if (tokenStack.Peek().Type == TokenType.Identifier)
            {
                integerVariable.Name.Append(tokenStack.Pop().Value);
            }
            CreateVariableDefinition(integerVariable);

            return integerVariable;
        }
        /// <summary>
        /// Determines whether the variable should be in the global namespace
        /// or local namespace. If there is no parent as bodystatement, it is global
        /// </summary>
        /// <param name="variable">Variable that will be put into
        /// the list of variables.</param>
        private void DetermineGlobalOrLocalVariable(IVariable variable)
        {
            if (parentBodyStatements.Count == 0)
            {
                MainClass.GlobalVariables.Add(variable);
            }
            else
            {
                parentBodyStatements.Peek().LocalVariables.Add(variable);
            }
        }
        /// <summary>
        /// Retrieves the definition of a variable so that
        /// it can be later used to recreate the final value.
        /// </summary>
        /// <param name="variable">Variable that will be given the definition.</param>
        private void CreateVariableDefinition(IVariable variable)
        {
            CheckAndRemoveEqualsSign();
            Token newToken;
            while ((newToken = tokenStack.Pop()).Type != TokenType.Semicolon && newToken.Type != TokenType.Comma && newToken.Type != TokenType.RightParenthesis)
            {
                if (variable is IIntegerVariable integerVariable)
                {
                    integerVariable.TokensExpression.Push(newToken);
                }
                else if (variable is IStringVariable stringVariable)
                {
                    if (newToken.Type == TokenType.Plus)
                    {
                        continue;
                    }
                    stringVariable.Value.Append(newToken.Value);
                }
            }
            if (variable is IIntegerVariable intVariable)
            {
                intVariable.Calculate();
            }
        }
        /// <summary>
        /// Checks whether an equals sign is
        /// present and removes it from the stack.
        /// </summary>
        private void CheckAndRemoveEqualsSign()
        {
            if (tokenStack.Peek().Type == TokenType.Equals)
            {
                tokenStack.Pop();
            }
        }
    }
}
