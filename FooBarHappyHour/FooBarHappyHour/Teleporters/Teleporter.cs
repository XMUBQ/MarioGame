using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Physics;
using FooBarHappyHour.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FooBarHappyHour.Teleporters
{
    public class Teleporter : ITeleporter
    {
        public Rectangle Rectangle => new Rectangle((int)SceneryPhysics.Location.X, (int)SceneryPhysics.Location.Y, Utils.Instance.CommonObjectSize, Utils.Instance.CommonObjectSize);
        public IPhysics Physics { get => SceneryPhysics; }
        public SceneryPhysics SceneryPhysics { get; private set; }
        public bool Collidable { get; set; }
        public bool RemovalFlag { get; set; }
        public bool OutTeleporter { get; private set; }

        public Teleporter(Vector2 location, bool outTeleporter)
        {
            OutTeleporter = outTeleporter;
            Collidable = false;
            RemovalFlag = false;
            SceneryPhysics = new SceneryPhysics(location);
        }

        public void Update(GameTime gameTime)
        {
            // Nothing to update.
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Nothing to draw.
        }
    }
}
