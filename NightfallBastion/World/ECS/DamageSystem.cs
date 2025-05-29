using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace NightfallBastion.World
{
    public class DamageSystem(GameWorld world) : System(world)
    {
        private readonly List<Entity> _damageEntities = [];
        private MouseState _previousMouseState;

        public override void Update(GameTime gameTime)
        {
            ProcessExistingDamageComponents();
            CheckMouseInput();
        }

        private void ProcessExistingDamageComponents()
        {
            foreach (var entity in _damageEntities)
            {
                var damageComponent = _world.ECSManager.GetComponent<DamageComponent>(entity);
                if (damageComponent == null || damageComponent.IsApplied)
                    continue;

                var tileMap = _world.TileMap;
                if (tileMap == null)
                    continue;

                if (
                    damageComponent.TilePosition.X < 0
                    || damageComponent.TilePosition.X >= tileMap.Width
                    || damageComponent.TilePosition.Y < 0
                    || damageComponent.TilePosition.Y >= tileMap.Height
                )
                    continue;

                var tile = tileMap.GetTile(
                    damageComponent.TilePosition.X,
                    damageComponent.TilePosition.Y
                );
                if (tile != null)
                {
                    bool damaged = tile.TakeDamage(damageComponent.DamageAmount);

                    damageComponent.IsApplied = true;

                    if (damaged)
                    {
                        Console.WriteLine(
                            $"Tile at {damageComponent.TilePosition} took {damageComponent.DamageAmount} damage. "
                                + $"Current health: {tile.CurrentHealth}/{tile.MaxHealth}, "
                                + $"Building type: {tile.Building?.GetType().Name ?? "None"}"
                        );
                    }
                }
            }

            for (int i = _damageEntities.Count - 1; i >= 0; i--)
            {
                var entity = _damageEntities[i];
                var damageComponent = _world.ECSManager.GetComponent<DamageComponent>(entity);
                if (damageComponent != null && damageComponent.IsApplied)
                {
                    _world.ECSManager.DestroyEntity(entity);
                    _damageEntities.RemoveAt(i);
                }
            }
        }

        private void CheckMouseInput()
        {
            var currentMouseState = Mouse.GetState();

            if (
                currentMouseState.LeftButton == ButtonState.Pressed
                && _previousMouseState.LeftButton == ButtonState.Released
            )
            {
                var mousePosition = new Point(currentMouseState.X, currentMouseState.Y);

                int tileSize = _world.Game.CoreSettings.DefaultTileSize;
                var worldPos = _world.ScreenToWorld(new Vector2(mousePosition.X, mousePosition.Y));

                var tileX = (int)(worldPos.X / tileSize);
                var tileY = (int)(worldPos.Y / tileSize);

                var damageEntity = _world.ECSManager.CreateEntity();
                var damageComponent = new DamageComponent(new Point(tileX, tileY), 25);

                _world.ECSManager.AddComponent(damageEntity, damageComponent);
                _damageEntities.Add(damageEntity);

                Console.WriteLine($"Create damage at tile {tileX}, {tileY}");
            }

            _previousMouseState = currentMouseState;
        }
    }
}
