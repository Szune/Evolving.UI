using System.Windows;
using E.UI.Fluent;
using E.UI.Utilities;
using E.UI.WPF.Controls;

namespace E.UI.WPF
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