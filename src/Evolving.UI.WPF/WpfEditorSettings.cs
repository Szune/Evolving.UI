﻿#region License & Terms
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

namespace Evolving.UI.WPF
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
