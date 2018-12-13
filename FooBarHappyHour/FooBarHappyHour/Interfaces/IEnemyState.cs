using FooBarHappyHour.Players;
using static FooBarHappyHour.Collision.CollisionDetection;

namespace FooBarHappyHour.Interfaces
{
    public interface IEnemyState : IGameObjectState
    {
        void GoLeft();
        void GoRight();
        void Jump();
        void TakeDamage();
    }
}
