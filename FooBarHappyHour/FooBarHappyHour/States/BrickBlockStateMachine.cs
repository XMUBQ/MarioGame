using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Factories;
using FooBarHappyHour.Blocks;
using FooBarHappyHour.Physics;
using FooBarHappyHour.Utility;

namespace FooBarHappyHour.States
{
    public class BrickBlockStateMachine
    {
        public int Width { get; set; }
        public int Height { get; set; }
        private ISprite BrickBlockSprite;
        private BrickBlock brickBlock;
        IBlock upLeftBrokenBlock;
        IBlock downLeftBrokenBlock;
        IBlock upRightBrokenBlock;
        IBlock downRightBrokenBlock;

        public BrickBlockStateMachine(BrickBlock brickBlock)
        {
            BrickBlockSprite = BlockSpriteFactory.Instance.CreateBrickBlockSprite();
            Width = BrickBlockSprite.Width;
            Height = BrickBlockSprite.Height;
            this.brickBlock = brickBlock;
        }

        public bool CanDestroy()
        {
            return brickBlock.Broken && upLeftBrokenBlock == null && downLeftBrokenBlock == null && upRightBrokenBlock == null && downRightBrokenBlock == null;
        }

        public void Update(GameTime gameTime)
        {
            if (brickBlock.Broken)
            {
                if (upLeftBrokenBlock != null)
                {
                    upLeftBrokenBlock.Update(gameTime);
                    if (upLeftBrokenBlock.Physics.Location.Y > Utils.Instance.RemovalPosition)
                    {
                        upLeftBrokenBlock = null;
                    }
                }
                if (downLeftBrokenBlock != null)
                {
                    downLeftBrokenBlock.Update(gameTime);
                    if (downLeftBrokenBlock.Physics.Location.Y > Utils.Instance.RemovalPosition)
                    {
                        downLeftBrokenBlock = null;
                    }
                }
                if (upRightBrokenBlock != null)
                {
                    upRightBrokenBlock.Update(gameTime);
                    if (upRightBrokenBlock.Physics.Location.Y > Utils.Instance.RemovalPosition)
                    {
                        upRightBrokenBlock = null;
                    }
                }
                if (downRightBrokenBlock != null)
                {
                    downRightBrokenBlock.Update(gameTime);
                    if (downRightBrokenBlock.Physics.Location.Y > Utils.Instance.RemovalPosition)
                    {
                        downRightBrokenBlock = null;
                    }
                }                  
            }
            else
            {
                BrickBlockSprite.Update(gameTime);
            }
        }
        public void BecomeBroken()
        {
            upLeftBrokenBlock = new BrokenBlock(brickBlock.Physics.Location);
            ((BlockPhysics)(upLeftBrokenBlock.Physics)).BlockExplodeUpLeft();
            downLeftBrokenBlock = new BrokenBlock(new Vector2(brickBlock.Physics.Location.X, brickBlock.Physics.Location.Y - Utils.Instance.BrokenBlockShift));
            ((BlockPhysics)(downLeftBrokenBlock.Physics)).BlockExplodeDownLeft();
            upRightBrokenBlock = new BrokenBlock(new Vector2(brickBlock.Physics.Location.X + Utils.Instance.BrokenBlockShift, brickBlock.Physics.Location.Y));
            ((BlockPhysics)(upRightBrokenBlock.Physics)).BlockExplodeUpRight();
            downRightBrokenBlock = new BrokenBlock(new Vector2(brickBlock.Physics.Location.X + Utils.Instance.BrokenBlockShift, brickBlock.Physics.Location.Y - Utils.Instance.BrokenBlockShift));
            ((BlockPhysics)(downRightBrokenBlock.Physics)).BlockExplodeDownRight();
        }

        public void BecomeUsed()
        {
            BrickBlockSprite = BlockSpriteFactory.Instance.CreateUsedBlockSprite();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (brickBlock.Broken)
            {
                if(upLeftBrokenBlock != null)
                {
                    upLeftBrokenBlock.Draw(spriteBatch);
                }
                if(downLeftBrokenBlock != null)
                {
                    downLeftBrokenBlock.Draw(spriteBatch);
                }
                if (upRightBrokenBlock != null)
                {
                    upRightBrokenBlock.Draw(spriteBatch);
                }
                if (downRightBrokenBlock != null)
                {
                    downRightBrokenBlock.Draw(spriteBatch);
                }
            }
            else
            {
                BrickBlockSprite.Draw(spriteBatch, brickBlock.Physics.Location);
            }
        }
    }
}
