using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FooBarHappyHour.Sprites
{
    class DynamicSprite : ISprite
    {
        public int Width { get; }
        public int Height { get; }
        public double XRate { get; set; }
        public double YRate { get; set; }
        private readonly Texture2D texture;
        private int currentFrame;
        private readonly int totalFrames;
        private double currentTime;
        private double frameTime;
        private bool transparency;

        // Constructor for sprites with standard framerate and full opacity
        public DynamicSprite(Texture2D texture, int totalFrames)
        {
            this.texture = texture;
            this.totalFrames = totalFrames;
            currentFrame = 0;
            Width = texture.Width / totalFrames;
            Height = texture.Height;
            frameTime = Utils.Instance.FrameTime;
            XRate = 1;
            YRate = 1;
        }

        // Constructor for sprites with custom framerate and opacity
        public DynamicSprite(Texture2D texture, int totalFrames, double frameTime, bool transparency)
        {
            this.texture = texture;
            this.totalFrames = totalFrames;
            this.transparency = transparency;
            this.frameTime = frameTime;
            currentFrame = 0;
            Width = texture.Width / totalFrames;
            Height = texture.Height;
            XRate = 1;
            YRate = 1;
        }

        public void Update(GameTime gameTime)
        {
            currentTime += gameTime.ElapsedGameTime.TotalSeconds;
            if (currentTime > frameTime)
            {
                currentFrame++;
                currentTime = 0d;
            }
            if (currentFrame == totalFrames)
            {
                currentFrame = 0;
            }  
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            Rectangle source = new Rectangle(Width * currentFrame, 0, (int)(Width*XRate), (int)(Height*YRate));
            Rectangle destination = new Rectangle((int)location.X, (int)location.Y, (int)(Width * XRate), (int)(Height * YRate));
            Color spriteColor = transparency ? Color.White * 0.5f : Color.White;
            spriteBatch.Draw(texture, destination, source, spriteColor);
        }

        public void SetTransparent(bool isTransparent)
        {
            transparency = isTransparent;
        }

    }
}
