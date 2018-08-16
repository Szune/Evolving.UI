using System;
using E.UI.Delegates;

namespace E.UI.Fluent
{
    public class WithGetter
    {
        internal GenerateType GenerateType { get; }
        internal Func<ControlGetter, object> ControlGetter { get; }

        public WithGetter(GenerateType generateType, Func<ControlGetter, object> controlGetter)
        {
            GenerateType = generateType;
            ControlGetter = controlGetter;
        }

        public WithSetter WithSetter(Action<ControlSetter> controlSetter)
        {
            // register type
            UITypes.Register(GenerateType.ForType.Type, GenerateType.ControlGenerator, ControlGetter, controlSetter, false);
            return new WithSetter(this, controlSetter);
        }
    }
}
