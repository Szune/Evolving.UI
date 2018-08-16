using System;

namespace E.UI.Delegates
{
    public class ControlGetter
    {
        /// <summary>
        /// The control from which to get the value.
        /// </summary>
        public object Control { get; }
        /// <summary>
        /// The type of the member being edited.
        /// </summary>
        public Type MemberType { get; }

        public ControlGetter(object control, Type memberType)
        {
            Control = control;
            MemberType = memberType;
        }
    }
}
