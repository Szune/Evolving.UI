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
#define FluentStyle
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using Evolving.UI.Fluent;
using Evolving.UI.Utilities;
using Evolving.UI.WPF.Controls;
#if FluentStyle
#endif

namespace Evolving.UI.WPF
{
    public class ControlResolver
    {
        #region Pre-made types
        private const char SeparatorChar = ',';

        static ControlResolver()
        {
            /* Because these pre-made types don't overwrite,
             * it doesn't matter if they are added before or after consumer-supplied types are added */
            SimilarTypes.Register();

#if FluentStyle
            For<string>
                .Generate(generator => new ETextBox { VerticalContentAlignment = VerticalAlignment.Center, IsReadOnly = generator.IsReadOnly})
                .WithGetter(getter => ((TextBox) getter.Control).Text)
                .WithSetter(setter => ((TextBox) setter.Control).Text = setter.Value.SafeToString());
            
            For<bool>
                .Generate(generator => new CheckBox{ VerticalAlignment = VerticalAlignment.Center, IsThreeState = generator.IsNullable, IsEnabled = !generator.IsReadOnly })
                .WithGetter(getter =>
                {
                    var value = ((CheckBox) getter.Control).IsChecked;
                    if (!getter.MemberType.IsNullable())
                        ThrowException.IfNull(() => value);
                    return value;
                })
                .WithSetter(setter => ((CheckBox)setter.Control).IsChecked = (bool?)setter.Value);

            For<Rect>
                .Generate(generator => new ETextBox("0,0,0,0"){ VerticalContentAlignment = VerticalAlignment.Center, IsReadOnly = generator.IsReadOnly })
                .WithGetter(getter =>
                {
                    var value = ((ETextBox) getter.Control).Text;
                    if (!getter.MemberType.IsNullable())
                        ThrowException.IfEmpty(() => value);

                    if (value == "")
                        return null;
                    var str = value.Split(SeparatorChar);
                    return new Rect(double.Parse(str[0]), double.Parse(str[1]), double.Parse(str[2]), double.Parse(str[3]));
                })
                .WithSetter(setter =>
                    ((ETextBox)setter.Control).Text = ((Rect?)setter.Value)?.ToString(CultureInfo.InvariantCulture));

            For<Enum>
                .Generate(generator => new ComboBox {VerticalContentAlignment = VerticalAlignment.Center, IsEnabled = !generator.IsReadOnly})
                .WithGetter(getter =>
                {
                    var holder = (EnumHolder)((ComboBox)getter.Control).SelectedValue;
                    if (!getter.MemberType.IsNullable())
                        ThrowException.IfNull(() => holder.Value);
                    if (holder.Value == null)
                        return null;
                    return Enum.Parse(holder.EnumType.GetNullableType(), holder.Value);
                })
                .WithSetter(setter =>
                {
                    var comboBox = (ComboBox) setter.Control;
                    var names = Enum.GetNames(setter.MemberType.GetNullableType());
                    
                    comboBox.Items.Clear();
                    var index = 0;
                    foreach (var name in names)
                    {
                        comboBox.Items.Add(new EnumHolder(name, setter.MemberType));
                        if (setter.Value?.ToString() == name)
                            index = comboBox.Items.Count - 1;
                    }
                    
                    if (setter.MemberType.IsNullable())
                    {
                        comboBox.Items.Add(new EnumHolder(null, setter.MemberType));
                        if (setter.Value == null)
                            index = comboBox.Items.Count - 1;
                    }

                    comboBox.SelectedIndex = index;
                });

            For<DateTime>
                .Generate(generator => new ETextBox(DateTime.MinValue.ToString()) { VerticalContentAlignment = VerticalAlignment.Center, IsReadOnly = generator.IsReadOnly })
                .WithGetter(getter =>
                {
                    var value = ((ETextBox) getter.Control).Text;
                    if(!getter.MemberType.IsNullable())
                        ThrowException.IfEmpty(() => value);
                    if (value == "")
                        return null;
                    return DateTime.Parse(((ETextBox) getter.Control).Text);
                })
                .WithSetter(setter => ((ETextBox) setter.Control).Text = ((DateTime?)setter.Value)?.ToString());
#else
            EditableTypes.Register(typeof(string),
                generator => new TextBox {VerticalContentAlignment = VerticalAlignment.Center, IsReadOnly =
 generator.IsReadOnly},
                getter => ((TextBox) getter.Control).Text,
                setter => ((TextBox) setter.Control).Text = setter.Value.SafeToString());
            EditableTypes.Register(typeof(bool),
                generator => new CheckBox {VerticalAlignment = VerticalAlignment.Center, IsEnabled =
 !generator.IsReadOnly, IsThreeState = generator.IsNullable},
                getter =>
                {
                    var value = ((CheckBox) getter.Control).IsChecked;
                    if (!getter.MemberType.IsNullable())
                        ThrowException.IfNull(() => value);
                    return value;
                },
                setter => ((CheckBox)setter.Control).IsChecked = (bool?)setter.Value);
            EditableTypes.Register(typeof(Rect),
                generator => new TextBox{VerticalContentAlignment = VerticalAlignment.Center, IsReadOnly =
 generator.IsReadOnly},
                getter =>
                {
                    var value = ((TextBox) getter.Control).Text;
                    if (!getter.MemberType.IsNullable())
                        ThrowException.IfEmpty(() => value);
                    if (value == "")
                        return null;
                    var str = value.Split(SeparatorChar);
                    return new Rect(double.Parse(str[0]), double.Parse(str[1]), double.Parse(str[2]), double.Parse(str[3]));
                },
                setter => ((TextBox)setter.Control).Text =
 ((Rect?)setter.Value)?.ToString(CultureInfo.InvariantCulture) ?? "0,0,0,0");

            EditableTypes.Register(typeof(Enum),
                generator => new ComboBox {VerticalContentAlignment = VerticalAlignment.Center, IsEnabled =
 !generator.IsReadOnly},
                getter =>
                {
                    var holder = (EnumHolder)((ComboBox)getter.Control).SelectedValue;
                    if (!getter.MemberType.IsNullable())
                        ThrowException.IfNull(() => holder.Value);
                    if (holder.Value == null)
                        return null;
                    return Enum.Parse(holder.EnumType.GetNullableType(), holder.Value);
                },
                setter =>
                {
                    var comboBox = (ComboBox) setter.Control;
                    var names = Enum.GetNames(setter.MemberType.GetNullableType());
                    
                    comboBox.Items.Clear();
                    var index = 0;
                    foreach (var name in names)
                    {
                        comboBox.Items.Add(new EnumHolder(name, setter.MemberType));
                        if (setter.Value?.ToString() == name)
                            index = comboBox.Items.Count - 1;
                    }

                    if (setter.MemberType.IsNullable())
                    {
                        comboBox.Items.Add(new EnumHolder(null, setter.MemberType));
                        if (setter.Value == null)
                            index = comboBox.Items.Count - 1;
                    }

                    comboBox.SelectedIndex = index;
                });
            #endif
        }
        #endregion

        /// <summary>
        /// Links control to member.
        /// </summary>
        private readonly Dictionary<EMemberInfo, UIElement> _memberControls = new Dictionary<EMemberInfo, UIElement>();
        private readonly IMemberResolver _memberResolver;
        private readonly WpfEditorSettings _settings;

        public ControlResolver(IMemberResolver memberResolver, WpfEditorSettings settings)
        {
            _memberResolver = memberResolver;
            _settings = settings;
        }

        
        public UIElement GetControl(object objectToEdit, EMemberInfo member)
        {
            var editableType = GetEditableType(member.MemberType);
            // create control
            var isReadOnly = _memberResolver.IsReadOnly(member);
            var control = editableType.GetControl(member.MemberType, isReadOnly) as UIElement;
            // set control value
            editableType.SetValue(control, member.GetValue(objectToEdit), member.MemberType);
            // save control for later
            _memberControls[member] = control;
            
            if (_memberResolver.HasLabel(member))
            {
                return CreateControlWithLabel(control, member.Name);
            }
            return control;
        }

        public void SaveControlValues(object objectToEdit)
        {
            foreach (var kvp in _memberControls)
            {
                // set member values
                kvp.Key.SetValue(objectToEdit, GetControlValue(kvp.Key.MemberType, kvp.Value));
            }
        }

        private static object GetControlValue(Type memberType, UIElement element)
        {
            return GetEditableType(memberType).GetValue(element, memberType);
        }

        private static UIType GetEditableType(Type t)
        {
            return UITypes.Get(t);
        }
        
        private Grid CreateControlWithLabel(UIElement editableElement, string memberName)
        {
            // create grid with label on the left and control on the right
            var label = new Label { Content = $"{memberName}:" };
            var grid = new Grid
            {
                ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength(_settings.LeftColumnWidth, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(_settings.RightColumnWidth, GridUnitType.Star) }
                }
            };
            grid.Children.Add(label);
            grid.Children.Add(editableElement);
            Grid.SetColumn(label, 0);
            Grid.SetColumn(editableElement, 1);
            return grid;
        }
    }
}
