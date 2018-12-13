using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Misc;
using FooBarHappyHour.Utility;
using FooBarHappyHour.Score;
using static FooBarHappyHour.Collision.CollisionDetection;
using FooBarHappyHour.Factories;

namespace FooBarHappyHour.Collision
{
    public static class FireballEnemyCollisionHandler
    {
        public static void HandleCollision(Fireball fireball, IEnemy enemy)
        {
            if (fireball.Collidable && enemy.Collidable)
            {
                enemy.TakeDamage();
                ScoreManager.CollectEnemyScore(enemy, false);
                fireball.Explode();
                SoundFactory.Instance.PlayKickEnemySound();
            }
        }
    }
}
