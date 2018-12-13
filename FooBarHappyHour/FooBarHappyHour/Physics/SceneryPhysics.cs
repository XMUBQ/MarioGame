using Microsoft.Xna.Framework;

namespace FooBarHappyHour.Physics
{
    public class SceneryPhysics : GeneralPhysics
    {
        public SceneryPhysics(Vector2 location)
        {
            Locked = true;
            Gravity = false;
            Location = location;
            Velocity = new Vector2();
            Acceleration = new Vector2();
            OriginalLocation = location;
        }
    }
}