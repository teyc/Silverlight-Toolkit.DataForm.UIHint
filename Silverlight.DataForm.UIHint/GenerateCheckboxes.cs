using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Silverlight.DataForm.UIHint
{
    public class GenerateCheckBoxes : GenerateControlBase<ItemsControl>
    {
        public override FrameworkElement Generate()
        {
            return new CheckedListBox();
        }

        public override FrameworkElement Bind(FrameworkElement uiElement, UIHintAttribute uiHint, PropertyInfo propInfo)
        {
            base.Bind(uiElement, uiHint, propInfo);

            uiElement.SetBinding(CheckedListBox.SelectedItemsProperty,
                new Binding(propInfo.Name)
                {
                    Mode = BindingMode.TwoWay
                });

           return uiElement;
        }
    }
}