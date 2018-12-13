using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using FooBarHappyHour.Commands;
using FooBarHappyHour.Interfaces;
using FooBarHappyHour.MetaStates;
using Microsoft.Xna.Framework;

namespace FooBarHappyHour.Controllers
{
    public class KeyboardController : IController
    {
        public bool PlayerReceivedUserInput { get; private set; }
        private readonly Dictionary<Keys, ICommand> playerCommandMap;
        private readonly Dictionary<Keys, ICommand> enemyCommandMap;
        public KeyboardController(GameStateManager gameStateManager)
        {
            PlayerReceivedUserInput = false;            
            playerCommandMap = new Dictionary<Keys, ICommand>
            {
                { Keys.P, new PauseCommand(gameStateManager) },
                { Keys.Q, new QuitCommand(gameStateManager) },
                { Keys.X, new AbilityCommand(gameStateManager) },
                { Keys.Space, new JumpCommand(gameStateManager) },
                { Keys.A, new LeftCommand(gameStateManager) },
                { Keys.D, new RightCommand(gameStateManager) },
                { Keys.S, new DownCommand(gameStateManager) },
                { Keys.W, new UpCommand(gameStateManager) }
            };
            enemyCommandMap = new Dictionary<Keys, ICommand>
            {
                { Keys.NumPad4, new LeftCommand(gameStateManager) },
                { Keys.NumPad6, new RightCommand(gameStateManager) },
                { Keys.NumPad7, new SwitchLeftCommand(gameStateManager) },
                { Keys.NumPad9, new SwitchRightCommand(gameStateManager) },
                { Keys.NumPad8, new JumpCommand(gameStateManager) }
            };
        }

        public void Update(GameTime gameTime)
        {                       
            KeyboardState state = Keyboard.GetState();
            PlayerReceivedUserInput = false;
            foreach (KeyValuePair<Keys, ICommand> keyCommandPair in playerCommandMap)
            {
                if (state.IsKeyDown(keyCommandPair.Key))
                {
                    keyCommandPair.Value.Execute(PlayerSelection.Player);
                    PlayerReceivedUserInput = true;
                }
            }
            foreach (KeyValuePair<Keys, ICommand> keyCommandPair in enemyCommandMap)
            {
                if (state.IsKeyDown(keyCommandPair.Key))
                {
                    keyCommandPair.Value.Execute(PlayerSelection.Enemy);
                }
            }            
        }
    }
}
