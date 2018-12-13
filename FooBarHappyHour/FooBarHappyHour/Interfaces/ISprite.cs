using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FooBarHappyHour.Interfaces
{
    public interface ISprite
    {
        int Width { get; }
        int Height { get; }
        double XRate { get; set; }
        double YRate { get; set; }
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch, Vector2 location);
        void SetTransparent(bool transparency);
    }
}
