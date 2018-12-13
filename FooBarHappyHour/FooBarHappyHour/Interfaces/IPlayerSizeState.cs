using Microsoft.Xna.Framework;

namespace FooBarHappyHour.Interfaces
{
    public interface IPlayerSizeState
    {
        string StateName { get; }
        void Small();
        void Big();
        void Update(GameTime gameTime);
    }
}
