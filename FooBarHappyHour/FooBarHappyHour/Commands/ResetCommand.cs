using FooBarHappyHour.Interfaces;
using FooBarHappyHour.MetaStates;

namespace FooBarHappyHour.Commands
{
    public class ResetCommand : ICommand
    {
        private GameStateManager gameStateManager;

        public ResetCommand(GameStateManager gameStateManager)
        {
            this.gameStateManager = gameStateManager;
        }

        public void Execute(PlayerSelection playerSelection)
        {
            gameStateManager.Reset();
        }
    }
}
