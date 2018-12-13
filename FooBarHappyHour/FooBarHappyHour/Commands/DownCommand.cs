using FooBarHappyHour.Interfaces;
using FooBarHappyHour.MetaStates;

namespace FooBarHappyHour.Commands
{
    public class DownCommand : ICommand
    {
        private GameStateManager gameStateManager;

        public DownCommand(GameStateManager gameStateManager)
        {
            this.gameStateManager = gameStateManager;
        }

        public void Execute(PlayerSelection playerSelection)
        {
            gameStateManager.Down(playerSelection);
        }
    }
}
