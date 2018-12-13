using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Enemies;
using FooBarHappyHour.Factories;
using static FooBarHappyHour.Collision.CollisionDetection;
using FooBarHappyHour.Misc;
using FooBarHappyHour.Utility;

namespace FooBarHappyHour.States
{
    public class KoopaStompedIdleState : IKoopaState
    {
        public int Width { get; set; }
        public int Height { get; set; }
        
        private Koopa koopa;
        private ISprite sprite;
        private float revivalTimer;

        public KoopaStompedIdleState(Koopa koopa)
        {
            this.koopa = koopa;
            koopa.EnemyCollidable = true;
            sprite = EnemySpriteFactory.Instance.CreateKoopaStompedSprite();
            Width = sprite.Width;
            Height = sprite.Height;
            revivalTimer = Utils.Instance.EnemyTimeToRevive;

            koopa.Physics.ResetMotion();
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
            koopa.State = new KoopaFlippedState(koopa);
        }

        public void BeStomped()
        {
            BeKicked();
        }

        public void BeKicked()
        {
            koopa.State = new KoopaKickedState(koopa);
            SoundFactory.Instance.PlayKickEnemySound();
        }

        public void Revive()
        {
            koopa.State = new KoopaRevivingState(koopa);
        }

        public void ChangeDirection(CollisionSide side)
        {
            // Koopa cannot move while stomped
        }

        public void TakeDamage()
        {
            koopa.BeFlipped();
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch, location);
        }

        public void Update(GameTime gameTime)
        {
            if (revivalTimer <= 0) Revive();
            revivalTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            koopa.Physics.Update(gameTime);
            sprite.Update(gameTime);
        }
    }
}
