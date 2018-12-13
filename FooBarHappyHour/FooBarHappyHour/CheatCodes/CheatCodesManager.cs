using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using FooBarHappyHour.Interfaces;
using FooBarHappyHour.MetaStates;
using Microsoft.Xna.Framework;
using FooBarHappyHour.Utility;
using FooBarHappyHour.Factories;
using System;

namespace FooBarHappyHour.CheatCodes
{
    public class CheatCodesManager : ICheatCodes
    {
        private float inputCooldown;
        private bool disableCheatCodes { get; set; }

        private Queue<string> inputQueue;
        private int queueLength;
        private bool keyPressed;

        private static Dictionary<string, Action> CheatCodesDictionary() => new Dictionary<string, Action>
        {
                { Utils.Instance.BigCheatCode, () => ExecuteBig()},
                { Utils.Instance.FireCheatCode, () => ExecuteFire()},
                { Utils.Instance.LiveCheatCode, () => ExecuteExtraLive()},
                { Utils.Instance.StarCheatCode, () => ExecuteStar()},
                { Utils.Instance.ResetCode, () => ExecuteReset() },
                { Utils.Instance.TimeCheatCode, () => ExecuteExtraTime() },
                { Utils.Instance.ScoreCheatCode, () => ExecuteExtraScore() },
                { Utils.Instance.ClearEnemyCheatCode, () =>  ExecuteClearEnemies() }
        };

        public CheatCodesManager()
        {
            inputCooldown = 0f;
            disableCheatCodes = false;
            keyPressed = false;
            queueLength = Utils.Instance.CheatCodeLength;
            inputQueue = new Queue<string>();
        }

        private void KeyDown(Keys pressedKey)
        {
            if (!keyPressed)
            {
                keyPressed = true;
                inputQueue.Enqueue(pressedKey.ToString());
                if (inputQueue.Count > queueLength) inputQueue.Dequeue();
            }
        }

        private void KeyUp()
        {
            if (keyPressed) keyPressed = false;
        }

        private static void ExecuteBig()
        {
            if (!SuperMarioBros.Instance.GameStateManager.Player.IsBig) SuperMarioBros.Instance.GameStateManager.Player.UsePowerUp();
            SoundFactory.Instance.PlayGainPowerUpSound();
        }

        private static void ExecuteFire()
        {
            if (SuperMarioBros.Instance.GameStateManager.Player.IsBig) SuperMarioBros.Instance.GameStateManager.Player.UsePowerUp();
            SoundFactory.Instance.PlayGainPowerUpSound();
        }

        private static void ExecuteStar()
        {
            SuperMarioBros.Instance.GameStateManager.Player.UseSuperStar();
            SoundFactory.Instance.PlayGainPowerUpSound();
        }

        private static void ExecuteExtraLive()
        {
            SuperMarioBros.Instance.GameStateManager.Lives++;
            SoundFactory.Instance.PlayExtraLifeSound();
        }
        
        private static void ExecuteExtraTime()
        {
            SuperMarioBros.Instance.GameStateManager.Time += Utils.Instance.ExtraTime;
            if (SuperMarioBros.Instance.GameStateManager.Time > Utils.Instance.MaxTimeCap) SuperMarioBros.Instance.GameStateManager.Time = Utils.Instance.MaxTimeCap;
            SoundFactory.Instance.PlayExtraLifeSound();
        }

        private static void ExecuteExtraScore()
        {
            if (SuperMarioBros.Instance.GameStateManager.Score < Utils.Instance.ScoreCap)
            {
                SuperMarioBros.Instance.GameStateManager.Score += Utils.Instance.ExtraScore;
                SoundFactory.Instance.PlayExtraLifeSound();
            }
        }

        private static void ExecuteClearEnemies()
        {
            foreach(IEnemy enemy in SuperMarioBros.Instance.GameStateManager.PrimaryWorld.Enemies)
            {
                enemy.TakeDamage();
            }
            SoundFactory.Instance.PlayKickEnemySound();
        }

        private static void ExecuteReset()
        {
            SuperMarioBros.Instance.GameStateManager.Reset();
        }

        private void UpdateInputQueue()
        {
            Keys[] pressedKeys = Keyboard.GetState().GetPressedKeys();
            if (pressedKeys.Length > 0) KeyDown(pressedKeys[0]);
            else KeyUp();
            CheckCheatCodes(InputQueueToString());
        }

        private string InputQueueToString()
        {
            string code = string.Empty;
            foreach (string str in inputQueue)
            {
                code = code + str;
            }
            return code;
        }

        private void CheckCheatCodes(string code)
        {
            var cheatCode = CheatCodesDictionary();
            if (CheatCodesDictionary().ContainsKey(code))
            {
                cheatCode[code].Invoke();
                ResetCheatCode();                
            }
        }

        private void InduceCooldown()
        {
            disableCheatCodes = true;
            inputCooldown = Utils.Instance.CheatCodeCooldown;
        }

        private void CooldownTimer(GameTime gameTime)
        {
            inputCooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (inputCooldown <= 0f)
            {
                disableCheatCodes = false;
                inputCooldown = 0f;
            }
        }

        private void ResetCheatCode()
        {
            inputQueue.Clear();
            InduceCooldown();
        }

        public void Update(GameTime gameTime)
        {
            CooldownTimer(gameTime);
            if (!disableCheatCodes) UpdateInputQueue();            
        }
    }
}
