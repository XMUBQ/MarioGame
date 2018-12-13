using FooBarHappyHour.Physics;
using Microsoft.Xna.Framework;

namespace FooBarHappyHour.Interfaces
{
    public interface IPlayerMovementState
    {
        Vector2 Location { get; set; }
        Vector2 Velocity { get; set; }
        PlayerPhysics PlayerPhysics { get; }
        bool InitalJumpAvailable { get; set; }
        bool AirJumpAvailable { get; set; }
        bool TeleportAnimationComplete { get; }
        void Idle();
        void MoveLeft();
        void MoveRight();
        void Jump();
        void ResetJump();
        void Run();
        void Bounce();
        void PlayTeleportUpAnimation();
        void PlayTeleportDownAnimation();
        void CollisionVertical(float verticalDelta);
        void CollisionHorizontal(float horizontalDelta);
        void Update(GameTime gameTime);
    }
}
