using FooBarHappyHour.Utility;
using Microsoft.Xna.Framework;
using System;

namespace FooBarHappyHour.Physics
{
    public class EnemyPhysics : GeneralPhysics
    {
        public enum Direction { Left, Right };
        public Direction MoveDirection { get; set; }
        public bool CanJump { get; set; }
        
        private float enemySpeed;

        public EnemyPhysics(Vector2 location, bool locked, bool gravity)
        {
            Locked = locked;
            Gravity = gravity;
            Location = location;
            Velocity = new Vector2();
            Acceleration = new Vector2();
            OriginalLocation = location;
            CanJump = true;
            FaceLeft();
            EnemyWalk();
        }

        public void Flip()
        {
            Location += new Vector2(0, -Utils.Instance.EnemyFlipHeight);
        }

        public void GoombaMove()
        {
            Acceleration = new Vector2(0, Acceleration.Y);
            SetVelocity();
            Velocity = new Vector2(enemySpeed, Velocity.Y);
        }

        public void KoopaMove()
        {
            Acceleration = new Vector2(0, Acceleration.Y);
            SetVelocity();
            Velocity = new Vector2(enemySpeed, Velocity.Y);
        }

        public void FaceLeft()
        {
            MoveDirection = Direction.Left;
        }

        public void FaceRight()
        {
            MoveDirection = Direction.Right;
        }

        public void EnemyWalk()
        {
            enemySpeed = Utils.Instance.EnemyWalkSpeed;
        }

        public void KoopaKicked()
        {
            enemySpeed = Utils.Instance.KoopaKickedSpeed;
        }

        public void PiranhaAppear()
        {
            Acceleration = new Vector2(0, 0);
            Velocity = new Vector2(0, -enemySpeed);
        }

        public void PiranhaMoveFlip()
        {
            Acceleration = new Vector2(0, 0);
            Velocity = new Vector2(0, -Velocity.Y);
        }

        public void Run()
        {
            if (MoveDirection == Direction.Left)
            {
                enemySpeed = -64f;
            }
            else if (MoveDirection == Direction.Right)
            {
                enemySpeed = 64f;
            }
        }

        public void Jump()
        {
            if (CanJump)
            {
                Velocity = new Vector2(Velocity.X, -Utils.Instance.PlayerJumpVelocity);
                CanJump = false;
                Factories.SoundFactory.Instance.PlayEnemyJumpSound();
            }
        }

        private void SetVelocity()
        {
            if (MoveDirection == Direction.Left)
            {
                enemySpeed = -Math.Abs(enemySpeed);
            }
            else if (MoveDirection == Direction.Right)
            {
                enemySpeed = Math.Abs(enemySpeed);
            }
        }
    }
}
