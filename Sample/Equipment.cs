namespace Prototyping
{
    public class Equipment
    {
        public Equipment(int id, string equipmentName)
        {
            EquipmentId = id;
            EquipmentName = equipmentName;
        }

        public int EquipmentId { get; set; }

        public string EquipmentName { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Equipment && ((Equipment)obj).EquipmentId == EquipmentId) return true;

            return base.Equals(obj);
        }
    }
}