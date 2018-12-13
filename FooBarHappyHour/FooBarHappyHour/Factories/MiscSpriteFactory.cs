using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Sprites;
using FooBarHappyHour.Utility;

namespace FooBarHappyHour.Factories
{
    public class MiscSpriteFactory
    {
        private Texture2D fireballLeftSpritesheet;
        private Texture2D fireballRightSpritesheet;
        private Texture2D fireballExplosionSpritesheet;
        private Texture2D flagpoleSpritesheet;
        private Texture2D castleSpritesheet;
        private Texture2D enemySpawnerSpritesheet;
        private static readonly MiscSpriteFactory instance = new MiscSpriteFactory();
        public static MiscSpriteFactory Instance { get => instance; }

        private MiscSpriteFactory()
        {

        }

        public void LoadAllTextures(ContentManager content)
        {
            fireballLeftSpritesheet = content.Load<Texture2D>("MiscSprites/FireballLeft");
            fireballRightSpritesheet = content.Load<Texture2D>("MiscSprites/FireballRight");
            fireballExplosionSpritesheet = content.Load<Texture2D>("MiscSprites/FireballExplosion");
            flagpoleSpritesheet = content.Load<Texture2D>("MiscSprites/Flagpole");
            castleSpritesheet = content.Load<Texture2D>("MiscSprites/Castle");
            enemySpawnerSpritesheet = content.Load<Texture2D>("MiscSprites/EnemySpawner");
        }

        public ISprite CreateFireballLeftSprite()
        {
            return new DynamicSprite(fireballLeftSpritesheet, 4);
        }

        public ISprite CreateFireballRightSprite()
        {
            return new DynamicSprite(fireballRightSpritesheet, 4);
        }

        public ISprite CreateFireballExplosionSprite()
        {
            return new DynamicSprite(fireballExplosionSpritesheet, 3, Utils.Instance.ItemFrameRate, false);
        }

        public ISprite CreateFlagpoleSprite()
        {
            return new DynamicSprite(flagpoleSpritesheet, Utils.Instance.FlagPoleFrames, Utils.Instance.FlagFrameRate, false);
        }

        public ISprite CreateCastleSprite()
        {
            return new StaticSprite(castleSpritesheet);
        }

        public ISprite CreateEnemySpawnerSprite()
        {
            return new DynamicSprite(enemySpawnerSpritesheet, 4);
        }
    }
}
