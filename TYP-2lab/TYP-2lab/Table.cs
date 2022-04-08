using System;
using System.Collections.Generic;

namespace TYP_2lab
{
    // структура, описывающая лексему

    public class Table
    {

        public struct Token
        {
            public Token(int numTable, int numSymbol)
            {
                this.numTable = numTable;
                this.numSymbol = numSymbol;
            }

            public int numTable;
            public int numSymbol;
        };

        private readonly List<string> TableSeveredWord = new List<string>()
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
        private readonly List<string> TableRazdeliteli = new List<string>()
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
            {"\n"}
        };

        public List<string> TableDigit = new List<string>();
        public List<string> TableInfdificate = new List<string>();
        public List<Token> Lexemes = new List<Token>();

        public List<string> ItemValuesTableSeveredWord()
        {
            return TableSeveredWord;
        }
        public List<string> ItemTableRazdeliteli()
        {
            return TableRazdeliteli;
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