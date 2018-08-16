using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace E.UI.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class UIClassAttribute : Attribute
    {
        public string Title { get; set; }
        public bool IsReadOnly { get; set; }
        public bool IsBaseClass { get; set; }
        public bool IncludePrivateMembers { get; set; }
        public UIClassAttribute(bool isReadOnly = false, bool isBaseClass = false, bool includePrivateMembers = false, [CallerMemberName] string text = "")
        {
            Title = text;
            IsReadOnly = isReadOnly;
            IsBaseClass = isBaseClass;
            IncludePrivateMembers = includePrivateMembers;
        }
    }
}
