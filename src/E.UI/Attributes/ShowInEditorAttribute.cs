using System;
using System.Runtime.CompilerServices;

namespace E.UI.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class ShowInEditorAttribute : Attribute
    {
        public string DisplayText { get; set; }
        public bool IsReadOnly { get; set; }
        public bool IsAdvanced { get; set; }
        public bool IsBaseClass { get; set; }

        public ShowInEditorAttribute(bool isReadOnly = false, bool isAdvanced = false, bool isBaseClass = false, [CallerMemberName] string text = "")
        {
            DisplayText = text;
            IsReadOnly = isReadOnly;
            IsAdvanced = isAdvanced;
            IsBaseClass = isBaseClass;
        }
    }
}
