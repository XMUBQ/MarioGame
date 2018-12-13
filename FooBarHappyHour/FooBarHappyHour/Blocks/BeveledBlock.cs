using FooBarHappyHour.Factories;
using FooBarHappyHour.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FooBarHappyHour.Physics;

namespace FooBarHappyHour.Blocks
{
    public class BeveledBlock : IBlock
    {
        public Rectangle Rectangle => new Rectangle((int)Physics.Location.X, (int)Physics.Location.Y, blockSprite.Width, blockSprite.Height);
        public IPhysics Physics { get => BlockPhysics; }
        public BlockPhysics BlockPhysics { get; private set; }
        public bool Collidable { get; set; }
        public bool RemovalFlag { get; set; }
        private ISprite blockSprite;

        public BeveledBlock(Vector2 location)
        {
            Collidable = true;
            BlockPhysics = new BlockPhysics(location, true, false);
            blockSprite = BlockSpriteFactory.Instance.CreateBeveledBlockSprite();
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
