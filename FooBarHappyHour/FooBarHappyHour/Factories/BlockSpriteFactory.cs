using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Sprites;
using FooBarHappyHour.Utility;

namespace FooBarHappyHour.Factories
{
    public class BlockSpriteFactory
    {
        private Texture2D smallPipeSpritesheet;
        private Texture2D mediumPipeSpritesheet;
        private Texture2D bigPipeSpritesheet;
        private Texture2D brickBlockSpritesheet;      
        private Texture2D questionBlockAnimatedSpritesheet;
        private Texture2D hiddenBlockSpritesheet;
        private Texture2D usedBlockSpritesheet;
        private Texture2D groundBlockSpritesheet;
        private Texture2D beveledBlockSpritesheet;
        private Texture2D brokenBlockSpritesheet;  
        private static readonly BlockSpriteFactory instance = new BlockSpriteFactory();
        public static BlockSpriteFactory Instance { get => instance; }

        private BlockSpriteFactory()
        {

        }

        public void LoadAllTextures(ContentManager content)
        {
            smallPipeSpritesheet = content.Load<Texture2D>(Utils.Instance.SmallPipeSpriteDirectory);
            mediumPipeSpritesheet = content.Load<Texture2D>(Utils.Instance.MediumPipeSpriteDirectory);
            bigPipeSpritesheet = content.Load<Texture2D>(Utils.Instance.BigPipeSpriteDirectory);
            brickBlockSpritesheet = content.Load<Texture2D>(Utils.Instance.BrickBlockSpriteDirectory);
            brokenBlockSpritesheet = content.Load<Texture2D>(Utils.Instance.BrokenBlockSpriteDirectory);
            questionBlockAnimatedSpritesheet = content.Load<Texture2D>(Utils.Instance.QuestionBlockSpriteDirectory);
            hiddenBlockSpritesheet = content.Load<Texture2D>(Utils.Instance.HiddenBlockSpriteDirectory);
            usedBlockSpritesheet = content.Load<Texture2D>(Utils.Instance.UsedBlockSpriteDirectory);
            groundBlockSpritesheet = content.Load<Texture2D>(Utils.Instance.GroundBlockSpriteDirectory);
            beveledBlockSpritesheet = content.Load<Texture2D>(Utils.Instance.BeveledSpriteDirectory);
        }

        public ISprite CreateUsedBlockSprite()
        {
            return new StaticSprite(usedBlockSpritesheet);
        }

        public ISprite CreateHiddenBlockSprite()
        {
            return new StaticSprite(hiddenBlockSpritesheet);
        }
        
        public ISprite CreateBrokenBlockSprite()
        {
            return new StaticSprite(brokenBlockSpritesheet);
        }
        
        public ISprite CreateQuestionBlockSprite()
        {
            return new DynamicSprite(questionBlockAnimatedSpritesheet, 3);
        }
        public ISprite CreateBrickBlockSprite()
        {
            return new StaticSprite(brickBlockSpritesheet);
        }

        public ISprite CreateGroundBlockSprite()
        {
            return new StaticSprite(groundBlockSpritesheet);
        }

        public ISprite CreateBeveledBlockSprite()
        {
            return new StaticSprite(beveledBlockSpritesheet);
        }

        public ISprite CreateSmallPipeSprite()
        {
            return new StaticSprite(smallPipeSpritesheet);
        }

        public ISprite CreateMediumPipeSprite()
        {
            return new StaticSprite(mediumPipeSpritesheet);
        }

        public ISprite CreateBigPipeSprite()
        {
            return new StaticSprite(bigPipeSpritesheet);
        }
    }
}
