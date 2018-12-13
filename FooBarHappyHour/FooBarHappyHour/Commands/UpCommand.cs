using FooBarHappyHour.Interfaces;
using FooBarHappyHour.MetaStates;

namespace FooBarHappyHour.Commands
{
    public class UpCommand : ICommand
    {
        private GameStateManager gameStateManager;

        public UpCommand(GameStateManager gameStateManager)
        {
            this.gameStateManager = gameStateManager;
        }

        public void Execute(PlayerSelection playerSelection)
        {
            gameStateManager.Up(playerSelection);
        }
    }
}
