using FooBarHappyHour.Interfaces;
using FooBarHappyHour.MetaStates;

namespace FooBarHappyHour.Commands
{
    public class JumpCommand : ICommand
    {
        private GameStateManager gameStateManager;

        public JumpCommand(GameStateManager gameStateManager)
        {
            this.gameStateManager = gameStateManager;
        }

        public void Execute(PlayerSelection playerSelection)
        {
            gameStateManager.Jump(playerSelection);
        }
    }
}
