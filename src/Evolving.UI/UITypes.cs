#region License & Terms
// MIT License

// Copyright (c) 2018 Erik Iwarson

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion
using System;
using System.Collections.Generic;
using Evolving.UI.Delegates;
using Evolving.UI.Utilities;

namespace Evolving.UI
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