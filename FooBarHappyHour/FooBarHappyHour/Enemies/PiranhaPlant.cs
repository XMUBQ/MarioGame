using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Physics;
using FooBarHappyHour.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FooBarHappyHour.Utility;

namespace FooBarHappyHour.Enemies
{
    public class PiranhaPlant : IEnemy
    {
        public bool Collidable { get => !IsDead; set => Collidable = value; }
        public bool IsDead { get; set; }
        public bool RemovalFlag { get; set; }
        public IPhysics Physics { get => EnemyPhysics; }
        public EnemyPhysics EnemyPhysics { get; private set; }
        public Rectangle Rectangle => new Rectangle((int)EnemyPhysics.Location.X, (int)EnemyPhysics.Location.Y, sprite.Width, sprite.Height);
        public bool EnemyCollidable { get; set; }
        private ISprite sprite;
        private double timeCounter;

        public PiranhaPlant(Vector2 location)
        {
            EnemyPhysics = new EnemyPhysics(location, false, false);
            sprite = EnemySpriteFactory.Instance.CreatePiranhaPlantSprite();
            IsDead = false;
            RemovalFlag = false;
            timeCounter = 0;
            EnemyPhysics.PiranhaAppear();
            EnemyCollidable = true;
        }

        public void GoLeft()
        {
            // Does nothing.
        }

        public void GoRight()
        {
            // Does nothing.
        }

        public void Jump()
        {
            // Does nothing.
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.YRate = (double)(EnemyPhysics.OriginalLocation.Y - EnemyPhysics.Location.Y) / sprite.Height;
            sprite.Draw(spriteBatch, EnemyPhysics.Location);
        }

        public void TakeDamage()
        {
            IsDead = true;
            RemovalFlag = true;
        }

        public void Update(GameTime gameTime)
        {
            if (ReachedIdleSpot())
            {
                EnemyPhysics.PiranhaMoveFlip();
                EnemyPhysics.Locked = true;
                timeCounter = 0;
            }
            else if (timeCounter >= Utils.Instance.IdleDuration)
            {
                EnemyPhysics.Locked = false;
            }
            timeCounter += gameTime.ElapsedGameTime.TotalSeconds;
            EnemyPhysics.Update(gameTime);
            sprite.Update(gameTime);
        }

        private bool ReachedIdleSpot()
        {
            bool ReachedTop = (EnemyPhysics.Location.Y < EnemyPhysics.OriginalLocation.Y - sprite.Height) && !EnemyPhysics.IsMovingDown();
            bool ReachedBottom = (EnemyPhysics.Location.Y > EnemyPhysics.OriginalLocation.Y) && EnemyPhysics.IsMovingDown();

            return ReachedTop || ReachedBottom;
        }
    }
}
