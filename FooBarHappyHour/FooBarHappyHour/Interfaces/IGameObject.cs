using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FooBarHappyHour.Interfaces
{
    public interface IGameObject
    {
        Rectangle Rectangle { get; }
        IPhysics Physics { get; }
        bool Collidable { get; set; }
        bool RemovalFlag { get; set; }
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}
