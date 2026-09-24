using CoffeeMachine.Models.Core;
using CoffeeMachine.Models.Enums;

namespace CoffeeMachine.Repository
{
    public class CoffeeRepository
    {
        private List<Coffee> _coffees = new List<Coffee>()
        {
            new Coffee(1, CoffeeType.Americano, 250m, 10),
            new Coffee(2, CoffeeType.Espresso, 350m, 15),
            new Coffee(3, CoffeeType.Cappuccino, 550m, 30),
            new Coffee(4, CoffeeType.Mocha, 200m, 40),
            new Coffee(5, CoffeeType.ColdBrew, 250m, 30),
        };

        public IReadOnlyList<Coffee> GetAll()
        {
            return _coffees;
        }
    }
}
