using CoffeeMachine.Models.Enums;

namespace CoffeeMachine.Models.Core
{
    public class Order
    {
        public Order(Guid orderId, int coffeeId, OrderStatus status, DateTime orderedTime)
        {
            OrderId = orderId;
            CoffeeId = coffeeId;
            Status = status;
            OrderedTime = orderedTime;
        }

        public Guid OrderId { get; set; }
        public int CoffeeId { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime OrderedTime { get; set; }

    }
}
