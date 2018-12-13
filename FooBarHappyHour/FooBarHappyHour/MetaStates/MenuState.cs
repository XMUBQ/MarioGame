using FooBarHappyHour.Audio;
using FooBarHappyHour.Factories;
using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FooBarHappyHour.MetaStates
{
    class MenuState : IMetaState
    {
        private enum MenuOption { OnePlayer, TwoPlayer, Exit }
        private MenuOption currentOption;
        private GameStateManager gameStateManager;
        private SpriteFont spriteFont;
        private ISprite logo;
        private ISprite selector;
        private Vector2 logoLocation;
        private Vector2 selectorLocation1;
        private Vector2 selectorLocation2;
        private Vector2 selectorLocation3;
        private Vector2 onePlayerLocation;
        private Vector2 twoPlayerLocation;
        private Vector2 exitLocation;
        private double remainingCooldownTime;
        private bool cooldown;

        public MenuState(GameStateManager gameStateManager)
        {
            SongManager.Instance.PlayMenuSong();
            currentOption = MenuOption.OnePlayer;
            this.gameStateManager = gameStateManager;
            spriteFont = HUDFactory.Instance.SpriteFont;
            logo = HUDFactory.Instance.CreateLogoSprite();
            selector = HUDFactory.Instance.CreateRandomSelectorSprite();
            int width = SuperMarioBros.Instance.Window.ClientBounds.Width / 2;
            int height = SuperMarioBros.Instance.Window.ClientBounds.Height / 2;
            logoLocation = new Vector2(width - logo.Width / 2, height - logo.Height);
            selectorLocation1 = new Vector2(width - Utils.Instance.OnePlayer.Length * Utils.Instance.SelectorGap - selector.Width - 8, height + Utils.Instance.MenuPaddingSmall);
            selectorLocation2 = new Vector2(width - Utils.Instance.TwoPlayer.Length * Utils.Instance.SelectorGap - selector.Width - 8, height + Utils.Instance.MenuPaddingMedium);
            selectorLocation3 = new Vector2(width - Utils.Instance.Exit.Length * Utils.Instance.SelectorGap - selector.Width - 8, height + Utils.Instance.MenuPaddingLarge);
            onePlayerLocation = new Vector2(width - Utils.Instance.OnePlayer.Length * Utils.Instance.SelectorGap, height + Utils.Instance.MenuPaddingSmall);
            twoPlayerLocation = new Vector2(width - Utils.Instance.TwoPlayer.Length * Utils.Instance.SelectorGap, height + Utils.Instance.MenuPaddingMedium);
            exitLocation = new Vector2(width - Utils.Instance.Exit.Length * Utils.Instance.SelectorGap, height + Utils.Instance.MenuPaddingLarge);
            cooldown = false;
            remainingCooldownTime = Utils.Instance.MenuCoolDownTime;
        }

        public void Up(PlayerSelection playerSelection)
        {
            if (playerSelection == PlayerSelection.Player)
            {
                if (cooldown) return;

                cooldown = true;
                switch (currentOption)
                {
                    case MenuOption.OnePlayer:
                        // Does not change.
                        break;
                    case MenuOption.TwoPlayer:
                        currentOption = MenuOption.OnePlayer;
                        break;
                    case MenuOption.Exit:
                        currentOption = MenuOption.TwoPlayer;
                        break;
                }
                SoundFactory.Instance.PlayFireballSound();
            }
        }

        public void Down(PlayerSelection playerSelection)
        {
            if (playerSelection == PlayerSelection.Player)
            {
                if (cooldown) return;

                cooldown = true;
                switch (currentOption)
                {
                    case MenuOption.OnePlayer:
                        currentOption = MenuOption.TwoPlayer;
                        break;
                    case MenuOption.TwoPlayer:
                        currentOption = MenuOption.Exit;
                        break;
                    case MenuOption.Exit:
                        // Does not change.
                        break;
                }
                SoundFactory.Instance.PlayFireballSound();
            }
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
            if (playerSelection == PlayerSelection.Player)
            {
                if (cooldown) return;

                cooldown = true;
                switch (currentOption)
                {
                    case MenuOption.OnePlayer:
                        gameStateManager.Multiplayer = false;
                        break;
                    case MenuOption.TwoPlayer:
                        gameStateManager.Multiplayer = true;
                        break;
                    case MenuOption.Exit:
                        GameStateManager.ExitGame();
                        break;
                }
                gameStateManager.State = new IntroState(gameStateManager);
                SoundFactory.Instance.PlayCollectCoinSound();
            }
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
            // Not used.
        }

        public void Update(GameTime gameTime)
        {
            selector.Update(gameTime);
            if (cooldown)
            {
                remainingCooldownTime -= gameTime.ElapsedGameTime.TotalSeconds;
                if (remainingCooldownTime < 0)
                {
                    cooldown = false;
                    remainingCooldownTime = Utils.Instance.MenuCoolDownTime;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            graphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            logo.Draw(spriteBatch, logoLocation);
            switch (currentOption)
            {
                case MenuOption.OnePlayer:
                    selector.Draw(spriteBatch, selectorLocation1);
                    break;
                case MenuOption.TwoPlayer:
                    selector.Draw(spriteBatch, selectorLocation2);
                    break;
                case MenuOption.Exit:
                    selector.Draw(spriteBatch, selectorLocation3);
                    break;
            }
            spriteBatch.DrawString(spriteFont, Utils.Instance.OnePlayer, onePlayerLocation, Color.White);
            spriteBatch.DrawString(spriteFont, Utils.Instance.TwoPlayer, twoPlayerLocation, Color.White);
            spriteBatch.DrawString(spriteFont, Utils.Instance.Exit, exitLocation, Color.White);
            spriteBatch.End();
        }
    }
}
