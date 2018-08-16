using E.UI.Delegates;
using System;

namespace E.UI.Fluent
{
    public class WithSetter
    {
        internal WithGetter WithGetter;
        internal Action<ControlSetter> ControlSetter;

        public WithSetter(WithGetter withGetter, Action<ControlSetter> controlSetter)
        {
            WithGetter = withGetter;
            ControlSetter = controlSetter;
        }

        /// <summary>
        /// Overwrites previous implementations for this type.
        /// </summary>
        public void Overwrite()
        {
            // overwrite type
            UITypes.Register(WithGetter.GenerateType.ForType.Type,
                WithGetter.GenerateType.ControlGenerator,
                WithGetter.ControlGetter,
                ControlSetter,
                true);
        }
    }
}
