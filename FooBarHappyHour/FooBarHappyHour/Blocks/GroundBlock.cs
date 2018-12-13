using FooBarHappyHour.Factories;
using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FooBarHappyHour.Blocks
{
    public class GroundBlock : IBlock
    {
        public Rectangle Rectangle => new Rectangle((int)BlockPhysics.Location.X, (int)BlockPhysics.Location.Y, blockSprite.Width, blockSprite.Height);
        public IPhysics Physics { get => BlockPhysics; }
        public BlockPhysics BlockPhysics { get; private set; }
        public bool Collidable { get; set; }
        public bool RemovalFlag { get; set; }
        private ISprite blockSprite;

        public GroundBlock(Vector2 location)
        {
            Collidable = true;
            blockSprite = BlockSpriteFactory.Instance.CreateGroundBlockSprite();
            BlockPhysics = new BlockPhysics(location, true, false);
            RemovalFlag = false;
        }

        public void Update(GameTime gameTime)
        {
            // Nothing to update.
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            blockSprite.Draw(spriteBatch, BlockPhysics.Location);
        }
    }
}
