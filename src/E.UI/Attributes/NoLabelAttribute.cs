using System;

namespace E.UI.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class NoLabelAttribute : Attribute
    {
    }
}
