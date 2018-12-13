using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FooBarHappyHour.Interfaces;
using FooBarHappyHour.States;
using FooBarHappyHour.Physics;
using static FooBarHappyHour.Collision.CollisionDetection;

namespace FooBarHappyHour.Enemies
{
    public class Koopa : IEnemy
    {
        public Rectangle Rectangle => new Rectangle((int)EnemyPhysics.Location.X, (int)EnemyPhysics.Location.Y, State.Width, State.Height);
        public IPhysics Physics { get => EnemyPhysics; }
        public EnemyPhysics EnemyPhysics { get; private set; }
        public bool Collidable { get => !IsDead; set => Collidable = value; }
        public IKoopaState State { get; set; }
        public bool IsDead { get; set; }
        public bool RemovalFlag { get; set; }
        public bool IsKicked { get => State is KoopaKickedState; set => IsKicked = value; }
        public bool EnemyCollidable { get; set; }

        public Koopa(Vector2 location)
        {
            EnemyPhysics = new EnemyPhysics(location, false, true);
            State = new KoopaWalkingState(this);
            IsDead = false;
            RemovalFlag = false;
            EnemyCollidable = true;
        }

        public void GoLeft()
        {
            State.GoLeft();
        }

        public void GoRight()
        {
            State.GoRight();
        }

        public void Jump()
        {
            State.Jump();
        }

        public void BeStomped()
        {
            State.BeStomped();
        }

        public void BeKicked()
        {
            State.BeKicked();
        }

        public void BeFlipped()
        {
            IsDead = true;
            State.BeFlipped();
        }

        public void ChangeDirection(CollisionSide side)
        {
            State.ChangeDirection(side);
        }

        public void Update(GameTime gameTime)
        {
            State.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            State.Draw(spriteBatch, EnemyPhysics.Location);
        }

        public void TakeDamage()
        {
            State.TakeDamage();
        }
    }
}
