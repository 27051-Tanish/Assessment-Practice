using CoffeeMachine.Models.Core;

namespace CoffeeMachine.Repository
{
    public class MachineRepository
    {
        private List<Machine> _machines = new List<Machine>()
        {
            new Machine(1, "Machine A", true),
            new Machine(2, "Machine B", true),
            new Machine(3, "Machine C", true),
        };

        public IReadOnlyList<Machine> GetAll()
        {
            return _machines;
        }

        public Machine? GetMachine(int machineId)
        {
            return _machines.Find(m => m.MachineId == machineId);
        }
    }
}
