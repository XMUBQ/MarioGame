using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FooBarHappyHour.Interfaces;
using FooBarHappyHour.States;
using FooBarHappyHour.Physics;
using System.Collections.Generic;
using FooBarHappyHour.Factories;
using FooBarHappyHour.Utility;
using FooBarHappyHour.Score;

namespace FooBarHappyHour.Blocks
{
    public class BrickBlock : IBlock
    {
        public Rectangle Rectangle => new Rectangle((int)BlockPhysics.Location.X, (int)BlockPhysics.Location.Y, stateMachine.Width, stateMachine.Height);
        public IPhysics Physics { get => BlockPhysics; }
        public BlockPhysics BlockPhysics { get; private set; }
        public bool Collidable { get; set; }
        public bool RemovalFlag { get => stateMachine.CanDestroy(); set => RemovalFlag = value; }
        public bool Breakable { get; private set; } // False if brick block contained an item, true otherwise
        public bool Broken { get; set; }
        public bool Depleted { get => ContainedItems.Count == 0; }


        private BrickBlockStateMachine stateMachine;
        private Queue<string> ContainedItems;
        private float persistenceTimer;

        public BrickBlock(Vector2 location)
        {
            Collidable = true;
            stateMachine = new BrickBlockStateMachine(this);
            BlockPhysics = new BlockPhysics(location, true, false);
            Breakable = true;
            ContainedItems = new Queue<string>();
        }

        public void Break()
        {
            ScoreManager.CollectBrokenBlock();
            Broken = true;
            persistenceTimer = Utils.Instance.PersistentTime;
            stateMachine.BecomeBroken();
            SoundFactory.Instance.PlayBreakBlockSound();
        }

        public void BeBumped()
        {
            BlockPhysics.BlockBump();
            SoundFactory.Instance.PlayBumpBlockSound();
        }

        public void BecomeUsed()
        {
            stateMachine.BecomeUsed();
        }

        public void AddItem(string item)
        {
            if (Breakable) Breakable = false;
            ContainedItems.Enqueue(item);
        }

        public void SpawnItem()
        {
            if (ContainedItems.Count > 0)
            {
                ItemFactory.CreateItem(ContainedItems.Dequeue(), BlockPhysics.Location);
            }
        }

        public void Update(GameTime gameTime)
        {
            // Coded to allow block bumping to kill enemies above with some leniency error
            if (Broken)
            {
                if (persistenceTimer <= 0)
                {
                    Collidable = false;
                }
                else
                {
                    persistenceTimer -= Utils.Instance.PersistentTime;
                }
            }
            stateMachine.Update(gameTime);
            BlockPhysics.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            stateMachine.Draw(spriteBatch);
        }
    }
}