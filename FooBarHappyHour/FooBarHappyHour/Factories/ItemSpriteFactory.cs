using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Sprites;
using FooBarHappyHour.Utility;

namespace FooBarHappyHour.Factories
{
    public class ItemSpriteFactory
    {
        private Texture2D superStarSpritesheet;
        private Texture2D greenMushroomSpritesheet;
        private Texture2D redMushroomSpritesheet;
        private Texture2D fireFlowerSpritesheet;
        private Texture2D coinSpritesheet;
        private Texture2D hiddenWorldCoinSpritesheet;
        private static readonly ItemSpriteFactory instance = new ItemSpriteFactory();
        public static ItemSpriteFactory Instance { get => instance; }

        private ItemSpriteFactory()
        {
            
        }

        public void LoadAllTextures(ContentManager content)
        {
            superStarSpritesheet = content.Load<Texture2D>("ItemSprites/Star");
            greenMushroomSpritesheet = content.Load<Texture2D>("ItemSprites/GreenMushroom");
            redMushroomSpritesheet = content.Load<Texture2D>("ItemSprites/RedMushroom");
            fireFlowerSpritesheet = content.Load<Texture2D>("ItemSprites/Flower");
            coinSpritesheet = content.Load<Texture2D>("ItemSprites/Coin");
            hiddenWorldCoinSpritesheet = content.Load<Texture2D>("ItemSprites/HiddenWorldCoin");
        }

        public ISprite CreateSuperStarSprite()
        {
            return new DynamicSprite(superStarSpritesheet, 4, Utils.Instance.ItemFrameRate, false);
        }

        public ISprite CreateGreenMushroomSprite()
        {
            return new StaticSprite(greenMushroomSpritesheet);
        }

        public ISprite CreateRedMushroomSprite()
        {
            return new StaticSprite(redMushroomSpritesheet);
        }

        public ISprite CreateFireFlowerSprite()
        {
            return new DynamicSprite(fireFlowerSpritesheet, 4, Utils.Instance.ItemFrameRate, false);
        }

        public ISprite CreateCoinSprite()
        {
            return new DynamicSprite(coinSpritesheet, 4, Utils.Instance.ItemFrameRate, false);
        }

        public ISprite CreateHiddenWorldCoin()
        {
            return new DynamicSprite(hiddenWorldCoinSpritesheet, 4, Utils.Instance.ItemFrameRate, false);
        }
    }
}
