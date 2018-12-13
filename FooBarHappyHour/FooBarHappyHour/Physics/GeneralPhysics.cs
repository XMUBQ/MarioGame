using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Utility;
using Microsoft.Xna.Framework;
using System;
using static FooBarHappyHour.Collision.CollisionDetection;

namespace FooBarHappyHour.Physics
{
    public abstract class GeneralPhysics : IPhysics
    {
        public bool Locked { get; set; }
        public bool Gravity { get; set; }
        public Vector2 Location { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }
        public Vector2 OriginalLocation { get; protected set; }

        public void ResetMotion()
        {
            Velocity = Vector2.Zero;
            Acceleration = Vector2.Zero;
        }
        
        public bool IsMovingDown()
        {
            return Velocity.Y > 0;
        }

        public bool IsMovingUp()
        {
            return Velocity.Y < 0;
        }

        public void CollisionVertical(float verticalDelta)
        {
            Location = new Vector2(Location.X, verticalDelta);
            Velocity = new Vector2(Velocity.X, 0f);
            Acceleration = new Vector2(Acceleration.X, 0f);
        }

        public void CollisionHorizontal(float horizontalDelta)
        {
            Location = new Vector2(horizontalDelta, Location.Y);
            Velocity = new Vector2(0f, Velocity.Y);
            Acceleration = new Vector2(0f, Acceleration.Y);
        }

        public static void RepelObject(IGameObject collidedObject, IGameObject colliderObject, CollisionSide side)
        {
            switch (side)
            {
                case CollisionSide.Top:
                    colliderObject.Physics.CollisionVertical(collidedObject.Physics.Location.Y - colliderObject.Rectangle.Height);
                    break;
                case CollisionSide.Left:
                    colliderObject.Physics.CollisionHorizontal(collidedObject.Physics.Location.X - colliderObject.Rectangle.Width);
                    break;
                case CollisionSide.Right:
                    colliderObject.Physics.CollisionHorizontal(collidedObject.Physics.Location.X + collidedObject.Rectangle.Width);
                    break;
                case CollisionSide.Bottom:
                    colliderObject.Physics.CollisionVertical(collidedObject.Physics.Location.Y + collidedObject.Rectangle.Height);
                    break;
            }
        }

        public void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (!Locked)
            {
                if (Gravity && Velocity.Y < Utils.Instance.TerminalVelocity)
                {
                    Velocity += new Vector2(0f, Utils.Instance.GravityConstant) * deltaTime;
                }
                Velocity += Acceleration * deltaTime;
                Location += Velocity * deltaTime;
            }
        }
    }
}
