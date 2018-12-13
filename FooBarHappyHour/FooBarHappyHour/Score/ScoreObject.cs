using FooBarHappyHour.Factories;
using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Physics;
using FooBarHappyHour.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FooBarHappyHour.Score
{
    public class ScoreObject
    {
        private SpriteFont spriteFont;
        private readonly string score;
        private Vector2 scoreLocation;
        private Vector2 originalScoreLocation;
        private bool lockScoreLocation;
        public bool RemovalFlag { get; set; }

        public ScoreObject(int score, Vector2 scoreLocation, bool lockScoreLocation)
        {
            spriteFont = HUDFactory.Instance.SpriteFont;
            SuperMarioBros.Instance.GameStateManager.Score += score;
            this.score = ((int)score).ToString();
            this.scoreLocation = scoreLocation;
            this.originalScoreLocation = scoreLocation;
            this.lockScoreLocation = lockScoreLocation;
            this.RemovalFlag = false;
        }

        public void Update()
        {
            scoreLocation.Y --;
            if(!lockScoreLocation) scoreLocation.X = SuperMarioBros.Instance.GameStateManager.Player.MovementState.Location.X;
            if ((int)Math.Abs(((scoreLocation.Y) - (originalScoreLocation.Y))) > Utils.Instance.ScoreHeight)
            {
                RemovalFlag = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(spriteFont, score, scoreLocation, Color.White);
        }
    }
}
