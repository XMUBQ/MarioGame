using FooBarHappyHour.Enemies;
using FooBarHappyHour.Factories;
using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Score;
using FooBarHappyHour.Utility;
using static FooBarHappyHour.Collision.CollisionDetection;

namespace FooBarHappyHour.Collision
{
    public static class EnemyEnemyCollisionHandler
    {
        public static void HandleCollision(IEnemy colliderEnemy, IEnemy collidedEnemy, CollisionSide side)
        {
            if (!collidedEnemy.Collidable) return;
            if (!collidedEnemy.EnemyCollidable) return;
            if (!colliderEnemy.EnemyCollidable) return;

            switch (colliderEnemy)
            {
                case Goomba goomba:
                    HandleGoombaCollision(goomba, collidedEnemy, side);
                    break;
                case Koopa koopa:
                    HandleKoopaCollision(koopa, collidedEnemy, side);
                    break;
            }
        }

        public static void HandleGoombaCollision(Goomba goomba, IEnemy collidedEnemy, CollisionSide side)
        {
            Physics.GeneralPhysics.RepelObject(collidedEnemy, goomba, side);

            if (!IsHorizontalCollision(side)) return;

            switch (collidedEnemy)
            {
                case Goomba collidedGoomba:
                    collidedGoomba.ChangeDirection(GetOppositeSide(side));
                    break;
                case Koopa collidedKoopa:
                    if (collidedKoopa.IsKicked)
                    {
                        goomba.TakeDamage();
                        SoundFactory.Instance.PlayKickEnemySound();
                    }
                    else
                    {
                        collidedKoopa.ChangeDirection(GetOppositeSide(side));
                    }
                    break;
            }
            goomba.ChangeDirection(side);
        }

        public static void HandleKoopaCollision(Koopa koopa, IEnemy collidedEnemy, CollisionSide side)
        {
            Physics.GeneralPhysics.RepelObject(collidedEnemy, koopa, side);

            if (koopa.IsKicked)
            {
                collidedEnemy.TakeDamage();
                SoundFactory.Instance.PlayKickEnemySound();
            }
            else
            {
                if (!IsHorizontalCollision(side)) return;

                switch (collidedEnemy)
                {
                    case Goomba collidedGoomba:
                        collidedGoomba.ChangeDirection(GetOppositeSide(side));
                        break;
                    case Koopa collidedKoopa:
                        if (collidedKoopa.IsKicked)
                        {
                            koopa.TakeDamage();
                            SoundFactory.Instance.PlayKickEnemySound();
                        }
                        else
                        {
                            collidedKoopa.ChangeDirection(GetOppositeSide(side));
                        }
                        break;
                }

                koopa.ChangeDirection(side);
            }
        }
    }
}
