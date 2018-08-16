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
using System.Linq;

namespace Evolving.UI.Utilities
{
    public class MemberControl
    {
        public MemberControl(string displayText, bool isReadOnly, bool hasLabel)
        {
            DisplayText = PascalCasedToSpaceSeparated(displayText);
            IsReadOnly = isReadOnly;
            HasLabel = hasLabel;
        }

        private string PascalCasedToSpaceSeparated(string displayText)
        {
            // not pretty, but.. meh
            return string.Join("", displayText.Select(c => IsUpperCase(c) ? $" {c}" : c.ToString())).TrimStart();
        }

        private bool IsUpperCase(char chr)
        {
            return chr >= 'A' && chr <= 'Z';
        }

        public string DisplayText { get; }
        public bool IsReadOnly { get; }
        public bool HasLabel { get; }
        
    }
}
