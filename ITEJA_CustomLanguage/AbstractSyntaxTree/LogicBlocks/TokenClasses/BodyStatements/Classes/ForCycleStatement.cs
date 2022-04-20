using ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses.Variables;
using ITEJA_CustomLanguage.Lexer;
using System.Collections.Generic;

namespace ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses.BodyStatements.Classes
{
    /// <summary>
    /// Interface for forcycle statements in the program
    /// </summary>
    public class ForCycleStatement : IForCycleStatement
    {
        /// <summary>
        /// Keeps a variable in itself that monitors how many
        /// loops have already been done.
        /// </summary>
        public IIntegerVariable InnerCounterVariable { get; set; } = new IntegerVariable();
        /// <summary>
        /// Keeps a variable that has the maximum allowed
        /// number of loops.
        /// </summary>
        public IIntegerVariable MaximumAllowedCounter { get; set; } = new IntegerVariable();
        /// <summary>
        /// List of comparison operators that will be used for evaluating.
        /// </summary>
        public IList<Token> ComparisonOperators { get; set; } = new List<Token>();
        /// <summary>
        /// If the loop has i++ then it's incremental, 
        /// otherwise it's decremental.
        /// </summary>
        public bool IsIncremental { get; set; }
        /// <summary>
        /// Contains list of all statements that will be later on executed.
        /// </summary>
        public IList<IStatement> Statements { get; } = new List<IStatement>();
        /// <summary>
        /// Contains a list of all local variables.
        /// </summary>
        public IList<IVariable> LocalVariables { get; } = new List<IVariable>();
        /// <summary>
        /// Constains a reference to the parent bodystatement.
        /// </summary>
        public IBodyStatement Parent { get; set; }
        private readonly Stack<Token> expressionTokens = new Stack<Token>();
        private ICondition condition;
        /// <summary>
        /// Executes the for loop and initialzies the expression.
        /// </summary>
        public void Execute()
        {
            UpdateExpression();
            CycleStatements();
        }
        /// <summary>
        /// Cycles the statements until the condition is true.
        /// </summary>
        private void CycleStatements()
        {
            condition = new Condition();
            if (condition.IsConditionTrue(expressionTokens, this))
            {
                foreach (var statement in Statements)
                {
                    statement.Execute();
                }
                AddStepToInnerCounterVariable();
                CycleStatements();
            }
        }
        /// <summary>
        /// Increments (or decrements) the inner counter variable.
        /// </summary>
        private void AddStepToInnerCounterVariable()
        {
            if (IsIncremental)
            {
                foreach (var localVariable in LocalVariables)
                {
                    if (localVariable.Name.Equals(InnerCounterVariable.Name.ToString()))
                    {
                        int previousValue = int.Parse(localVariable.Value.ToString());
                        localVariable.Value.Clear();
                        localVariable.Value.Append(previousValue + 1);
                        break;
                    }
                }
            }
            else
            {
                foreach (var localVariable in LocalVariables)
                {
                    if (localVariable.Name.Equals(InnerCounterVariable.Name.ToString()))
                    {
                        int previousValue = int.Parse(localVariable.Value.ToString());
                        localVariable.Value.Clear();
                        localVariable.Value.Append(previousValue - 1);
                        break;
                    }
                }
            }
            UpdateExpression();
        }
        /// <summary>
        /// Updates the expressions so that it has the most
        /// recent results for the counter variable.
        /// </summary>
        private void UpdateExpression()
        {
            Token innerCounterToken = new Token
            {
                Type = TokenType.NumberCharacters,
                Value = InnerCounterVariable.Value.ToString()
            };

            Token maxCounterToken = new Token
            {
                Type = TokenType.NumberCharacters,
                Value = MaximumAllowedCounter.Value.ToString()
            };
            expressionTokens.Clear();
            expressionTokens.Push(innerCounterToken);
            foreach (var oper in ComparisonOperators)
            {
                expressionTokens.Push(oper);
            }
            expressionTokens.Push(maxCounterToken);
        }
    }
}
