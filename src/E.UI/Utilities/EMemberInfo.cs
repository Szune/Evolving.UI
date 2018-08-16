using System;
using System.Reflection;

namespace E.UI.Utilities
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
