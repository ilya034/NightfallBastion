using System.Linq;
using Microsoft.Xna.Framework;
using NightfallBastion.World.ECS.Components;

namespace NightfallBastion.World.ECS.Systems
{
    public class DistanceMapSystem(GameWorld world) : System(world)
    {
        public override void Update(GameTime gameTime)
        {
            var map = _world
                .ECSManager.GetEntitiesWithComponents<TileMapComp, DistanceMapComp>()
                .FirstOrDefault();

            if (map == null)
                return;

            var tileMap = _world.ECSManager.GetComponent<TileMapComp>(map);
            var distanceMap = _world.ECSManager.GetComponent<DistanceMapComp>(map);

            distanceMap.Distances = new int[tileMap.Width, tileMap.Height];

            var costMap = new float[tileMap.Width, tileMap.Height];

            for (int x = 0; x < tileMap.Width; x++)
            {
                for (int y = 0; y < tileMap.Height; y++)
                    costMap[x, y] = tileMap.TileMap[x, y].GetTileWalkCost(_world);
            }

            var core = _world
                .ECSManager.GetEntitiesWithComponents<TilePositionComp, CoreMarkComp>()
                .FirstOrDefault();

            
        }
    }
}
