namespace CoffeeMachine.Models.Core
{
    public class Machine
    {
        public Machine(int machineId, string machineName, bool isMachineBusy)
        {
            MachineId = machineId;
            MachineName = machineName;
            IsMachineBusy = isMachineBusy;
        }
        public int MachineId { get; set; }
        public string MachineName { get; set; } = string.Empty;
        public bool IsMachineBusy { get; set; }
        public Guid OrderId { get; set; }
    }
}
