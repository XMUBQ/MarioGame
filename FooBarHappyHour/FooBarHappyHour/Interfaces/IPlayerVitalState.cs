using Microsoft.Xna.Framework;

namespace FooBarHappyHour.Interfaces
{
    public interface IPlayerVitalState
    {
        string StateName { get; }
        void Alive();
        void Dead();
        void Update(GameTime gameTime);
    }
}
