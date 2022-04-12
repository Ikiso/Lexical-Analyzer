using System.Collections.Generic;

namespace TYP_2lab
{
    internal class Parser
    {

        public static Table Tables = new Table();
        private static Table.Token _il;
        private static int _i;
        public static string ErroreCode = "";


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

            return _il.numSymbol == numLexem ? _il.numSymbol : -1;
        }

        public static bool Compare(int number, string word)
        {
            return _il.numSymbol == Look(number, word);
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
            _il.numSymbol = 0;
            _il.numTable = 0;

            ReadNextElement();


            if (Compare(_il.numTable, "program"))
            {
                ReadNextElement();

                if (Compare(_il.numTable, "var"))
                {
                    ReadNextElement();

                    if(Desc())
                    {
                        if (Compare(_il.numTable, "begin"))
                        {

                            ErroreCode = @"E003 - Отсутсвтует ключевое слово end";
                            Message(ErroreCode);

                            ReadNextElement();

                            if (SOper())
                            {

                                if (Compare(_il.numTable, "end"))
                                {
                                    ReadNextElement();

                                    if (Compare(_il.numTable, "."))
                                    {
                                        ErroreCode = "";
                                        Message(ErroreCode);
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
                                }
                            }
                            else
                            {
                                if (Compare(_il.numTable, "end"))
                                {
                                    ReadNextElement();

                                    if (Compare(_il.numTable, "."))
                                    {
                                        ErroreCode = "";
                                        Message(ErroreCode);
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
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            ErroreCode = @"E002 - Отсутсвует ключевое слово begin";
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

        public static bool IsLineEnd()
        {
            return Compare(_il.numTable, ";");
        }

        /// <summary> Desc - check? </summary>
        public static bool Desc()
        {
            Desc:
            if (SIden())
            {
                if (Compare(_il.numTable, ":"))
                {
                    ReadNextElement();
                    if (Type())
                    {
                        if (IsLineEnd())
                        {
                            ReadNextElement();
                            goto Desc;
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
            else if (Compare(_il.numTable, ","))
            {
                ErroreCode = @"E002 - Отсутсвует индификатор после ,";
                Message(ErroreCode);
                return false;
            }
            else
            {
                return true;
            }

        }
        /// <summary> SOper - check? </summary>
        public static bool SOper()
        {
            // Oper | SOper ; Oper
            SOper:
            if (Oper())
            {
                if (IsLineEnd())
                {
                    ReadNextElement();
                    goto SOper;
                }
                else
                {
                    ErroreCode = @"Отсутствует ; - SOper()";
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

        public static bool Operand()
        {
            // Sum | Operand OperGrAdd Sum

            if (Num() || Iden())
            {
                ReadNextElement();
            }
            else
            {
                return false;
            }

            return true;
        }

        public static bool Oper()
        {
            // [Comp] | Assig | Cond | FixLoop | CondLoop | In | Out 


            if (Compare(_il.numTable, "if"))
            {
                return Cond();
            }
            else if (Compare(_il.numTable, "for"))
            {
                return FixLoop();
            }
            else if (Compare(_il.numTable, "while"))
            {
                return CondLoop();
            }
            else if (Compare(_il.numTable, "read"))
            {
                return In();
            }
            else if (Compare(_il.numTable, "write"))
            {
                return Out();
            }
            else if (Iden())
            {
                ReadNextElement();
                return Assig();
            }

            else
            {
                return false;
            }

        }

        /// <summary> Assig - доделать момент с несколькими выражениями и корректный вывод ошибок </summary>
        public static bool Assig()

        {
            // Assig -> Iden as Exp


            if (Compare(_il.numTable, "as"))
            {
                ReadNextElement();

                if (Exp())
                {

                }
                else
                {
                    ErroreCode = @"E312 - Ошибка отсутствует вырожение";
                    Message(ErroreCode);
                    return false;
                }
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


            return true;
        }

        public static bool FixLoop()
        {
            // FixLoop ->  for Assig to Exp do Oper 
            ReadNextElement();

            return false;
        }

        public static bool CondLoop()
        {
            // CondLoop ->  while Exp do Oper
            ReadNextElement();

            return false;
        }

        public static bool In()
        {
            // In -> read(Iden)

            ReadNextElement();

            if (Compare(_il.numTable, "("))
            {
                ReadNextElement();
                if (Iden())
                {
                    ReadNextElement();
                    if (Compare(_il.numTable, ")"))
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

            if (Compare(_il.numTable, "("))
            {
                ReadNextElement();
                if (Iden())
                {
                    ReadNextElement();
                    if (Compare(_il.numTable, ")"))
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
            SExp:
            if (Exp())
            {
                ReadNextElement();

                if (Compare(_il.numTable, ","))
                {
                    ReadNextElement();
                    goto SExp;
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
            // Exp -> Oper | Exp OperaGrRelat Oper
            Operands:
            if (Operand())
            {

                if (OperGrAdd())
                {
                    ReadNextElement();

                    if (Iden() || Num())
                    {
                        ReadNextElement();
                        goto Operands;
                    }
                    else
                        return false;
                }
                else if (OperaGrRelat())
                {
                    ReadNextElement();

                    if (Num() || Iden())
                    {
                        ReadNextElement();
                        goto Operands;
                    }
                    else
                        return false;
                }
                else if (OperGrMult())
                {
                    ReadNextElement();

                    if (Iden() || Num())
                    {
                        ReadNextElement();
                        goto Operands;
                    }
                    else
                        return false;
                }
                else if (Compare(_il.numTable, ";"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                ErroreCode = @"E956 - Ошибка Выражения";
                Message(ErroreCode);
                return false;
            }

        }

        /// <summary> SIden - check ? </summary>
        public static bool SIden()
        {
            // Iden | SIden , Iden
            SIden:
            if (Iden())
            {
                ReadNextElement();

                if (Compare(_il.numTable, ","))
                {
                    ReadNextElement();
                    goto SIden;
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

        }

        /// <summary> Iden - check </summary>
        public static bool Iden()
        {

            if (Tables.TableInfdificate.Count != 0)
            {
                if (_il.numTable != 4) return false;

                var s = Tables.TableInfdificate.ToArray()[_il.numSymbol];

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

                if (_il.numTable != 3) return false;

                var s = Tables.TableDigit.ToArray()[_il.numSymbol];

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
            return Compare(_il.numTable, "true") || Compare(_il.numTable, "false");
        }

        /// <summary> Type - check </summary>
        public static bool Type()
        {
            // int | float | bool
            var isType = Compare(_il.numTable, "int") || Compare(_il.numTable, "float") || Compare(_il.numTable, "bool");

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

            return Compare(_il.numTable, "NE") || Compare(_il.numTable, "EQ") || Compare(_il.numTable, "LT") ||
                   Compare(_il.numTable, "LE") || Compare(_il.numTable, "GT") || Compare(_il.numTable, "GE");
        }

        public static bool OperGrMult()
        {
            return Compare(_il.numTable, "div") || Compare(_il.numTable, "mult") || Compare(_il.numTable, "and");
        }

        public static bool OperGrAdd()
        {
            return Compare(_il.numTable, "plus") || Compare(_il.numTable, "min") || Compare(_il.numTable, "or");
        }
    }
}