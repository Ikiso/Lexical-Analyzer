﻿using System.Collections.Generic;

namespace TYP_2lab
{
    internal class Parser
    {

        public static Table Tables = new();
        public static string ErroreCode = "";
        private static Table.Token _il;
        private static int _i;


        public Parser(Table tables)
        {
            Tables = tables;
        }

        public static void ReadNextElement()
        {
            if (_i < Tables.Lexemes.Count)
            {
                _il = Tables.Lexemes[_i];
                _i++;
            }
        }

        public static void RTheElementTransition()
        {
            if (Compare(_il.NumTable, "\n"))
            {
                while (Compare(_il.NumTable, "\n"))
                {
                    ReadNextElement();
                    if (_i == Tables.Lexemes.Count)
                    {
                        break;
                    }
                }
            }
            else
            {
                ReadNextElement();
                if (!Compare(_il.NumTable, "\n"))
                {
                    return;
                }
                else
                {
                    RTheElementTransition();
                }
            }
        }

        public static int Look(int number, string word)
        {
            var index = number switch
            {
                1 => Find(Tables.ItemValuesTableSeveredWord(), word),
                2 => Find(Tables.ItemTableRazdeliteli(), word),
                3 => Find(Tables.TableDigit, word),
                4 => Find(Tables.TableInfdificate, word),
                _ => 0
            };

            return index;
        }

        public static int Find(List<string> tables, string word)
        {
            var numLexem = tables.FindIndex(x => x == word);

            return _il.NumSymbol == numLexem ? _il.NumSymbol : -1;
        }

        public static bool Compare(int number, string word)
        {
            return _il.NumSymbol == Look(number, word);
        }

        public static string Message(string erroreCode)
        {
            const string message = @"Синтаксический анализ завршенн корректно";

            return erroreCode == "" ? message : erroreCode;
        }

        public static bool Prog()
        {
            // Prog -> program var Desc begin SOper end.

            _i = 0;
            _il.NumSymbol = 0;
            _il.NumTable = 0;

            RTheElementTransition();


            if (Compare(_il.NumTable, "program"))
            {
                RTheElementTransition();

                if (Compare(_il.NumTable, "var"))
                {
                    RTheElementTransition();

                    if(Desc())
                    {
                        if (Compare(_il.NumTable, "begin"))
                        {

                            ErroreCode = @"E003 - Ошибка оператор";
                            Message(ErroreCode);

                            RTheElementTransition();

                            if (SOper())
                            {
                                if (!ProgEnd())
                                    return false;
                            }
                        }
                        else
                        {
                            ErroreCode = @"E002 - Отсутсвует ключевое слово begin/Некорретный дескриптор";
                            Message(ErroreCode);
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    ErroreCode = @"E001 - Отсутсвует ключевое слово var";
                    Message(ErroreCode);
                    return false;
                }

            }
            else
            {
                ErroreCode = @"E000 - Отсутствует ключевое слово program";
                Message(ErroreCode);
                return false;
            }

            return true;
        }

        public static bool ProgEnd()
        {
            if(!Compare(_il.NumTable, "end")) RTheElementTransition();

            if (Compare(_il.NumTable, "end"))
            {
                RTheElementTransition();

                if (Compare(_il.NumTable, "."))
                {
                    ErroreCode = "";
                    Message(ErroreCode);
                    return true;
                }
                else
                {
                    ErroreCode = @"E004 - Ошибка отсутствует .";
                    Message(ErroreCode);
                    return false;
                }
            }
            else
            {
                ErroreCode = @"E003 - Отсутсвтует ключевое слово end";
                Message(ErroreCode);
                return false;
            }
        }

        public static bool IsLineEnd()
        {
            return Compare(_il.NumTable, ";");
        }

        /// <summary> Desc - check? </summary>
        public static bool Desc()
        {
            if (SIden())
            {
                if (Compare(_il.NumTable, ":"))
                {
                    RTheElementTransition();
                    if (Type())
                    {
                        if (IsLineEnd())
                        {
                            RTheElementTransition();
                            if (!Desc())
                                return false;
                        }
                        else
                        {
                            ErroreCode = @"Отсутствует ;";
                            Message(ErroreCode);
                            return false;
                        }
                    }
                    else
                    {
                        Message(ErroreCode);
                        return false;
                    }
                }
                else
                {
                    ErroreCode = @"E002 - Отсутсвует :";
                    Message(ErroreCode);
                    return false;
                }
            }
            else if (Compare(_il.NumTable, ","))
            {
                ErroreCode = @"E002 - Отсутсвует индификатор после ,";
                Message(ErroreCode);
                return false;
            }

            return true;
        }
        /// <summary> SOper - check? </summary>
        public static bool SOper()
        {
            // Oper | SOper ; Oper
            if (Oper())
            {
                if (IsLineEnd())
                {
                    RTheElementTransition();
                    if (Compare(_il.NumTable, "end"))
                    {
                        ErroreCode = @"После оператором должна отсутствовать ;";
                        Message(ErroreCode);
                        return false;
                    }
                    else if (!SOper())
                    {
                        return false;
                    }
                }
                else if (!IsLineEnd())
                {
                    if(Compare(_il.NumTable, "\n"))
                        RTheElementTransition();
                    if (SOper())
                    {
                        ErroreCode = @"Перед оператором отсутствует ;";
                        Message(ErroreCode);
                        return false;
                    }
                }
                else if (Compare(_il.NumTable, "]"))
                {
                    RTheElementTransition();
                    if (!SOper())
                        return false;
                }
                else
                {
                    ErroreCode = @"Отсутствует ; - для обычного оператор/отсутствует знак перехода для составного";
                    return false;
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        public static bool Operand()
        {
        // Sum | Operand OperGrAdd Sum
            if (Summation())
            {

                if (OperGrAdd())
                {
                    RTheElementTransition();
                    if (!Operand())
                        return false;
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        /// <summary>Summation(слагаемое)</summary>
        public static bool Summation()
        {

            if (Multiplier())
            {
                if (OperGrMult())
                {
                    RTheElementTransition();
                    if (!Summation())
                        return false;
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        /// <summary>Multiplier(множитель)</summary>
        public static bool Multiplier()
        {
            if (Iden())
            {
                ReadNextElement();
                return true;
            }
            else if (Num())
            {
                ReadNextElement();
                return true;
            }
            else if (LogCon())
            {
                ReadNextElement();
                return true;
            }
            else if (UnaryOperation())
            {
                ReadNextElement();
                if (!Multiplier())
                    return false;
                return true;
            }
            else  if (Compare(_il.NumTable, "("))
            {
                ReadNextElement();
                if (!Compare(_il.NumTable, "(") )//&& Exp())
                {
                    if (!Exp())
                    {
                        ErroreCode = @"После ( ожидается выражение";
                        Message(ErroreCode);
                        return false;
                    }
                    if (Compare(_il.NumTable, ")"))
                    {
                        ReadNextElement();
                        return true;
                    }
                    else
                    {
                        ErroreCode = @"Отсутствует )";
                        Message(ErroreCode);
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static bool Oper()
        {
            // [Compound] | Assig | Cond | FixLoop | CondLoop | In | Out 

            if (Compare(_il.NumTable, "["))
            {
                RTheElementTransition();
                if (!Compound())
                    return false;
                else if (Compare(_il.NumTable, "]"))
                {
                    RTheElementTransition();

                    return true;
                }
                else
                {
                    ErroreCode = @"Ошибка ошибка составного оператора отсутствует ]";
                    Message(ErroreCode);
                    return false;
                }
            }
            else if (Compare(_il.NumTable, "if"))
            {
                return Cond();
            }
            else if (Compare(_il.NumTable, "for"))
            {
                return FixLoop();
            }
            else if (Compare(_il.NumTable, "while"))
            {
                return CondLoop();
            }
            else if (Compare(_il.NumTable, "read"))
            {
                return In();
            }
            else if (Compare(_il.NumTable, "write"))
            {
                return Out();
            }
            else if (Iden())
            {
                RTheElementTransition();
                return Assig();
            }

            else
            {
                return false;
            }

        }

        /// <summary> Comp  - доделать </summary>
        public static bool Compound()
        {
            // Compound -> Operator | Compound Symbol Operator
            if (!Compare(_il.NumTable, "[") && Oper())
            {
                if (Symbol())
                {
                    ReadNextElement();
                    if (Compare(_il.NumTable, ":"))
                    {
                        ReadNextElement();
                        if (!Compound())
                        {
                            ErroreCode = @"После : отсутствует оператор";
                            Message(ErroreCode);
                            return false;
                        }
                    }
                    else if (Compare(_il.NumTable, "\n"))
                    {
                        ReadNextElement();
                        if (!Compound())
                        {
                            ErroreCode = @"Ожидается что после перевода строки будет оператор";
                            Message(ErroreCode);
                            return false;
                        }
                    }
                    else if (!Symbol() && !Compound() && !Compare(_il.NumTable, "]"))
                        return false;
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        public static bool Symbol()
        {
            return Compare(_il.NumTable, "\n") || Compare(_il.NumTable, ":");
        }

        /// <summary> Assig - доделать момент с несколькими выражениями и корректный вывод ошибок </summary>
        public static bool Assig()

        {
            // Assig -> Iden as Exp
            if (Compare(_il.NumTable, "as"))
            {
                RTheElementTransition();
                if (!Exp())
                    return false;
            }
            else
            {
                ErroreCode = @"Ошибка отсутствует as";
                Message(ErroreCode);
                return false;
            }


            return true;
        }

        /// <summary> Cond (Доделат момент с ( ) - скобками и [ ] - скобками) </summary>
        public static bool Cond()
        {
            // if Exp then Oper | if Exp then Oper else Oper

            ReadNextElement();
            if (Exp())
            {
                if (Compare(_il.NumTable, "\n"))
                {
                    RTheElementTransition();
                }
                if (Compare(_il.NumTable, "then"))
                {
                    RTheElementTransition();
                    if (Oper())
                    {
                        if(Compare(_il.NumTable, ";"))
                        { }
                        if (Compare(_il.NumTable, "\n"))
                        {
                            RTheElementTransition();
                        }
                        if (Compare(_il.NumTable, "else"))
                        {
                            RTheElementTransition();
                            if (!Oper())
                                return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else if(Compare(_il.NumTable ,"and") || Compare(_il.NumTable, "or"))
                    {
                        if (!Oper())
                            return false;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    ErroreCode = @"Отсутствует ключевое слово then";
                    Message(ErroreCode);
                    return false;
                }
            }
            else
            {
                return false;
            }


            return true;
        }

        public static bool FixLoop()
        {
            // FixLoop ->  for Assig to Exp do Oper 
            ReadNextElement();
            if (Iden())
            {
                ReadNextElement();
                if (Assig())
                {
                    if (Compare(_il.NumTable, "to"))
                    {
                        ReadNextElement();
                        if (Exp())
                        {
                            if (Compare(_il.NumTable, "do"))
                            {
                                RTheElementTransition();
                                if (!Oper())
                                    return false;
                            }
                            else
                            {
                                ErroreCode = @"Отсутствует ключевое слово do";
                                Message(ErroreCode);
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        ErroreCode = @"Отсутствует ключевое слово to";
                        Message(ErroreCode);
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                ErroreCode = @"Отсутствует индификатор";
                Message(ErroreCode);
                return false;
            }

            return true;
        }

        public static bool CondLoop()
        {
            // CondLoop ->  while Exp do Oper
            ReadNextElement();
            if (Exp())
            {
                if (Compare(_il.NumTable, "do"))
                {
                    RTheElementTransition();
                    if (!Oper())
                        return false;
                }
                else
                {
                    ErroreCode = @"Отсутствует ключевое слово do";
                    Message(ErroreCode);
                    return false;
                }
            }
            else
            {
                return false;
            }


            return true;
        }

        public static bool In()
        {
            // In -> read(Iden)

            ReadNextElement();

            if (Compare(_il.NumTable, "("))
            {
                ReadNextElement();
                if (Iden())
                {
                    RTheElementTransition();
                    if (Compare(_il.NumTable, ")"))
                    {
                        ReadNextElement();
                    }
                    else
                    {
                        ErroreCode = @"E009 - Ошибка In() - отсутсвтует )";
                        Message(ErroreCode);
                        return false;
                    }
                }
                else
                {
                    ErroreCode = @"Ошибка отсутствует выражение для вывода";
                    return false;
                }
            }
            else
            {
                ErroreCode = @"E010 - Ошибка In() - отсутствует (";
                Message(ErroreCode);
                return false;
            }

            return true;
        }

        public static bool Out()
        {
            // Out -> write(SExp)

            ReadNextElement();

            if (Compare(_il.NumTable, "("))
            {
                ReadNextElement();
                if (SExp())
                {
                    if (Compare(_il.NumTable, ")"))
                    {
                        ReadNextElement();
                    }
                    else
                    {
                        ErroreCode = @"E011 - Ошибка Out() - отсутсвтует )";
                        Message(ErroreCode);
                        return false;
                    }
                }
                else
                {
                    ErroreCode = @"Отсутствует или неверно записан внутреннее выражение";
                    Message(ErroreCode);
                    return false;
                }
            }
            else
            {
                ErroreCode = @"E012 - Ошибка Out() - отсутствует (";
                Message(ErroreCode);
                return false;
            }

            return true;
        }

        public static bool SExp()
        {
            // SExp -> Exp | SExp, Exp
            if (Exp())
            {
                if (Compare(_il.NumTable, ","))
                {
                    RTheElementTransition();
                    if (!SExp())
                        return false;
                }
            }
            else
            {
                ErroreCode = @"Отсутствует выражение - SExp()";
                return false;
            }

            return true;
        }

        public static bool Exp()
        {
            // Exp -> Operand | Exp OperaGrRelat Operand
            if (Operand())
            {
                if (OperaGrRelat())
                {
                    RTheElementTransition();

                    if (!Exp())
                        return false;
                    return true;
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        /// <summary> SIden - check ? </summary>
        public static bool SIden()
        {
            // Iden | SIden , Iden
            if (Iden())
            {
                RTheElementTransition();

                if (Compare(_il.NumTable, ","))
                {
                    ReadNextElement();
                    if (!SIden())
                        return false;
                }
                else
                {
                    return true;
                }

            }
            else
            {
                ErroreCode = @"Ошибка отсутствует индификатор";
                return false;
            }

            return true;
        }

        /// <summary> Iden - check </summary>
        public static bool Iden()
        {

            if (Tables.TableInfdificate.Count != 0)
            {
                if (_il.NumTable != 4) return false;

                var s = Tables.TableInfdificate.ToArray()[_il.NumSymbol];

                if (_i <= Tables.Lexemes.Count && Find(Tables.TableInfdificate, s) != -1)
                {
                    return true;
                }
                else
                {
                    ErroreCode = @"Ошибка отсутствует индификатор";
                    Message(ErroreCode);
                    return false;
                }

            }
            else
            {
                return false;
            }
        }

        /// <summary> Num - check </summary>
        public static bool Num()
        {

            if (Tables.TableDigit.Count != 0)
            {

                if (_il.NumTable != 3) return false;

                var s = Tables.TableDigit.ToArray()[_il.NumSymbol];

                if (_i <= Tables.Lexemes.Count && Find(Tables.TableDigit, s) != -1)
                {
                    {
                        return true;
                    }
                }
                else
                {
                    ErroreCode = @"Ошибка Num()";
                    Message(ErroreCode);
                    return false;
                }

            }
            else
            {
                return false;
            }
        }

        /// <summary> LogCon - check </summary>
        public static bool LogCon()
        {
            return Compare(_il.NumTable, "true") || Compare(_il.NumTable, "false");
        }

        /// <summary> Type - check </summary>
        public static bool Type()
        {
            // int | float | bool
            var isType = Compare(_il.NumTable, "int") || Compare(_il.NumTable, "float") || Compare(_il.NumTable, "bool");

            if (isType)
            {
                ReadNextElement();
            }
            else
            {
                ErroreCode = @"E014 - Ошибка Type() - неверный тип данных";
                Message(ErroreCode);
                return false;
            }

            return true;
        }

        public static bool OperaGrRelat()
        {
            // OperGrRelat -> NE | EQ | LT | LE | GT | GE

            return Compare(_il.NumTable, "NE") || Compare(_il.NumTable, "EQ") || Compare(_il.NumTable, "LT") ||
                   Compare(_il.NumTable, "LE") || Compare(_il.NumTable, "GT") || Compare(_il.NumTable, "GE");
        }

        public static bool OperGrMult()
        {
            return Compare(_il.NumTable, "div") || Compare(_il.NumTable, "mult") || Compare(_il.NumTable, "and");
        }

        public static bool OperGrAdd()
        {
            return Compare(_il.NumTable, "plus") || Compare(_il.NumTable, "min") || Compare(_il.NumTable, "or");
        }

        public static bool UnaryOperation()
        {
            return Compare(_il.NumTable, "~");
        }
    }
}