using FooBarHappyHour.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FooBarHappyHour.Collision.CollisionDetection;

namespace FooBarHappyHour.Collision
{
    public static class EnemyCollisionHandler
    {
        public static void HandleCollision(IEnemy enemy, IGameObject collidedObject, CollisionSide side)
        {
            if (!enemy.Collidable) return;

            switch (collidedObject)
            {
                case IPlayer player:
                    PlayerEnemyCollisionHandler.HandleCollision(player, enemy, side);
                    break;
                case IEnemy collidedEnemy:
                    EnemyEnemyCollisionHandler.HandleCollision(enemy, collidedEnemy, side);
                    break;
                case IBlock block:
                    EnemyBlockCollisionHandler.HandleCollision(enemy, block, side);
                    break;
            }
        }

        public static void ChangeDirection(IEnemy enemy, CollisionSide side)
        {
            if (side == CollisionSide.Left)
            {
                enemy.EnemyPhysics.FaceLeft();
            } 
            else if (side == CollisionSide.Right)
            {
                enemy.EnemyPhysics.FaceRight();
            }
        }
    }
}
