using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Items;
using FooBarHappyHour.Utility;
using FooBarHappyHour.Score;
using System;

namespace FooBarHappyHour.Collision
{
    public static class PlayerTeleportedCollisionHandler
    {
        public static void HandleCollision(IPlayer player, IItem item)
        {
            switch (item)
            {
                case Coin coin:
                    HandleCoinCollision(coin);
                    break;
                case GreenMushroom greenMushroom:
                    HandleGreenMushroomCollision(player, greenMushroom);
                    break;
                case PowerUp powerUp:
                    HandlePowerUpCollision(player, powerUp);
                    break;
                case SuperStar superStar:
                    HandleSuperStarCollision(player, superStar);
                    break;
            }
        }

        private static void HandlePowerUpCollision(IPlayer player, PowerUp powerUp)
        {
            player.UsePowerUp();
            powerUp.BeCollected();
        }

        private static void HandleGreenMushroomCollision(IPlayer player, GreenMushroom greenMushroom)
        {
            player.UseGreenMushroom();
            greenMushroom.BeCollected();
        }

        private static void HandleSuperStarCollision(IPlayer player, SuperStar superStar)
        {
            player.UseSuperStar();
            superStar.BeCollected();
        }

        private static void HandleCoinCollision(Coin coin)
        {
            if (coin.IsStatic) coin.BeCollected();
        }
    }
}
