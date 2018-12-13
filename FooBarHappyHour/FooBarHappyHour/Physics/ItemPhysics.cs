using FooBarHappyHour.Utility;
using Microsoft.Xna.Framework;
using System;

namespace FooBarHappyHour.Physics
{
    public class ItemPhysics : GeneralPhysics
    {
        private float itemHorizontalV;

        public ItemPhysics(Vector2 location, bool locked, bool gravity)
        {
            Locked = locked;
            Gravity = gravity;
            Location = location;
            Velocity = new Vector2();
            Acceleration = new Vector2();
            OriginalLocation = location;

            MoveRight();
        }

        public bool IsVelocityZero()
        {
            return Velocity.X == 0 && Velocity.Y == 0;
        }

        public void CoinMovingUp()
        {
            Velocity = new Vector2(0, - Utils.Instance.CoinMovingUpSpeed);
            Acceleration = new Vector2(0, 0);
        }

        public void ComingOutOfBlock()
        {
            Acceleration = new Vector2(0, -Utils.Instance.GravityConstant);
            Velocity = new Vector2(0, -Utils.Instance.ItemComingOutOfBlockVelocity);
        }

        public void ItemMovement()
        {
            Acceleration = new Vector2(0f, 0f);
            Velocity = new Vector2(itemHorizontalV, Velocity.Y);
        }

        public void MoveLeft()
        {
            itemHorizontalV = -Utils.Instance.ItemMovingVelocity;
        }

        public void MoveRight()
        {
            itemHorizontalV = Utils.Instance.ItemMovingVelocity;
        }

        public void StarJump()
        {
            Velocity = new Vector2(Velocity.X, -(Utils.Instance.StarJumpVelocity-Velocity.Y));
        }

        public Rectangle BlockRectangle()
        {
            return new Rectangle((int)OriginalLocation.X, (int)OriginalLocation.Y, Utils.Instance.CommonObjectSize,Utils.Instance.CommonObjectSize);
        }
    }
}
