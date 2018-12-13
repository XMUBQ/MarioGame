using FooBarHappyHour.Factories;
using FooBarHappyHour.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FooBarHappyHour.Physics;

namespace FooBarHappyHour.Misc
{
    public class Castle : IMisc
    {
        public Rectangle Rectangle => new Rectangle((int)SceneryPhysics.Location.X, (int)SceneryPhysics.Location.Y, castleSprite.Width, castleSprite.Height);
        public IPhysics Physics { get => SceneryPhysics; }
        public SceneryPhysics SceneryPhysics { get; private set; }
        public bool Collidable { get; set; }
        public bool RemovalFlag { get; set; }
        private ISprite castleSprite;

        public Castle(Vector2 location)
        {
            Collidable = true;
            RemovalFlag = false;
            castleSprite = MiscSpriteFactory.Instance.CreateCastleSprite();
            SceneryPhysics = new SceneryPhysics(location);
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            castleSprite.Draw(spriteBatch, Physics.Location);
        }
    }
}
