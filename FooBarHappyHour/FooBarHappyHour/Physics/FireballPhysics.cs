using FooBarHappyHour.Utility;
using Microsoft.Xna.Framework;

namespace FooBarHappyHour.Physics
{
    public class FireballPhysics : GeneralPhysics
    {

        public FireballPhysics(bool locked, bool gravity, Vector2 location)
        {
            Locked = locked;
            Gravity = gravity;
            Location = location;
            Velocity = new Vector2();
            Acceleration = new Vector2();
            OriginalLocation = location;
        }

        public void Jump()
        {
            Velocity += new Vector2(0f, - Utils.Instance.FireballBounceHeight);
        }

        public void MoveLeft()
        {
            Velocity = new Vector2(-Utils.Instance.FireballMoveSpeed, Velocity.Y);
        }

        public void MoveRight()
        {
            Velocity = new Vector2(Utils.Instance.FireballMoveSpeed, Velocity.Y);
        }
    }
}
