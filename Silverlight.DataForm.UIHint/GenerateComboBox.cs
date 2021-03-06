﻿using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace Silverlight.DataForm.UIHint
{
    public class GenerateComboBox : GenerateControlBase<ComboBox>
    {
        public override FrameworkElement Generate()
        {
            return new ComboBox();
        }

        public override FrameworkElement Bind(FrameworkElement uiElement, UIHintAttribute uiHint, PropertyInfo propInfo)
        {
            base.Bind(uiElement, uiHint, propInfo);

            uiElement.SetBinding(Selector.SelectedItemProperty,
                new Binding(propInfo.Name)
                {
                    Mode = BindingMode.TwoWay,
                    Converter = new NullableItemConverter()
                });

            SetComboBoxItemMinHeight((ComboBox) uiElement);

            return uiElement;
        }

        private static void SetComboBoxItemMinHeight(ComboBox uiElement)
        {
            if (uiElement.ItemContainerStyle == null)
            {
                uiElement.ItemContainerStyle = new Style() {TargetType = typeof (FrameworkElement)};
            }
            uiElement.ItemContainerStyle.Setters.Add(new Setter(FrameworkElement.MinHeightProperty, 20.0));
        }
    }
}