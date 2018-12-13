using FooBarHappyHour.MetaStates;

namespace FooBarHappyHour.Interfaces
{
    public interface ICommand
    {
        void Execute(PlayerSelection playerSelection);
    }
}
