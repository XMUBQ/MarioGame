using FooBarHappyHour.Factories;
using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Physics;
using FooBarHappyHour.Utility;
using FooBarHappyHour.Score;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FooBarHappyHour.Items
{
    public class PowerUp : IItem
    {
        public Rectangle Rectangle => new Rectangle((int)ItemPhysics.Location.X, (int)ItemPhysics.Location.Y, sprite.Width, sprite.Height);
        public IPhysics Physics { get => ItemPhysics; }
        public ItemPhysics ItemPhysics { get; private set; }
        public bool Collidable { get; set; }
        public bool RemovalFlag { get; set; }

        private readonly bool marioIsSmall;
        private ISprite sprite;
        private float delayCounter;
        private CollectDelegate collect;

        public PowerUp(Vector2 location, bool marioIsSmall,CollectDelegate collectDelegate)
        {
            Collidable = false;
            RemovalFlag = false;

            ItemPhysics = new ItemPhysics(location, false, true);
            this.marioIsSmall = marioIsSmall;
            sprite = marioIsSmall ? ItemSpriteFactory.Instance.CreateRedMushroomSprite() : ItemSpriteFactory.Instance.CreateFireFlowerSprite();
            delayCounter = Utils.Instance.BlockBumpDelay;
            collect = collectDelegate;

            ItemPhysics.ComingOutOfBlock();
        }

        public void BeCollected()
        {
            collect(this);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (delayCounter <= 0)
            {
                sprite.Draw(spriteBatch, ItemPhysics.Location);
            }
        }

        public void Update(GameTime gameTime)
        {
            Rectangle blockBox = ItemPhysics.BlockRectangle();
            if (delayCounter > 0)
            {
                delayCounter -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                if (!Rectangle.Intersects(blockBox)) Collidable = true;
                
                if (Collidable)
                { 
                    if (marioIsSmall)
                    {
                        ItemPhysics.ItemMovement(); // Behave like Red Mushroom if Mario is small
                    }
                    else
                    {
                        ItemPhysics.ResetMotion();  // Behave like Fire Flower if Mario is big
                    }
                }
                ItemPhysics.Update(gameTime);
            }
            sprite.Update(gameTime);
        }
    }
}
