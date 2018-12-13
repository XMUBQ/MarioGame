using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Factories;
using FooBarHappyHour.Physics;

namespace FooBarHappyHour.Blocks
{
    public class BrokenBlock : IBlock
    {
        public Rectangle Rectangle => new Rectangle((int)BlockPhysics.Location.X, (int)BlockPhysics.Location.Y, blockSprite.Width, blockSprite.Height);
        public IPhysics Physics { get => BlockPhysics; }
        public BlockPhysics BlockPhysics { get; private set; }

        public bool Collidable { get; set; }
        public bool RemovalFlag { get; set; }
        private ISprite blockSprite;

        public BrokenBlock(Vector2 location)
        {
            Collidable = false;
            RemovalFlag = false;
            blockSprite = BlockSpriteFactory.Instance.CreateBrokenBlockSprite();
            BlockPhysics = new BlockPhysics(location, false, true);
        }

        public void Update(GameTime gameTime)
        {
            BlockPhysics.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            blockSprite.Draw(spriteBatch, BlockPhysics.Location);
        }
    }
}
