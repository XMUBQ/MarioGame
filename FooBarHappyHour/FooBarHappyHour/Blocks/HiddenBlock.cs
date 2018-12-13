using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FooBarHappyHour.Interfaces;
using FooBarHappyHour.States;
using FooBarHappyHour.Physics;
using System.Collections.Generic;
using FooBarHappyHour.Factories;

namespace FooBarHappyHour.Blocks
{
    public class HiddenBlock : IBlock
    {
        public Rectangle Rectangle => new Rectangle((int)BlockPhysics.Location.X, (int)BlockPhysics.Location.Y, stateMachine.Width, stateMachine.Height);
        public IPhysics Physics { get => BlockPhysics; }
        public BlockPhysics BlockPhysics { get; private set; }
        public bool Collidable { get => IsUsed; set => Collidable = value; }
        public bool RemovalFlag { get; set; }
        public bool IsUsed { get; set; }

        private HiddenBlockStateMachine stateMachine;
        private Queue<string> ContainedItems;

        public HiddenBlock(Vector2 location)
        {
            stateMachine = new HiddenBlockStateMachine();
            BlockPhysics = new BlockPhysics(location, true, false);
            RemovalFlag = false;
            IsUsed = false;
            ContainedItems = new Queue<string>();
        }

        public void BecomeUsed()
        {
            IsUsed = true;
            stateMachine.BecomeUsed();
        }

        public void BeBumped()
        {
            BlockPhysics.BlockBump();
            SoundFactory.Instance.PlayBumpBlockSound();
        }

        public void AddItem(string item)
        {
            ContainedItems.Enqueue(item);
        }

        public void SpawnItem()
        {
            if (!IsUsed && ContainedItems.Count > 0)
            {
                ItemFactory.CreateItem(ContainedItems.Dequeue(), BlockPhysics.Location);
            }
        }

        public void Update(GameTime gameTime)
        {
            BlockPhysics.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            stateMachine.Draw(spriteBatch, BlockPhysics.Location);
        }
    }
}
