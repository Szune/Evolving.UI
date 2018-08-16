namespace E.UI.Delegates
{
    public class ControlGenerator
    {
        /// <summary>
        /// Whether the member being edited is nullable.
        /// </summary>
        public bool IsNullable { get; }
        /// <summary>
        /// Whether the member being edited is read-only.
        /// </summary>
        public bool IsReadOnly { get; }

        public ControlGenerator(bool isNullable, bool isReadOnly)
        {
            IsNullable = isNullable;
            IsReadOnly = isReadOnly;
        }
    }
}
