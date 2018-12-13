using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FooBarHappyHour.Collision.CollisionDetection;

namespace FooBarHappyHour.Interfaces
{
    public interface IKoopaState : IEnemyState
    {
        void BeStomped();
        void BeFlipped();
        void BeKicked();
        void ChangeDirection(CollisionSide side);
    }
}
