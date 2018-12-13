using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Factories;

namespace FooBarHappyHour.States
{
    public class QuestionBlockStateMachine
    {
        public int Width { get; set; }
        public int Height { get; set; }
        private ISprite sprite;

        public QuestionBlockStateMachine()
        {
            sprite = BlockSpriteFactory.Instance.CreateQuestionBlockSprite();
            Width = sprite.Width;
            Height = sprite.Height;
        }
        public void Update(GameTime gameTime)
        {
            sprite.Update(gameTime);
        }
        public void BecomeUsed()
        {
            sprite = BlockSpriteFactory.Instance.CreateUsedBlockSprite();
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch, location);
        }
    }
}
