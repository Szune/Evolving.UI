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
using System.Reflection;

namespace Evolving.UI.Utilities
{
    /// <summary>
    /// Wrapper for <see cref="FieldInfo"/> and <see cref="PropertyInfo"/>.
    /// </summary>
    public class EMemberInfo
    {
        // Something is very wrong if _type is not a defined enum value, it's probably fair to throw an exception
#pragma warning disable S2372 // Exceptions should not be thrown from property getters
#pragma warning disable S3877 // Exceptions should not be thrown from unexpected methods
        private enum FieldOrProperty
        {
            Field,
            Property
        }

        private readonly FieldInfo _field;
        private readonly PropertyInfo _property;
        private readonly FieldOrProperty _type;
        public Type MemberType
        {
            get
            {
                switch (_type)
                {
                    case FieldOrProperty.Field:
                        return _field.FieldType;
                    case FieldOrProperty.Property:
                        return _property.PropertyType;
                    default:
                        throw new ArgumentOutOfRangeException($"'{_type}' is not defined.");
                }
            }
        }

        public string Name
        {
            get
            {
                switch (_type)
                {
                    case FieldOrProperty.Field:
                        return _field.Name;
                    case FieldOrProperty.Property:
                        return _property.Name;
                    default:
                        throw new ArgumentOutOfRangeException($"'{_type}' is not defined.");
                }
            }
        }

        public Type ParentType { get; }

        public EMemberInfo(PropertyInfo property, Type parentType)
        {
            _property = property;
            ParentType = parentType;
            _type = FieldOrProperty.Property;
        }

        public EMemberInfo(FieldInfo field, Type parentType)
        {
            _field = field;
            ParentType = parentType;
            _type = FieldOrProperty.Field;
        }

        public void SetValue(object obj, object value)
        {
            switch (_type)
            {
                case FieldOrProperty.Field:
                    _field.SetValue(obj, value);
                    break;
                case FieldOrProperty.Property:
                    _property.SetValue(obj, value);
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"'{_type}' is not defined.");
            }
        }

        public object GetValue(object obj)
        {
            switch (_type)
            {
                case FieldOrProperty.Field:
                    return _field.GetValue(obj);
                case FieldOrProperty.Property:
                    return _property.GetValue(obj);
                default:
                    throw new ArgumentOutOfRangeException($"'{_type}' is not defined.");
            }
        }

        public T GetCustomAttribute<T>() where T : Attribute
        {
            switch (_type)
            {
                case FieldOrProperty.Field:
                    return _field.GetCustomAttribute<T>();
                case FieldOrProperty.Property:
                    return _property.GetCustomAttribute<T>();
                default:
                    throw new ArgumentOutOfRangeException($"'{_type}' is not defined.");
            }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is EMemberInfo member))
                return false;
            switch (_type)
            {
                case FieldOrProperty.Field:
                    return member._type == FieldOrProperty.Field && _field.Equals(member._field);
                case FieldOrProperty.Property:
                    return member._type == FieldOrProperty.Property && _property.Equals(member._property);
                default:
                    throw new ArgumentOutOfRangeException($"'{_type}' is not defined.");
            }
        }

        public override int GetHashCode()
        {
            switch (_type)
            {
                case FieldOrProperty.Field:
                    return _field.GetHashCode();
                case FieldOrProperty.Property:
                    return _property.GetHashCode();
                default:
                    throw new ArgumentOutOfRangeException($"'{_type}' is not defined.");
            }
        }

        public override string ToString()
        {
            switch (_type)
            {
                case FieldOrProperty.Field:
                    return _field.ToString();
                case FieldOrProperty.Property:
                    return _property.ToString();
                default:
                    throw new ArgumentOutOfRangeException($"'{_type}' is not defined.");
            }
        }
    }
}
