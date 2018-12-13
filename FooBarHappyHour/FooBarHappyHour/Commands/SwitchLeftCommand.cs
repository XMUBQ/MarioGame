using FooBarHappyHour.Interfaces;
using FooBarHappyHour.MetaStates;

namespace FooBarHappyHour.Commands
{
    public class SwitchLeftCommand : ICommand
    {
        private GameStateManager gameStateManager;

        public SwitchLeftCommand(GameStateManager gameStateManager)
        {
            this.gameStateManager = gameStateManager;
        }

        public void Execute(PlayerSelection playerSelection)
        {
            gameStateManager.SwitchLeft();
        }
    }
}
