using System;
using System.ComponentModel.DataAnnotations;
using System.Windows.Data;
using Caliburn.Micro;
using Silverlight.DataForm.UIHint;

namespace Prototyping
{
    public class TipsChangeOutViewModel
    {
        private delegate Binding BindingDelegate();

        public TipsChangeOutViewModel()
        {
            Equipments = new BindableCollection<Equipment>();
            Equipments.Add(new Equipment(1, "DRE1"));
            Equipments.Add(new Equipment(2, "DRE2"));
            Equipments.Add(new Equipment(3, "DRE3"));

            OptionalEquipments = new NullableItemsSource<Equipment>() {ItemsSource = Equipments};
        }

        [Display(Name = "Changeout Date")]
        [Required]
        public DateTime? ChangeoutDate { get; set; }

        [Display(AutoGenerateField = false)]
        public BindableCollection<Equipment> Equipments { get; set; }

        [Display(AutoGenerateField = false)]
        public NullableItemsSource<Equipment> OptionalEquipments { get; set; }

        [Display(Name = "Required Equipment")]
        [Required]
        [UIHint("Silverlight.DataForm.UIHint.GenerateComboBox, Silverlight.DataForm.UIHint", "Silverlight", 
            "ItemsSourceProperty", "{Binding Equipments}",
            "DisplayMemberPath", "EquipmentName")]
        public Equipment SelectedEquipment { get; set; }

        [Display(Name = "Optional equipment")]
        [UIHint("Silverlight.DataForm.UIHint.GenerateComboBox, Silverlight.DataForm.UIHint", "Silverlight",
            "ItemsSourceProperty", "{Binding OptionalEquipments}",
            "DisplayMemberPath", "EquipmentName")]
        public Equipment OptionalEquipment { get; set; }
    }
}
