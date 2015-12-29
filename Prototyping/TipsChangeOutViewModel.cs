using System;
using System.ComponentModel.DataAnnotations;
using System.Windows.Data;
using Caliburn.Micro;

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

        }

        [Display(Name = "Changeout Date")]
        [Required]
        public DateTime? ChangeoutDate { get; set; }

        [Display(AutoGenerateField = false)]
        public BindableCollection<Equipment> Equipments { get; set; }

        [Display(Name = "Your Equipment")]
        [Required]
        [UIHint("Prototyping.GenerateComboBox, Prototyping", "Silverlight", 
            "ItemsSourceProperty", "{Binding Equipments}",
            "DisplayMemberPath", "EquipmentName")]
        public Equipment SelectedEquipment { get; set; }
    }
}
