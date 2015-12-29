using System.ComponentModel.DataAnnotations;

namespace Prototyping
{
    public class Topping
    {
        public Topping(int toppingId, string toppingName)
        {
            ToppingId = toppingId;
            ToppingName = toppingName;
        }

        public int ToppingId { get; set; }

        public string ToppingName { get; set; }

        public override string ToString()
        {
            return ToppingName;
        }
    }
}