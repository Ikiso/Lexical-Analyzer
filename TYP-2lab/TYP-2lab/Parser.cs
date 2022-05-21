using System;
using System.Collections.Generic;
using System.Linq;

namespace TYP_2lab
{
    internal class Parser
    {

        public static Table Tables = new();
        public static string ErroreCode = "";           // Code error sitacs analyzer
        public static string ErroreSemanticCode = "";   // Code error semantic analyzer

        private static Table.Token _il;
        private static int _i;
        private static string _boolBuffer = "";

        private static List<string> _bufferListType = new();

        internal enum States
        {
            Descritpor,
            Operator,
            Assign,
            While,
            If
        }

        private static States _state = States.Descritpor;

        public Parser(Table tables)
        {
            Tables = tables;
        }

        /// <summary> Конвертируем число к соответствующему типу </summary>
        public static void Convertor()
        {
            if (Tables.TableDigit.Count == 0) return;
            foreach (var x in Tables.TableDigit.ToArray())
            {
                TypeDigit(ref Tables.DigitTypes, x, x.Contains(".") ? "float" : "int");
            }
        }
        /// <summary> Функция выполняющая код в зависимости от состояния </summary>
        public static void Switcher()
        {

            switch (_state)
            {
                case States.Assign:
                {
                    InsertBufferType();
                    if(_bufferListType.Count > 1)
                        CompareType();
                    break;
                }
                case States.While:
                {
                    ClearBufferType();
                    BoolExpression();
                    break;
                }
                case States.If:
                {
                    ClearBufferType();
                    BoolExpression();
                    break;
                }
                default:
                {
                    ErroreSemanticCode = @"Не удалось выполнить не один из блоков функции Switcher";
                    ErrorSemanticMessage(ErroreSemanticCode);
                    break;
                }
            }
        }
        
        /// <summary> Сравнивание типов объектов </summary>
        public static bool CompareType()
        {
            for (var i = 1; i < _bufferListType.Count; i++)
            {
                if (_bufferListType[0] == _bufferListType[i])
                {
                    if(!ErroreSemanticCode.Contains(@"не был объявлен"))
                        ErroreSemanticCode = "";
                }
                else
                {
                    if (ErroreSemanticCode.Contains("Ошибка типы не равны друг другу, тип должен быть"))
                        return false;
                    ErroreSemanticCode += $"Ошибка типы не равны друг другу, тип должен быть {_bufferListType[0]}"
                                        + Environment.NewLine;
                    ErrorSemanticMessage(ErroreSemanticCode);
                    return false;
                }
            }
            return true;
        }


        /// <summary> Вывод ошибки семантического анализатора </summary>
        public static string ErrorSemanticMessage(string erroreCode)
        {
            const string message = @"Семантический анализ завршенн корректно";

            return erroreCode == "" ? message : erroreCode;
        }

        ///<summary> Проверка является ли это bool - типом </summary>>
        public static bool IsBool()
        {
            return _bufferListType.Count == 1 && _bufferListType[0] == "bool";
        }

        ///<summary> Проверка являеться ли выражение if или while типа bool </summary>
        public static void BoolExpression()
        {
            AddBoolBuffer();
            if (IsBool())
            {
                _boolBuffer = "";
                ClearBufferType();
            }
            else
            {
                if(ErroreSemanticCode.Contains("Выражение не являеться bool типа")) return;
                ErroreSemanticCode += @"Выражение не являеться bool типа" + Environment.NewLine;
                ErrorSemanticMessage(ErroreSemanticCode);
            }

        }

        ///<summary> Присваивание типа bool в выражениях </summary>
        public static void AddBoolBuffer()
        {
            switch (_il.NumTable)
            {
                case 1:
                {
                    if (OperaGrRelat() || LogCon())
                    {
                        _boolBuffer += Tables.ItemValuesTableSeveredWord().ToArray()[_il.NumSymbol];
                    }
                    break;
                }
                case 4:
                {
                    if (Tables.TableInfdificate.Count != 0)
                    {
                        _boolBuffer += Tables.TableInfdificate.ToArray()[_il.NumSymbol];
                    }
                    else
                    {
                        ErroreSemanticCode = @"Не удалось записать в boolBuffer индификатор";
                        ErrorSemanticMessage(ErroreSemanticCode);
                    }
                    break;
                }
                case 3:
                {
                    if (Tables.TableDigit.Count != 0)
                    {
                        _boolBuffer += Tables.TableDigit.ToArray()[_il.NumSymbol];
                    }
                    else
                    {
                        ErroreSemanticCode = @"Не удалось записать в boolBuffer число";
                        ErrorSemanticMessage(ErroreSemanticCode);
                    }
                    break;
                }

            }

            if (_boolBuffer.Contains("NE") || _boolBuffer.Contains("EQ") || _boolBuffer.Contains("LT")
                || _boolBuffer.Contains("LE") || _boolBuffer.Contains("GT") || _boolBuffer.Contains("GE")
                || _boolBuffer is "true" or "false" or "1" or "0")
            {
                if (_boolBuffer.Last() == 'E'|| _boolBuffer.Last() == 'Q' || _boolBuffer.Last() == 'T')
                { return; }
                if(CheckBoolExpression())
                    _bufferListType.Add("bool");
            }
            else if (Tables.InfdificateType.Count != 0)
            {
                foreach (var x in Tables.InfdificateType.ToArray())
                {
                    if (!ReferenceEquals(x.Item, _boolBuffer)) continue;
                    if (ReferenceEquals(x.Type, "bool"))
                    {
                        ReadNextElement();
                        if (Compare(_il.NumTable, ")"))
                        {
                            BackToLastElement();
                            _bufferListType.Add("bool");
                        }
                        else if (_i == Tables.Lexemes.Count)
                        {
                            BackToLastElement();
                            _bufferListType.Add("bool");
                        }
                        else
                        {
                            BackToLastElement();
                            break;
                        }
                    }
                    else
                    {
                        if(ErroreSemanticCode.Contains($"Ошибка тип индификатора: {_boolBuffer}, не являеться bool;"))
                            return;
                        ErroreSemanticCode += $"Ошибка тип индификатора: {_boolBuffer}, не являеться bool;" + Environment.NewLine;
                        ErrorSemanticMessage(ErroreSemanticCode);
                    }
                }
            }
            else
            {
                ErroreSemanticCode = @"Ошибка _boolBuffer, не содержит ничего из перечисленного";
                ErrorSemanticMessage(ErroreSemanticCode);
            }
        }

        ///<summary> Проверка типов в выражениях if, while </summary>
        public static bool CheckBoolExpression()
        {
            if(Tables.InfdificateType.Count != 0)
                foreach (var x in Tables.InfdificateType.ToArray())
                {
                    if (!_boolBuffer.Contains(x.Item.ToString() ?? string.Empty)) continue;
                    _bufferListType.Add(x.Type.ToString());
                }
            if(Tables.DigitTypes.Count != 0)
                foreach (var d in Tables.DigitTypes.ToArray())
                {
                    if (!_boolBuffer.Contains(d.Item.ToString() ?? string.Empty)) continue;
                    _bufferListType.Add(d.Type.ToString());
                    if (!CompareType()) continue;
                    ClearBufferType();
                    return true;
                }
            ClearBufferType();
            return false;
        } 

        ///<summary> Очистка буффера типов </summary>
        public static void ClearBufferType()
        {
            if (_bufferListType.Count != 0)
            {
                _bufferListType.Clear();
            }
        }

        ///<summary> Заносим элемент в bufferType </summary>
        public static void InsertBufferType()
        {
            switch (_il.NumTable)
            {
                case 4:
                {
                    try
                    {
                        if (Tables.InfdificateType.Count != 0)
                        {
                            var i = Tables.InfdificateType.ToArray()[_il.NumSymbol].Item;
                            var t = Tables.InfdificateType.ToArray()[_il.NumSymbol].Type;
                            if (!ReferenceEquals(t, "0"))
                            {
                                _bufferListType.Add(t.ToString());
                            }
                            else
                            {
                                ErroreSemanticCode = $"Ошикба тип {i} индификатора равен: {t}";
                                ErrorSemanticMessage(ErroreSemanticCode);
                            }
                        }
                        else
                        {
                            ErroreSemanticCode = "Ошикба таблица типа индификаторов содержит " +
                                                 $"{Tables.InfdificateType.Count} элементов";
                            ErrorSemanticMessage(ErroreSemanticCode);
                        }
                    }
                    catch
                    {
                        ErroreSemanticCode += @"Ошибка не удалось записать в буфер тип индификатора" + Environment.NewLine;
                        ErrorSemanticMessage(ErroreSemanticCode);
                    }
                    break;
                }
                case 3:
                {
                    try
                    {
                        if(Tables.DigitTypes.Count != 0)
                        {
                            var i = Tables.DigitTypes.ToArray()[_il.NumSymbol].Item;
                            var t = Tables.DigitTypes.ToArray()[_il.NumSymbol].Type;
                            if (t.ToString() == "int" || t.ToString() == "float")
                            {
                                _bufferListType.Add(t.ToString());
                            }
                            else
                            {
                                ErroreSemanticCode = $"Ошибка тип числа {i}, не равен известным, {t}";
                                ErrorSemanticMessage(ErroreSemanticCode);
                            }
                        }
                        else
                        {
                            ErroreSemanticCode = $"Ошибка таблица типа чисел содержит " +
                                                 $"{Tables.DigitTypes.Count} элементов";
                            ErrorSemanticMessage(ErroreSemanticCode);
                        }
                    }
                    catch
                    {
                        ErroreSemanticCode = @"Ошибка не удалось записать в буфер тип числа";
                        ErrorSemanticMessage(ErroreSemanticCode);
                        throw;
                    }
                    break;
                }
                default:
                {
                    ErroreSemanticCode = @"Ошибка не удалось записать в буфер что-либо";
                    ErrorSemanticMessage(ErroreSemanticCode);
                    break;
                }
            }
        }

        /// <summary> Присваивания типа числу </summary> подумать как сделать лучше
        public static void TypeDigit(ref List<Table.TokenType> tlist, string word, string type)
        {
            try
            {
                var tokenType = new Table.TokenType(word, type);
                tlist.Add(tokenType);
                ErroreSemanticCode = "";
            }
            catch
            {
                ErroreSemanticCode = @"Ошибка таблица чисел пуста не удаёться добавить элемент";
                ErrorSemanticMessage(ErroreSemanticCode);
                throw;
            }
        }

        /// <summary> Проверка типа </summary>
        public static string CheckType()
        {
            if (Compare(_il.NumTable, "int"))
            {
                return "int";
            }
            else if (Compare(_il.NumTable, "float"))
            {
                return "float";
            }
            else if (Compare(_il.NumTable, "bool"))
            {
                return "bool";
            }

            return "0";
        }

        /// <summary> Присваивания типа идентификатору </summary>
        public static void IdenAddType(ref List<Table.TokenType> tlist, string word,string type)
        {

            var tokenType = new Table.TokenType(word, type);

            if (Tables.TableInfdificate.Count == 0) return;
            if (Tables.InfdificateType.Count != 0)
            {
                if (Tables.ItemTableIdenType().Contains(tokenType.Item))
                {
                    ErroreSemanticCode += $"Индификатор {tokenType.Item} уже объявлен" + Environment.NewLine;
                    ErrorSemanticMessage(ErroreSemanticCode);
                }
                else
                {
                    if (ErroreSemanticCode != "") return;
                    tlist.Add(tokenType);
                    ErroreSemanticCode = @"";
                }
            }
            else
            {
                tlist.Add(tokenType);
            }
        }

        /// <summary>
        /// Проверка был ли объявлен индификатор
        /// </summary>
        /// <param name="item"> Параметр отвечающий за индификатор </param>
        public static void CheckIden(string item)
        {
            if(Tables.InfdificateType.Count == 0) return;
            foreach (var x in Tables.InfdificateType.ToArray())
            {
                if (!ReferenceEquals(x.Item, item))
                {
                    ErroreSemanticCode += $"{item}, не был объявлен" + Environment.NewLine;
                    ErrorSemanticMessage(ErroreSemanticCode);
                }
                else
                { 
                    if(ErroreSemanticCode != "") return;
                    ErroreSemanticCode = @"";
                    return;
                }
            }
        }

        /// <summary> Переопределение типа индификатора </summary>
        public static void IdenRedefiningType(string type)
        {
            if(Tables.InfdificateType.Count != 0)
            {
                foreach (var x in Tables.InfdificateType.ToArray())
                {
                    if (!ReferenceEquals(x.Type, "0")) continue;
                    Tables.InfdificateType.Remove(x);
                    var tokenType = new Table.TokenType(x.Item, type);
                    Tables.InfdificateType.Add(tokenType);
                }
            }
            else
            {
                ErroreSemanticCode = @"Таблица типов индификаторов пуста!";
                ErrorSemanticMessage(ErroreSemanticCode);
            }
        }

        public static void ReadNextElement()
        {
            if (_i < Tables.Lexemes.Count)
            {
                _il = Tables.Lexemes[_i];
                _i++;
            }
        }

        public static void BackToLastElement()
        {
            if (Tables.Lexemes.Count == 0) return;
            _i--;
            _il = Tables.Lexemes[_i];
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

            // Сбрасываем все первоночальные элементы в 0
            ErroreSemanticCode = "";
            _boolBuffer = "";
            ClearBufferType();
            _i = 0;
            _il.NumSymbol = 0;
            _il.NumTable = 0;

            // Добавляем числам тип
            Convertor();

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
                            _state = States.Operator;
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

        /// <summary> Desc - check </summary>
        public static bool Desc()
        {
            _state = States.Descritpor;
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
                Switcher();
                ReadNextElement();
                return true;
            }
            else if (Num())
            {
                Switcher();
                ReadNextElement();
                return true;
            }
            else if (LogCon())
            {
                if(_state == States.If || _state== States.While)
                    AddBoolBuffer();
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
                _state = States.If;
                return Cond();
            }
            else if (Compare(_il.NumTable, "for"))
            {
                return FixLoop();
            }
            else if (Compare(_il.NumTable, "while"))
            {
                _state = States.While;
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
                _state = States.Assign;
                Switcher();
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
                _state = States.Assign;
                Switcher();
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
                    Switcher();
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

        /// <summary> SIden - несколько индификаторов </summary>
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

        /// <summary> Iden - индификатор </summary>
        public static bool Iden()
        {

            if (Tables.TableInfdificate.Count != 0)
            {
                if (_il.NumTable != 4) return false;

                var s = Tables.TableInfdificate.ToArray()[_il.NumSymbol];

                if (_i <= Tables.Lexemes.Count && Find(Tables.TableInfdificate, s) != -1)
                {
                    if(_state == States.Descritpor)
                        IdenAddType(ref Tables.InfdificateType,s,"0");
                    else if (_state != States.Descritpor)
                        CheckIden(s);
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

        /// <summary> Num - число </summary>
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

        /// <summary> LogCon - Логическая константа </summary>
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
                // Переопределяем тип индификатора
                IdenRedefiningType(CheckType());
                // Считываем следующий элемент
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