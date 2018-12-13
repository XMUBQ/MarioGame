using FooBarHappyHour.Audio;
using FooBarHappyHour.Factories;
using FooBarHappyHour.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FooBarHappyHour.MetaStates
{
    public class GameOverState : IMetaState
    {
        private GameStateManager gameStateManager;
        private SpriteFont spriteFont;
        private Vector2 gameOverLocation;
        private double remainingIntroTime;

        public GameOverState(GameStateManager gameStateManager)
        {
            SongManager.Instance.PlayGameOverMusic();
            this.gameStateManager = gameStateManager;
            remainingIntroTime = Utils.Instance.IntroTime;
            spriteFont = HUDFactory.Instance.SpriteFont;
            gameOverLocation = new Vector2(SuperMarioBros.Instance.Window.ClientBounds.Width / 2 - Utils.Instance.GameOver.Length * Utils.Instance.SelectorGap, SuperMarioBros.Instance.Window.ClientBounds.Height / 2);
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
            remainingIntroTime -= gameTime.ElapsedGameTime.TotalSeconds;
            if (remainingIntroTime < 0)
            {
                gameStateManager.ReturnToMenu();
            }
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            graphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            spriteBatch.DrawString(spriteFont, Utils.Instance.GameOver, gameOverLocation, Color.White);
            spriteBatch.End();
        }
    }
}
