using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using Silverlight.DataForm.UIHint.Annotations;

namespace Silverlight.DataForm.UIHint
{
    public class NullableItemsSource<T> : DependencyObject, INotifyPropertyChanged, IEnumerable<T>
        where T : class
    {
        // Using a DependencyProperty as the backing store for ItemsSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof (IEnumerable<T>), typeof (NullableItemsSource<T>),
                new PropertyMetadata(null, ItemsSourceChanged));

        public IEnumerable<T> ItemsSource
        {
            get { return (IEnumerable<T>) GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public IEnumerable<T> Items
        {
            get
            {
                yield return null;
                foreach (var obj in ItemsSource)
                {
                    yield return obj;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private static void ItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((NullableItemsSource<T>) d).OnPropertyChanged(nameof(Items));
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}