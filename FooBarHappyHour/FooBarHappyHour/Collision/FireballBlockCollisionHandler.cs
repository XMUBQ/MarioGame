using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Misc;
using static FooBarHappyHour.Collision.CollisionDetection;

namespace FooBarHappyHour.Collision
{
    public static class FireballBlockCollisionHandler
    {
        public static void HandleCollision(Fireball fireball, IGameObject block, CollisionSide side)
        {
            if (fireball.Collidable && block.Collidable)
            {
                switch (side)
                {
                    case CollisionSide.Left:
                    case CollisionSide.Right:
                        fireball.Explode();
                        Factories.SoundFactory.Instance.PlayBumpBlockSound();
                        break;
                    case CollisionSide.Top:
                        fireball.Physics.CollisionVertical(block.Physics.Location.Y - fireball.Rectangle.Height);
                        fireball.Jump();
                        break;
                    case CollisionSide.Bottom:
                        fireball.Physics.CollisionVertical(block.Physics.Location.Y - fireball.Rectangle.Height);
                        break;
                }
            }
        }
    }
}
