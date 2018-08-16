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
using System.Windows.Input;

namespace Evolving.UI.WPF
{
    public class WpfEditor : IWpfEditor
    {
        private readonly ControlResolver _controlResolver;
        private readonly WpfEditorSettings _settings;
        private readonly IMemberResolver _memberResolver;

        public event EventHandler<EventArgs> SaveClicked;

        public WpfEditor(WpfEditorSettings settings)
        {
            _memberResolver = new MemberResolver();
            _controlResolver = new ControlResolver(_memberResolver, settings);
            _settings = settings;
        }

        public WpfEditor(WpfEditorSettings settings, IMemberResolver memberResolver)
        {
            _memberResolver = memberResolver;
            _controlResolver = new ControlResolver(_memberResolver, settings);
            _settings = settings;
        }

        public UIElement GetControls(object objectToEdit)
        {
            var editableMembers = _memberResolver.GetMembers(objectToEdit);

            var stackPanel = new StackPanel{Margin = _settings.Margin};
            foreach (var member in editableMembers)
            {
                var control = _controlResolver.GetControl(objectToEdit, member);
                stackPanel.Children.Add(control);
            }

            var saveButton = new Button {Content = "Save"};
            saveButton.Click += SaveButton_Click;
            stackPanel.Children.Add(saveButton);

            // register enter keydown as save
            stackPanel.KeyDown += (sender, args) =>
            {
                if (args.Key == Key.Enter)
                {
                    args.Handled = true;
                    SaveClicked?.Invoke(this, EventArgs.Empty);
                }
            };
            return stackPanel;
        }

        public void SaveValues(object objectToSaveTo)
        {
            _controlResolver.SaveControlValues(objectToSaveTo);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
