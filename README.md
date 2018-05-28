Silverlight.DataForm.UIHint
------------------------------

[![Build status](https://ci.appveyor.com/api/projects/status/3q0346wxjg2bxipu?svg=true)](https://ci.appveyor.com/project/teyc/silverlight-toolkit-dataform-uihint)

This package provides support for UIHint attribute to be used with your Silverlight data forms

Usage:

Via NuGet Package Manager console run

`> Install-Package Silverlight-Toolkit.DataForm.UIHint`

Add `please:Use.UiHint` to your DataForm

        <toolkit:DataForm
            CommandButtonsVisibility="Commit,Cancel"
            CurrentItem="{Binding .}"
            Grid.Column="1"
            Grid.Row="1"
            xmlns:please="clr-namespace:Silverlight.DataForm.UIHint;assembly=Silverlight.DataForm.UIHint"
            please:Use.UiHint="true" />

Next, annotate your properties like below

    [Display(Name = "Required Equipment")] 
    [Required]
    [UIHint("Silverlight.DataForm.UIHint.GenerateComboBox, Silverlight.DataForm.UIHint", "Silverlight", 
        "ItemsSourceProperty", "{Binding Equipments}",
        "DisplayMemberPath", "EquipmentName")]
    public Equipment SelectedEquipment { get; set; }

Let's take it apart and see what it means:

1. The first parameter `"Silverlight.DataForm.UIHint.GenerateComboBox, Silverlight.DataForm.UIHint"`
   identifies the type of control that will be generated, and where the assembly is. Unfortunately,
   just specifying "System.Windows.Controls.ComboxBox, System.Windows.Controls" doesn't work, as
   the control still needs to be data bound to the right fields.

2. The second line `"ItemsSourceProperty", "{Binding Equipments}"` creates a data binding between
    the ItemsSource and the list of equipments that the user can select from.

3. The third line `"DisplayMemberPath", "EquipmentName"` assigns a string to `comboBox.DisplayMemberPath`

Predefined UiHits
==========================

1. Comboboxes

2. List of CheckBoxes


Combobox
-------------------

    [Display(Name = "Required Equipment")] 
    [Required]
    [UIHint("Silverlight.DataForm.UIHint.GenerateComboBox, Silverlight.DataForm.UIHint", "Silverlight", 
        "ItemsSourceProperty", "{Binding Equipments}",
        "DisplayMemberPath", "EquipmentName")]
    public Equipment SelectedEquipment { get; set; }

List of CheckBoxes
-------------------

This example generates a list containing CheckBoxes

        [Display(Name = "Any of these")]
        [UIHint("Silverlight.DataForm.UIHint.GenerateCheckBoxes, Silverlight.DataForm.UIHint", "Silverlight",
            "DisplayMemberPath", "ToppingName",
            "ItemsSourceProperty", "{Binding Toppings}")]
        public BindableCollection<Topping> SelectedToppings { get; set; }

        [Display(AutoGenerateField = false)]
        public BindableCollection<Topping> Toppings { get; set; }

Other Helpers
---------------

Occassionally, you may want to allow the user to leave the item as null. I've given you a NullableItemsSource
class that you can use to bind to a list, and it will add `null` to the beginning of the list.

    [Display(AutoGenerateField = false)]
    public NullableItemsSource<Equipment> OptionalEquipments { get; set; }

    [Display(Name = "Optional equipment")]
    [UIHint("Silverlight.DataForm.UIHint.GenerateComboBox, Silverlight.DataForm.UIHint", "Silverlight",
        "ItemsSourceProperty", "{Binding OptionalEquipments}",
        "DisplayMemberPath", "EquipmentName")]
    public Equipment OptionalEquipment { get; set; }

In your constructor, you can load the NullableItemsSource with a list

    public MyViewModel()
    {
        OptionalEquipments = new NullableItemsSource<Equipment>() {ItemsSource = Equipments};
    }

   