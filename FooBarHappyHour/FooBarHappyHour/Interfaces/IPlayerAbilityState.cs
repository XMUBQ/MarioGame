using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FooBarHappyHour.Interfaces
{
    public interface IPlayerAbilityState
    {
        void UseAbility();
        void Run();
        void ShootFire();
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}