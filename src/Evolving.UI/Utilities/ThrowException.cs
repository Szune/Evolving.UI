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
using System.Linq.Expressions;

namespace Evolving.UI.Utilities
{
    public static class ThrowException
    {
        public static void IfNull(Expression<Func<object>> expression)
        {
            var value = expression.Compile().Invoke();
            if (value == null)
            {
                var paramName = GetNameFromExpressionBody(expression.Body);
                throw new ArgumentNullException(paramName);
            }
        }

        public static void IfEmpty(Expression<Func<string>> expression)
        {
            var value = expression.Compile().Invoke();
            if (string.IsNullOrWhiteSpace(value))
            {
                var paramName = GetNameFromExpressionBody(expression.Body);
                throw new ArgumentNullException(paramName);
            }
        }

        private static string GetNameFromExpressionBody(Expression body)
        {
            MemberExpression memberExpression;
            if (body is UnaryExpression unaryExpression)
            {
                memberExpression = unaryExpression.Operand as MemberExpression;
            }
            else
                memberExpression = body as MemberExpression;
            if (memberExpression != null)
                return memberExpression.Member.Name;
            throw new InvalidOperationException($"Did not find variable name in expression: {body}");
        }
    }
}
