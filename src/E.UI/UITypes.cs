using System;
using System.Collections.Generic;
using E.UI.Delegates;
using E.UI.Utilities;

namespace E.UI
{
    public static class UITypes
    {
        private static readonly Dictionary<Type,UIType> RegisteredTypes = new Dictionary<Type, UIType>();
        public static void Register(Type t, Func<ControlGenerator, object> controlGenerator, Func<ControlGetter, object> controlGetter, Action<ControlSetter> controlSetter, bool overwrite = false)
        {
            if (overwrite)
            {
                RegisteredTypes[t] = new UIType(t, controlGenerator, controlGetter, controlSetter);
                return;
            }

            if (!RegisteredTypes.ContainsKey(t))
                RegisteredTypes[t] = new UIType(t, controlGenerator, controlGetter, controlSetter);
        }

        public static UIType Get(Type t)
        {
            var cType = t.GetNullableType();
            if (cType.BaseType == typeof(Enum))
                cType = typeof(Enum);
            if(!RegisteredTypes.ContainsKey(cType))
                throw new KeyNotFoundException($"Type '{cType}' has not been registered.");

            return RegisteredTypes[cType];
        }

        public static void Reset()
        {
            RegisteredTypes.Clear();
        }
    }
}