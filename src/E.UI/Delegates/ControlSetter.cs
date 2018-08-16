using System;

namespace E.UI.Delegates
{
    public class ControlSetter
    {
        /// <summary>
        /// The control on which to set the value.
        /// </summary>
        public object Control { get; }
        /// <summary>
        /// The value being set.
        /// </summary>
        public object Value { get; }
        /// <summary>
        /// The type of the member being edited.
        /// </summary>
        public Type MemberType { get; }

        public ControlSetter(object control, object value, Type memberType)
        {
            Control = control;
            Value = value;
            MemberType = memberType;
        }
    }
}
