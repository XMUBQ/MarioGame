using FooBarHappyHour.Interfaces;
using Microsoft.Xna.Framework;
using System;
using FooBarHappyHour.Blocks;
using FooBarHappyHour.Misc;
using System.Collections.Generic;
using FooBarHappyHour.Utility;

namespace FooBarHappyHour.Collision
{
    public class CollisionDetection
    {
        public enum CollisionSide { Left, Right, Top, Bottom };
        private IWorld world;

        public CollisionDetection(IWorld world)
        {
            this.world = world;
        }

        public void CheckAllCollisions()
        {
            if (SuperMarioBros.Instance.GameStateManager.Player.Collidable)
            {
                CheckPlayerBlockCollision();
                CheckPlayerItemCollision();  // Check items first so that powerup effects take place before detecting collision with enemies
                CheckPlayerEnemyCollision(); // Check enemies next so that we don't have to check blocks if mario is dead
                CheckPlayerTeleportedCollision();
                CheckPlayerFlagpoleCollision();
                CheckPlayerCastleCollision();
            }
            CheckEnemyBlockCollision();
            CheckItemBlockCollision();
            CheckFireballBlockCollision();
            CheckEnemyEnemyCollision();
            CheckFireballEnemyCollision();
        }

        private IList<IBlock> FindBlocksCollidedWithGameObject(IGameObject gameObject)
        {
            IList<IBlock> blocksInScope = new List<IBlock>();
            float gameObjectLocationX = gameObject.Physics.Location.X;
            float gameObjectLocationY = gameObject.Physics.Location.Y;
            int blocksWidth = Utils.Instance.CommonObjectSize;
            int blocksHeight = Utils.Instance.CommonObjectSize;
            int blockIndexX = (int)Math.Floor(gameObjectLocationX / blocksWidth);
            int blockIndexY = (int)Math.Floor(gameObjectLocationY / blocksHeight);
            int detectionRange = Utils.Instance.BlockDetectionRange;
            for (int blockIndexOffsetX = -detectionRange; blockIndexOffsetX <= detectionRange; blockIndexOffsetX++)
            {
                for (int blockIndexOffsetY = -detectionRange; blockIndexOffsetY <= detectionRange; blockIndexOffsetY++)
                {
                    int newBlockIndexX = blockIndexX + blockIndexOffsetX;
                    int newBlockIndexY = blockIndexY + blockIndexOffsetY;
                    if (BlockIsInBounds(newBlockIndexX, newBlockIndexY))
                    {
                        IBlock block = world.Blocks[newBlockIndexX].OneBlockLevel[newBlockIndexY];
                        blocksInScope.Add(block);
                    }
                }
            }
            return blocksInScope;
        }

        private void CheckPlayerBlockCollision()
        {
            IList<IBlock> blocksInScope = FindBlocksCollidedWithGameObject(SuperMarioBros.Instance.GameStateManager.Player);

            foreach (IBlock block in blocksInScope)
            {
                if (block != null)
                {
                    Rectangle playerHitbox = SuperMarioBros.Instance.GameStateManager.Player.Rectangle;
                    Rectangle blockHitbox = block.Rectangle;
                    Rectangle intersection = Rectangle.Intersect(playerHitbox, blockHitbox);
                    if (!intersection.IsEmpty)
                    {
                        CollisionSide side = GetCollisionSide(intersection, playerHitbox, blockHitbox);
                        PlayerBlockCollisionHandler.HandleCollision(SuperMarioBros.Instance.GameStateManager.Player, block, side);
                    }
                }
            }
        }

        private void CheckPlayerEnemyCollision()
        {
            Rectangle playerHitbox = SuperMarioBros.Instance.GameStateManager.Player.Rectangle;
            foreach (IEnemy enemy in world.Enemies)
            {
                Rectangle enemyHitbox = enemy.Rectangle;
                Rectangle intersection = Rectangle.Intersect(playerHitbox, enemyHitbox);

                if (!intersection.IsEmpty)
                {
                    CollisionSide side = GetCollisionSide(intersection, playerHitbox, enemyHitbox);
                    EnemyCollisionHandler.HandleCollision(enemy, SuperMarioBros.Instance.GameStateManager.Player, side);
                }
            }
        }

        private void CheckPlayerTeleportedCollision()  // Slight repeating of code because handling collision between game objects are sufficiently different, avoid tight coupling
        {
            Rectangle playerHitbox = SuperMarioBros.Instance.GameStateManager.Player.Rectangle;
            Rectangle outTeleporterHitbox = world.OutTeleporter.Rectangle;
            Rectangle outIntersection = Rectangle.Intersect(playerHitbox, outTeleporterHitbox);

            if (!outIntersection.IsEmpty)
            {
                CollisionSide side = GetCollisionSide(outIntersection, playerHitbox, outTeleporterHitbox);
                PlayerTeleporterCollisionHandler.HandleCollision(SuperMarioBros.Instance.GameStateManager.Player, SuperMarioBros.Instance.GameStateManager.PrimaryWorld.OutTeleporter, side);
            }
        }
        private void CheckPlayerFlagpoleCollision()
        {
            Rectangle playerHitbox = SuperMarioBros.Instance.GameStateManager.Player.Rectangle;
            foreach (Flagpole flagpole in world.Flagpoles)
            {
                Rectangle flagpoleHitbox = flagpole.Rectangle;
                Rectangle intersection = Rectangle.Intersect(playerHitbox, flagpoleHitbox);

                if (!intersection.IsEmpty) PlayerFlagpoleCollisionHandler.HandleCollision(SuperMarioBros.Instance.GameStateManager.Player, flagpole);
            }
        }

        private void CheckPlayerCastleCollision()
        {
            Rectangle playerHitbox = SuperMarioBros.Instance.GameStateManager.Player.Rectangle;
            foreach (Castle castle in world.Castles)
            {
                Rectangle castleHitbox = castle.Rectangle;
                Rectangle intersection = Rectangle.Intersect(playerHitbox, castleHitbox);

                if (!intersection.IsEmpty) PlayerCastleCollisionHandler.HandleCollision(SuperMarioBros.Instance.GameStateManager.Player, castle);
            }

        }
        private void CheckPlayerItemCollision()  // Slight repeating of code because handling collision between game objects are sufficiently different, avoid tight coupling
        {
            foreach (IItem item in world.Items)
            {
                Rectangle playerHitbox = SuperMarioBros.Instance.GameStateManager.Player.Rectangle;
                Rectangle itemHitbox = item.Rectangle;
                Rectangle intersection = Rectangle.Intersect(playerHitbox, itemHitbox);

                if (!intersection.IsEmpty) PlayerTeleportedCollisionHandler.HandleCollision(SuperMarioBros.Instance.GameStateManager.Player, item);
            }
        }

        private void CheckItemBlockCollision()
        {
            foreach (IItem item in world.Items)
            {
                IList<IBlock> blocksInScope = FindBlocksCollidedWithGameObject(item);

                foreach (IBlock block in blocksInScope)
                {
                    if (block != null)
                    {
                        Rectangle itemHitbox = item.Rectangle;
                        Rectangle blockHitbox = block.Rectangle;
                        Rectangle intersection = Rectangle.Intersect(itemHitbox, blockHitbox);
                        if (!intersection.IsEmpty)
                        {
                            CollisionSide side = GetCollisionSide(intersection, itemHitbox, blockHitbox);
                            ItemBlockCollisionHandler.HandleCollision(block, item, side);
                        }
                    }
                }
            }
        }

        private void CheckEnemyBlockCollision()
        {
            foreach (IEnemy enemy in world.Enemies)
            {
                IList<IBlock> blocksInScope = FindBlocksCollidedWithGameObject(enemy);

                foreach (IBlock block in blocksInScope)
                {
                    if (block != null)
                    {
                        Rectangle enemyHitbox = enemy.Rectangle;
                        Rectangle blockHitbox = block.Rectangle;
                        Rectangle intersection = Rectangle.Intersect(enemyHitbox, blockHitbox);
                        if (!intersection.IsEmpty)
                        {
                            CollisionSide side = GetCollisionSide(intersection, enemyHitbox, blockHitbox);
                            EnemyCollisionHandler.HandleCollision(enemy, block, side);
                        }
                    }
                }
            }
        }

        private void CheckEnemyEnemyCollision()
        {
            foreach (IEnemy colliderEnemy in world.Enemies)
            {
                foreach (IEnemy collidedEnemy in world.Enemies)
                {
                    if (collidedEnemy != colliderEnemy)
                    {
                        Rectangle colliderEnemyHitbox = colliderEnemy.Rectangle;
                        Rectangle enemyHitbox = collidedEnemy.Rectangle;
                        Rectangle intersection = Rectangle.Intersect(colliderEnemyHitbox, enemyHitbox);

                        if (!intersection.IsEmpty)
                        {
                            CollisionSide side = GetCollisionSide(intersection, colliderEnemyHitbox, enemyHitbox);
                            EnemyCollisionHandler.HandleCollision(colliderEnemy, collidedEnemy, side);
                        }
                    }
                    
                }
            }
        }

        private void CheckFireballEnemyCollision()
        {
            foreach (Fireball fireball in world.Fireballs)
            {
                foreach (IEnemy collidedEnemy in world.Enemies)
                {
                    Rectangle fireballHitbox = fireball.Rectangle;
                    Rectangle enemyHitbox = collidedEnemy.Rectangle;
                    Rectangle intersection = Rectangle.Intersect(fireballHitbox, enemyHitbox);

                    if (!intersection.IsEmpty && !collidedEnemy.IsDead)
                    {
                        FireballEnemyCollisionHandler.HandleCollision(fireball, collidedEnemy);
                    }

                }
            }
        }

        private void CheckFireballBlockCollision()
        {
            foreach (Fireball fireball in world.Fireballs)
            {
                IList<IBlock> blocksInScope = FindBlocksCollidedWithGameObject(fireball);

                foreach (IBlock block in blocksInScope)
                {
                    if (block != null)
                    {
                        Rectangle fireballHitbox = fireball.Rectangle;
                        Rectangle blockHitbox = block.Rectangle;
                        Rectangle intersection = Rectangle.Intersect(fireballHitbox, blockHitbox);
                        if (!intersection.IsEmpty)
                        {
                            CollisionSide side = GetCollisionSide(intersection, fireballHitbox, blockHitbox);
                            FireballBlockCollisionHandler.HandleCollision(fireball, block, side);
                        }
                    }
                }
            }
        }

        private bool BlockIsInBounds(int blockIndexX, int blockIndexY)
        {
            bool objectIsInHorizontalBounds = (blockIndexX >= 0) && (blockIndexX < world.Width);
            bool objectIsInVerticalBounds = (blockIndexY >= 0) && (blockIndexY < world.Height);
            return objectIsInHorizontalBounds && objectIsInVerticalBounds;
        }

        private static CollisionSide GetCollisionSide(Rectangle intersection, Rectangle colliderHitbox, Rectangle collidedHitbox)
        {
            if (intersection.Width >= intersection.Height)  // Vertical collision
            {
                return colliderHitbox.Top <= collidedHitbox.Top ? CollisionSide.Top : CollisionSide.Bottom;
            }
            else // Horizontal collision
            {
                return colliderHitbox.Left >= collidedHitbox.Left ? CollisionSide.Right : CollisionSide.Left;
            }
        }

        public static CollisionSide GetOppositeSide(CollisionSide side)
        {
            CollisionSide oppositeSide;
            switch (side)
            {
                case CollisionSide.Top:
                    oppositeSide = CollisionSide.Bottom;
                    break;
                case CollisionSide.Bottom:
                    oppositeSide = CollisionSide.Top;
                    break;
                case CollisionSide.Left:
                    oppositeSide = CollisionSide.Right;
                    break;
                case CollisionSide.Right:
                    oppositeSide = CollisionSide.Left;
                    break;
                default:
                    throw new InvalidOperationException("Invalid collision side provided");
            }
            return oppositeSide;
        }

        public static bool IsHorizontalCollision(CollisionSide side)
        {
            return (side == CollisionSide.Left) || (side == CollisionSide.Right);
        }
    }
}
