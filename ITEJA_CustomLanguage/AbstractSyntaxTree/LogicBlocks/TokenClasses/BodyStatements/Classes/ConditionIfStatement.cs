using ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses.BodyStatements;
using ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses.Variables;
using ITEJA_CustomLanguage.Lexer;
using System.Collections.Generic;

namespace ITEJA_CustomLanguage.AbstractSyntaxTree.LogicBlocks.TokenClasses
{
    /// <summary>
    /// Interface for condition statements in the program.
    /// </summary>
    public class ConditionIfStatement : IConditionIfStatement
    {
        /// <summary>
        /// Contains a list of all statements inside the condition.
        /// </summary>
        public IList<IStatement> Statements { get; } = new List<IStatement>();
        /// <summary>
        /// Contains list local variables inside the condition.
        /// </summary>
        public IList<IVariable> LocalVariables { get; } = new List<IVariable>();
        /// <summary>
        /// Reference to the BodyStatement of the parent.
        /// </summary>
        public IBodyStatement Parent { get; set; }
        /// <summary>
        /// Contains another condition in itself that will
        /// be used as else (can be null)
        /// </summary>
        public IConditionIfStatement ElseCondition { get; set; }
        /// <summary>
        /// Contains an expression that will be used to check and evaluate the condition.
        /// </summary>
        public Stack<Token> ExpressionTokens { get; set; } = new Stack<Token>();
        private ICondition condition;
        /// <summary>
        /// Executes a condition to perform itself and statemnets in it.
        /// </summary>
        public void Execute()
        {
            condition = new Condition();
            if (condition.IsConditionTrue(MainClass.ReplaceIdentifiersInExpression(ExpressionTokens, Parent), Parent))
            {
                foreach (var statement in Statements)
                {
                    statement.Execute();
                }
            }
            else
            {
                if (ElseCondition != null)
                {
                    foreach (var statement in ElseCondition.Statements)
                    {
                        statement.Execute();
                    }
                }
            }
        }
    }
}
