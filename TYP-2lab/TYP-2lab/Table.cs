using System.Collections.Generic;
using System.Linq;

namespace TYP_2lab
{
    public class Table
    {
        public struct Token
        {
            public Token(int numTable, int numSymbol)
            {
                this.NumTable = numTable;
                this.NumSymbol = numSymbol;
            }

            public int NumTable;
            public int NumSymbol;
        };

        public struct TokenType
        {
            public TokenType(object item, object type)
            {
                this.Item = item;
                this.Type = type;
            }

            public object Item;
            public object Type;
        }

        private readonly List<string> _tableSeveredWord = new()
        {
            {"program"},
            {"var"},
            {"begin"},
            {"end"},
            {"true"},
            {"false"},
            {"int"},
            {"float"},
            {"bool"},
            {"as"},
            {"if"},
            {"then"},
            {"else"},
            {"for"},
            {"to"},
            {"do"},
            {"while"},
            {"write"},
            {"read"},
            {"NE"},
            {"EQ"},
            {"LT"},
            {"LE"},
            {"GT"},
            {"GE"},
            {"or"},
            {"plus"},
            {"min"},
            {"and"},
            {"mult"},
            {"div"}
        };
        private readonly List<string> _tableRazdeliteli = new()
        {
            {"("},
            {")"},
            {"{"},
            {"}"},
            {"["},
            {"]"},
            {":"},
            {";"},
            {","},
            {"."},
            {"\r"},
            {"\n"},
            {"!"},
            {"!F"},
            {"W"},
            {"R"}
        };

        public List<string> TableDigit = new();
        public List<string> TableInfdificate = new();

        public List<Token> Lexemes = new();

        public List<TokenType> DigitTypes = new();
        public List<TokenType> InfdificateType = new();

        public List<string> ItemValuesTableSeveredWord()
        {
            return _tableSeveredWord;
        }
        public List<string> ItemTableRazdeliteli()
        {
            return _tableRazdeliteli;
        }

        public List<string> ItemTableIdenType()
        {
            return InfdificateType.ToArray().Select(x => x.Item.ToString()).ToList();
        }
        public string[] ItemTableDigit()
        {
            return TableDigit.ToArray();
        }
        public string[] ItemTableIndificate()
        {
            return TableInfdificate.ToArray();
        }
    }
}