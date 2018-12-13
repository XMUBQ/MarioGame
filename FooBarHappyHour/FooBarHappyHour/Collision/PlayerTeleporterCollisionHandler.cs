using FooBarHappyHour.Factories;
using FooBarHappyHour.Interfaces;
using static FooBarHappyHour.Collision.CollisionDetection;

namespace FooBarHappyHour.Collision
{
    public static class PlayerTeleporterCollisionHandler
    {
        public static void HandleCollision(IPlayer player, ITeleporter teleporter, CollisionSide side)
        {
            if (side == CollisionSide.Top)
            {
                if (player.IsDown && teleporter.OutTeleporter) SuperMarioBros.Instance.GameStateManager.SwitchWorld();
            }
        }
    }
}
