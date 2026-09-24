using CoffeeMachine.Models.Enums;

namespace CoffeeMachine.Models.Core
{
    public class Coffee
    {
        public Coffee(int coffeeId, CoffeeType coffeeName, decimal coffeeAmount, int preparationTime)
        {
            CoffeeId = coffeeId;
            CoffeeName = coffeeName;
            CoffeeAmount = coffeeAmount;
            PreparationTime = preparationTime;
        }

        public int CoffeeId { get; set; }
        public CoffeeType CoffeeName { get; set; }
        public decimal CoffeeAmount { get; set; }
        public int PreparationTime { get; set; }
    }
}
