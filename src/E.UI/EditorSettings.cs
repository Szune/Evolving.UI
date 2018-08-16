namespace E.UI
{
    public class EditorSettings
    {
        public EditorSettings(string title, int width, int height)
        {
            Title = title;
            Width = width;
            Height = height;
        }

        public string Title { get; }
        public int Width { get; }
        public int Height { get; }
    }
}
