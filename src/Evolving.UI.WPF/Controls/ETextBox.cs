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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Evolving.UI.WPF.Controls
{
    /// <summary>
    /// An extension of <see cref="TextBox"/> that shows a placeholder text if its text is empty.
    /// </summary>
    public class ETextBox : TextBox
    {
        public static readonly DependencyProperty PlaceholderTextProperty = DependencyProperty.Register(nameof(PlaceholderText),
            typeof(string), typeof(ETextBox),
            new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.AffectsRender));

        public string PlaceholderText
        {
            get => (string) GetValue(PlaceholderTextProperty);
            set => SetValue(PlaceholderTextProperty, value);
        }

        // overwrite base class Text to easily tell if text was changed programmatically or not
        // maybe make this a wrapper class instead, so it's impossible to accidentally cast to TextBox instead of ETextBox and get the wrong Text value.
        public new string Text
        {
            get => _textFromActualInput ?? "";
            set
            {
                _textFromActualInput = value;
                if (base.Text == value)
                {
                    // fix for when Text is set to a string equal to PlaceholderText
                    RestoreVisuals(); // because the TextChanged event won't fire then (which makes sense, since nothing changed)
                    return;
                }

                _textChangedProgrammatically = true;
                base.Text = value;
            }}


        private bool _textChangedProgrammatically;
        private bool _clearedTextBox;
        private string _textFromActualInput;

        public ETextBox()
        {
            GotKeyboardFocus += ETextBox_GotKeyboardFocus;
            LostKeyboardFocus += ETextBox_LostKeyboardFocus;
            TextChanged += ETextBox_TextChanged;
        }

        public ETextBox(string placeholderText)
        {
            PlaceholderText = placeholderText;
            GotKeyboardFocus += ETextBox_GotKeyboardFocus;
            LostKeyboardFocus += ETextBox_LostKeyboardFocus;
            TextChanged += ETextBox_TextChanged;
            SetPlaceholderVisuals();
        }

        private void ETextBox_LostKeyboardFocus(object sender, System.Windows.Input.KeyboardFocusChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(_textFromActualInput))
            {
                SetPlaceholderVisuals();
            }
        }

        private void ETextBox_GotKeyboardFocus(object sender, System.Windows.Input.KeyboardFocusChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(_textFromActualInput))
            {
                _clearedTextBox = true;
                base.Text = "";
                return;
            }

            // there is text in the textbox that is not the placeholder text
            SelectAll();
        }

        private void ETextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            HandleStates();
        }

        private void HandleStates()
        {
            if (_clearedTextBox)
            {
                RestoreVisuals();
                _clearedTextBox = false;
                _textChangedProgrammatically = false;
                return;
            }
            
            if (_textChangedProgrammatically)
            {
                if (!string.IsNullOrWhiteSpace(base.Text))
                {
                    RestoreVisuals();
                }
                else
                {
                    SetPlaceholderVisuals();
                }

                _textChangedProgrammatically = false;
                return;
            }

            _textFromActualInput = base.Text;
        }

        private void SetPlaceholderVisuals()
        {
            if (string.IsNullOrWhiteSpace(PlaceholderText))
                return;
            _textChangedProgrammatically = true;
            base.Text = PlaceholderText;
            Foreground = Brushes.Gray;
        }

        private void RestoreVisuals()
        {
            Foreground = Brushes.Black;
        }
    }
}
