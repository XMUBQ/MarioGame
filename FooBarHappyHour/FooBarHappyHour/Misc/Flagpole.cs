using FooBarHappyHour.Factories;
using FooBarHappyHour.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FooBarHappyHour.Physics;
using FooBarHappyHour.Utility;

namespace FooBarHappyHour.Misc
{
    public class Flagpole : IMisc
    {
        public Rectangle Rectangle => new Rectangle((int)SceneryPhysics.Location.X + flagSprite.Width / 2, (int)SceneryPhysics.Location.Y, flagSprite.Width, flagSprite.Height);
        public IPhysics Physics { get => SceneryPhysics; }
        public SceneryPhysics SceneryPhysics { get; private set; }
        public bool Collidable { get; set; }
        public bool RemovalFlag { get; set; }
        public bool LowerFlag { get; set; }
        public int flagFrame { get; set; }
        public bool FlagReached { get; set; }
        public bool FlagLowered { get; set; }
        private double FlagFrameTime;
        private double currentTime;
        private ISprite flagSprite;

        public Flagpole(Vector2 location)
        {
            Collidable = true;
            RemovalFlag = false;
            LowerFlag = false;
            FlagLowered  = false;
            FlagReached  = false;
            flagFrame = 1;
            FlagFrameTime = Utils.Instance.FlagFrameRate;
            flagSprite = MiscSpriteFactory.Instance.CreateFlagpoleSprite();
            SceneryPhysics = new SceneryPhysics(location);
        }

        public void Update(GameTime gameTime)
        {
            if (LowerFlag)
            {
                flagSprite.Update(gameTime);
                 if (flagFrame == Utils.Instance.FlagPoleFrames)
                {
                    LowerFlag = false;
                    FlagLowered = true;
                } 
                currentTime += gameTime.ElapsedGameTime.TotalSeconds;
                if (currentTime > FlagFrameTime)
                {
                    flagFrame++;
                    currentTime = 0d;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            flagSprite.Draw(spriteBatch, Physics.Location);
        }
    }
}
