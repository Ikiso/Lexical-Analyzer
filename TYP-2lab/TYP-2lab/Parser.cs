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
            return _il.numSymbol == Look(number,word);
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

            ReadNextElement();

            if (Compare(_il.numTable, "program"))
            {
                ReadNextElement();

                if (Compare(_il.numTable, "var"))
                {
                    ReadNextElement();

                    if (Desc())
                    {
                        if (Compare(_il.numTable, "begin"))
                        {
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

        public static bool Desc()
        {
            // Desc -> SIden : Type;

            if (SIden())
            {

                if (Compare(_il.numTable, ":"))
                {

                    ReadNextElement();

                    if (Type())
                    {

                        if (Compare(_il.numTable, ";"))
                        {
                            ReadNextElement();
                        }
                        else
                        {
                            ErroreCode = @"E006 - Ошибка отсутсвует ; - линия 159";
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
                    ErroreCode = @"E005 - Ошибка отсутсвует : - линия 151";
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


        public static bool SOper()
        {
            // Oper | SOper ; Oper

            if (Oper())
            {
                if (Compare(_il.numTable, ";"))
                {
                    ReadNextElement();

                    if (SOper())
                    {
                        return false;
                    }

                }
            }

            else
            {
                ErroreCode = @"E007 - Ошибка SOper() линия 193";
                Message(ErroreCode);
                return false;
            }

            return true;
        }

        public static bool Oper()
        {
            // [Comp] | Assig | Cond | FixLoop | CondLoop | In | Out
            if (Compare(_il.numTable, "read"))
            {
                In();
            }
            else if (Compare(_il.numTable, "write"))
            {
                Out();
            }
            else
            {
                ErroreCode = @"E008 - Ошибка Oper() линия 221";
                Message(ErroreCode);
                return false;
            }

            return true;
        }

        public static bool In()
        {
            // In -> read(SIden)

            ReadNextElement();

            if (Compare(_il.numTable, "("))
            {
                ReadNextElement();
                SIden();
                if (Compare(_il.numTable, ")"))
                {
                    ReadNextElement();
                }
                else
                {
                    ErroreCode = @"E009 - Ошибка In() - отсутсвтует ) - линия 251";
                    Message(ErroreCode);
                    return false;
                }
            }
            else
            {
                ErroreCode = @"E010 - Ошибка In() - отсутствует ( - линия 247";
                Message(ErroreCode);
                return false;
            }

            return true;
        }

        public static bool Out()
        {
            // Out -> write(SIden)

            ReadNextElement();

            if (Compare(_il.numTable, "("))
            {
                ReadNextElement();
                SIden();
                if (Compare(_il.numTable, ")"))
                {
                    ReadNextElement();
                }
                else
                {
                    ErroreCode = @"E011 - Ошибка Out() - отсутсвтует ) - линия 282";
                    Message(ErroreCode);
                    return false;
                }
            }
            else
            {
                ErroreCode = @"E012 - Ошибка Out() - отсутствует ( - линия 278";
                Message(ErroreCode);
                return false;
            }

            return true;
        }

        public static bool SIden()
        {
            // Iden | SIden , Iden
            if (Iden())
            {
                ReadNextElement();

                if (Compare(_il.numTable, ","))
                {
                    ReadNextElement();
                    if (SIden())
                    {
                        return true;
                    }
                }


                else if (Compare(_il.numTable, ";"))
                {
                    if (!SIden())
                    {
                        return false;
                    }
                }

            }
            else
            {
                ErroreCode = @"E013 - Ошибка SIden() - линия 305";
                Message(ErroreCode);
                return false;
            }


            return true;
        }

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

        public static bool Type()
        {
            // int | float | bool
            var isType = Compare(1, "int") || Compare(1, "float") || Compare(1, "bool");

            if (isType)
            {
                ReadNextElement();
            }
            else
            {
                ErroreCode = @"E014 - Ошибка Type() - неверный тип данных - линия - 344";
                Message(ErroreCode);
                return false;
            }

            return true;
        }

    }
}
