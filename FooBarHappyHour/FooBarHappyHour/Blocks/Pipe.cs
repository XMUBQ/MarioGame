using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Factories;
using FooBarHappyHour.Physics;
using System.Collections.Generic;

namespace FooBarHappyHour.Blocks
{
    public class Pipe : IBlock
    {
        public Rectangle Rectangle => new Rectangle((int)BlockPhysics.Location.X, (int)BlockPhysics.Location.Y, pipeSprite.Width, pipeSprite.Height);
        public IPhysics Physics { get => BlockPhysics; }
        public BlockPhysics BlockPhysics { get; private set; }
        public bool RemovalFlag { get; set; }
        public bool Collidable { get; set; }
        private ISprite pipeSprite;

        public Pipe(Vector2 location, string pipeType)
        {
            switch (pipeType)
            {
                case "SmallPipe":
                    pipeSprite = BlockSpriteFactory.Instance.CreateSmallPipeSprite();
                    break;
                case "MediumPipe":
                    pipeSprite = BlockSpriteFactory.Instance.CreateMediumPipeSprite();
                    break;
                case "BigPipe":
                    pipeSprite = BlockSpriteFactory.Instance.CreateBigPipeSprite();
                    break;
            }
            Collidable = true;
            BlockPhysics = new BlockPhysics(location, true, false);
            RemovalFlag = false;
        }

        public void Update(GameTime gameTime)
        {
            // Nothing to update.
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            pipeSprite.Draw(spriteBatch, BlockPhysics.Location);
        }
    }
}
