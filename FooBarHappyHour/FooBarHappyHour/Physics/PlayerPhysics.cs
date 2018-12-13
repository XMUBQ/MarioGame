using FooBarHappyHour.Utility;
using Microsoft.Xna.Framework;
using System;

namespace FooBarHappyHour.Physics
{
    public class PlayerPhysics : GeneralPhysics
    {
        private float horizontalAcceleration;
        private float maxSpeed;
        private bool isRunning;

        public PlayerPhysics(Vector2 location, bool locked, bool gravity)
        {
            Locked = locked;
            Gravity = gravity;
            Location = location;
            Velocity = new Vector2();
            Acceleration = new Vector2();
            OriginalLocation = location;
        }

        public void Idle()
        {
            Acceleration = new Vector2(0, Utils.Instance.GravityConstant);
            Velocity = new Vector2(Velocity.X * Utils.Instance.PlayerFriction, Velocity.Y);
            if (Velocity.Y < 0)
            {
                Velocity = new Vector2(Velocity.X, Velocity.Y * Utils.Instance.PlayerFriction);  // Apply damping force
            }
        }

        public void MoveLeft()
        {
            maxSpeed = isRunning ? Utils.Instance.PlayerMaxRunSpeed : Utils.Instance.PlayerMaxWalkSpeed;
            horizontalAcceleration = isRunning ? Utils.Instance.PlayerrRunAcceleration:Utils.Instance.PlayerWalkAcceleration;

            if (Velocity.X > -maxSpeed && Velocity.X <= 0) // Accelerate leftward
            {
                Acceleration = new Vector2(-horizontalAcceleration, 0f);
            }
            else if (Velocity.X > -maxSpeed && Velocity.X > 0) // Apply deceleration from opposite direction
            {
                Acceleration = new Vector2(-horizontalAcceleration * 2, 0f);
            }
            else
            {
                Acceleration = new Vector2(0, 0);   // Constant velocity once max speed is reached
                if (!isRunning) Velocity = new Vector2(-maxSpeed, Velocity.Y);  // Reduce speed to walking speed if stopped running
            }
        }

        public void MoveRight()
        {
            maxSpeed = isRunning ? Utils.Instance.PlayerMaxRunSpeed : Utils.Instance.PlayerMaxWalkSpeed;
            horizontalAcceleration = isRunning ? Utils.Instance.PlayerrRunAcceleration : Utils.Instance.PlayerWalkAcceleration;

            if (Velocity.X < maxSpeed && Velocity.X >= 0) // Accelerate rightward
            {
                Acceleration = new Vector2(horizontalAcceleration, 0f);
            }
            else if (Velocity.X < maxSpeed && Velocity.X < 0) // Apply deceleration from opposite direction
            {
                Acceleration = new Vector2(horizontalAcceleration * 2, 0f);
            }
            else
            {
                Acceleration = new Vector2(0, 0);   // Constant velocity once max speed is reached
                if (!isRunning) Velocity = new Vector2(maxSpeed, Velocity.Y);   // Reduce speed to walking speed if stopped running
            }
        }

        public void Fly()
        {
            Acceleration = new Vector2(Acceleration.X, -Utils.Instance.PlayerFlyVelocity);
        }

        public void Jump()
        {
            Velocity = new Vector2(Velocity.X, -Utils.Instance.PlayerJumpVelocity);
        }

        public void Run()
        {
            isRunning = true;
        }

        public void Bounce()
        {
            Velocity = new Vector2(0, -Utils.Instance.PlayerBounceVelocity);
        }

        public void ClimbDownFlag()
        {
            ResetMotion();
            Gravity = false;
            Velocity = new Vector2(0, Utils.Instance.PlayerClimbDownFlagVelocity);
            
        }

        public void WalkToCastle()
        {
            Gravity = true;
            Velocity = new Vector2(Utils.Instance.PlayerCastleWalk, Velocity.Y);
        }

        public void DeathSequence()
        {
            ResetMotion();
            Velocity = new Vector2(0, -Utils.Instance.PlayerDeathBounceVelocity);
        }

        public void RaisingSprite()
        {
            Location += new Vector2(0, -Utils.Instance.CommonObjectSize);
        }

        public void Refresh()
        {
            isRunning = false;
        }
    }
}
