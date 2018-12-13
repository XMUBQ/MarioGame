using FooBarHappyHour.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FooBarHappyHour.Sprites
{
    class StaticSprite : ISprite
    {
        public int Width { get; }
        public int Height { get; }
        public double XRate { get; set; }
        public double YRate { get; set; }
        private readonly Texture2D texture;
        private bool transparency;

        public StaticSprite(Texture2D texture)
        {
            this.texture = texture;
            Width = texture.Width;
            Height = texture.Height;
            XRate = 1;
            YRate = 1;
        }

        public void Update(GameTime gameTime)
        {
            // Does not update anything for a static sprite.
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            Color spriteColor = transparency ? Color.White * 0.5f : Color.White;
            spriteBatch.Draw(texture, location.ToPoint().ToVector2(), spriteColor);
        }

        public void SetTransparent(bool isTransparent)
        {
            transparency = isTransparent;
        }
    }
}
