using System.Collections.Generic;

namespace TYP_2lab
{
    internal class Lexema
    {

        public static string BufferLexem = "";
        public static string ErroreCode = "";
        public static string Text = " ";
        public static char current = ' ';
        public static int Index = 0;
        public static int i;
        public static Table Tables = new Table();
        public static States State = States.START;

        public Lexema(Table table)
        {
            Tables = table;
        }

        public static void InQueue(string text)
        {
            Text += text;
        }

        /// <summary>
        /// Состояния
        /// </summary>
        internal enum States
        {
            START,              // начало
            SINDIFICATEOR,      // идентификатор
            SBINARYNUM,         // Бинарное число
            SNUM8,              // Восьмиричное число
            SNUM16,             // Шестн-число
            SNUM10,             // Десятичное число
            SSIGNOFORDER,       // знак порядка
            SORDER,             // порядок
            SLIMITER,           // ограничитель
            SCOM,               // комментарий
            SLESS,              // <
            SGREATER,           // >
            B,                  // 'B' или 'b'
            O,                  // 'O' или 'o'
            D,                  // 'D' или 'd'
            HX,                 // 'H' или 'h'
            SDOT,               // . 
            SERRORE,            // ошибка
            SFIN                // выход
        };

        /// <summary>
        /// Число
        /// </summary>
        public static bool IsDigit()
        {
            return char.IsDigit(current);
        }

        ///<summary>
        /// Буква
        /// </summary>>
        public static bool IsLetter()
        {
            return char.IsLetter(current);
        }

        /// <summary>
        /// Считывание следующего элемента из очереди
        /// </summary>
        public static void GC()
        {
            if (!IsEndOfProgram())
                current = Text[i++];
        }
        /// <summary>
        /// Поиск лексемы в таблице
        /// </summary>
        public static int look(List<string> table)
        {
            return table.IndexOf(BufferLexem);
        }

        public static int find(List<string> table)
        {
            return table.IndexOf(current.ToString());
        }

        public static bool IsEndOfProgram()
        {
            return !(i < Text.Length);
        }

        /// <summary>
        /// Обнуление страки
        /// </summary>
        public static void nill()
        {
            BufferLexem = "";
        }

        /// <summary>
        /// Помещение лексемы в таблицу
        /// </summary>
        public static int put(ref List<string> table)
        {
            table.Add(BufferLexem);
            return look(table);
        }

        /// <summary>
        ///  Заполнение итогового листа
        /// </summary>
        public static void Out()
        {
            string number = "";
            string str;

            Index = look(Tables.ItemValuesTableSeveredWord());

            if (Index == -1)
            {
                Index = look(Tables.TableInfdificate);
                if (Index == -1)
                {
                    Index = look(Tables.TableDigit);
                    if (Index == -1)
                    {
                        Index = look(Tables.ItemTableRazdeliteli());
                        if (Index != -1)
                        {
                            number = "2";
                        }
                    }
                    else
                    {
                        number = "3";
                    }
                }
                else
                {
                    number = "4";
                }
            }
            else
            {
                number = "1";
            }

            str = "(" + number + ", " + Index + ")";
            Tables.Lexemes.Add(str);
        }

        /// <summary>
        ///  Добавление в буффер
        /// </summary>
        public static string add()
        {
            return BufferLexem += current;
        }

        /// <summary>
        ///  Сообщение
        /// </summary>
        public static string Message(States state, string erroreCode)
        {
            var message = @"Анализ завршенн корректно";

            if (state == States.SERRORE)
                return erroreCode;

            return message;
        }

        /// <summary>
        /// Бинарноче число?
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static bool IsBinaryFigure()
        {
            return (current == '0' || current == '1');
        }

        /// <summary>
        /// Восьмиричное число?
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static bool IsOctalFigure()
        {
            return (current >= '0' && current <= '7');
        }

        /// <summary>
        /// Шестнадцетиричное число(буквы)?
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static bool IsHexadecimalLetter()
        {
            return (current >= 'A' && current <= 'F' || current >= 'a' && current <= 'f');
        }

        /// <summary>
        /// Шестандцетиричное число?
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public static bool IsHexadecimalFigure()
        {
            return (IsDigit() || IsHexadecimalLetter());
        }

        /// <summary>
        /// Порядок?
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public static bool IsExponent()
        {
            return (current == 'E' || current == 'e');
        }

        /// <summary>
        /// B or b?
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public static bool IsBinary()
        {
            return (current == 'B' || current == 'b');
        }

        /// <summary>
        /// O or o?
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static bool IsOctal()
        {
            return (current == 'O' || current == 'o');
        }

        /// <summary>
        /// D or d?
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static bool IsDecimal()
        {
            return (current == 'D' || current == 'd');
        }

        /// <summary>
        /// H or h?
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public static bool IsHexadecimal()
        {
            return (current == 'H' || current == 'h');
        }

        /// <summary>
        /// Scan lexem function
        /// </summary>
        /// <param name="text"></param>
        public static void Scan(string text)
        {

            InQueue(text);
            GC();
            while (!IsEndOfProgram())
            {

                switch (State)
                {

                    case States.START:
                        {
                            nill();

                            while (current == ' ' || current == '\r' || current == '\n')
                            {
                                GC();
                            }

                            if (IsLetter())
                                State = States.SINDIFICATEOR;

                            else if (IsBinaryFigure())
                            {
                                State = States.SBINARYNUM;
                            }

                            else if (IsOctalFigure())
                            {
                                State = States.SNUM8;
                            }

                            else if (IsDigit())
                            {
                                State = States.SNUM10;
                            }

                            else if (current == '{')
                                State = States.SCOM;

                            else if (current == 'L')
                                State = States.SLESS;

                            else if (current == 'G')
                                State = States.SGREATER;

                            else if (current == '.')
                            {
                                State = States.SDOT;
                            }

                            else
                                State = States.SLIMITER;

                            break;
                        } // Case Start - начало

                    case States.SINDIFICATEOR:
                        {

                            while (IsLetter() || IsDigit())
                            {
                                add();
                                GC();
                            }

                            Index = look(Tables.ItemValuesTableSeveredWord());

                            if (Index == -1)
                            {
                                Index = look(Tables.TableInfdificate);
                                if (Index == -1)
                                    Tables.TableInfdificate.Add(BufferLexem);
                                    //put(ref Tables.TableInfdificate);

                            }

                            Out();
                            State = States.START;

                            break;
                        } // Индификатор

                    case States.SBINARYNUM:
                        {
                            while (IsBinaryFigure())
                            {
                                add();
                                GC();
                            }

                            if (IsOctalFigure())
                                State = States.SNUM8;

                            else if (IsDigit())
                                State = States.SNUM10;

                            else if (IsBinary())
                            {
                                State = States.B;
                            }

                            else if (IsDecimal())
                            {
                                State = States.D;
                            }

                            else if (IsOctal())
                            {
                                State = States.O;
                            }

                            else if (IsHexadecimalLetter())
                            {
                                State = States.SNUM16;
                            }

                            else if (current == '.')
                            {
                                State = States.SDOT;
                            }

                            else if (IsLetter())
                            {
                                ErroreCode = @"Error: Ex04 - Ошибка типа данных (Десятичное число).";
                                State = States.SERRORE;
                            }

                            else
                            {
                                State = States.SNUM10;
                            }

                            break;
                        } // Двоичное число

                    case States.SNUM8:
                        {
                            while (IsOctalFigure())
                            {
                                add();
                                GC();
                            }

                            if (IsOctal())
                            {
                                State = States.O;
                            }

                            else if (IsDigit())
                            {
                                State = States.SNUM10;
                            }

                            else if (IsHexadecimalLetter())
                            {
                                State = States.SNUM16;
                            }

                            else if (IsDecimal())
                            {
                                State = States.D;
                            }

                            else if (current == '.')
                            {
                                State = States.SDOT;
                            }

                            else if (IsLetter())
                            {
                                State = States.SERRORE;
                                ErroreCode = @"Error: Ex04 - Ошибка типа данных (Десятичное число).";
                            }

                            else
                            {
                                State = States.SNUM10;
                            }

                            break;
                        } // Восьмиричное число

                    case States.SNUM10:
                        {
                            while (IsDigit())
                            {
                                add();
                                GC();
                            }

                            if (IsHexadecimalLetter())
                            {
                                State = States.SNUM16;
                            }

                            else if (IsDecimal())
                            {
                                State = States.D;
                            }

                            else if (current == '.')
                            {
                                State = States.SDOT;
                            }

                            else if (IsLetter())
                            {
                                State = States.SERRORE;
                                ErroreCode = @"Error: Ex04 - Ошибка типа данных (Десятичное число).";
                            }

                            else
                            {
                                State = States.D;
                            }

                            break;
                        } // Десятичное число

                    case States.SNUM16:
                        {

                            while (IsHexadecimalFigure())
                            {
                                add();
                                GC();
                            }

                            if (IsHexadecimal())
                            {
                                State = States.HX;
                            }

                            else
                            {
                                State = States.SERRORE;
                                ErroreCode = @"Error: Ex04 - Ошибка типа данных (Десятичное число).";
                            }

                            break;
                        } // Шестьнадцетиричное

                    case States.B:
                        {
                            add();
                            GC();

                            if (IsHexadecimalFigure())
                            {
                                State = States.SNUM16;
                            }

                            else if (IsHexadecimal())
                            {
                                State = States.HX;
                            }

                            else if (!IsBinary() && (IsLetter() || IsDigit()))
                            {
                                ErroreCode = @"Error: Ex02 - Ошибка типа данных (Бинарное число).";
                                State = States.SERRORE;
                            }

                            else
                            {
                                Index = look(Tables.TableDigit);

                                if (Index == -1)
                                {
                                    //put(ref Tables.TableDigit);
                                    Tables.TableDigit.Add(BufferLexem);
                                    Index = look(Tables.TableDigit);
                                }

                                Out();
                                State = States.START;
                            }

                            break;
                        } // "B" or "b"

                    case States.O:
                        {
                            add();
                            GC();

                            Index = look(Tables.TableDigit);

                            if (Index == -1)
                            {
                                put(ref Tables.TableDigit);
                            }

                            else if (!IsOctal() && (IsLetter() || IsDigit()))
                            {
                                ErroreCode = @"Error: Ex03 - Ошибка типа данных (Восьмиричное число).";
                                State = States.SERRORE;
                            }

                            Out();
                            State = States.START;

                            break;
                        } // "O" or "o"

                    case States.D:
                        {
                            if (IsDecimal())
                            {
                                add();
                                GC();
                            }


                            else if (IsHexadecimal())
                            {
                                State = States.HX;
                            }

                            else if (IsHexadecimalFigure())
                            {
                                State = States.SNUM16;
                            }

                            else if (find(Tables.ItemTableRazdeliteli()) != -1 || current == ' ')
                            {
                                Index = look(Tables.TableDigit);

                                if (Index == -1)
                                {
                                   // put(ref Tables.TableDigit);
                                    Tables.TableDigit.Add(BufferLexem);
                                    Index = look(Tables.TableDigit);
                                }

                                Out();
                                State = States.START;
                            }

                            else
                            {
                                ErroreCode = @"Error: Ex04 - Ошибка типа данных (Десятичное число).";
                                State = States.SERRORE;
                            }

                            break;
                        } // "D" or "d"

                    case States.HX:
                        {
                            add();
                            GC();

                            Index = look(Tables.TableDigit);

                            if (Index == -1)
                            {
                                put(ref Tables.TableDigit);
                                Index = look(Tables.TableDigit);
                            }

                            else
                            {
                                ErroreCode = @"Error: Ex03 - Ошибка типа данных (Шестнадцетиричное число).";
                                State = States.SERRORE;
                            }

                            Out();
                            State = States.START;

                            break;
                        } // "H" or "h"

                    case States.SORDER:
                        {
                            add();
                            GC();

                            if (current == '+' || current == '-')
                                State = States.SSIGNOFORDER;

                            break;
                        } // Порядок


                    case States.SSIGNOFORDER:
                        {
                            add();
                            GC();

                            while (IsDigit())
                            {
                                add();
                                GC();
                            }

                            Index = look(Tables.TableDigit);
                            if (Index == -1)
                            {
                                put(ref Tables.TableDigit);
                                Index = look(Tables.TableDigit);
                            }

                            else if (IsLetter() || current == '+' || current == '-')
                            {
                                ErroreCode = @"Error: Ex9 - Ошибка знака порядка.";
                            }

                            Out();
                            State = States.START;

                            break;
                        } // Знак порядка

                    case States.SDOT:
                        {
                            add();
                            GC();

                            while (IsDigit())
                            {
                                add();
                                GC();
                            }

                            if (BufferLexem == ".")
                            {
                                State = States.SFIN;
                            }
                            else if (IsExponent())
                            {
                                State = States.SORDER;
                            }

                            else if (IsLetter())
                            {
                                ErroreCode = @"Error: Ex04 - Ошибка состояния Точка.";
                                State = States.SERRORE;
                            }

                            else
                            {
                                State = States.SNUM10;
                            }

                            break;
                        } // Точка

                    case States.SLIMITER:
                        {
                            add();
                            GC();

                            var z = look(Tables.ItemTableRazdeliteli());

                            if (z != -1)
                            {
                                Out();
                                State = States.START;
                            }

                            else
                            {
                                State = States.SERRORE;
                                ErroreCode = @"Error: Ex05 - Ошибка такой разделитель отсутствует.";
                            }

                            break;
                        } // Case LIMITER - ограничитель

                    case States.SCOM:
                        {
                            add();
                            Out();
                            while (current != '}')
                            {
                                if (!IsEndOfProgram())
                                    GC();
                                else
                                {
                                    State = States.SERRORE;
                                    ErroreCode = @"Errore: E12x03 - Ошибка комментария.";
                                    break;
                                }
                            }

                            if (current == '}')
                            {
                                nill();
                                add();
                                Out();

                                GC();
                                State = States.START;
                            }

                            break;
                        } // Case COM - комментарий

                    case States.SGREATER:
                        {

                            GC();

                            if (current == 'T')
                                Out();

                            else if (current == 'E')
                            {
                                Out();
                                State = States.START;
                            }

                            else if (current == 'Q')
                            {
                                Out();
                                State = States.START;
                            }

                            else
                            {
                                ErroreCode = @"Error: Ex05 - Ошибка не верный индификатор сравнения.";
                                State = States.SERRORE;
                            }

                            break;
                        } // >


                    case States.SLESS:
                        {
                            GC();

                            if (current == 'T')
                                Out();

                            else if (current == 'E')
                            {
                                Out();
                                State = States.START;
                            }

                            else if (current == 'Q')
                            {
                                Out();
                                State = States.START;
                            }

                            else
                            {
                                ErroreCode = @"Error: Ex05 - Ошибка не верный индификатор сравнения.";
                                State = States.SERRORE;
                            }

                            break;
                        } // <

                    case States.SERRORE:
                        {
                            Message(State, ErroreCode);

                            break;
                        } // Case Errore - ошибка

                    case States.SFIN:
                        {
                            Out();
                            GC();
                            State = States.START;

                            break;
                        } // Case Fin - окончание

                    default:
                        {
                            GC();
                            State = States.START;
                            break;
                        }

                }
            }

        }


    }

}