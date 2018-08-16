using System.Windows;

namespace E.UI.WPF
{
    public class WpfEditorSettings : EditorSettings
    {
        public double LeftColumnWidth { get; }
        public double RightColumnWidth { get; }
        public Thickness Margin { get; }

        public WpfEditorSettings(string title, int width, int height, Thickness margin, double leftColumnWidth, double rightColumnWidth) : base(title, width, height)
        {
            Margin = margin;
            LeftColumnWidth = leftColumnWidth;
            RightColumnWidth = rightColumnWidth;
        }

        public static WpfEditorSettings Default => new WpfEditorSettings("Editor", 400, 400, new Thickness(5,5,5,5), 0.5, 1);
    }
}
