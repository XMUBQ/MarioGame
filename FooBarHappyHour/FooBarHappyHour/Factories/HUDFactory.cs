using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Sprites;
using FooBarHappyHour.Utility;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FooBarHappyHour.Factories
{
    public class HUDFactory
    {
        public SpriteFont SpriteFont { get; private set; }
        private Texture2D marioNormalSpritesheet;
        private Texture2D marioSuperSpritesheet;
        private Texture2D marioFireSpritesheet;
        private Texture2D marioSmallInvincibleSpritesheet;
        private Texture2D marioBigInvincibleSpritesheet;
        private Texture2D logoSpritesheet;
        private Texture2D superStarSpritesheet;
        private Texture2D greenMushroomSpritesheet;
        private Texture2D redMushroomSpritesheet;
        private Texture2D fireFlowerSpritesheet;
        private Texture2D coinSpritesheet;
        private static readonly HUDFactory instance = new HUDFactory();
        public static HUDFactory Instance { get => instance; }

        private HUDFactory()
        {

        }

        public void LoadAllFonts(ContentManager content)
        {
            SpriteFont = content.Load<SpriteFont>("HUD/Consolas");
            marioNormalSpritesheet = content.Load<Texture2D>("HUD/MarioNormal");
            marioSuperSpritesheet = content.Load<Texture2D>("HUD/MarioSuper");
            marioFireSpritesheet = content.Load<Texture2D>("HUD/MarioFire");
            marioSmallInvincibleSpritesheet = content.Load<Texture2D>("HUD/MarioSmallInvincible");
            marioBigInvincibleSpritesheet = content.Load<Texture2D>("HUD/MarioBigInvincible");
            logoSpritesheet = content.Load<Texture2D>("HUD/Logo");
            superStarSpritesheet = content.Load<Texture2D>("HUD/Star");
            greenMushroomSpritesheet = content.Load<Texture2D>("HUD/GreenMushroom");
            redMushroomSpritesheet = content.Load<Texture2D>("HUD/RedMushroom");
            fireFlowerSpritesheet = content.Load<Texture2D>("HUD/Flower");
            coinSpritesheet = content.Load<Texture2D>("HUD/Coin");
        }

        public ISprite CreateMarioSprite(string size, string powerUp)
        {
            if (size == Utils.Instance.PlayerSmall)
            {
                if (powerUp == Utils.Instance.PlayerNormal)
                {
                    return new StaticSprite(marioNormalSpritesheet);
                }
                else
                {
                    return new DynamicSprite(marioSmallInvincibleSpritesheet, 4);
                }
            }
            else
            {
                if (powerUp == Utils.Instance.PlayerSuper)
                {
                    return new StaticSprite(marioSuperSpritesheet);
                }
                else if (powerUp == Utils.Instance.PlayerFire)
                {
                    return new StaticSprite(marioFireSpritesheet);
                }
                else
                {
                    return new DynamicSprite(marioBigInvincibleSpritesheet, 4);
                }
            }
        }

        public ISprite CreateLogoSprite()
        {
            return new StaticSprite(logoSpritesheet);
        }

        public ISprite CreateCoinSprite()
        {
            return new DynamicSprite(coinSpritesheet, 4, Utils.Instance.ItemFrameRate, false);
        }

        public ISprite CreateRandomSelectorSprite()
        {
            Random random = new Random();
            int number = random.Next(0, 4);
            if (number == 0)
            {
                return new DynamicSprite(superStarSpritesheet, 4, Utils.Instance.ItemFrameRate, false);
            }
            else if(number == 1)
            {
                return new StaticSprite(greenMushroomSpritesheet);
            }
            else if (number == 2)
            {
                return new StaticSprite(redMushroomSpritesheet);
            }
            else if (number == 3)
            {
                return new DynamicSprite(fireFlowerSpritesheet, 4, Utils.Instance.ItemFrameRate, false);
            }
            else
            {
                return new DynamicSprite(coinSpritesheet, 4, Utils.Instance.ItemFrameRate, false);
            }
        }
    }
}
