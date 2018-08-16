using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace E.UI.Utilities
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
