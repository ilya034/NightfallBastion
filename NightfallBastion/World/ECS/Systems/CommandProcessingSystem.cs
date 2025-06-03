using System;
using Microsoft.Xna.Framework;
using NightfallBastion.World.ECS.Commands;

namespace NightfallBastion.World.ECS.Systems
{
    public class CommandProcessingSystem : System
    {
        private readonly CommandBus _commandBus;

        public CommandProcessingSystem(GameWorld world, CommandBus commandBus) : base(world)
        {
            _commandBus = commandBus;
        }

        public override void Update(GameTime gameTime)
        {
            // Команды обрабатываются в GameWorld.Update() перед системами
            // Эта система может использоваться для специальной логики обработки команд
            // или для мониторинга количества команд в очереди
            
            // Например, можно логировать, если очередь команд становится слишком большой
            if (_commandBus.QueuedCommandsCount > 100)
            {
                Console.WriteLine($"Warning: Command queue is getting large: {_commandBus.QueuedCommandsCount} commands");
            }
        }
    }
}