using System;
using E.UI.Delegates;

namespace E.UI.Fluent
{
    public class GenerateType
    {
        internal ForType ForType { get; }
        internal Func<ControlGenerator, object> ControlGenerator { get; }

        public GenerateType(ForType forType, Func<ControlGenerator, object> controlGenerator)
        {
            ForType = forType;
            ControlGenerator = controlGenerator;
        }

        public WithGetter WithGetter(Func<ControlGetter, object> controlGetter)
        {
            return new WithGetter(this, controlGetter);
        }
    }
}
