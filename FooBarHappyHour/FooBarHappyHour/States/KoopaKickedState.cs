using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Enemies;
using FooBarHappyHour.Factories;
using static FooBarHappyHour.Collision.CollisionDetection;
using System;
using FooBarHappyHour.Misc;

namespace FooBarHappyHour.States
{
    public class KoopaKickedState : IKoopaState
    {
        public int Width { get; set; }
        public int Height { get; set; }
        private Koopa koopa;
        private ISprite sprite;

        public KoopaKickedState(Koopa koopa)
        {
            this.koopa = koopa;
            koopa.EnemyCollidable = true;
            sprite = EnemySpriteFactory.Instance.CreateKoopaStompedSprite();
            Width = sprite.Width;
            Height = sprite.Height;

            koopa.EnemyPhysics.KoopaKicked();
            koopa.EnemyPhysics.KoopaMove();
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
            koopa.State = new KoopaStompedIdleState(koopa);
            SoundFactory.Instance.PlayStompEnemySound();
        }

        public void BeKicked()
        {
            // Koopa can't be kicked in this state
        }

        public void ChangeDirection(CollisionSide side)
        {
            if (side == CollisionSide.Left)
            {
                koopa.EnemyPhysics.FaceLeft();
            }
            else if (side == CollisionSide.Right)
            {
                koopa.EnemyPhysics.FaceRight();
            }
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
            koopa.Physics.Update(gameTime);
            koopa.EnemyPhysics.KoopaMove();
            sprite.Update(gameTime);
        }
    }
}
