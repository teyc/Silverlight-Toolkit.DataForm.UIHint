using System.ComponentModel;
using Silverlight.DataForm.UIHint.Annotations;

namespace Silverlight.DataForm.UIHint
{
    public class SelectedItemWrapper: INotifyPropertyChanged
    {
        private bool _isSelected;

        public SelectedItemWrapper(bool isSelected, object item)
        {
            IsSelected = isSelected;
            Item = item;
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value == _isSelected) return;
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        public object Item { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}