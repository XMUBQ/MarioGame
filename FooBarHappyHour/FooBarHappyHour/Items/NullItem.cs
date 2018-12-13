using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FooBarHappyHour.Items
{
    public class NullItem : IItem
    {
        public Rectangle Rectangle => new Rectangle((int)ItemPhysics.Location.X, (int)ItemPhysics.Location.Y, 0, 0);
        public IPhysics Physics { get => ItemPhysics; }
        public ItemPhysics ItemPhysics { get; set; }
        public bool Collidable { get; set; }
        public bool RemovalFlag { get; set; }

        public NullItem()
        {
            Collidable = false;
            RemovalFlag = true;
        }

        public void BeCollected()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Nothing to draw.
        }

        public void Update(GameTime gameTime)
        {
            // Nothing to update.
        }
    }
}
