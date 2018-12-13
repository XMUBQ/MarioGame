using FooBarHappyHour.Enemies;
using FooBarHappyHour.Interfaces;
using FooBarHappyHour.States;
using FooBarHappyHour.Score;
using FooBarHappyHour.Utility;
using Microsoft.Xna.Framework;
using System;
using static FooBarHappyHour.Collision.CollisionDetection;
using FooBarHappyHour.Factories;

namespace FooBarHappyHour.Collision
{
    public static class PlayerEnemyCollisionHandler
    {
        public static void HandleCollision(IPlayer player, IEnemy enemy, CollisionSide side)
        {
            switch (enemy)
            {
                case Goomba goomba:
                    HandleGoombaCollision(player, goomba, side);
                    break;
                case Koopa koopa:
                    HandleKoopaCollision(player, koopa, side);
                    break;
                case PiranhaPlant piranhaPlant:
                    HandlePiranhaCollision(player, piranhaPlant);
                    break;
            }
        }

        public static void HandleGoombaCollision(IPlayer player, Goomba goomba, CollisionSide side)
        {
            if (player.IsInvincible)
            {
                goomba.BeFlipped();
                ScoreManager.CollectEnemyScore(goomba, false);
                SoundFactory.Instance.PlayKickEnemySound();
            }
            else
            {
                if (side == CollisionSide.Top)
                {
                    goomba.BeStomped();
                    ScoreManager.CollectEnemyScore(goomba, true);
                    player.MovementState.Bounce();
                    SoundFactory.Instance.PlayStompEnemySound();
                }
                else
                {
                    player.TakeDamage();
                }
            }
        }

        public static void HandleKoopaCollision(IPlayer player, Koopa koopa, CollisionSide side)
        {
            if (player.IsInvincible)
            {
                koopa.BeFlipped();
                ScoreManager.CollectEnemyScore(koopa, false);
                SoundFactory.Instance.PlayKickEnemySound();
            }
            else
            {
                if (koopa.State is KoopaStompedIdleState || koopa.State is KoopaRevivingState)
                {
                    switch (side)
                    {
                        case CollisionSide.Top:
                            if (koopa.Physics.Location.X <= player.MovementState.Location.X)
                            {
                                koopa.EnemyPhysics.FaceLeft();
                            }
                            else
                            {
                                koopa.EnemyPhysics.FaceRight();
                            }
                            player.MovementState.Bounce();
                            ScoreManager.CollectEnemyScore(koopa, true);
                            break;
                        case CollisionSide.Left:
                            koopa.EnemyPhysics.FaceRight();
                            break;
                        case CollisionSide.Right:
                            koopa.EnemyPhysics.FaceLeft();
                            break;
                        default:
                            break;
                    }
                    koopa.BeKicked();
                }
                else
                {
                    if (side == CollisionSide.Top)
                    {
                        koopa.BeStomped();
                        ScoreManager.CollectEnemyScore(koopa, true);
                        player.MovementState.Bounce();
                    }
                    else
                    {
                        player.TakeDamage();
                    }
                }
            }
        }

        public static void HandlePiranhaCollision(IPlayer player, PiranhaPlant piranhaPlant)
        {
            if (player.IsInvincible)
            {
                piranhaPlant.TakeDamage();
                ScoreManager.CollectEnemyScore(piranhaPlant, false);
                SoundFactory.Instance.PlayKickEnemySound();
            }
            else
            {
                player.TakeDamage();
            }
        }
    }
}
