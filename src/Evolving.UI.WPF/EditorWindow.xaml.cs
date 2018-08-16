#region License & Terms
// MIT License

// Copyright (c) 2018 Erik Iwarson

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion
using System;
using System.Windows;
using System.Windows.Controls;

namespace Evolving.UI.WPF
{
    /// <summary>
    /// Interaction logic for EditorWindow.xaml
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
