using FooBarHappyHour.Physics;

namespace FooBarHappyHour.Interfaces
{
    public delegate void CollectBlockDelegate(IBlock block);
    public interface IBlock : IGameObject
    {
        BlockPhysics BlockPhysics { get; }
    }
}
