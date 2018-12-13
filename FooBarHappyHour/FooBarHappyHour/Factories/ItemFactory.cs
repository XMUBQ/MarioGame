using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Items;
using FooBarHappyHour.Players;
using FooBarHappyHour.Score;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FooBarHappyHour.Factories
{
    public static class ItemFactory
    {

        public static void CreateItem(string itemType, Vector2 location)
        {
            switch (itemType)
            {
                case "PowerUp":
                    SuperMarioBros.Instance.GameStateManager.PrimaryWorld.Items.Add(new PowerUp(location, SuperMarioBros.Instance.GameStateManager.Player.IsSmall, new CollectDelegate(ScoreManager.Instance.CollectPowerUp)));
                    SoundFactory.Instance.PlaySpawnPowerUpSound();
                    break;
                case "Star":
                    SuperMarioBros.Instance.GameStateManager.PrimaryWorld.Items.Add(new SuperStar(location, new CollectDelegate(ScoreManager.Instance.CollectStar)));
                    SoundFactory.Instance.PlaySpawnPowerUpSound();
                    break;
                case "GreenMushroom":
                    SoundFactory.Instance.PlaySpawnPowerUpSound();
                    SuperMarioBros.Instance.GameStateManager.PrimaryWorld.Items.Add(new GreenMushroom(location, new CollectDelegate(ScoreManager.Instance.CollectGreenMushroom)));
                    break;
                case "Coin":
                    SoundFactory.Instance.PlayCollectCoinSound();
                    SuperMarioBros.Instance.GameStateManager.PrimaryWorld.Items.Add(new Coin(location, false, new CollectDelegate(ScoreManager.Instance.CollectDynamicCoin)));
                    break;
            } 
        }
    }
}
