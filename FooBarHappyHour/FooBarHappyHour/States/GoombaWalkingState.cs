using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Enemies;
using FooBarHappyHour.Factories;
using System;
using static FooBarHappyHour.Collision.CollisionDetection;
using FooBarHappyHour.Misc;

namespace FooBarHappyHour.States
{
    public class GoombaWalkingState : IGoombaState
    {
        public int Width { get; set; }
        public int Height { get; set; }
        private Goomba goomba;
        private ISprite sprite;

        public GoombaWalkingState(Goomba goomba)
        {
            this.goomba = goomba;
            sprite = EnemySpriteFactory.Instance.CreateGoombaMovingSprite();
            Width = sprite.Width;
            Height = sprite.Height;

            goomba.EnemyPhysics.EnemyWalk();
            goomba.EnemyPhysics.GoombaMove();
        }

        public void GoLeft()
        {
            goomba.EnemyPhysics.FaceLeft();
            goomba.EnemyPhysics.Run();
        }

        public void GoRight()
        {
            goomba.EnemyPhysics.FaceRight();
            goomba.EnemyPhysics.Run();
        }

        public void Jump()
        {
            goomba.EnemyPhysics.Jump();
        }

        public void ChangeDirection(CollisionSide side)
        {
            if (side == CollisionSide.Left)
            {
                goomba.EnemyPhysics.FaceLeft();
            }
            else if (side == CollisionSide.Right)
            {
                goomba.EnemyPhysics.FaceRight();
            }
        }

        public void BeFlipped()
        {
            goomba.State = new GoombaFlippedState(goomba);
        }

        public void BeStomped()
        {
            goomba.State = new GoombaStompedState(goomba);
        }

        public void TakeDamage()
        {
            goomba.BeFlipped();
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch, location);
        }

        public void Update(GameTime gameTime)
        {
            goomba.Physics.Update(gameTime);
            goomba.EnemyPhysics.GoombaMove();
            sprite.Update(gameTime);
            goomba.EnemyPhysics.EnemyWalk();
        }
    }
}
