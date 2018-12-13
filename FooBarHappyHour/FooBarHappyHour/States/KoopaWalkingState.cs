using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Enemies;
using FooBarHappyHour.Factories;
using static FooBarHappyHour.Collision.CollisionDetection;
using FooBarHappyHour.Misc;

namespace FooBarHappyHour.States
{
    public class KoopaWalkingState : IKoopaState
    {
        public int Width { get; set; }
        public int Height { get; set; }
        private Koopa koopa;
        private ISprite sprite;
        private bool facingLeft;

        public KoopaWalkingState(Koopa koopa)
        {
            this.koopa = koopa;
            sprite = EnemySpriteFactory.Instance.CreateKoopaMovingLeftSprite();
            Width = sprite.Width;
            Height = sprite.Height;

            koopa.EnemyPhysics.FaceLeft();
            koopa.EnemyPhysics.EnemyWalk();
            koopa.EnemyPhysics.KoopaMove();
            facingLeft = true;
        }

        public void GoLeft()
        {
            koopa.EnemyPhysics.FaceLeft();
            koopa.EnemyPhysics.Run();
            if (!facingLeft)
            {
                sprite = EnemySpriteFactory.Instance.CreateKoopaMovingLeftSprite();
                facingLeft = true;
            }
        }

        public void GoRight()
        {
            koopa.EnemyPhysics.FaceRight();
            koopa.EnemyPhysics.Run();
            if (facingLeft)
            {
                sprite = EnemySpriteFactory.Instance.CreateKoopaMovingRightSprite();
                facingLeft = false;
            }
        }

        public void Jump()
        {
            koopa.EnemyPhysics.Jump();
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
            // Koopa can't be kicked in this state;
        }

        public void ChangeDirection(CollisionSide side)
        {
            if (side == CollisionSide.Left)
            {
                koopa.EnemyPhysics.FaceLeft();
                sprite = EnemySpriteFactory.Instance.CreateKoopaMovingLeftSprite();
                facingLeft = true;
            }
            else if (side == CollisionSide.Right)
            {
                koopa.EnemyPhysics.FaceRight();
                sprite = EnemySpriteFactory.Instance.CreateKoopaMovingRightSprite();
                facingLeft = false;
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
            koopa.EnemyPhysics.EnemyWalk();
        }
    }
}
