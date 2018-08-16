using System;

namespace E.UI.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ReadOnlyAttribute : Attribute
    {
    }
}
