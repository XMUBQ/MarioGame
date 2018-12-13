using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Factories;
using FooBarHappyHour.Enemies;
using static FooBarHappyHour.Collision.CollisionDetection;
using FooBarHappyHour.Utility;

namespace FooBarHappyHour.States
{
    public class GoombaStompedState : IGoombaState
    {
        public int Width { get; set; }
        public int Height { get; set; }
        private ISprite sprite;
        private float disappearTimer;
        private Goomba goomba;

        public GoombaStompedState(Goomba goomba)
        {
            this.goomba = goomba;
            goomba.EnemyCollidable = true;
            sprite = EnemySpriteFactory.Instance.CreateGoombaStompedSprite();
            Width = sprite.Width;
            Height = sprite.Height;
            disappearTimer = Utils.Instance.EnemyTimeToDisappear;

            goomba.Physics.ResetMotion();
            goomba.Physics.Locked = true;
        }

        public void GoLeft()
        {
            // Can't take player input in this state.
        }

        public void GoRight()
        {
            // Can't take player input in this state.
        }

        public void Jump()
        {
            // Can't take player input in this state.
        }

        public void BeFlipped()
        {
            // Already stomped, dead goomba cannot be flipped again
        }

        public void BeStomped()
        {
            // Already stomped, dead goomba cannot be stomped again
        }

        public void ChangeDirection(CollisionSide side)
        {
            // Already stomped, dead goomba no longer moves
        }

        public void TakeDamage()
        {
            // Stomped goomba is already dead, does not take further damage
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch, location);
        }

        public void Update(GameTime gameTime)
        {
            if (disappearTimer <= 0) goomba.RemovalFlag = true;
            disappearTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            sprite.Update(gameTime);
        }
    }
}
