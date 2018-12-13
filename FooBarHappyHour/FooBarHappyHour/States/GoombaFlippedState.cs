using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Factories;
using FooBarHappyHour.Enemies;
using static FooBarHappyHour.Collision.CollisionDetection;
using FooBarHappyHour.Utility;

namespace FooBarHappyHour.States
{
    public class GoombaFlippedState : IGoombaState
    {
        public int Width { get; set; }
        public int Height { get; set; }
        private ISprite sprite;
        private Goomba goomba;

        public GoombaFlippedState(Goomba goomba)
        {
            this.goomba = goomba;
            goomba.EnemyCollidable = true;
            sprite = EnemySpriteFactory.Instance.CreateGoombaFlippedSprite();
            Width = sprite.Width;
            Height = sprite.Height;

            goomba.EnemyPhysics.Flip();
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
            // Already flipped, flipped goomba cannot be flipped again
        }

        public void BeStomped()
        {
            // Already flipped, flipped goomba cannot be stomped
        }

        public void ChangeDirection(CollisionSide side)
        {
            // Already flipped, flipped goomba cannot move
        }

        public void TakeDamage()
        {
            // Flipped goomba is already dead, does not take further damage
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch, location);
        }

        public void Update(GameTime gameTime)
        {
            goomba.Physics.Update(gameTime);

            if (goomba.Physics.Location.Y > Utils.Instance.RemovalPosition)
            {
                goomba.RemovalFlag = true;
            }

            sprite.Update(gameTime);
        }
    }
}
