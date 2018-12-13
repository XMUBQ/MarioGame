using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FooBarHappyHour.Interfaces;
using FooBarHappyHour.States;
using FooBarHappyHour.Physics;
using static FooBarHappyHour.Collision.CollisionDetection;
using FooBarHappyHour.Factories;
using FooBarHappyHour.Score;
using FooBarHappyHour.Utility;

namespace FooBarHappyHour.Enemies
{
    public class Goomba : IEnemy
    {
        public Rectangle Rectangle => new Rectangle((int)EnemyPhysics.Location.X, (int)EnemyPhysics.Location.Y, State.Width, State.Height);
        public IPhysics Physics { get => EnemyPhysics; }
        public EnemyPhysics EnemyPhysics { get; private set; }
        public bool Collidable { get => !IsDead; set => Collidable = value; }
        public IGoombaState State { get; set; }
        public bool IsDead { get; set; }
        public bool RemovalFlag { get; set; }
        public bool EnemyCollidable { get; set; }

        public Goomba(Vector2 location)
        {
            EnemyPhysics = new EnemyPhysics(location, false, true);
            State = new GoombaWalkingState(this);
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

        public void ChangeDirection(CollisionSide side)
        {
            State.ChangeDirection(side);
        }

        public void BeStomped()
        {
            IsDead = true;
            State.BeStomped();
        }

        public void BeFlipped()
        {
            IsDead = true;
            State.BeFlipped();
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
