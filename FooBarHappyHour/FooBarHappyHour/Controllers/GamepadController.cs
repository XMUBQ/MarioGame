using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using FooBarHappyHour.Commands;
using FooBarHappyHour.Interfaces;
using Microsoft.Xna.Framework;
using FooBarHappyHour.MetaStates;

namespace FooBarHappyHour.Controllers
{
    public class GamepadController : IController
    {
        public bool PlayerReceivedUserInput { get; private set; }
        private readonly Dictionary<Buttons, ICommand> buttonCommandMap;

        public GamepadController(GameStateManager gameStateManager)
        {
            PlayerReceivedUserInput = false;
            buttonCommandMap = new Dictionary<Buttons, ICommand>
            {
                { Buttons.X, new PauseCommand(gameStateManager) },
                { Buttons.Back, new QuitCommand(gameStateManager) },
                { Buttons.Start, new ResetCommand(gameStateManager)},
                { Buttons.A, new JumpCommand(gameStateManager) },
                { Buttons.B, new AbilityCommand(gameStateManager) },
                { Buttons.DPadLeft, new LeftCommand(gameStateManager) },
                { Buttons.DPadRight, new RightCommand(gameStateManager) },
                { Buttons.DPadDown, new DownCommand(gameStateManager) },
                { Buttons.DPadUp, new UpCommand(gameStateManager) }
            };
        }

        public void Update(GameTime gameTime)
        {
            PlayerReceivedUserInput = false;
            GamePadState state = GamePad.GetState(PlayerIndex.One);
            foreach (KeyValuePair<Buttons, ICommand> buttonCommandPair in buttonCommandMap)
            {
                if (state.IsButtonDown(buttonCommandPair.Key))
                {
                    buttonCommandPair.Value.Execute(PlayerSelection.Player);
                    PlayerReceivedUserInput = true;
                } 
            }
        }
    }
}
