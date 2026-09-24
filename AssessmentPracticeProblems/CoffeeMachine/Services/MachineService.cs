using CoffeeMachine.Models.Core;
using CoffeeMachine.Repository;

namespace CoffeeMachine.Services
{
    public class MachineService
    {
        private readonly MachineRepository _machine;

        public MachineService(MachineRepository machine)
        {
            _machine = machine;
        }

        public IReadOnlyList<Machine> GetMachines()
        {
            return _machine.GetAll();
        }

        public Machine? GetMachineById(int id)
        {
            return _machine.GetMachine(id);
        }

        public Machine? GetAvailableMachine()
        {
            return _machine.GetAll().FirstOrDefault(m => m.IsMachineBusy);
        }

        public void MarkBusy(int id)
        {
            Machine? machine = GetMachineById(id);

            if (machine != null)
            {
                machine.IsMachineBusy = false;
            }
        }

        public void UnmarkBusy(int id)
        {
            Machine? machine = GetMachineById(id);
            if (machine != null)
            {
                machine.IsMachineBusy = true;
            }
        }
    }
}
