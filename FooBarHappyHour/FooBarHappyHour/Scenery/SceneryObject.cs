using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Factories;
using FooBarHappyHour.Physics;

namespace FooBarHappyHour.Scenery
{
    class SceneryObject : IScenery
    {
        public Rectangle Rectangle => new Rectangle((int)SceneryPhysics.Location.X, (int)SceneryPhysics.Location.Y, sprite.Width, sprite.Height);
        public IPhysics Physics { get => SceneryPhysics; }
        public SceneryPhysics SceneryPhysics { get; private set; }
        public bool Collidable { get; set; }
        public bool RemovalFlag { get; set; }
        private ISprite sprite;

        public SceneryObject(Vector2 location, string type)
        {
            Collidable = false;
            RemovalFlag = false;
            SceneryPhysics = new SceneryPhysics(location);
            sprite = ScenerySpriteFactory.Instance.CreateScenery(type);
        }

        public void Update(GameTime gameTime)
        {
            // Nothing to update.
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, SceneryPhysics.Location);
        }
    }
}
