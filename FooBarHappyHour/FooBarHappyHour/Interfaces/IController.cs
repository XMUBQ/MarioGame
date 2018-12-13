using Microsoft.Xna.Framework;

namespace FooBarHappyHour.Interfaces
{
    public interface IController
    {
        bool PlayerReceivedUserInput { get; }
        void Update(GameTime gameTime);
    }
}
