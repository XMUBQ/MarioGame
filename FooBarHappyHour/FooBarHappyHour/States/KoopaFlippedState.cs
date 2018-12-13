using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Factories;
using FooBarHappyHour.Enemies;
using FooBarHappyHour.Collision;
using static FooBarHappyHour.Collision.CollisionDetection;
using FooBarHappyHour.Utility;

namespace FooBarHappyHour.States
{
    public class KoopaFlippedState : IKoopaState
    {
        public int Width { get; set; }
        public int Height { get; set; }
        private ISprite sprite;
        private Koopa koopa;

        public KoopaFlippedState(Koopa koopa)
        {
            this.koopa = koopa;
            koopa.EnemyCollidable = true;
            sprite = EnemySpriteFactory.Instance.CreateKoopaFlippedSprite();
            Width = sprite.Width;
            Height = sprite.Height;
            koopa.Physics.Location += new Vector2(0, -Utils.Instance.EnemyFlipHeight);
            koopa.IsDead = true;
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
            // Already flipped, cannot be flipped again
        }

        public void BeStomped()
        {
            // Flipped koopa cannot be stomped
        }

        public void BeKicked()
        {
            // Koopa can't be kicked in this state
        }

        public void ChangeDirection(CollisionSide side)
        {
            // Flipped koopa cannot move
        }

        public void TakeDamage()
        {
            // Flipped Koopa does not take any further damage
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch, location);
        }

        public void Update(GameTime gameTime)
        {
            if (koopa.Physics.Location.Y > Utils.Instance.RemovalPosition)
            {
                koopa.RemovalFlag = true;
            }
            koopa.Physics.Update(gameTime);
            sprite.Update(gameTime);
        }
    }
}
