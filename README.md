Silverlight.DataForm.UIHint
------------------------------

This package provides support for UIHint attribute to be used with your Silverlight data forms

Usage:

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

