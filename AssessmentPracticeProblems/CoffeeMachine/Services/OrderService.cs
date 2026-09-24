using System.Timers;
using CoffeeMachine.Models.Core;
using CoffeeMachine.Models.Enums;
using CoffeeMachine.Repository;

namespace CoffeeMachine.Services
{
    public class OrderService
    {
        public event Action<string> OnReady;
        private Queue<Order> _orderQueue;
        private OrderRepository _order;
        private readonly CoffeeService _coffeeService;
        private readonly MachineService _machineService;
        private bool _isLoopRunning = false;

        private readonly object _locker = new object();

        public OrderService(OrderRepository order, CoffeeService coffee, MachineService machineService)
        {
            _orderQueue = new Queue<Order>();
            _order = order;
            _coffeeService = coffee;
            _machineService = machineService;
        }

        public IReadOnlyCollection<Order> GetAllOrders()
        {
            return _orderQueue;
        }
        
        public IReadOnlyList<Order> GetHistory()
        {
            return _order.GetOrdersHistory();
        }

        public void PlaceOrder(int coffeeId)
        {
            Coffee? coffee = _coffeeService.GetCoffeeById(coffeeId);
            if (coffee != null)
            {
                Order order = new Order(Guid.NewGuid(), coffee.CoffeeId, OrderStatus.Pending, DateTime.Now);
                lock (_locker)
                {
                    _orderQueue.Enqueue(order);
                    if (_isLoopRunning) return;
                    _isLoopRunning = true;
                }

                _ = ProcessOrderAsync();
            }
        }


        public async Task ProcessOrderAsync()
        {
            try
            {
                while (true)
                {
                    lock (_locker)
                    {
                        if (_orderQueue.Count == 0)
                        {
                            return;
                        }
                    }

                    Machine? machine = _machineService.GetAvailableMachine();

                    if (machine == null)
                    {
                        // Keep checking throughout execution every second if machines are busy
                        await Task.Delay(1000);
                        continue;
                    }

                    Order order;
                    lock (_locker)
                    {
                        if (_orderQueue.Count == 0) continue;
                        order = _orderQueue.Dequeue();
                    }

                    Coffee? coffee = _coffeeService.GetCoffeeById(order.CoffeeId);
                    if (coffee == null) continue;

                    _machineService.MarkBusy(machine.MachineId);
                    await Task.Delay(coffee.PreparationTime * 1000);

                    order.Status = OrderStatus.Completed;
                    _machineService.UnmarkBusy(machine.MachineId);
                    _order.AddOrderHistory(order);

                    OnReady?.Invoke($"{coffee.CoffeeName} is ready, [{machine.MachineName} is now Available]");
                }
            }
            finally
            {
                lock( _locker)
                {
                    _isLoopRunning = false;
                }
            }
        }

        public void GetOrderById(Guid id)
        {
            _order.GetOrder(id);
        }
    }
}
