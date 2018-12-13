using Microsoft.Xna.Framework;

namespace FooBarHappyHour.Interfaces
{
    public enum Direction { Left, Right }
    public interface IPlayerAnimationState
    {
        bool IsFacingRight { get; }
        bool IsFacingLeft { get; }
        string StateName { get; }
        string DirectionName { get; }
        void Idle();
        void Left();
        void Right();
        void Up();
        void Down();
        void Update(GameTime gameTime);
    }
}
