using System.Text;
using System.Text.RegularExpressions;
using CoffeeMachine.Models.Core;
using CoffeeMachine.Models.Enums;
using CoffeeMachine.Services;
using CoffeeMachine.View;

namespace CoffeeMachine.Controller
{
    public class CoffeeShopController
    {
        private readonly ConsoleView _view;
        private readonly CoffeeService _coffeeService;
        private readonly MachineService _machineService;
        private readonly OrderService _orderService;

        public CoffeeShopController(ConsoleView view, CoffeeService coffeeService, MachineService machineService, OrderService orderService)
        {
            _view = view;
            _coffeeService = coffeeService;
            _machineService = machineService;
            _orderService = orderService;

            _orderService.OnReady += BackgroundProcess;
        }

        private void BackgroundProcess(string message)
        {
            _view.ShowMessage(message);
        }

        public async Task RunApp()
        {
            int choice;
            MainMenu menu;

            do
            {
                _view.ShowTitle("MAIN MENU");
                StringBuilder menuBuilder = new StringBuilder();
                foreach (MainMenu menuOption in Enum.GetValues<MainMenu>())
                {
                    int optionNumber = (int)menuOption;
                    string readableName = Regex.Replace(menuOption.ToString(), "([a-z])([A-Z])", "$1 $2");
                    menuBuilder.AppendLine($"[{optionNumber}]. {readableName}");
                }
                _view.ShowMessage(menuBuilder.ToString().TrimEnd());
                choice = _view.GetIntInput("Enter your choice: ", "Please enter valid menu choice from the menu [1 to 6]");
                if (!Enum.IsDefined(typeof(MainMenu), choice))
                {
                    _view.ShowMessage("Please enter valid menu choice from the menu [1 to 6]");
                    menu = (MainMenu)(-1);
                    continue;
                }

                menu = (MainMenu)choice;

                switch (menu)
                {
                    case MainMenu.ViewMenu:
                        this.ViewCoffeeMenu();
                        break;
                    case MainMenu.OrderCoffee:
                        this.PlaceNewOrder();
                        break;
                    case MainMenu.ViewOrders:
                        this.ViewOrders();
                        break;
                    case MainMenu.ViewMachines:
                        this.ViewMachines();
                        break;
                    case MainMenu.ViewOrderHistory:
                        this.ViewOrderHistory(); 
                        break;
                    case MainMenu.Exit:
                        break;
                }
            }
            while (menu != MainMenu.Exit);
        }

        private void ViewCoffeeMenu()
        {
            IReadOnlyList<Coffee> coffees = _coffeeService.GetAllCoffee();
            _view.DisplayCoffeeMenu(coffees);
        }

        private void  PlaceNewOrder()
        {
            this.ViewCoffeeMenu();
            int coffeeId = _view.GetIntInput("Select coffee ID to order: ", "Please select from the given coffee menu.");
            _orderService.PlaceOrder(coffeeId);
            _view.ShowMessage("Order added to queue");
        }

        private async Task ProcessOrderBackgroundAsync()
        {
            await _orderService.ProcessOrderAsync();
        }

        private void ViewOrders()
        {
            IReadOnlyCollection<Order> orders = _orderService.GetAllOrders();
            if (orders.Count == 0)
            {
                _view.ShowMessage("There is no orders placed.");
                return;
            }
            _view.DisplayOrders(orders);
        }

        public void ViewOrderHistory()
        {
            IReadOnlyList<Order> orders = _orderService.GetHistory();
            if (orders.Count == 0)
            {
                _view.ShowMessage("There is no orders in the history.");
                return;
            }
            _view.DisplayOrders(orders);
        }

        private void ViewMachines()
        {
            IReadOnlyList<Machine> machines = _machineService.GetMachines();
            _view.DisplayMachines(machines);
        }
    }
}
