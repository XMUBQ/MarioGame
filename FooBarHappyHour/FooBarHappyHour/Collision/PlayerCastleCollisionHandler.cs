using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Score;
using FooBarHappyHour.Misc;
using FooBarHappyHour.Utility;
using Microsoft.Xna.Framework;
using System;
using static FooBarHappyHour.Collision.CollisionDetection;

namespace FooBarHappyHour.Collision
{
    public static class PlayerCastleCollisionHandler
    {
        public static void HandleCollision(IPlayer player, Castle castle)
        {
            Vector2 playerLocation = player.MovementState.Location;
            Vector2 castleLocation = castle.SceneryPhysics.Location;

            float castleDoorRightBoundary = (castleLocation.X + (castle.Rectangle.Width - Utils.Instance.CastleDoorRelaiveLocationX));
            float castleDoorLeftBoundary = (castleLocation.X + Utils.Instance.CastleDoorRelaiveLocationX);
            float castleDoorTopBoundary = (castleLocation.Y + Utils.Instance.CastleDoorRelaiveLocationY);
            float castleDoorButtomBoundary = (castleLocation.Y + castle.Rectangle.Height);

            bool playerDoorRelativeLocationX = (playerLocation.X <= castleDoorRightBoundary) && (playerLocation.X >= castleDoorLeftBoundary);
            bool playerDoorRelativeLocationY = (playerLocation.Y <= castleDoorButtomBoundary) && (playerLocation.Y >= castleDoorTopBoundary);

            if (playerDoorRelativeLocationX && playerDoorRelativeLocationY) SuperMarioBros.Instance.GameStateManager.Win = true;
        }
    }
}
