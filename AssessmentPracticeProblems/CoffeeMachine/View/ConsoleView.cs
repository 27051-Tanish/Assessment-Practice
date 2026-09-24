using CoffeeMachine.Models.Core;

namespace CoffeeMachine.View
{
    public class ConsoleView
    {
        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        public string? ReadInput()
        {
            return Console.ReadLine();
        }

        public void ShowTitle(string title)
        {
            this.ShowMessage(new string('=', 35));
            this.ShowMessage($"           {title}");
            this.ShowMessage(new string('=', 35));
        }

        public int GetIntInput(string inputMessage, string errorMessage)
        {
            ShowMessage(inputMessage);
            while (true)
            {
                if (int.TryParse(ReadInput(), out int value))
                {
                    return value;
                }

                ShowMessage(errorMessage);
            }
        }

        public void DisplayCoffeeMenu(IReadOnlyList<Coffee> coffees)
        {
            ShowMessage($"{"Coffee ID",-7} | {"Coffee Name",-15} | {"Price",-10} | {"Preparation time",-16}|");
            ShowMessage(new string('-', 63));
            foreach (Coffee coffee in coffees)
            {

                ShowMessage($" {coffee.CoffeeId,-7} | {coffee.CoffeeName,-15} | {coffee.CoffeeAmount,-10} | {coffee.PreparationTime,-16} |");
                ShowMessage(new string('-', 63));
            }
        }

        public void DisplayOrders(IReadOnlyCollection<Order> orders)
        {
            ShowMessage($"{"Order ID",-7} | {"Coffee Name",-15} | {"Status",-10} | {"Ordered time",-16}|");
            ShowMessage(new string('-', 63));
            foreach (Order order in orders)
            {
                string shortId = order.OrderId.ToString()[..7];

                ShowMessage($" {shortId,-7} | {order.CoffeeId,-15} | {order.Status,-10} | {order.OrderedTime,-16} |");
                ShowMessage(new string('-', 63));
            }
        }

        public void DisplayMachines(IReadOnlyList<Machine> machines)
        {
            ShowMessage($"{"Machine ID",-7} | {"Machine Name",-15} | {"Status",-10} |");
            ShowMessage(new string('-', 40));
            foreach (Machine machine in machines)
            {
                string machineAvailability = (machine.IsMachineBusy) ? "Available" : "Not Available";
                ShowMessage($" {machine.MachineId,-7} | {machine.MachineName,-15} | {machineAvailability,-10} |");
                ShowMessage(new string('-', 40));
            }
        }
    }
}
