using CoffeeMachine.Models.Core;

namespace CoffeeMachine.Repository
{
    public class OrderRepository
    {
        private List<Order> _orders = new List<Order>();

        public IReadOnlyList<Order> GetOrdersHistory()
        {
            return _orders;
        }

        public void AddOrderHistory(Order order)
        {
            _orders.Add(order);
        }
        public Order? GetOrder(Guid id)
        {
            return _orders.Find(o => o.OrderId == id);
        }
    }
}
