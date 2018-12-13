using FooBarHappyHour.Blocks;
using FooBarHappyHour.Enemies;
using FooBarHappyHour.Factories;
using FooBarHappyHour.Interfaces;
using static FooBarHappyHour.Collision.CollisionDetection;

namespace FooBarHappyHour.Collision
{
    public static class EnemyBlockCollisionHandler
    {
        public static void HandleCollision(IEnemy enemy, IBlock block, CollisionSide side)
        {
            if (!block.Collidable) return;

            if (side == CollisionSide.Top)
            {
                enemy.EnemyPhysics.CanJump = true;
            }

            switch (enemy)
            {
                case Goomba goomba:
                    HandleGoombaCollision(goomba, block, side);
                    break;
                case Koopa koopa:
                    HandleKoopaCollision(koopa, block, side);
                    break;
            }
        }

        public static void HandleGoombaCollision(Goomba goomba, IBlock block, CollisionSide side)
        {
            Physics.GeneralPhysics.RepelObject(block, goomba, side);

            if (block.BlockPhysics.BlockBumped)
            {
                goomba.BeFlipped();
                SoundFactory.Instance.PlayKickEnemySound();
            }
            else
            {
                if (IsHorizontalCollision(side))
                {
                    goomba.ChangeDirection(side);
                }
            }
        }

        public static void HandleKoopaCollision(Koopa koopa, IBlock block, CollisionSide side)
        {
            Physics.GeneralPhysics.RepelObject(block, koopa, side);

            if (block.BlockPhysics.BlockBumped)
            {
                koopa.BeFlipped();
                SoundFactory.Instance.PlayKickEnemySound();
            }
            else
            {
                if (IsHorizontalCollision(side))
                {
                    koopa.ChangeDirection(side);
                    if (koopa.IsKicked) SoundFactory.Instance.PlayBumpBlockSound();
                }
            }
        }
    }
}
