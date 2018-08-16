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
using Evolving.UI.Fluent;
using Evolving.UI.Utilities;
using Evolving.UI.WPF.Controls;

namespace Evolving.UI.WPF
{
	public static class SimilarTypes
	{
		/// <summary>
		/// Registers types that use the same code. Currently <see cref="int"/>, <see cref="long"/>, <see cref="float"/>, <see cref="double"/>, <see cref="decimal"/>.
		/// </summary>
		public static void Register()
		{
			For<int>
				.Generate(generator => new ETextBox("0") {VerticalContentAlignment = VerticalAlignment.Center, IsReadOnly = generator.IsReadOnly})
				.WithGetter(getter =>
				{
					var value = ((ETextBox) getter.Control).Text;
					if (!getter.MemberType.IsNullable())
						ThrowException.IfEmpty(() => value);
					return value == "" ? null : (int?) int.Parse(value);
				})
				.WithSetter(setter =>
					((ETextBox) setter.Control).Text = setter.Value.SafeToString());
			
			For<long>
				.Generate(generator => new ETextBox("0") {VerticalContentAlignment = VerticalAlignment.Center, IsReadOnly = generator.IsReadOnly})
				.WithGetter(getter =>
				{
					var value = ((ETextBox) getter.Control).Text;
					if (!getter.MemberType.IsNullable())
						ThrowException.IfEmpty(() => value);
					return value == "" ? null : (long?) long.Parse(value);
				})
				.WithSetter(setter =>
					((ETextBox) setter.Control).Text = setter.Value.SafeToString());
			
			For<float>
				.Generate(generator => new ETextBox("0") {VerticalContentAlignment = VerticalAlignment.Center, IsReadOnly = generator.IsReadOnly})
				.WithGetter(getter =>
				{
					var value = ((ETextBox) getter.Control).Text;
					if (!getter.MemberType.IsNullable())
						ThrowException.IfEmpty(() => value);
					return value == "" ? null : (float?) float.Parse(value);
				})
				.WithSetter(setter =>
					((ETextBox) setter.Control).Text = setter.Value.SafeToString());
			
			For<double>
				.Generate(generator => new ETextBox("0") {VerticalContentAlignment = VerticalAlignment.Center, IsReadOnly = generator.IsReadOnly})
				.WithGetter(getter =>
				{
					var value = ((ETextBox) getter.Control).Text;
					if (!getter.MemberType.IsNullable())
						ThrowException.IfEmpty(() => value);
					return value == "" ? null : (double?) double.Parse(value);
				})
				.WithSetter(setter =>
					((ETextBox) setter.Control).Text = setter.Value.SafeToString());
			
			For<decimal>
				.Generate(generator => new ETextBox("0") {VerticalContentAlignment = VerticalAlignment.Center, IsReadOnly = generator.IsReadOnly})
				.WithGetter(getter =>
				{
					var value = ((ETextBox) getter.Control).Text;
					if (!getter.MemberType.IsNullable())
						ThrowException.IfEmpty(() => value);
					return value == "" ? null : (decimal?) decimal.Parse(value);
				})
				.WithSetter(setter =>
					((ETextBox) setter.Control).Text = setter.Value.SafeToString());
			
		}
	}
}