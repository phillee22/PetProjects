using System;

namespace LogicEvalLib
{
    public class LogicNode
    {
        public LogicNode leftchild = null;
        public LogicNode rightchild = null;
        public LogicNode modifier = null;
        CharType _ctype;
        char _value;

        public LogicNode(char Value)
        {
            _value = Value;
            if (_value == '^' || _value == 'V')
            {
                this._ctype = CharType.Logic;
            }
            else if (_value == '(')
            {
                this._ctype = CharType.LeftGrouping;
            }
            else if (_value == ')')
            {
                this._ctype = CharType.RightGrouping;
            }
            else if (_value == '!')
            {
                this._ctype = CharType.Modifier;
            }
            else if (char.IsLetter(_value))
            {
                this._ctype = CharType.Variable;
            }
            else
                throw new ArgumentException("Invalid character:  " + _value);
        }

        public override string ToString()
        {
            string ret = string.Empty;

            // then add the left child value (could be a tree)
            if (this.leftchild != null)
            {
                ret += this.leftchild.ToString();
            }
            // then if a variable and modified, add the modifier
            if (this.Type == CharType.Variable && this.modifier != null)
            {
                ret += this.modifier.ToString();
            }

            // add the actual value of this node...
            ret += _value;

            // add the right child/tree...
            if (this.rightchild != null)
            { 
                ret += this.rightchild.ToString();
            }
            return ret;
        }

        public CharType Type
        {
            get { return _ctype; }
        }

        public char Value
        {
            get { return _value;  }
        }

        public int Size
        {
            get
            {
                int ret = 1;

                if (this.rightchild != null)
                    ret += this.rightchild.Size;

                if (this.leftchild != null)
                    ret += this.leftchild.Size;

                return ret;
            }
        }
    }
}
