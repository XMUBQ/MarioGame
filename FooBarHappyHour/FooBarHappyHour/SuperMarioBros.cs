using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using FooBarHappyHour.Controllers;
using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Factories;
using FooBarHappyHour.MetaStates;
using System.Threading;
using System.Windows.Input;
using FooBarHappyHour.Utility;

namespace FooBarHappyHour
{
    public class SuperMarioBros : Game
    {
        public GameStateManager GameStateManager { get; set; }
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private List<IController> controllerList;
        private static readonly SuperMarioBros instance = new SuperMarioBros();
        public static SuperMarioBros Instance { get => instance; }

        private SuperMarioBros()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {
            controllerList = new List<IController>();
            graphics.PreferredBackBufferWidth = 640;
            graphics.PreferredBackBufferHeight = 480;
            graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            PlayerSpriteFactory.Instance.LoadAllTextures(Content);
            EnemySpriteFactory.Instance.LoadAllTextures(Content);
            ItemSpriteFactory.Instance.LoadAllTextures(Content);
            BlockSpriteFactory.Instance.LoadAllTextures(Content);
            ScenerySpriteFactory.Instance.LoadAllTextures(Content);
            MiscSpriteFactory.Instance.LoadAllTextures(Content);
            SoundFactory.Instance.LoadAllSFX(Content);
            HUDFactory.Instance.LoadAllFonts(Content);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GameStateManager == null)
            {
                GameStateManager = new GameStateManager();
                controllerList = new List<IController>
                {
                    new KeyboardController(GameStateManager),
                    new GamepadController(GameStateManager)
                };
            }
            else
            {
                bool playerReceivedUserInput = false;
                foreach (IController controller in controllerList)
                {
                    controller.Update(gameTime);
                    if (controller.PlayerReceivedUserInput)
                    {
                        playerReceivedUserInput = true;
                    }
                }
                if (!playerReceivedUserInput)
                {
                    GameStateManager.Idle();
                }
                GameStateManager.Update(gameTime);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            if (GameStateManager != null)
            {
                GameStateManager.Draw(spriteBatch, GraphicsDevice);
            }
        }
    }
}
