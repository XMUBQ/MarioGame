using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Sprites;

namespace FooBarHappyHour.Factories
{
    public class EnemySpriteFactory
    {
        private Texture2D selectedEnemySpritesheet;
        private Texture2D goombaMovingSpritesheet;
        private Texture2D goombaStompedSpritesheet;
        private Texture2D goombaFlippedSpritesheet;
        private Texture2D koopaMovingLeftSpritesheet;
        private Texture2D koopaMovingRightSpritesheet;
        private Texture2D koopaStompedSpritesheet;
        private Texture2D koopaRevivingSpritesheet;
        private Texture2D koopaFlippedSpritesheet;
        private Texture2D piranhaPlantSpritesheet;
        private static readonly EnemySpriteFactory instance = new EnemySpriteFactory();
        public static EnemySpriteFactory Instance { get => instance; }

        private EnemySpriteFactory()
        {

        }

        public void LoadAllTextures(ContentManager content)
        {
            selectedEnemySpritesheet = content.Load<Texture2D>("EnemySprites/SelectedEnemy");
            goombaMovingSpritesheet = content.Load<Texture2D>("EnemySprites/GoombaWalk");
            goombaStompedSpritesheet = content.Load<Texture2D>("EnemySprites/GoombaStomped");
            goombaFlippedSpritesheet = content.Load<Texture2D>("EnemySprites/GoombaFlipped");
            koopaMovingLeftSpritesheet = content.Load<Texture2D>("EnemySprites/KoopaWalkLeft");
            koopaMovingRightSpritesheet = content.Load<Texture2D>("EnemySprites/KoopaWalkRight");
            koopaStompedSpritesheet = content.Load<Texture2D>("EnemySprites/KoopaStomped");
            koopaRevivingSpritesheet = content.Load<Texture2D>("EnemySprites/KoopaRevive");
            koopaFlippedSpritesheet = content.Load<Texture2D>("EnemySprites/KoopaFlipped");
            piranhaPlantSpritesheet = content.Load<Texture2D>("EnemySprites/Piranha");
        }

        public ISprite CreateSelectedEnemySprite()
        {
            return new StaticSprite(selectedEnemySpritesheet);
        }

        public ISprite CreateGoombaMovingSprite()
        {
            return new DynamicSprite(goombaMovingSpritesheet, 2);
        }

        public ISprite CreateGoombaStompedSprite()
        {
            return new StaticSprite(goombaStompedSpritesheet);
        }

        public ISprite CreateGoombaFlippedSprite()
        {
            return new StaticSprite(goombaFlippedSpritesheet);
        }

        public ISprite CreateKoopaMovingLeftSprite()
        {
            return new DynamicSprite(koopaMovingLeftSpritesheet, 2);
        }
        
        public ISprite CreateKoopaMovingRightSprite()
        {
            return new DynamicSprite(koopaMovingRightSpritesheet, 2);
        }

        public ISprite CreateKoopaStompedSprite()
        {
            return new StaticSprite(koopaStompedSpritesheet);
        }

        public ISprite CreateKoopaRevivingSprite()
        {
            return new DynamicSprite(koopaRevivingSpritesheet, 2);
        }

        public ISprite CreateKoopaFlippedSprite()
        {
            return new StaticSprite(koopaFlippedSpritesheet);
        }

        public ISprite CreatePiranhaPlantSprite()
        {
            return new DynamicSprite(piranhaPlantSpritesheet,2);
        }
    }
}
