using CoffeeMachine.Models.Core;
using CoffeeMachine.Repository;

namespace CoffeeMachine.Services
{
    public class CoffeeService
    {
        private CoffeeRepository _coffee;

        public CoffeeService(CoffeeRepository coffee)
        {
            _coffee = coffee;
        }

        public IReadOnlyList<Coffee> GetAllCoffee()
        {
            return _coffee.GetAll();
        }

        public Coffee? GetCoffeeById(int id)
        {
            return _coffee.GetAll().FirstOrDefault(c => c.CoffeeId == id);
        }
    }
}
