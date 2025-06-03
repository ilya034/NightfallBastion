namespace NightfallBastion.World.ECS.Commands
{
    public interface ICommand
    {
        void Execute(ECSManager ecsManager);
    }
}