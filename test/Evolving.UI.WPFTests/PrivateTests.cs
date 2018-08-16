using Evolving.UI.Attributes;

namespace Evolving.UI.WPFTests
{
    [UIClass(IncludePrivateMembers = true)]
    public class PrivateTests
    {
        private int TestPrivateInt { get; set; } = 10;
    }
}
