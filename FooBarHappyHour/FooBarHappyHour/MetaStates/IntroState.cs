using FooBarHappyHour.Factories;
using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Audio;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FooBarHappyHour.Players;
using FooBarHappyHour.Utility;

namespace FooBarHappyHour.MetaStates
{
    public class IntroState : IMetaState
    {
        private GameStateManager gameStateManager;
        private double introTime;
        private double remainingIntroTime;
        private SpriteFont spriteFont;
        private ISprite mario;
        private string theWorld;
        private string theLives;
        private Vector2 marioLocation;
        private Vector2 worldLocation;
        private Vector2 livesLocation;

        public IntroState(GameStateManager gameStateManager)
        {
            SongManager.StopMusic();
            introTime = Utils.Instance.IntroTime;

            this.gameStateManager = gameStateManager;
            if (gameStateManager.Player == null)
            {
                gameStateManager.Player = new Player();
            }
            gameStateManager.CreatePrimaryWorld();
            gameStateManager.CreateHiddenWorld();
            remainingIntroTime = introTime;
            spriteFont = HUDFactory.Instance.SpriteFont;
            mario = HUDFactory.Instance.CreateMarioSprite(gameStateManager.Player.SizeState.StateName, gameStateManager.Player.PowerUpState.StateName);
            theWorld = Utils.Instance.World + gameStateManager.PrimaryWorld.MajorWorldID.ToString() + Utils.Instance.Dash + gameStateManager.PrimaryWorld.MinorWorldID.ToString();
            theLives = Utils.Instance.LivesX + gameStateManager.Lives.ToString(); ;
            int width = SuperMarioBros.Instance.Window.ClientBounds.Width / 2;
            int height = SuperMarioBros.Instance.Window.ClientBounds.Height / 2;
            worldLocation = new Vector2(width - theWorld.Length * Utils.Instance.HUDScreenWidthMultiplier, height);
            livesLocation = new Vector2(width - theLives.Length * Utils.Instance.HUDScreenWidthMultiplier + mario.Width / 2, height + Utils.Instance.IntroTextOffset);
            marioLocation = new Vector2(width - theLives.Length * Utils.Instance.HUDScreenWidthMultiplier - mario.Width, height + Utils.Instance.IntroTextOffset);
        }

        public void Up(PlayerSelection playerSelection)
        {
            // Not used.
        }

        public void Down(PlayerSelection playerSelection)
        {
            // Not used.
        }

        public void Left(PlayerSelection playerSelection)
        {
            // Not used.
        }

        public void Right(PlayerSelection playerSelection)
        {
            // Not used.
        }

        public void Ability(PlayerSelection playerSelection)
        {
            // Not used.
        }

        public void Jump(PlayerSelection playerSelection)
        {
            // Not used.
        }

        public void SwitchLeft()
        {
            // Not used.
        }

        public void SwitchRight()
        {
            // Not used.
        }

        public void Quit()
        {
            gameStateManager.ReturnToMenu();
        }

        public void Update(GameTime gameTime)
        {
            mario.Update(gameTime);
            remainingIntroTime -= gameTime.ElapsedGameTime.TotalSeconds;
            if (remainingIntroTime < 0)
            {
                gameStateManager.PrimaryWorldReady();
                gameStateManager.State = new WorldState(gameStateManager);
            }
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            graphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            mario.Draw(spriteBatch, marioLocation);
            spriteBatch.DrawString(spriteFont, theWorld, worldLocation, Color.White);
            spriteBatch.DrawString(spriteFont, theLives, livesLocation, Color.White);
            spriteBatch.End();
        }
    }
}
