using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Sprites;

namespace FooBarHappyHour.Factories
{
    public class ScenerySpriteFactory
    {
        private Texture2D bigBushSpritesheet;
        private Texture2D smallBushSpritesheet;
        private Texture2D bigCloudSpritesheet;
        private Texture2D smallCloudSpritesheet;
        private Texture2D bigHillSpritesheet;
        private Texture2D smallHillSpritesheet;
        private static readonly ScenerySpriteFactory instance = new ScenerySpriteFactory();
        public static ScenerySpriteFactory Instance { get => instance; }

        private ScenerySpriteFactory()
        {

        }

        public void LoadAllTextures(ContentManager content)
        {
            bigBushSpritesheet = content.Load<Texture2D>("ScenerySprites/BigBush");
            smallBushSpritesheet = content.Load<Texture2D>("ScenerySprites/SmallBush");
            bigCloudSpritesheet = content.Load<Texture2D>("ScenerySprites/BigCloud");
            smallCloudSpritesheet = content.Load<Texture2D>("ScenerySprites/SmallCloud");
            bigHillSpritesheet = content.Load<Texture2D>("ScenerySprites/BigHill");
            smallHillSpritesheet = content.Load<Texture2D>("ScenerySprites/SmallHill");
        }

        public ISprite CreateScenery(string type)
        {
            ISprite sprite = null;
            switch (type)
            {
                case "BigBush":
                    sprite = new StaticSprite(bigBushSpritesheet);
                    break;
                case "SmallBush":
                    sprite = new StaticSprite(smallBushSpritesheet);
                    break;
                case "BigCloud":
                    sprite = new StaticSprite(bigCloudSpritesheet);
                    break;
                case "SmallCloud":
                    sprite = new StaticSprite(smallCloudSpritesheet);
                    break;
                case "BigHill":
                    sprite = new StaticSprite(bigHillSpritesheet);
                    break;
                case "SmallHill":
                    sprite = new StaticSprite(smallHillSpritesheet);
                    break;
            }
            return sprite;
        }
    }
}
