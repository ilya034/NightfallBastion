using System.Collections.Generic;

namespace NightfallBastion.World.ECS.Commands
{
    public class CommandBus
    {
        private readonly Queue<ICommand> _commandQueue = new();
        private readonly ECSManager _ecsManager;

        public CommandBus(ECSManager ecsManager)
        {
            _ecsManager = ecsManager;
        }

        public void EnqueueCommand(ICommand command)
        {
            _commandQueue.Enqueue(command);
        }

        public void ProcessCommands()
        {
            while (_commandQueue.Count > 0)
            {
                var command = _commandQueue.Dequeue();
                command.Execute(_ecsManager);
            }
        }

        public int QueuedCommandsCount => _commandQueue.Count;

        public void Clear()
        {
            _commandQueue.Clear();
        }
    }
}