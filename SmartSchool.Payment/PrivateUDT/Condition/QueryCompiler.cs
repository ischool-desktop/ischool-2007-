using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Private.UDT.Condition
{
    /// <summary>
    /// 條件式的編譯器
    /// 支援( ) and or >= <= <> > = < is not in ,等運算子
    /// </summary>
    public class QueryCompiler
    {
        private List<Symbol> _Symbols = new List<Symbol>();
        private List<Symbol> _PostfixSymbols = new List<Symbol>();
        private Dictionary<string, DataType> _Fields = new Dictionary<string, DataType>();
        private Dictionary<string, int> _Keywords = new Dictionary<string, int>();
        private List<char> _Breaks = new List<char>("\n\t\r\f\v ".ToCharArray());

        internal static XmlDocument Doc = new XmlDocument();
        /// <summary>
        /// 建構子
        /// </summary>
        public QueryCompiler()
        {
            _Keywords.Add("(", 3);
            _Keywords.Add(")", 3);
            _Keywords.Add("and", 2);
            _Keywords.Add("or", 2);
            _Keywords.Add(">=", 0);
            _Keywords.Add("<=", 0);
            _Keywords.Add("<>", 0);
            _Keywords.Add(">", 0);
            _Keywords.Add("=", 0);
            _Keywords.Add("<", 0);
            _Keywords.Add("is", 0);
            _Keywords.Add("not", 1);
            _Keywords.Add("in", 1);
            _Keywords.Add(",", -1);
        }
        /// <summary>
        /// 取得編譯期間切割過的集合
        /// </summary>
        public List<Symbol> Symbols { get { return _Symbols; } }
        /// <summary>
        /// 取得編譯期間轉換成後置後的集合
        /// </summary>
        public List<Symbol> PostfixSymbols { get { return _PostfixSymbols; } }
        /// <summary>
        /// 取得，所有欄位及型別的對照表
        /// </summary>
        public Dictionary<string, DataType> Fields { get { return _Fields; } }
        /// <summary>
        /// 執行編譯
        /// </summary>
        /// <param name="query">條件式， 支援( ) and or >= <= <> > = < is not in ,等運算子</param>
        /// <returns>與條件式同意含的物件</returns>
        public ICondition Compiler(string query)
        {
            _Symbols = new List<Symbol>();
            _PostfixSymbols = new List<Symbol>();
            #region 切割
            for ( int i = 0 ; i < query.Length ; )
            {
                char startchar = query.ToLower()[i];
                if ( _Breaks.Contains(startchar) ) { i++; continue; }//分隔字元跳過
                bool bk = false;
                if ( !bk )
                {
                    foreach ( var field in _Fields.Keys )
                    {
                        if ( field.ToLower()[0] == startchar && query.Length >= i + field.Length && query.Substring(i, field.Length).ToLower() == field.ToLower() )//是欄位
                        {
                            _Symbols.Add(new Symbol() { Index = i, Thing = "Field", Value = field });
                            i += field.Length;
                            bk = true;
                            break;
                        }
                    }
                }
                if ( !bk )
                {
                    foreach ( var op in _Keywords.Keys )
                    {
                        if ( op[0] == startchar && query.Length >= i + op.Length && query.Substring(i, op.Length).ToLower() == op.ToLower() )//是關鍵字
                        {
                            if ( ( op == "not" || op == "in" || op == "and" || op == "or" || op == "is" ) && ( query.Length >= i + op.Length + 1 && !( _Breaks.Contains(query[i + op.Length]) || query[i + op.Length] == '(' ) ) )//一場誤會
                            {
                                continue;
                            }
                            _Symbols.Add(new Symbol() { Index = i, Thing = "Operator", Value = op });
                            i += op.Length;
                            bk = true;
                            break;
                        }
                    }
                }
                if ( !bk )
                {
                    //是值
                    int length = 1;
                    if ( query[i] == '\'' )
                    {
                        //處裡字串
                        for ( ; length + i < query.Length ; length++ )
                        {
                            char ch = query[i + length];
                            if ( ch == '\'' )
                            {
                                length++;
                                break;
                            }
                        }
                    }
                    else
                    {
                        for ( ; length + i < query.Length ; length++ )
                        {
                            char ch = query[i + length];
                            if ( _Breaks.Contains(ch) )
                                break;
                            bool isop = false;
                            foreach ( var op in _Keywords.Keys )
                            {
                                if ( op[0] == ch && query.Length >= i + length + op.Length && query.Substring(i + length, op.Length).ToLower() == op.ToLower() )//是關鍵字
                                {
                                    if ( ( op == "not" || op == "in" || op == "and" || op == "or" || op == "is" ) && ( query.Length >= i + op.Length + 1 && ( !_Breaks.Contains(query[i + op.Length]) || query[i + op.Length] == '(' ) ) )//一場誤會
                                    {
                                        continue;
                                    }
                                    isop = true;
                                    break;
                                }
                            }
                            if ( isop )
                                break;
                        }
                    }
                    _Symbols.Add(new Symbol() { Index = i, Thing = "Value", Value = query.Substring(i, length) });
                    i += length;
                }
            }
            #endregion
            #region 預先處理
            int parenthesis = 0;
            for ( int i = 0 ; i < _Symbols.Count ; i++ )
            {
                var item = _Symbols[i];
                if ( item.Value.ToLower() == "in" )
                {
                    if ( _Symbols.Count <= i + 1 || _Symbols[i + 1].Value != "(" )
                        throw new CompilerFailedException(query, "001", item, "缺少 ( ");
                    i++;
                    bool finish = false;
                    bool hasValue = false;
                    while ( _Symbols.Count > ++i )
                    {
                        if ( _Symbols[i].Value == "," )
                        {
                            if ( _Symbols.Count <= i + 1 || _Symbols[i - 1].Thing != "Value" || _Symbols[i + 1].Thing != "Value" )
                                throw new CompilerFailedException(query, "002", item, "\" ,\"必需置於兩個值中間 ");
                        }
                        else if ( _Symbols[i].Thing == "Value" )
                        {
                            hasValue = true;
                        }
                        else if ( _Symbols[i].Value == ")" )
                        {
                            finish = true;
                            break;
                        }
                        else
                            throw new CompilerFailedException(query, "003", item, "不允許的內容 ");
                    }
                    if ( !finish || !hasValue )
                        throw new CompilerFailedException(query, "004", item);
                }
                if ( item.Thing == "Value" && !( ValidateType(DataType.String, item.Value) || ValidateType(DataType.Boolean, item.Value) || ValidateType(DataType.DateTime, item.Value) || ValidateType(DataType.Number, item.Value) ) )
                    throw new CompilerFailedException(query, "073", item, "未定義的變數名稱：" + item.Value);
                if ( item.Thing == "Value" && item.Value[0] == '\'' )
                {
                    if ( item.Value.Length == 1 || item.Value[item.Value.Length - 1] != '\'' )
                        throw new CompilerFailedException(query, "005", item, "缺少結尾的\'符號");
                }
                if ( item.Thing == "Value" && item.Value[item.Value.Length - 1] == '\'' )
                {
                    if ( item.Value.Length == 1 || item.Value[0] != '\'' )
                        throw new CompilerFailedException(query, "006", item, "缺少開頭的\'符號");
                }
                if ( item.Value == "," )
                    throw new CompilerFailedException(query, "007", item, "\",\"只能在in的引數中使用");
                if ( item.Value == "(" )
                {
                    parenthesis++;
                    if ( _Symbols.Count <= i + 1 || _Symbols[i + 1].Value == ")" )
                        throw new CompilerFailedException(query, "008", item);
                }
                if ( item.Value == ")" )
                    parenthesis--;
            }
            if ( parenthesis != 0 )
                throw new CompilerFailedException(query, "009", "\"(\"與\")\"數量不相等無法配對");
            #endregion
            #region 轉後置
            Stack<Symbol> opstack = new Stack<Symbol>(_Symbols.Count);
            foreach ( var item in _Symbols )
            {
                if ( item.Thing != "Operator" )
                {
                    _PostfixSymbols.Add(item);
                }
                else
                {
                    if ( item.Value == ")" )
                    {
                        bool pass = false;
                        while ( opstack.Count > 0 )
                        {
                            Symbol s = opstack.Pop();
                            if ( s.Value != "(" )
                                _PostfixSymbols.Add(s);
                            else
                            {
                                pass = true;
                                break;
                            }
                        }
                        if ( !pass )
                            throw new CompilerFailedException(query, "010", item, "缺少 ( ");
                    }
                    else
                    {
                        if ( item.Value != "(" )
                        {
                            while ( opstack.Count > 0 && opstack.Peek().Value != "(" && _Keywords[opstack.Peek().Value] <= _Keywords[item.Value] )
                                _PostfixSymbols.Add(opstack.Pop());
                        }
                        opstack.Push(item);
                    }
                }
            }
            while ( opstack.Count > 0 )
            {
                var item = opstack.Pop();
                if ( item.Value == "(" )
                    throw new CompilerFailedException(query, "011", item, "缺少 ) ");
                _PostfixSymbols.Add(item);

            }
            #endregion
            #region 編譯
            System.Collections.Stack stack = new System.Collections.Stack();
            foreach ( var item in _PostfixSymbols )
            {
                if ( item.Thing == "Value" || item.Thing == "Field" )
                    stack.Push(item);
                if ( item.Thing == "Operator" )
                {
                    switch ( item.Value.ToLower() )
                    {
                        case ">=":
                            {
                                if ( stack.Count < 2 )
                                    throw new CompilerFailedException(query, "012", item, "引數錯誤");
                                Symbol after = stack.Pop() as Symbol;
                                Symbol before = stack.Pop() as Symbol;
                                if ( after == null || before == null )
                                    throw new CompilerFailedException(query, "013", item, "引數錯誤");
                                if ( before.Thing == "Field" )
                                {
                                    if ( after.Thing != "Value" )
                                        throw new CompilerFailedException(query, "014", item, "引數錯誤：必需是欄位與值做比較");
                                    if ( !ValidateType(_Fields[before.Value], after.Value) )
                                        throw new CompilerFailedException(query, "015", after, "無法轉型為" + _Fields[before.Value]);
                                    stack.Push(new GreaterEqualsCondition() { Field = before.Value, Value = ParseType(_Fields[before.Value], after.Value) });
                                }
                                else if ( after.Thing == "Field" )
                                {
                                    if ( before.Thing != "Value" )
                                        throw new CompilerFailedException(query, "016", item, "引數錯誤：必需是欄位與值做比較");
                                    if ( !ValidateType(_Fields[after.Value], before.Value) )
                                        throw new CompilerFailedException(query, "017", before, "無法轉型為" + _Fields[after.Value]);
                                    stack.Push(new LessEqualsCondition() { Field = after.Value, Value = ParseType(_Fields[after.Value], before.Value) });
                                }
                                else
                                    throw new CompilerFailedException(query, "018", item, "引數錯誤");
                            }
                            break;
                        case "<=":
                            {
                                if ( stack.Count < 2 )
                                    throw new CompilerFailedException(query, "019", item, "引數錯誤");
                                Symbol after = stack.Pop() as Symbol;
                                Symbol before = stack.Pop() as Symbol;
                                if ( after == null || before == null )
                                    throw new CompilerFailedException(query, "020", item, "引數錯誤");
                                if ( before.Thing == "Field" )
                                {
                                    if ( after.Thing != "Value" )
                                        throw new CompilerFailedException(query, "021", item, "引數錯誤：必需是欄位與值做比較");
                                    if ( !ValidateType(_Fields[before.Value], after.Value) )
                                        throw new CompilerFailedException(query, "022", after, "無法轉型為" + _Fields[before.Value]);
                                    stack.Push(new LessEqualsCondition() { Field = before.Value, Value = ParseType(_Fields[before.Value], after.Value) });
                                }
                                else if ( after.Thing == "Field" )
                                {
                                    if ( before.Thing != "Value" )
                                        throw new CompilerFailedException(query, "023", item, "引數錯誤：必需是欄位與值做比較");
                                    if ( !ValidateType(_Fields[after.Value], before.Value) )
                                        throw new CompilerFailedException(query, "024", before, "無法轉型為" + _Fields[after.Value]);
                                    stack.Push(new GreaterEqualsCondition() { Field = after.Value, Value = ParseType(_Fields[after.Value], before.Value) });
                                }
                                else
                                    throw new CompilerFailedException(query, "025", item, "引數錯誤");
                            }
                            break;
                        case "<>":
                            {
                                if ( stack.Count < 2 )
                                    throw new CompilerFailedException(query, "026", item, "引數錯誤");
                                Symbol after = stack.Pop() as Symbol;
                                Symbol before = stack.Pop() as Symbol;
                                if ( after == null || before == null )
                                    throw new CompilerFailedException(query, "027", item, "引數錯誤");
                                if ( before.Thing == "Field" )
                                {
                                    if ( after.Thing != "Value" )
                                        throw new CompilerFailedException(query, "028", item, "引數錯誤：必需是欄位與值做比較");
                                    if ( !ValidateType(_Fields[before.Value], after.Value) )
                                        throw new CompilerFailedException(query, "029", after, "無法轉型為" + _Fields[before.Value]);
                                    stack.Push(new NotEqualsCondition() { Field = before.Value, Value = ParseType(_Fields[before.Value], after.Value) });
                                }
                                else if ( after.Thing == "Field" )
                                {
                                    if ( before.Thing != "Value" )
                                        throw new CompilerFailedException(query, "030", item, "引數錯誤：必需是欄位與值做比較");
                                    if ( !ValidateType(_Fields[after.Value], before.Value) )
                                        throw new CompilerFailedException(query, "031", before, "無法轉型為" + _Fields[after.Value]);
                                    stack.Push(new NotEqualsCondition() { Field = after.Value, Value = ParseType(_Fields[after.Value], before.Value) });
                                }
                                else
                                    throw new CompilerFailedException(query, "032", item, "引數錯誤");
                            }
                            break;
                        case ">":
                            {
                                if ( stack.Count < 2 )
                                    throw new CompilerFailedException(query, "033", item, "引數錯誤");
                                Symbol after = stack.Pop() as Symbol;
                                Symbol before = stack.Pop() as Symbol;
                                if ( after == null || before == null )
                                    throw new CompilerFailedException(query, "034", item, "引數錯誤");
                                if ( before.Thing == "Field" )
                                {
                                    if ( after.Thing != "Value" )
                                        throw new CompilerFailedException(query, "035", item, "引數錯誤：必需是欄位與值做比較");
                                    if ( !ValidateType(_Fields[before.Value], after.Value) )
                                        throw new CompilerFailedException(query, "036", after, "無法轉型為" + _Fields[before.Value]);
                                    stack.Push(new GreaterCondition() { Field = before.Value, Value = ParseType(_Fields[before.Value], after.Value) });
                                }
                                else if ( after.Thing == "Field" )
                                {
                                    if ( before.Thing != "Value" )
                                        throw new CompilerFailedException(query, "037", item, "引數錯誤：必需是欄位與值做比較");
                                    if ( !ValidateType(_Fields[after.Value], before.Value) )
                                        throw new CompilerFailedException(query, "038", before, "無法轉型為" + _Fields[after.Value]);
                                    stack.Push(new LessCondition() { Field = after.Value, Value = ParseType(_Fields[after.Value], before.Value) });
                                }
                                else
                                    throw new CompilerFailedException(query, "039", item, "引數錯誤");
                            }
                            break;
                        case "=":
                            {
                                if ( stack.Count < 2 )
                                    throw new CompilerFailedException(query, "040", item, "引數錯誤");
                                Symbol after = stack.Pop() as Symbol;
                                Symbol before = stack.Pop() as Symbol;
                                if ( after == null || before == null )
                                    throw new CompilerFailedException(query, "041", item, "引數錯誤");
                                if ( before.Thing == "Field" )
                                {
                                    if ( after.Thing != "Value" )
                                        throw new CompilerFailedException(query, "042", item, "引數錯誤：必需是欄位與值做比較");
                                    if ( after.Value.ToLower() == "null" )//如果值是null做isnull處理
                                    {
                                        stack.Push(new IsNullCondition() { Field = before.Value });
                                    }
                                    else
                                    {
                                        if ( !ValidateType(_Fields[before.Value], after.Value) )
                                            throw new CompilerFailedException(query, "043", after, "無法轉型為" + _Fields[before.Value]);
                                        stack.Push(new EqualsCondition() { Field = before.Value, Value = ParseType(_Fields[before.Value], after.Value) });
                                    }
                                }
                                else if ( after.Thing == "Field" )
                                {
                                    if ( before.Thing != "Value" )
                                        throw new CompilerFailedException(query, "044", item, "引數錯誤：必需是欄位與值做比較");
                                    if ( before.Value.ToLower() == "null" )
                                    {
                                        stack.Push(new IsNullCondition() { Field = after.Value });
                                    }
                                    else
                                    {
                                        if ( !ValidateType(_Fields[after.Value], before.Value) )
                                            throw new CompilerFailedException(query, "045", before, "無法轉型為" + _Fields[after.Value]);
                                        stack.Push(new EqualsCondition() { Field = after.Value, Value = ParseType(_Fields[after.Value], before.Value) });
                                    }
                                }
                                else
                                    throw new CompilerFailedException(query, "010", item, "引數錯誤");
                            }
                            break;
                        case "is":
                            {
                                if ( stack.Count < 2 )
                                    throw new CompilerFailedException(query, "046", item, "引數錯誤");
                                Symbol after = stack.Pop() as Symbol;
                                Symbol before = stack.Pop() as Symbol;
                                if ( after == null || before == null )
                                    throw new CompilerFailedException(query, "047", item, "引數錯誤");
                                if ( before.Thing != "Field" )
                                    throw new CompilerFailedException(query, "048", before, "引數錯誤：必需是欄位");
                                if ( after.Value.ToLower() != "null" )
                                    throw new CompilerFailedException(query, "049", after, "語法錯誤");
                                stack.Push(new IsNullCondition() { Field = before.Value });
                            }
                            break;
                        case "<":
                            {
                                if ( stack.Count < 2 )
                                    throw new CompilerFailedException(query, "050", item, "引數錯誤");
                                Symbol after = stack.Pop() as Symbol;
                                Symbol before = stack.Pop() as Symbol;
                                if ( after == null || before == null )
                                    throw new CompilerFailedException(query, "051", item, "引數錯誤");
                                if ( before.Thing == "Field" )
                                {
                                    if ( after.Thing != "Value" )
                                        throw new CompilerFailedException(query, "052", item, "引數錯誤：必需是欄位與值做比較");
                                    if ( !ValidateType(_Fields[before.Value], after.Value) )
                                        throw new CompilerFailedException(query, "053", after, "無法轉型為" + _Fields[before.Value]);
                                    stack.Push(new LessCondition() { Field = before.Value, Value = ParseType(_Fields[before.Value], after.Value) });
                                }
                                else if ( after.Thing == "Field" )
                                {
                                    if ( before.Thing != "Value" )
                                        throw new CompilerFailedException(query, "054", item, "引數錯誤：必需是欄位與值做比較");
                                    if ( !ValidateType(_Fields[after.Value], before.Value) )
                                        throw new CompilerFailedException(query, "055", before, "無法轉型為" + _Fields[after.Value]);
                                    stack.Push(new GreaterCondition() { Field = after.Value, Value = ParseType(_Fields[after.Value], before.Value) });
                                }
                                else
                                    throw new CompilerFailedException(query, "056", item, "引數錯誤");
                            }
                            break;
                        case "not":
                            {
                                if ( stack.Count < 1 )
                                    throw new CompilerFailedException(query, "057", item, "引數錯誤");
                                ICondition condtion = stack.Pop() as ICondition;
                                if ( condtion == null )
                                    throw new CompilerFailedException(query, "058", item, "引數錯誤");
                                stack.Push(new NotCondition() { Condtion = condtion });
                            }
                            break;
                        case "and":
                            {
                                if ( stack.Count < 2 )
                                    throw new CompilerFailedException(query, "059", item, "引數錯誤");
                                ICondition after = stack.Pop() as ICondition;
                                ICondition before = stack.Pop() as ICondition;
                                if ( after == null || before == null )
                                    throw new CompilerFailedException(query, "060", item, "引數錯誤");
                                stack.Push(new AndCondition() { Condtion1 = before, Condtion2 = after });
                            }
                            break;
                        case "or":
                            {
                                if ( stack.Count < 2 )
                                    throw new CompilerFailedException(query, "061", item, "引數錯誤");
                                ICondition after = stack.Pop() as ICondition;
                                ICondition before = stack.Pop() as ICondition;
                                if ( after == null || before == null )
                                    throw new CompilerFailedException(query, "062", item, "引數錯誤");
                                stack.Push(new OrCondition() { Condtion1 = before, Condtion2 = after });
                            }
                            break;
                        case "in":
                            {
                                if ( stack.Count < 1 )
                                    throw new CompilerFailedException(query, "063", item, "引數錯誤");
                                object value = stack.Pop();
                                Symbol field = stack.Pop() as Symbol;
                                if ( field == null )
                                    throw new CompilerFailedException(query, "064", item, "語法錯誤");
                                if ( field.Thing != "Field" )
                                    throw new CompilerFailedException(query, "065", item, "引數錯誤，" + field.Value + "不是欄位名稱");
                                if ( value is Symbol )
                                {
                                    Symbol val = (Symbol)value;
                                    if ( val.Thing != "Value" )
                                        throw new CompilerFailedException(query, "066", item, "引數錯誤");
                                    else
                                    {
                                        if ( !ValidateType(_Fields[field.Value], val.Value) )
                                            throw new CompilerFailedException(query, "067", val, "無法轉型為" + _Fields[field.Value]);
                                        stack.Push(new InCondition() { Field = field.Value, Values = new List<string>(new string[] { ParseType(_Fields[field.Value], val.Value) }) });
                                    }
                                }
                                else if ( value is MultiValues )
                                {
                                    List<string> values = new List<string>();
                                    foreach ( var val in ( value as MultiValues ).Values )
                                    {
                                        if ( !ValidateType(_Fields[field.Value], val.Value) )
                                            throw new CompilerFailedException(query, "068", val, "無法轉型為" + _Fields[field.Value]);
                                        values.Add(ParseType(_Fields[field.Value], val.Value));
                                    }
                                    stack.Push(new InCondition() { Field = field.Value, Values = values });
                                }
                                else
                                    throw new CompilerFailedException(query, "069", item, "引數錯誤");
                            }
                            break;
                        case ",":
                            {
                                if ( stack.Count < 2 )
                                    throw new CompilerFailedException(query, "070", item, "引數錯誤");
                                Symbol after = stack.Pop() as Symbol;
                                if ( after == null )
                                    throw new CompilerFailedException(query, "071", item, "引數錯誤");
                                object befor = stack.Pop();
                                MultiValues mv;
                                if ( befor is MultiValues )
                                    mv = (MultiValues)befor;
                                else
                                {
                                    Symbol bs = befor as Symbol;
                                    if ( bs == null )
                                        throw new CompilerFailedException(query, "072", item, "引數錯誤");
                                    mv = new MultiValues();
                                    mv.Values.Add(bs);
                                }
                                mv.Values.Add(after);
                                stack.Push(mv);
                            }
                            break;
                    }
                }
            }
            if ( stack.Count == 1 )
            {
                ICondition cond = stack.Pop() as ICondition;
                if ( cond == null )
                    throw new CompilerFailedException(query, "結構不正確");
                return cond;
            }
            else
                throw new CompilerFailedException(query, "結構不正確");
            #endregion
        }

        private bool ValidateType(DataType type, string value)
        {
            if ( type == DataType.String && ( value[0] != '\'' || value[value.Length - 1] != '\'' ) )
                return false;
            if ( type == DataType.DateTime )
            {
                DateTime dt = DateTime.Now;
                if (
                        ( value[0] == '\'' && ( value[value.Length - 1] != '\'' || value.Length == 1 ) ) ||// '開頭沒有'結尾
                        ( value[value.Length - 1] == '\'' && value[0] != '\'' ) ||// '結尾沒有'開頭
                        ( value[0] == '\'' && value[value.Length - 1] == '\'' && !DateTime.TryParse(value.Substring(1, value.Length - 2), out dt) ) ||// '開頭'結尾但格式錯誤
                        ( value[0] != '\'' && !DateTime.TryParse(value, out dt) )// 沒有'開頭但格式錯誤
                    )
                    return false;
            }
            if ( type == DataType.Number )
            {
                decimal d = 0m;
                if (
                        ( value[0] == '\'' && ( value[value.Length - 1] != '\'' || value.Length == 1 ) ) ||// '開頭沒有'結尾
                        ( value[value.Length - 1] == '\'' && value[0] != '\'' ) ||// '結尾沒有'開頭
                        ( value[0] == '\'' && value[value.Length - 1] == '\'' && !decimal.TryParse(value.Substring(1, value.Length - 2), out d) ) ||// '開頭'結尾但格式錯誤
                        ( value[0] != '\'' && !decimal.TryParse(value, out d) )// 沒有'開頭但格式錯誤
                    )
                    return false;
            }
            if ( type == DataType.Boolean )
            {
                if (
                        ( value[0] == '\'' && ( value[value.Length - 1] != '\'' || value.Length == 1 ) ) ||// '開頭沒有'結尾
                        ( value[value.Length - 1] == '\'' && value[0] != '\'' ) ||// '結尾沒有'開頭
                        ( value[0] == '\'' && value[value.Length - 1] == '\'' && !( value.Substring(1, value.Length - 2).ToLower() == "true" || value.Substring(1, value.Length - 2).ToLower() == "false" || value.Substring(1, value.Length - 2).ToLower() == "0" || value.Substring(1, value.Length - 2).ToLower() == "1" ) ) ||// '開頭'結尾但格式錯誤
                        ( value[0] != '\'' && !( value.ToLower() == "true" || value.ToLower() == "false" || value.ToLower() == "0" || value.ToLower() == "1" ) )// 沒有'開頭但格式錯誤
                    )
                    return false;
            }

            return true;
        }

        private string ParseType(DataType type, string value)
        {
            if ( type == DataType.String )
            {
                return value.Substring(1, value.Length - 2);
            }
            if ( type == DataType.DateTime )
            {
                DateTime dt = DateTime.Now;
                if ( value[0] == '\'' )
                    return DateTime.Parse(value.Substring(1, value.Length - 2)).ToString("yyyy/MM/dd HH:mm:ss");
                else
                    return DateTime.Parse(value).ToString("yyyy/MM/dd HH:mm:ss");
            }
            if ( type == DataType.Number )
            {
                decimal d = 0m;
                if ( value[0] == '\'' )
                    return decimal.Parse(value.Substring(1, value.Length - 2)).ToString();
                else
                    return decimal.Parse(value).ToString();
            }
            if ( type == DataType.Boolean )
            {
                if ( value[0] == '\'' )
                    value = value.Substring(1, value.Length - 2);
                if ( value.ToLower() == "true" || value.ToLower() == "1" )
                {
                    return "true";
                }
                if ( value.ToLower() == "false" || value.ToLower() == "0" )
                {
                    return "false";
                }
            }
            return value;
        }

        class MultiValues
        {
            public MultiValues() { Values = new List<Symbol>(); }
            public List<Symbol> Values { get; private set; }
        }
    }
    /// <summary>
    /// 編譯過程中的暫存物件
    /// </summary>
    public class Symbol
    {
        /// <summary>
        /// 取得或設定，表示是運算子或欄位或資料
        /// </summary>
        public string Thing { get; set; }
        /// <summary>
        /// 取得或設定，表示在條件式字串中以0為始的位置
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 取得或設定，內容
        /// </summary>
        public string Value { get; set; }
    }
}
