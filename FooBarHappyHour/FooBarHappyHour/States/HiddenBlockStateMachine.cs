using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Factories;

namespace FooBarHappyHour.States
{
    public class HiddenBlockStateMachine
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public bool IsUsed { get; set; }
        private ISprite sprite;

        public HiddenBlockStateMachine()
        {
            sprite = BlockSpriteFactory.Instance.CreateHiddenBlockSprite();
            Width = sprite.Width;
            Height = sprite.Height;
            IsUsed = false;
        }

        public void BecomeUsed()
        {
            IsUsed = true;
            sprite = BlockSpriteFactory.Instance.CreateUsedBlockSprite();
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch, location);
        }
        
    }
}
