﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Factories;
using FooBarHappyHour.Physics;
using System;
using FooBarHappyHour.Audio;
using FooBarHappyHour.Utility;
using FooBarHappyHour.Score;

namespace FooBarHappyHour.Items
{
    public class SuperStar : IItem
    {
        public Rectangle Rectangle => new Rectangle((int)ItemPhysics.Location.X, (int)ItemPhysics.Location.Y, sprite.Width, sprite.Height);
        public IPhysics Physics { get => ItemPhysics; }
        public ItemPhysics ItemPhysics { get; private set; }
        public bool Collidable { get; set; }
        public bool RemovalFlag { get; set; }
        
        private ISprite sprite;
        private float delayCounter;
        private CollectDelegate collect;

        public SuperStar(Vector2 location,CollectDelegate collectDelegate)
        {
            Collidable = false;
            RemovalFlag = false;
            ItemPhysics = new ItemPhysics(location, false, true);
            sprite = ItemSpriteFactory.Instance.CreateSuperStarSprite();
            delayCounter = Utils.Instance.BlockBumpDelay;
            collect = collectDelegate;

            ItemPhysics.ComingOutOfBlock();
        }

        public void BeCollected()
        {
            collect(this);
        }

        public void Jump()
        {
            ItemPhysics.StarJump();
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
                    ItemPhysics.ItemMovement();
                }

                ItemPhysics.Update(gameTime);
            }
            sprite.Update(gameTime);
        }
    }
}
