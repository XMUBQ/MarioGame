using FooBarHappyHour.Factories;
using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Physics;
using FooBarHappyHour.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FooBarHappyHour.Misc
{
    public class Fireball : IGameObject
    {
        public bool RemovalFlag { get; set; }
        public bool IsFacingRight { get; set; }
        public bool Collidable { get => !exploded; set => Collidable = value; }

        public IPhysics Physics { get => FireballPhysics; }
        public FireballPhysics FireballPhysics { get; private set; }
        public Rectangle Rectangle => new Rectangle((int)Physics.Location.X, (int)Physics.Location.Y, sprite.Width, sprite.Height);
        
        private float explosionTimer;
        private ISprite sprite;
        private bool exploded;

        public Fireball(Vector2 location, bool isFacingRight)
        {
            FireballPhysics = new FireballPhysics(false, true, location);
            IsFacingRight = isFacingRight;
            if (isFacingRight)
            {
                sprite = MiscSpriteFactory.Instance.CreateFireballRightSprite();
            }
            else
            {
                sprite = MiscSpriteFactory.Instance.CreateFireballLeftSprite();
            }
            explosionTimer = Utils.Instance.ExplosionTime;
        }

        public void Jump()
        {
            FireballPhysics.Jump();
        }

        public void Explode()
        {
            exploded = true;
            FireballPhysics.ResetMotion();
            FireballPhysics.Locked = true;
            sprite = MiscSpriteFactory.Instance.CreateFireballExplosionSprite();
        }

        public void Update(GameTime gameTime)
        {
            if (!exploded)
            {
                if (IsFacingRight)
                {
                    FireballPhysics.MoveRight();
                }
                else
                {
                    FireballPhysics.MoveLeft();
                }
            }
            else
            {
                explosionTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (explosionTimer <= 0) RemovalFlag = true;
            }
            FireballPhysics.Update(gameTime);
            sprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, Physics.Location);
        }
    }
}
