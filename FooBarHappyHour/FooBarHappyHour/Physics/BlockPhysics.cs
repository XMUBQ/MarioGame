using FooBarHappyHour.Utility;
using Microsoft.Xna.Framework;

namespace FooBarHappyHour.Physics
{
    public class BlockPhysics : GeneralPhysics
    {
        public bool BlockBumped { get; set; }   

        public BlockPhysics(Vector2 location, bool locked, bool gravity)
        {
            Locked = locked;
            Gravity = gravity;
            Location = location;
            Velocity = new Vector2();
            Acceleration = new Vector2();
            OriginalLocation = location;
        }

        public void BlockBump()
        {
            BlockBumped = true;
            Locked = false;
            Gravity = true;
            Velocity = new Vector2(0f, -Utils.Instance.BlockBumpedDistance);
        }

        public void ApplyBlockBump()
        {
            if (BlockBumped && Location.Y > OriginalLocation.Y)
            {
                ResetMotion();
                Location = OriginalLocation;
                Locked = true;
                Gravity = false;
                BlockBumped = false;
            }
        }

        public void BlockExplodeUpLeft()
        {
            Velocity = new Vector2(-Utils.Instance.BlockExplodedDistance,-Utils.Instance.BlockExplodedDistance);
        }

        public void BlockExplodeUpRight()
        {
            Velocity = new Vector2(Utils.Instance.BlockExplodedDistance, -Utils.Instance.BlockExplodedDistance);
        }

        public void BlockExplodeDownLeft()
        {
            Velocity = new Vector2(-Utils.Instance.BlockExplodedDistance, Utils.Instance.BlockExplodedDistance);
        }

        public void BlockExplodeDownRight()
        {
            Velocity = new Vector2(Utils.Instance.BlockExplodedDistance, Utils.Instance.BlockExplodedDistance);
        }

        public new void Update(GameTime gameTime)
        {
            ApplyBlockBump();
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (!Locked)
            {
                Acceleration = new Vector2(0, Utils.Instance.GravityConstant);
                Velocity += Acceleration * deltaTime;
                Location += Velocity * deltaTime;
            }
        }
    }
}