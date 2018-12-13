using FooBarHappyHour.Factories;
using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Items;
using FooBarHappyHour.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FooBarHappyHour.Score
{
    public class ScoreManager
    {
        private static readonly ScoreManager instance = new ScoreManager();
        public static ScoreManager Instance { get => instance; }
        private ScoreManager()
        {

        }
        public void CollectCoin(IItem coin)
        {
            coin.RemovalFlag = true;
            SoundFactory.Instance.PlayCollectCoinSound();
            SuperMarioBros.Instance.GameStateManager.Coins++;
            //SuperMarioBros.Instance.GameStateManager.Score += Utils.Instance.CoinScore;
            if (SuperMarioBros.Instance.GameStateManager.InPrimaryWorld) SuperMarioBros.Instance.GameStateManager.PrimaryWorld.Scores.Add(new ScoreObject(Utils.Instance.CoinScore,coin.ItemPhysics.Location, false));
            else SuperMarioBros.Instance.GameStateManager.HiddenWorld.Scores.Add(new ScoreObject(Utils.Instance.CoinScore,coin.ItemPhysics.Location, false));
        }

        public void CollectDynamicCoin(IItem coin)
        {
            SoundFactory.Instance.PlayCollectCoinSound();
            SuperMarioBros.Instance.GameStateManager.Coins++;
            //SuperMarioBros.Instance.GameStateManager.Score += Utils.Instance.CoinScore;
            if (SuperMarioBros.Instance.GameStateManager.InPrimaryWorld) SuperMarioBros.Instance.GameStateManager.PrimaryWorld.Scores.Add(new ScoreObject(Utils.Instance.CoinScore, coin.ItemPhysics.Location, false));
            else SuperMarioBros.Instance.GameStateManager.HiddenWorld.Scores.Add(new ScoreObject(Utils.Instance.CoinScore, coin.ItemPhysics.Location, false));
        }

        public void CollectGreenMushroom(IItem greenMushroom)
        {
            greenMushroom.RemovalFlag = true;
            SoundFactory.Instance.PlayExtraLifeSound();
            SuperMarioBros.Instance.GameStateManager.Lives++;
            if (SuperMarioBros.Instance.GameStateManager.InPrimaryWorld) SuperMarioBros.Instance.GameStateManager.PrimaryWorld.Scores.Add(new ScoreObject(Utils.Instance.PowerUpItemsScore,greenMushroom.ItemPhysics.Location, false));
            else SuperMarioBros.Instance.GameStateManager.HiddenWorld.Scores.Add(new ScoreObject(Utils.Instance.PowerUpItemsScore,greenMushroom.ItemPhysics.Location, false));
        }

        public void CollectPowerUp(IItem powerUp)
        {
            powerUp.RemovalFlag = true;
            SoundFactory.Instance.PlayGainPowerUpSound();
            //SuperMarioBros.Instance.GameStateManager.Score += Utils.Instance.PowerUpItemsScore;
            if (SuperMarioBros.Instance.GameStateManager.InPrimaryWorld) SuperMarioBros.Instance.GameStateManager.PrimaryWorld.Scores.Add(new ScoreObject(Utils.Instance.PowerUpItemsScore, powerUp.ItemPhysics.Location, false));
            else SuperMarioBros.Instance.GameStateManager.HiddenWorld.Scores.Add(new ScoreObject(Utils.Instance.PowerUpItemsScore,powerUp.ItemPhysics.Location, false));

        }
        
        public void CollectStar(IItem star)
        {
            star.RemovalFlag = true;
            SoundFactory.Instance.PlayGainPowerUpSound();
            //SuperMarioBros.Instance.GameStateManager.Score += Utils.Instance.PowerUpItemsScore;
            if (SuperMarioBros.Instance.GameStateManager.InPrimaryWorld) SuperMarioBros.Instance.GameStateManager.PrimaryWorld.Scores.Add(new ScoreObject(Utils.Instance.PowerUpItemsScore, star.ItemPhysics.Location, false));
            else SuperMarioBros.Instance.GameStateManager.HiddenWorld.Scores.Add(new ScoreObject(Utils.Instance.PowerUpItemsScore, star.ItemPhysics.Location, false));
        }

        public static void CollectBrokenBlock()
        {
            SuperMarioBros.Instance.GameStateManager.Score += Utils.Instance.BrokenBlockScore;
        }

        public static void CollectEnemyScore(IEnemy enemy, bool stomped)
        {
            int enemyDamageScore = Utils.Instance.EnemyDamageScore;

            if (stomped)
            {
                SuperMarioBros.Instance.GameStateManager.Player.StompComboCounter++;
                enemyDamageScore = (enemyDamageScore * SuperMarioBros.Instance.GameStateManager.Player.StompComboCounter);
            }

            if (SuperMarioBros.Instance.GameStateManager.InPrimaryWorld) SuperMarioBros.Instance.GameStateManager.PrimaryWorld.Scores.Add(new ScoreObject(enemyDamageScore, enemy.EnemyPhysics.Location, false));
            else SuperMarioBros.Instance.GameStateManager.HiddenWorld.Scores.Add(new ScoreObject(enemyDamageScore, enemy.EnemyPhysics.Location, false));
        }

    }
}
