using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace E.UI.WPF
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
