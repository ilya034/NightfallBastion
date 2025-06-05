using System.Collections.Generic;
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

            if (map == 0)
                return;

            var tileMapComp = _world.ECSManager.GetComponent<TileMapComp>(map);
            var distanceMapComp = _world.ECSManager.GetComponent<DistanceMapComp>(map);

            distanceMapComp.Distances = new float[tileMapComp.Width, tileMapComp.Height];

            var costMap = new float[tileMapComp.Width, tileMapComp.Height];

            for (int x = 0; x < tileMapComp.Width; x++)
            {
                for (int y = 0; y < tileMapComp.Height; y++)
                    costMap[x, y] = tileMapComp.TileMap[x, y].GetTileWalkCost(_world);
            }

            var core = _world
                .ECSManager.GetEntitiesWithComponents<TilePositionComp, CoreMarkComp>()
                .FirstOrDefault();

            distanceMapComp.Distances = CalculateDistanceMap(
                _world.ECSManager.GetComponent<TilePositionComp>(core).Position,
                costMap
            );

            _world.ECSManager.SetComponent(map, distanceMapComp);
        }

        private float[,] CalculateDistanceMap(Vector2 position, float[,] costMap)
        {
            int coreX = (int)position.X;
            int coreY = (int)position.Y;

            int width = costMap.GetLength(0);
            int height = costMap.GetLength(1);

            var queue = new PriorityQueue<(int x, int y), float>();

            var distanceMap = new float[width, height];
            for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                distanceMap[x, y] = float.PositiveInfinity;

            distanceMap[coreX, coreY] = 0;
            queue.Enqueue((coreX, coreY), 0);

            int[] dx = [0, 0, -1, 1];
            int[] dy = [-1, 1, 0, 0];

            while (queue.Count > 0)
            {
                var (currentX, currentY) = queue.Dequeue();
                var currentDistance = distanceMap[currentX, currentY];

                if (currentDistance > distanceMap[currentX, currentY])
                    continue;

                for (int i = 0; i < 4; i++)
                {
                    int newX = currentX + dx[i];
                    int newY = currentY + dy[i];

                    if (
                        newX < 0
                        || newX >= width
                        || newY < 0
                        || newY >= height
                        || costMap[newX, newY] < 0
                    )
                        continue;

                    var newDistance = currentDistance + costMap[newX, newY];
                    if (newDistance < distanceMap[newX, newY])
                    {
                        distanceMap[newX, newY] = newDistance;
                        queue.Enqueue((newX, newY), newDistance);
                    }
                }
            }

            return distanceMap;
        }
    }
}
