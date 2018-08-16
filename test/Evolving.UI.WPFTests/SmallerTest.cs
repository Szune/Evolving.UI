using System.Windows;
using Evolving.UI.Attributes;

namespace Evolving.UI.WPFTests
{
    [UIClass]
    public class SmallerTest
    {
        public Rect? RectangleNullable { get; set; }
        public Rect Rectangle { get; set; } = new Rect(0, 0, 0, 0);
    }
}
