using System.Linq;

namespace E.UI.Utilities
{
    public class MemberControl
    {
        public MemberControl(string displayText, bool isReadOnly, bool hasLabel)
        {
            DisplayText = PascalCasedToSpaceSeparated(displayText);
            IsReadOnly = isReadOnly;
            HasLabel = hasLabel;
        }

        private string PascalCasedToSpaceSeparated(string displayText)
        {
            // not pretty, but.. meh
            return string.Join("", displayText.Select(c => IsUpperCase(c) ? $" {c}" : c.ToString())).TrimStart();
        }

        private bool IsUpperCase(char chr)
        {
            return chr >= 'A' && chr <= 'Z';
        }

        public string DisplayText { get; }
        public bool IsReadOnly { get; }
        public bool HasLabel { get; }
        
    }
}
