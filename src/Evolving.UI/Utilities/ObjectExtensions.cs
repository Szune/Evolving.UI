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
using System.Linq;
using System.Reflection;

namespace Evolving.UI.Utilities
{
    public static class ObjectExtensions
    {
        public static bool ContainsAttribute<TAttribute>(this object obj) 
            where TAttribute : Attribute
        {
            // TODO: check for private members as well
            var type = obj.GetType();
            var properties = type.GetProperties();
            var fields = type.GetFields();

            return properties.Any(property => property.GetCustomAttributes<TAttribute>().Any())
                   || fields.Any(field => field.GetCustomAttributes<TAttribute>().Any());
        }

        public static IEnumerable<EMemberInfo> GetMembersWithAttribute<TAttribute>(this object obj)
            where TAttribute : Attribute
        {
            // TODO: check for private members as well
            var type = obj.GetType();
            var properties = type.GetProperties();
            var fields = type.GetFields();
            var fieldsWithAttribute = fields
                .Where(field => field.GetCustomAttributes<TAttribute>().Any())
                .Select(f => new EMemberInfo(f, type));
            var propertiesWithAttribute = properties
                .Where(property => property.GetCustomAttributes<TAttribute>().Any())
                .Select(p => new EMemberInfo(p, type));

            return propertiesWithAttribute.Concat(fieldsWithAttribute);
        }

        public static TAttribute GetCustomAttribute<TAttribute>(this Type obj)
            where TAttribute : Attribute
        {
            return obj.GetCustomAttributes<TAttribute>().FirstOrDefault();
        }

        public static string SafeToString(this object obj)
        {
            return obj?.ToString() ?? "";
        }
    }
}
