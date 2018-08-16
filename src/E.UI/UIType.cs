using System;
using E.UI.Delegates;
using E.UI.Utilities;

namespace E.UI
{
    public class UIType
    {
        public UIType(Type type, Func<ControlGenerator, object> controlGenerator, Func<ControlGetter, object> controlGetter, Action<ControlSetter> controlSetter)
        {
            Type = type;
            ControlGenerator = controlGenerator;
            Getter = controlGetter;
            Setter = controlSetter;
        }

        public Type Type { get; }
        private Func<ControlGenerator, object> ControlGenerator { get; }
        private Func<ControlGetter, object> Getter { get; }
        private Action<ControlSetter> Setter { get; }

        public object GetControl(Type memberType, bool isReadOnly)
        {
            return ControlGenerator(new ControlGenerator(memberType.IsNullable(), isReadOnly));
        }

        public object GetValue(object control, Type memberType)
        {
            return Getter(new ControlGetter(control, memberType));
        }

        public void SetValue(object control, object value, Type memberType)
        {
            Setter(new ControlSetter(control, value, memberType));
        }
    }
}
