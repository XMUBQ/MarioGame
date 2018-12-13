using FooBarHappyHour.Physics;

namespace FooBarHappyHour.Interfaces
{
    public interface IScenery : IGameObject
    {      
        SceneryPhysics SceneryPhysics { get; }
    }
}
