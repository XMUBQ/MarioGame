using FooBarHappyHour.Physics;

namespace FooBarHappyHour.Interfaces
{
    public interface IEnemy : IGameObject
    {
        EnemyPhysics EnemyPhysics { get; }
        bool EnemyCollidable { get; set; }
        void GoLeft();
        void GoRight();
        void Jump();
        bool IsDead { get; set; }
        void TakeDamage();
    }
}
