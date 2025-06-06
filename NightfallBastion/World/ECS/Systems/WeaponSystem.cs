using System;
using System.Linq;
using Microsoft.Xna.Framework;
using NightfallBastion.Utilities;
using NightfallBastion.World.ECS.Components;

namespace NightfallBastion.World.ECS.Systems
{
    public class WeaponSystem(GameWorld gameWorld) : System(gameWorld)
    {
        public override void Update(GameTime gameTime)
        {
            var entities = _world.ECSManager.GetEntitiesWithComponents<
                PositionComp,
                WeaponComp,
                TeamComp
            >();

            foreach (var attacker in entities)
            {
                var positionComp = _world.ECSManager.GetComponent<PositionComp>(attacker);
                var weaponComp = _world.ECSManager.GetComponent<WeaponComp>(attacker);
                var teamComp = _world.ECSManager.GetComponent<TeamComp>(attacker);

                if (weaponComp.Type == WeaponType.Kamikaze)
                {
                    var targets = _world.ECSManager.GetEntitiesWithComponents<
                        TilePositionComp,
                        HealthComp,
                        TeamComp
                    >();

                    foreach (var target in targets)
                    {
                        var targetTeamComp = _world.ECSManager.GetComponent<TeamComp>(target);
                        var targetPositionComp = _world.ECSManager.GetComponent<TilePositionComp>(
                            target
                        );

                        if (
                            targetTeamComp.Team != teamComp.Team
                            && Vector2.Distance(
                                positionComp.Position,
                                _world.TileToWorld(targetPositionComp.Position)
                            ) <= weaponComp.Range
                        )
                        {
                            _world.ECSManager.DestroyEntity(attacker);
                            Console.WriteLine(
                                $"Kamikaze attack at {positionComp.Position} with damage {weaponComp.Damage}"
                            );
                            EntitiesFactory.CreateDamage(
                                _world,
                                positionComp.Position,
                                weaponComp.Damage,
                                weaponComp.Range * 2,
                                1f
                            );
                        }
                    }
                }
            }
        }
    }
}
