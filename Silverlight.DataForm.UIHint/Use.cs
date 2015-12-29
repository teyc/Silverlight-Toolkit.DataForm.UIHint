using System.Windows;
using System.Windows.Controls;

namespace Silverlight.DataForm.UIHint
{
    public class Use
    {

        public static bool GetUiHint(DependencyObject obj)
        {
            return (bool)obj.GetValue(UiHintProperty);
        }

        public static void SetUiHint(DependencyObject obj, bool value)
        {
            obj.SetValue(UiHintProperty, value);
        }

        public static readonly DependencyProperty UiHintProperty =
            DependencyProperty.RegisterAttached("UiHint", typeof(bool), typeof(System.Windows.Controls.DataForm), 
                new PropertyMetadata(false, OnUiHintChanged));

        private static void OnUiHintChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dataForm = (System.Windows.Controls.DataForm)d;

            if (e.NewValue != e.OldValue)
            {
                if ((bool) e.NewValue)
                {
                    dataForm.AutoGeneratingField += DataForm_AutoGeneratingField;
                }
                else
                {
                    dataForm.AutoGeneratingField -= DataForm_AutoGeneratingField;
                }
            }
            
        }

        private static void DataForm_AutoGeneratingField(object sender, DataFormAutoGeneratingFieldEventArgs eventArgs)
        {
            var generator = new UIHintGenerator(eventArgs.PropertyName, eventArgs.PropertyType, (System.Windows.Controls.DataForm) sender);
            var control = generator.Generate();
            if (control != null)
            {
                control = generator.Bind(control, generator.UIHintAttribute, generator.PropInfo);
                eventArgs.Field.Content = control;
                eventArgs.Field.IsReadOnly = false;
            }
        }


    }
}