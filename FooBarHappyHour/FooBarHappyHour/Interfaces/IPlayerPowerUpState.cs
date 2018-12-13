using Microsoft.Xna.Framework;

namespace FooBarHappyHour.Interfaces
{
    public interface IPlayerPowerUpState
    {
        string StateName { get; }
        void Normal();
        void Super();
        void Firey();
        void Invincible();
        void Update(GameTime gameTime);
    }
}
