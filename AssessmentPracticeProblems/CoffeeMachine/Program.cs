using CoffeeMachine.Controller;
using CoffeeMachine.Repository;
using CoffeeMachine.Services;
using CoffeeMachine.View;

namespace CoffeeMachine
{
    public class Program
    {
        public static async Task Main()
        {
            ConsoleView view = new ConsoleView();

            CoffeeRepository coffeeRepository = new CoffeeRepository();
            CoffeeService coffeeService = new CoffeeService(coffeeRepository);

            MachineRepository machineRepository = new MachineRepository();
            MachineService machineService = new MachineService(machineRepository);

            OrderRepository orderRepository = new OrderRepository();
            OrderService orderService = new OrderService(orderRepository, coffeeService, machineService);

            CoffeeShopController controller = new CoffeeShopController(view, coffeeService, machineService, orderService);
            await controller.RunApp();

        }
    }
}
