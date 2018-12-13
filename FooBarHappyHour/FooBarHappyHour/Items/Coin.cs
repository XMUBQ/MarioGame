using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Factories;
using FooBarHappyHour.Physics;
using FooBarHappyHour.Utility;
using FooBarHappyHour.Score;
using System;

namespace FooBarHappyHour.Items
{
    public class Coin : IItem
    {
        public Rectangle Rectangle => new Rectangle((int)ItemPhysics.Location.X, (int)ItemPhysics.Location.Y, sprite.Width, sprite.Height);
        public IPhysics Physics { get => ItemPhysics; }
        public ItemPhysics ItemPhysics { get; private set; }
        public bool Collidable { get; set; }
        public bool RemovalFlag { get; set; }
        public bool IsStatic { get;set; }
        private ISprite sprite;
        private CollectDelegate collect;

        public Coin(Vector2 location,bool isStatic,CollectDelegate collectDelegate)
        {
            Collidable = false;
            RemovalFlag = false;
            IsStatic = isStatic;
            collect = collectDelegate;

            if (!isStatic)
            {
                ItemPhysics = new ItemPhysics(location, false, true);
                sprite = ItemSpriteFactory.Instance.CreateCoinSprite();
                ItemPhysics.CoinMovingUp();
                collect(this);
            }
            else
            {
                sprite = ItemSpriteFactory.Instance.CreateHiddenWorldCoin();
                ItemPhysics = new ItemPhysics(location, true, false);
            }
        }

        public void BeCollected()
        {
            collect(this);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, ItemPhysics.Location);
        }

        public void Update(GameTime gameTime)
        {
            ItemPhysics.Update(gameTime);
            if ((ItemPhysics.Location.Y > ItemPhysics.OriginalLocation.Y - Utils.Instance.PositionOffset) && ItemPhysics.IsMovingDown())
            {
                RemovalFlag = true;
            }
            sprite.Update(gameTime);
        }
    }
}