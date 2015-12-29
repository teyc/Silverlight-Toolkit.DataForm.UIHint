using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Silverlight.DataForm.UIHint
{
    public partial class CheckedListBox : UserControl
    {
        // Using a DependencyProperty as the backing store for DisplayMemberPath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisplayMemberPathProperty =
            DependencyProperty.Register("DisplayMemberPath", typeof (string), typeof (CheckedListBox),
                new PropertyMetadata(null, DisplayMemberPathChanged));

        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.Register("SelectedItems", typeof (IList), typeof (CheckedListBox),
                new PropertyMetadata(null, SelectedItemsChanged));

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof (IEnumerable<object>), typeof (CheckedListBox),
                new PropertyMetadata(null, ItemsSourceChanged));

        public CheckedListBox()
        {
            InitializeComponent();

        }

        public string DisplayMemberPath
        {
            get { return (string) GetValue(DisplayMemberPathProperty); }
            set { SetValue(DisplayMemberPathProperty, value); }
        }

        public IList SelectedItems
        {
            get { return (IList) GetValue(SelectedItemsProperty); }
            set { SetValue(SelectedItemsProperty, value); }
        }

        public IEnumerable<object> ItemsSource
        {
            get { return (IEnumerable<object>) GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        private static void DisplayMemberPathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var checkedListBox = (CheckedListBox) d;

            UpdateItemsSource(checkedListBox, checkedListBox.ItemsSource, checkedListBox.SelectedItems);
        }

        private static void SelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var checkedListBox = (CheckedListBox) d;
            var newSelectedItems = (IList) e.NewValue;

            UpdateItemsSource(checkedListBox, checkedListBox.ItemsSource, newSelectedItems);
        }

        private static void ItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var checkedListBox = (CheckedListBox) d;
            var newItemsSource = (IEnumerable<object>) e.NewValue;

            UpdateItemsSource(checkedListBox, newItemsSource, checkedListBox.SelectedItems);
        }

        private static void UpdateItemsSource(CheckedListBox checkedListBox, IEnumerable<object> newItemsSource,
            IList newSelectedItems)
        {
            var listBox = checkedListBox.listBox;

            listBox.ItemsSource?.OfType<SelectedItemWrapper>()
                .ToList()
                .ForEach(x => x.PropertyChanged -= checkedListBox.SelectionChanged);

            listBox.ItemsSource = newItemsSource?.Select(item => new SelectedItemWrapper(
                newSelectedItems != null && newSelectedItems.Cast<object>().Contains(item),
                item))
                .ToList();
            ;

            listBox.ItemsSource?.OfType<SelectedItemWrapper>()
                .ToList()
                .ForEach(x => x.PropertyChanged += checkedListBox.SelectionChanged);

            listBox.ItemTemplate = CreateItemTemplate(checkedListBox.DisplayMemberPath);
        }

        private void SelectionChanged(object sender, PropertyChangedEventArgs e)
        {
            SelectedItems.Clear();
            listBox.ItemsSource
                .Cast<SelectedItemWrapper>()
                .Where(x => x.IsSelected)
                .Select(x => x.Item)
                .ToList()
                .ForEach(x => SelectedItems.Add(x));
        }

        private static DataTemplate CreateItemTemplate(string displayMemberPath)
        {
            return
                (DataTemplate)
                    XamlReader.Load(
                        $@"
        <DataTemplate xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"" >
            <CheckBox IsChecked=""{{Binding IsSelected, Mode=TwoWay}}"" 
                      Content=""{{Binding Item.{
                            displayMemberPath}, Mode=TwoWay}}"" />
        </DataTemplate> ");
        }
    }
}