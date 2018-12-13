using FooBarHappyHour.Factories;
using FooBarHappyHour.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FooBarHappyHour.MetaStates
{
    public class WorldTransitionState : IMetaState
    {
        private GameStateManager gameStateManager;
        private double remainingtransitionTime;

        public WorldTransitionState(GameStateManager gameStateManager)
        {
            this.gameStateManager = gameStateManager;
            if (gameStateManager.InPrimaryWorld)
            {
                gameStateManager.HiddenWorldTeleport();
            }
            else
            {
                gameStateManager.PrimaryWorldTeleport();
            }
            remainingtransitionTime = Utils.Instance.WorldTransitionTime;
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
            // Not used
        }

        public void Update(GameTime gameTime)
        {
            remainingtransitionTime -= gameTime.ElapsedGameTime.TotalSeconds;
            if (remainingtransitionTime < 0)
            {
                gameStateManager.State = new WorldState(gameStateManager);
            }
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            graphicsDevice.Clear(Color.Black);
        }
    }
}
