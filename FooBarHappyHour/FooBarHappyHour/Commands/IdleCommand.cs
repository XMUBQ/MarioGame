using FooBarHappyHour.Interfaces;
using FooBarHappyHour.MetaStates;

namespace FooBarHappyHour.Commands
{
    public class IdleCommand : ICommand
    {
        private GameStateManager gameStateManager;

        public IdleCommand(GameStateManager gameStateManager)
        {
            this.gameStateManager = gameStateManager;
        }

        public void Execute(PlayerSelection playerSelection)
        {
            gameStateManager.Idle();
        }
    }
}
