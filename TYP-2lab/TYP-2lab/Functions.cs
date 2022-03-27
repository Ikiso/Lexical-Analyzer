using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace TYP_2lab
{
    internal class Lexema
    {
        public static Queue<char> TextQueue = new Queue<char>();
        public static string BufferLexem = "";
        public static string ErroreCode = "";
        public static char current = ' ';
        public static int Index = 0;
        public static int i = 0;
        public static Table Tables = new Table();
        public static States State = States.H;

        public Lexema(Table table)
        {
            Tables = table;
        }

        public static void InQueue(string text)
        {
            foreach (var x in text)
            {
                TextQueue.Enqueue(x);
            }

        }

        /// <summary>
        /// Состояния
        /// </summary>
        internal enum States
        {
            H,                  // начало
            I,                  // идентификатор
            N2,                 // Бинарное число
            N8,                 // Восьмиричное число
            N16,                // Шестн-число
            N10,                // Десятичное число
            ZN,                 // знак порядка
            E,                  // порядок
            OG,                 // ограничитель
            C,                  // комментарий
            B,                  // 'B' или 'b'
            O,                  // 'O' или 'o'
            D,                  // 'D' или 'd'
            HX,                 // 'H' или 'h'
            P,                  // . 
            ER,                 // ошибка
            V                   // выход
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
            {
                current = TextQueue.Dequeue();
            }
            else
            {
                i++;
                current = '\0';
            }
        }
        /// <summary>
        /// Поиск лексемы в таблице
        /// </summary>
        public static int look(List<string> table)
        {
            return table.IndexOf(BufferLexem.ToLower());
        }

        public static int find()
        {
            return Tables.ItemTableRazdeliteli().IndexOf(current.ToString());
        }

        public static bool IsEndOfProgram()
        {
            return !(i < TextQueue.Count);
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
            table.Add(BufferLexem.ToLower());
            return look(table);
        }

        /// <summary>
        ///  Заполнение итогового листа
        /// </summary>
        public static void Out()
        {
            var number = "";

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

            var str = "(" + number + ", " + Index + ")";
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
            const string message = @"Анализ завршенн корректно";

            return state == States.ER ? erroreCode : message;
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

        public static bool isEnd()
        {
            return (current != '\0' || find() != -1);
        }

        /// <summary>
        /// Scan lexem function
        /// </summary>
        /// <param name="text"></param>
        public static void Scan(string text)
        {
            InQueue(text);
            GC();

            i = 0;
            State = States.H;
            ErroreCode = "";

            while (!IsEndOfProgram())
            {
                switch (State)
                {
                    case States.H:
                        {
                            nill();
                            while (current == ' ')
                            {
                                GC();
                            }
                            if (IsLetter())
                                State = States.I;
                            else if (IsBinaryFigure())
                            {
                                State = States.N2;
                            }
                            else if (IsOctalFigure())
                            {
                                State = States.N8;
                            }
                            else if (IsDigit())
                            {
                                State = States.N10;
                            }
                            else if (current == '{')
                            {
                                State = States.C;
                            }
                            else if (current == '.')
                            {
                                State = States.P;
                            }
                            else if (current == '\0')
                            {
                                ++i;
                                State = States.ER;
                            }
                            else
                                State = States.OG;

                            break;
                        } // Case H - начало

                    case States.I:
                        {

                            while (IsLetter() || IsDigit())
                            {
                                add();
                                GC();
                            }

                            if (isEnd())
                            {
                                Index = look(Tables.ItemValuesTableSeveredWord());

                                if (Index == -1)
                                {
                                    Index = look(Tables.TableInfdificate);
                                    if (Index == -1)
                                        Tables.TableInfdificate.Add(BufferLexem);
                                }

                                Out();
                                State = States.H;
                            }
                            else
                            {
                                State = States.ER;
                                ErroreCode = "#0004";
                            }

                            break;
                        } // Индификатор

                    case States.C:

                        if(i > TextQueue.Count) break;
                        
                        add();
                        Out();

                        while (current != '}')
                        {
                            GC();
                            if (i > TextQueue.Count) break;
                        }

                        if (current == '}')
                        {
                            nill();
                            add();
                            Out();
                            GC();

                            State = States.H;
                        }

                        else
                        {
                            ErroreCode = "#012C3 Ошибка комментария\n";
                            State = States.ER;
                        }

                        break;

                    case States.N2:

                        while (IsBinaryFigure())
                        {
                            add();
                            GC();
                        }

                        if (IsBinary())
                            State = States.B;
                        else if (IsOctalFigure())
                            State = States.N8;
                        else if (IsOctal())
                            State = States.O;
                        else if (IsDigit())
                            State = States.N10;
                        else if (IsDecimal())
                            State = States.D;
                        else if (IsExponent())
                            State = States.E;
                        else if (IsHexadecimalLetter())
                            State = States.N16;
                        else if (current == '.')
                            State = States.P;
                        else if (isEnd())
                            State = States.D;
                        else
                        {
                            ErroreCode = "#0C124112 Ошибка тип Двоичное число.";
                            State = States.ER;
                        }
                        break;

                    case States.B:
                        add();
                        GC();

                        if (IsHexadecimalFigure())
                            State = States.N16;
                        else if (IsHexadecimal())
                            State = States.HX;
                        else if (isEnd())
                        {
                            Index = look(Tables.TableDigit);

                            if (Index == -1)
                            {
                                Tables.TableDigit.Add(BufferLexem);
                                Index = look(Tables.TableDigit);
                            }
                            Out();
                            State = States.H;
                        }
                        else
                        {
                            ErroreCode = "#00124C12 Ошибка B or b";
                            State = States.ER;
                        }

                        break;

                    case States.N8:

                        while (IsOctalFigure())
                        {
                            add();
                            GC();
                        }

                        if (IsOctal())
                            State = States.O;
                        else if (IsDigit())
                            State = States.N10;
                        else if (IsDecimal())
                            State = States.D;
                        else if (IsHexadecimalLetter())
                            State = States.N16;
                        else if (IsExponent())
                            State = States.E;
                        else if (current == '.')
                            State = States.P;
                        else if (isEnd())
                            State = States.D;
                        else
                        {
                            ErroreCode = @"0x142C1 Ошибка Восьмиричное число.";
                            State = States.ER;
                        }

                        break;


                    case States.O:
                        
                        add();
                        GC();
                        if(IsHexadecimalFigure())
                            State = States.N16;
                        else if (IsHexadecimal())
                            State = States.HX;
                        else if (isEnd())
                        {
                            Index = look(Tables.TableDigit);

                            if (Index == -1)
                            {
                                Tables.TableDigit.Add(BufferLexem);
                                Index = look(Tables.TableDigit);
                            }
                            Out();
                            State = States.H;
                        }
                        else
                        {
                            ErroreCode = "#00132C2 Ошибка O or o";
                            State = States.ER;
                        }

                        break;

                    case States.N10:

                        while (IsDigit())
                        {
                            add();
                            GC();
                        }

                        if (IsDecimal())
                            State = States.D;
                        else if (IsDecimal())
                            State = States.D;
                        else if (IsHexadecimalLetter())
                            State = States.N16;
                        else if (IsExponent())
                            State = States.E;
                        else if (current == '.')
                            State = States.P;
                        else if (isEnd())
                            State = States.D;
                        else
                        {
                            ErroreCode = @"#0112C1 Ошибка Десятичное число.";
                            State = States.ER;
                        }


                        break;


                    case States.D:

                        if (IsDecimal())
                        {
                            add();
                            GC();
                        }

                        if (IsHexadecimalFigure())
                            State = States.N16;
                        else if (IsHexadecimal())
                            State = States.HX;
                        else if (isEnd())
                        {

                            Index = look(Tables.TableDigit);
                            if (Index == -1)
                            {
                                Tables.TableDigit.Add(BufferLexem);
                                Index = look(Tables.TableDigit);
                            }

                            Out();
                            State = States.H;
                        }
                        else
                        {
                            ErroreCode = @"#12E153 Ошибка D or d.";
                            State = States.ER;
                        }

                        break;

                    case States.N16:

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
                            ErroreCode = @"#1351E12 - Ошибка N16 числа.\n";
                            State = States.ER;
                        }

                        break;

                    case States.HX:

                        add();
                        GC();

                        if (isEnd())
                        {
                            Index = look(Tables.TableDigit);
                            if (Index == -1)
                            {
                                Tables.TableDigit.Add(BufferLexem);
                                Index = look(Tables.TableDigit);
                            }

                            Out();
                            State = States.H;
                        }
                        else
                        {
                            ErroreCode = @"313E21 - Ошибка H or h\n";
                            State = States.ER;
                        }

                        break;

                    case States.P:

                        add();
                        GC();

                        var stat = false;

                        while (IsDigit())
                        {
                            add();
                            GC();
                            stat = true;
                        }

                        if (BufferLexem == ".")
                            State = States.V;
                        else if (stat)
                        {
                            if (IsExponent())
                                State = States.E;
                            else
                            {
                                State = States.D;
                            }
                        }
                        else
                        {
                            ErroreCode = @"#573E1 Ошибка - Точка\n";
                            State = States.ER;
                        }

                        break;

                    case States.E:

                        add();
                        GC();

                        if (current == '+' || current == '-')
                        {
                            State = States.ZN;
                        }
                        else
                        {
                            ErroreCode = @"#003 - Ошибка порядка.";
                            State = States.ER;
                        }

                        break;

                    case States.ZN:

                        add();
                        GC();

                        if (!IsDigit())
                        {
                            ErroreCode = @"E0X11 - Ошибка знака порядка.";
                            State = States.ER;
                            break;
                        }

                        while (IsDigit())
                        {
                            add();
                            GC();
                        }

                        if (isEnd())
                        {
                            Index = look(Tables.TableDigit);

                            if (Index == -1)
                            {
                                Tables.TableDigit.Add(BufferLexem);
                                Index = look(Tables.TableDigit);
                            }

                            Out();
                            State = States.H;
                        }
                        else
                        {
                            ErroreCode = @"E12 - Ошибка знака порядка.";
                            State = States.ER;
                        }

                        break;

                    case States.OG:

                        add();
                        GC();

                        Index = look(Tables.ItemTableRazdeliteli());

                        if (Index != -1)
                        {
                            Out();
                            State = States.H;
                        }
                        else
                        {
                            State = States.ER;
                            ErroreCode = @"E1OG - Ошибка индификатора.";
                        }

                        break;

                    case States.ER:
                        {
                            Message(State, ErroreCode);
                            while (!IsEndOfProgram())
                            {
                                i++;
                            }
                            break;
                        } // Case Errore - ошибка

                    case States.V:
                        {
                            Out();
                            GC();
                            State = States.H;

                            break;
                        } // Case Fin - окончание

                    default:
                        {
                            GC();
                            State = States.H;
                            break;
                        }
                }
            }
        }
    }

}