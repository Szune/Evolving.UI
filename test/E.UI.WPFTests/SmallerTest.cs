using System.Windows;
using E.UI.Attributes;

namespace E.UI.WPFTests
{
    [UIClass]
    public class SmallerTest
    {
        public Rect? RectangleNullable { get; set; }
        public Rect Rectangle { get; set; } = new Rect(0, 0, 0, 0);
    }
}
