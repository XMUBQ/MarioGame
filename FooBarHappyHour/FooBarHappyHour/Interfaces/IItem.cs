using FooBarHappyHour.Physics;

namespace FooBarHappyHour.Interfaces
{
    public delegate void CollectDelegate(IItem item);
    public interface IItem : IGameObject
    {
        ItemPhysics ItemPhysics { get; }
        void BeCollected();
    }
}
