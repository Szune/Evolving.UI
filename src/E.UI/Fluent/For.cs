using System;
using E.UI.Delegates;

namespace E.UI.Fluent
{
    public static class For
    {
        public static ForType Type(Type type)
        {
            return new ForType(type);
        }
    }

    public static class For<T>
    {
        public static ForType Generate()
        {
            return new ForType(typeof(T));
        }

        public static GenerateType Generate(Func<ControlGenerator, object> controlGenerator)
        {
            return new GenerateType(new ForType(typeof(T)), controlGenerator);
        }

        public static void Register(Func<ControlGenerator, object> controlGenerator,
            Func<ControlGetter, object> controlGetter, Action<ControlSetter> controlSetter, bool overwrite = false)
        {
            UITypes.Register(typeof(T), controlGenerator, controlGetter, controlSetter, overwrite);
        }
    }
}
