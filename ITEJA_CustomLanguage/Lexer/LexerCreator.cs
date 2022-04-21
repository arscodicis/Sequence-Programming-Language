using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace ITEJA_CustomLanguage.Lexer
{
    /// <summary>
    /// Analyzes source code and makes tokens out of all keywords
    /// </summary>
    public class LexerCreator : ILexerCreator
    {
        /// <summary>
        /// Dictionary with all keywords of the programming language V++
        /// </summary>
        private static readonly IDictionary<string, TokenType> keyWords = new Dictionary<string, TokenType>
        {
            {"integer", TokenType.IntegerDataType},
            {"string", TokenType.StringDataType},
            {"if", TokenType.If},
            {"else", TokenType.Else},
            {"forcycle", TokenType.Forcycle},
            {"run", TokenType.Run},
            {"program", TokenType.Program},
            {"println", TokenType.PrintLn},
            {"testsyntax", TokenType.TestSyntax},
            {"input", TokenType.Input}
        };
        /// <summary>
        /// Dictionary with all operators and punctators 
        /// that will be used throught the program.
        /// </summary>
        private static readonly IDictionary<char, TokenType> operatorsAndPunctators = new Dictionary<char, TokenType>
        {
            {'+', TokenType.Plus},
            {'-', TokenType.Minus},
            {'*', TokenType.Multiply},
            {'/', TokenType.Divide},
            {';', TokenType.Semicolon},
            {'(', TokenType.LeftParenthesis},
            {')', TokenType.RightParenthesis},
            {'{', TokenType.LeftBracket},
            {'}', TokenType.RightBracket},
            {'=', TokenType.Equals},
            {',', TokenType.Comma},
            {'<', TokenType.LessThan},
            {'>', TokenType.HigherThan},
            {'!', TokenType.ExclMark}
        };
        private readonly IList<Token> tokensList = new List<Token>();
        private readonly StringReader reader;
        public LexerCreator(string sourceCode)
        {
            reader = new StringReader(sourceCode);
            FindTokens();
        }
        /// <summary>
        /// Returns an enumerable collection containing all lexems.
        /// </summary>
        /// <returns>Enumerable collection of lexems.</returns>
        public IEnumerable<string> GetFoundLexems()
        {
            if (tokensList.Count == 0)
            {
                throw new InvalidDataException("No tokens were found!");
            }
            return tokensList.Select(x => x.Value);
        }
        /// <summary>
        /// Returns an enumerable collection containing all tokens.
        /// </summary>
        /// <returns>Enumerable collection of tokens.</returns>
        public IEnumerable<Token> GetFoundTokens()
        {
            if (tokensList.Count == 0)
            {
                throw new InvalidDataException("No tokens were found!");
            }
            return tokensList;
        }
        /// <summary>
        /// Creates tokens from the source code
        /// </summary>
        private void FindTokens()
        {
            char character;
            while (!(character = (char)reader.Read()).Equals('\uffff'))
            {
                if (operatorsAndPunctators.ContainsKey(character))
                {
                    CreateOperatorAndPunctatorToken(character);
                }
                else if (character.Equals('"'))
                {
                    CreateStringToken(character);
                }
                else if (char.IsLetter(character))
                {
                    CheckCharacterMeaning(character);
                }
                else if (char.IsDigit(character))
                {
                    CreateIntegerToken(character);
                }
            }
        }

        
        /// <summary>
        /// Creates an integer token.
        /// </summary>
        /// <param name="character">Initial character</param>
        /// 
        /// <summary>
        /// Creates a string token.
        /// </summary>
        /// <param name="character">Initial character.</param>
        private void CreateStringToken(char character)
        {
            StringBuilder stringBuilder = new StringBuilder();
            while (!(character = (char)reader.Read()).Equals('"'))
            {
                stringBuilder.Append(character);
            }
            AddToken(TokenType.StringCharacters, stringBuilder.ToString());
        }
        /// <summary>
        /// Creates an integer token.
        /// </summary>
        /// <param name="character">Initial character</param>
        private void CreateIntegerToken(char character)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(character);
            while (!(character = (char)reader.Peek()).Equals(',') && !char.IsWhiteSpace(character) && !character.Equals(';') && !character.Equals(')'))
            {
                stringBuilder.Append(character);
                reader.Read();
            }
            AddToken(TokenType.NumberCharacters, stringBuilder.ToString());
        }
        /// <summary>
        /// Checks whether the character is for a variable or if it's a keyword.
        /// </summary>
        /// <param name="character">Initial character</param>
        private void CheckCharacterMeaning(char character)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(character);
            while (char.IsLetterOrDigit(character = (char)reader.Peek()))
            {
                stringBuilder.Append(character);
                reader.Read();
            }

            if (keyWords.ContainsKey(stringBuilder.ToString()))
            {
                AddToken(keyWords[stringBuilder.ToString()], stringBuilder.ToString());
            }
            else
            {
                AddToken(TokenType.Identifier, stringBuilder.ToString());
            }
        }
        /// <summary>
        /// Creates an operator or punctator token.
        /// </summary>
        /// <param name="character">Initial character</param>
        private void CreateOperatorAndPunctatorToken(char character)
        {
            AddToken(operatorsAndPunctators[character], character.ToString());
        }
        /// <summary>
        /// Adds a token to the stack.
        /// </summary>
        /// <param name="type">Type of the token.</param>
        /// <param name="value">Value of the token.</param>
        private void AddToken(TokenType type, string value)
        {
            tokensList.Add(new Token()
            {
                Type = type,
                Value = value
            });
        }
    }
}
