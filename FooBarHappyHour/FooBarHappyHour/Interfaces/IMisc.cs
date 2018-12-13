using FooBarHappyHour.Physics;

namespace FooBarHappyHour.Interfaces
{
    public interface IMisc : IGameObject
    {      
        SceneryPhysics SceneryPhysics { get; }
    }
}
