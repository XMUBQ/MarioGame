using FooBarHappyHour.Interfaces;
using static FooBarHappyHour.Collision.CollisionDetection;
using FooBarHappyHour.Items;
using FooBarHappyHour.Blocks;
using Microsoft.Xna.Framework;
using System;

namespace FooBarHappyHour.Collision
{
    public static class ItemBlockCollisionHandler
    {
        public static void HandleCollision(IGameObject block, IItem item, CollisionSide side)
        {
            if (!block.Collidable || !item.Collidable) return;

            switch (item)
            {
                case GreenMushroom greenMushroom:
                    HandleGreenMushroomCollision(block, greenMushroom, side);
                    break;
                case SuperStar superStar:
                    HandleSuperStarCollision(block, superStar, side);
                    break;
                 case PowerUp powerUp:
                    HandlePowerUpCollision(block, powerUp, side);
                    break;
            }
        }

        public static void HandlePowerUpCollision(IGameObject block, IGameObject powerUp, CollisionSide side)
        {
            Physics.GeneralPhysics.RepelObject(block, powerUp, side);

            if (IsHorizontalCollision(side)) ChangeDirection(powerUp as PowerUp, side);
        }

        public static void HandleGreenMushroomCollision(IGameObject block, IGameObject greenMushroom, CollisionSide side)
        {
            Physics.GeneralPhysics.RepelObject(block, greenMushroom, side);

            if (IsHorizontalCollision(side)) ChangeDirection(greenMushroom as GreenMushroom, side);
        }

        public static void HandleSuperStarCollision(IGameObject block, SuperStar superStar, CollisionSide side)
        {
            Physics.GeneralPhysics.RepelObject(block, superStar, side);

            if (IsHorizontalCollision(side)) ChangeDirection(superStar, side);

            if (side == CollisionSide.Top) superStar.Jump();
        }

        private static void ChangeDirection(IItem item, CollisionSide side)
        {
            if (side == CollisionSide.Left)
            {
                item.ItemPhysics.MoveLeft();
            }
            else if (side == CollisionSide.Right)
            {
                item.ItemPhysics.MoveRight();
            }
        }
    }
}
