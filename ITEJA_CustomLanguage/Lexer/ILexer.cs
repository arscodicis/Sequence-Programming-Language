using System.Collections.Generic;

namespace ITEJA_CustomLanguage.Lexer
{
    /// <summary>
    /// Analyzes source code and makes tokens out of all keywords
    /// </summary>
    public interface ILexerCreator
    {
        /// <summary>
        /// Returns all lexems that are in a source file
        /// </summary>
        /// <returns>List of lexems</returns>
        IEnumerable<string> GetFoundLexems();
        /// <summary>
        /// Returns all tokens that are in a source file
        /// </summary>
        /// <returns>List of tokens</returns>
        IEnumerable<Token> GetFoundTokens();
    }
}
