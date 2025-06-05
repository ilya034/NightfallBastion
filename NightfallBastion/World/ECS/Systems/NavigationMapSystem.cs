using System.Collections.Generic;
using Microsoft.Xna.Framework;
using NightfallBastion.World.ECS.Components;

namespace NightfallBastion.World.ECS.Systems
{
    public class NavigationMapSystem : System
    {
        public NavigationMapSystem(GameWorld world) : base(world) { }

        public override void Update(GameTime gameTime)
        {
            var navEntities = _world.ECSManager.GetEntitiesWithComponents<NavigationMapComp, TileMapComp>();
            if (navEntities.Count == 0) return;

            var navEntity = navEntities[0];
            var navMap = _world.ECSManager.GetComponent<NavigationMapComp>(navEntity);
            var tileMap = _world.ECSManager.GetComponent<TileMapComp>(navEntity);

            if (!navMap.IsDirty) return;

            // Assume core is at center
            int coreX = tileMap.Width / 2;
            int coreY = tileMap.Height / 2;
            int[,] distanceMap = new int[tileMap.Width, tileMap.Height];
            for (int x = 0; x < tileMap.Width; x++)
                for (int y = 0; y < tileMap.Height; y++)
                    distanceMap[x, y] = int.MaxValue;

            var queue = new Queue<Point>();
            queue.Enqueue(new Point(coreX, coreY));
            distanceMap[coreX, coreY] = 0;

            int[] dx = { 0, 1, 0, -1 };
            int[] dy = { -1, 0, 1, 0 };

            while (queue.Count > 0)
            {
                var p = queue.Dequeue();
                for (int d = 0; d < 4; d++)
                {
                    int nx = p.X + dx[d];
                    int ny = p.Y + dy[d];
                    if (nx < 0 || ny < 0 || nx >= tileMap.Width || ny >= tileMap.Height)
                        continue;
                    if (tileMap.TileMap[nx, ny].BuildingID != 0)
                        continue; // Wall or obstacle
                    if (distanceMap[nx, ny] > distanceMap[p.X, p.Y] + 1)
                    {
                        distanceMap[nx, ny] = distanceMap[p.X, p.Y] + 1;
                        queue.Enqueue(new Point(nx, ny));
                    }
                }
            }

            navMap.DistanceMap = distanceMap;
            navMap.IsDirty = false;
            _world.ECSManager.SetComponent(navEntity, navMap);

            // --- Pathfinding for enemies ---
            // Mark all enemy entities with PathfindingComp as dirty if navMap.isDirty was true
            var enemyEntities = _world.ECSManager.GetEntitiesWithComponents<EnemyComp, PositionComp, PathfindingComp>();
            foreach (var enemy in enemyEntities)
            {
                var pathComp = _world.ECSManager.GetComponent<PathfindingComp>(enemy);
                pathComp.Path = new List<Vector2>(); // Invalidate path, will be recalculated in PathfindSystem
                _world.ECSManager.AddComponent(enemy, pathComp); // Use AddComponent to update
            }
        }
    }
}
