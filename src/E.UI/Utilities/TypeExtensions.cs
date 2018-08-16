using System;

namespace E.UI.Utilities
{
    public static class TypeExtensions
    {
        public static Type GetNullableType(this Type t)
        {
            var cType = t;
            if(t.IsNullable())
                cType = t.GenericTypeArguments[0];
            return cType;
        }

        public static bool IsNullable(this Type t)
        {
            if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
                return true;
            return false;
        }
    }
}
