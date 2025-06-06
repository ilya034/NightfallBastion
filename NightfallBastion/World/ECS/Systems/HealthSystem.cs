using System;
using System.Linq;
using Microsoft.Xna.Framework;
using NightfallBastion.World.ECS.Components;

namespace NightfallBastion.World.ECS.Systems
{
    public class HealthSystem(GameWorld gameWorld) : System(gameWorld)
    {
        public override void Update(GameTime gameTime)
        {
            var entities = _world.ECSManager.GetEntitiesWithComponents<HealthComp>();

            foreach (var entity in entities)
            {
                var healthComp = _world.ECSManager.GetComponent<HealthComp>(entity);

                if (healthComp.CurrentHealth <= 0)
                {
                    if (
                        _world.ECSManager.HasComponent<TilePositionComp>(entity)
                        && _world.ECSManager.HasComponent<BuildingComp>(entity)
                    )
                    {
                        var positionComp = _world.ECSManager.GetComponent<TilePositionComp>(entity);
                        var tilePosition = positionComp.Position;

                        var tileMap = _world
                            .ECSManager.GetEntitiesWithComponents<TileMapComp>()
                            .FirstOrDefault();

                        var tailMapComp = _world.ECSManager.GetComponent<TileMapComp>(tileMap);

                        tailMapComp.TileMap[(int)tilePosition.X, (int)tilePosition.Y].BuildingID = 0;

                        _world.ECSManager.SetComponent(tileMap, tailMapComp);
                    }

                    _world.ECSManager.DestroyEntity(entity);
                    Console.WriteLine($"Entity {entity} destroyed due to health depletion.");
                }
            }
        }
    }
}
