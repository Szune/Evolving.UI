using System;
using System.Linq.Expressions;

namespace E.UI.Utilities
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
