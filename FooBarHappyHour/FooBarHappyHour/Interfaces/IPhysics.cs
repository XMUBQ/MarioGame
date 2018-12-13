using Microsoft.Xna.Framework;

namespace FooBarHappyHour.Interfaces
{
    public interface IPhysics
    {
        bool Locked { get; set; }
        bool Gravity { get; set; }
        Vector2 Location { get; set; }
        Vector2 Velocity { get; set; }
        Vector2 Acceleration { get; set; }
        void ResetMotion();
        void CollisionVertical(float verticalDelta);
        void CollisionHorizontal(float horizontalDelta);
        void Update(GameTime gameTime);
    }
}
