using System;
using E.UI.Delegates;

namespace E.UI.Fluent
{
    public class ForType
    {
        internal Type Type { get; }

        public ForType(Type type)
        {
            Type = type;
        }

        public GenerateType Generate(Func<ControlGenerator, object> controlGenerator)
        {
            return new GenerateType(this, controlGenerator);
        }
    }
}
