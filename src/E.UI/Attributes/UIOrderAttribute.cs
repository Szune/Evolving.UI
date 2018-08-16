using System;

namespace E.UI.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class UIOrderAttribute : Attribute
    {
        public int Order { get; set; }

        public UIOrderAttribute(int order)
        {
            Order = order;
            throw new NotImplementedException();
        }
    }
}
