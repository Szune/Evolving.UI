using System;

namespace E.UI.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class UIDialogAttribute : Attribute
    {
        public Type DialogType { get; set; }
        public object[] Arguments { get; set; }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="dialogType">A class that can create some sort of dialog.</param>
        /// <param name="arguments">The arguments used when creating an instance of the dialog class.</param>
        public UIDialogAttribute(Type dialogType, params object[] arguments)
        {
            DialogType = dialogType;
            Arguments = arguments;
            throw new NotImplementedException();
        }
    }
}
