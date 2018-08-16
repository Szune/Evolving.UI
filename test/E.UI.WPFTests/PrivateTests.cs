using System.ComponentModel;
using E.UI.Attributes;

namespace E.UI.WPFTests
{
    [UIClass(IncludePrivateMembers = true)]
    public class PrivateTests
    {
        private int TestPrivateInt { get; set; } = 10;
    }
}
