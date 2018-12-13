using FooBarHappyHour.Interfaces;
using FooBarHappyHour.MetaStates;

namespace FooBarHappyHour.Commands
{
    public class SwitchRightCommand : ICommand
    {
        private GameStateManager gameStateManager;

        public SwitchRightCommand(GameStateManager gameStateManager)
        {
            this.gameStateManager = gameStateManager;
        }

        public void Execute(PlayerSelection playerSelection)
        {
            gameStateManager.SwitchRight();
        }
    }
}
