using System;
using Microsoft.Xna.Framework;
using NightfallBastion.World.ECS.Components;

namespace NightfallBastion.World.ECS.Systems
{
    public class WeaponSystem(GameWorld gameWorld) : System(gameWorld)
    {
        public override void Update(GameTime gameTime)
        {
            var entities = _world.ECSManager.GetEntitiesWithComponents<
                PositionComp,
                KamikazeWeaponComp,
                TeamComp
            >();

            foreach (var attacker in entities)
            {
                var positionComp = _world.ECSManager.GetComponent<PositionComp>(attacker);
                var weaponComp = _world.ECSManager.GetComponent<KamikazeWeaponComp>(attacker);
                var teamComp = _world.ECSManager.GetComponent<TeamComp>(attacker);

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
                        ) <= weaponComp.SenseRadius
                    )
                    {
                        _world.ECSManager.DestroyEntity(attacker);
                        Console.WriteLine(
                            $"Kamikaze attack at {positionComp.Position} with damage {weaponComp.Damage}"
                        );
                        _world.ECSManager.AddComponent(
                            target,
                            new DamageComp { Damage = weaponComp.Damage }
                        );
                    }
                }
            }
        }
    }
}
