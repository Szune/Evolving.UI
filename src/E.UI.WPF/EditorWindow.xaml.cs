using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace E.UI.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class EditorWindow : Window
    {
        private object _objectToEdit;
        private WpfEditorSettings _settings;
        private readonly IWpfEditor _editor;
        
        public EditorWindow(object objectToEdit) : this(objectToEdit, WpfEditorSettings.Default)
        {
        }

        public EditorWindow(object objectToEdit, WpfEditorSettings settings)
        {
            _objectToEdit = objectToEdit;
            InitializeComponent();
            ApplySettings(settings);
            var editor = new WpfEditor(settings);
            editor.SaveClicked += Editor_SaveClicked;
            _editor = editor;
            UpdateUI();
        }

        public EditorWindow(object objectToEdit, WpfEditorSettings settings, IWpfEditor editor)
        {
            _objectToEdit = objectToEdit;
            _editor = editor;
            InitializeComponent();
            ApplySettings(settings);
            UpdateUI();
        }

        public void SetObjectToEdit(object objectToEdit)
        {
            _objectToEdit = objectToEdit;
            UpdateUI();
        }

        private void UpdateUI()
        {
            var controls = _editor.GetControls(_objectToEdit);
            MainWindow.Content = controls;

            var current = controls;
            while (current is Panel panel)
            {
                 current = TryFocusFirstFocusableControl(panel);
            }
        }

        private static UIElement TryFocusFirstFocusableControl(Panel panel)
        {
            foreach (var child in panel.Children)
            {
                if (!(child is UIElement element))
                    continue;
                var isFocusable = (bool) FocusableProperty.GetMetadata(element.GetType()).DefaultValue;
                if (isFocusable)
                {
                    element.Focus();
                    return null;
                }

                if (element is Panel)
                {
                    return element;
                }
            }

            return null;
        }
        

        private void Editor_SaveClicked(object sender, EventArgs e)
        {
            _editor.SaveValues(_objectToEdit);
            Close();
        }

        public void ApplySettings(WpfEditorSettings settings)
        {
            _settings = settings;
            Width = _settings.Width;
            Height = _settings.Height;
            Title = _settings.Title;
            Margin = _settings.Margin;
        }
    }
}
