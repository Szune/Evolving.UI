using System;

namespace E.UI.Utilities
{
    public class EnumHolder
    {
        public EnumHolder(string value, Type enumType)
        {
            Value = value;
            EnumType = enumType;
        }

        public string Value { get; }
        public Type EnumType { get; }

        public override string ToString()
        {
            return Value ?? "{null}";
        }
    }
}
