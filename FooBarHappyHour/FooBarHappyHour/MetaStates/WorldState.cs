﻿using FooBarHappyHour.Audio;
using FooBarHappyHour.Factories;
using FooBarHappyHour.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FooBarHappyHour.Utility;
using System.Collections.Generic;
using FooBarHappyHour.Enemies;
using System.Diagnostics;
using FooBarHappyHour.Camera;

namespace FooBarHappyHour.MetaStates
{
    class WorldState : IMetaState
    {
        private GameStateManager gameStateManager;
        private SpriteFont spriteFont;
        private Vector2 marioLocation;
        private string score;
        private Vector2 scoreLocation;
        private ISprite coin;
        private Vector2 coinLocation;
        private string coins;
        private Vector2 coinsLocation;
        private Vector2 worldLocation;
        private string currentWorld;
        private Vector2 currentWorldLocation;
        private Vector2 timeLocation;
        private string currentTime;
        private Vector2 currentTimeLocation;
        private Vector2 pausedLocation;
        private bool hurryMode;
        private bool winSequence;
        private List<IEnemy> playerProximityEnemies;
        private IEnemy selectedEnemy;
        private ISprite selectedEnemyMarker;
        private double changeCooldownTime;
        private bool canChange;

        public WorldState(GameStateManager gameStateManager)
        {
            this.gameStateManager = gameStateManager;
            spriteFont = HUDFactory.Instance.SpriteFont;
            coin = HUDFactory.Instance.CreateCoinSprite();
            score = Utils.Instance.InitialScoresString;
            coins = Utils.Instance.InitialCoinsString;
            currentWorld = gameStateManager.PrimaryWorld.MajorWorldID.ToString() + Utils.Instance.Dash + gameStateManager.PrimaryWorld.MinorWorldID.ToString();
            currentTime = ((int)gameStateManager.Time).ToString();
            int width = SuperMarioBros.Instance.Window.ClientBounds.Width;
            int height = SuperMarioBros.Instance.Window.ClientBounds.Height;
            marioLocation = new Vector2(width / Utils.Instance.MenuGap, Utils.Instance.HudPaddingSmall);
            scoreLocation = new Vector2(width / Utils.Instance.MenuGap, Utils.Instance.HudPaddingLarge);
            coinLocation = new Vector2(Utils.Instance.CoinHUDOffset * width / Utils.Instance.MenuGap - coin.Width, Utils.Instance.HudPaddingLarge);
            coinsLocation = new Vector2(Utils.Instance.CoinHUDOffset * width / Utils.Instance.MenuGap, Utils.Instance.HudPaddingLarge);
            worldLocation = new Vector2(Utils.Instance.WorldHUDOffset * width / Utils.Instance.MenuGap, Utils.Instance.HudPaddingSmall);
            currentWorldLocation = new Vector2(Utils.Instance.WorldHUDOffset * width / Utils.Instance.MenuGap, Utils.Instance.HudPaddingLarge);
            timeLocation = new Vector2(Utils.Instance.TimeHUDOffset * width / Utils.Instance.MenuGap, Utils.Instance.HudPaddingSmall);
            currentTimeLocation = new Vector2(Utils.Instance.TimeHUDOffset * width / Utils.Instance.MenuGap, Utils.Instance.HudPaddingLarge);
            pausedLocation = new Vector2(width / 2 - Utils.Instance.Paused.Length * Utils.Instance.SelectorGap, height / 2);
            hurryMode = false;
            winSequence = false;
            SongManager.Instance.PlayOverworldMusic();
            selectedEnemyMarker = EnemySpriteFactory.Instance.CreateSelectedEnemySprite();
            changeCooldownTime = Utils.Instance.PlayerCoolDownTime;
            canChange = true;
        }

        public void Up(PlayerSelection playerSelection)
        {
            // Not used.
        }

        public void Down(PlayerSelection playerSelection)
        {
            if (!gameStateManager.ReachedFlag)
            {
                if (playerSelection == PlayerSelection.Player)
                {
                    gameStateManager.Player.Crouch();
                }
            }
        }

        public void Left(PlayerSelection playerSelection)
        {
            if (!gameStateManager.ReachedFlag)
            {
                if (playerSelection == PlayerSelection.Player)
                {
                    gameStateManager.Player.MoveLeft();
                }
                if (playerSelection == PlayerSelection.Enemy && gameStateManager.Multiplayer && selectedEnemy != null && !selectedEnemy.IsDead)
                {
                    selectedEnemy.GoLeft();
                }
            }
        }

        public void Right(PlayerSelection playerSelection)
        {
            if (!gameStateManager.ReachedFlag)
            {
                if (playerSelection == PlayerSelection.Player)
                {
                    gameStateManager.Player.MoveRight();
                }
                if (playerSelection == PlayerSelection.Enemy && gameStateManager.Multiplayer && selectedEnemy != null && !selectedEnemy.IsDead)
                {
                    selectedEnemy.GoRight();
                }
            }
        }

        public void Ability(PlayerSelection playerSelection)
        {
            if (!gameStateManager.ReachedFlag)
            {
                if (playerSelection == PlayerSelection.Player)
                {
                    gameStateManager.Player.UseAbility();
                }
            }
        }

        public void Jump(PlayerSelection playerSelection)
        {
            if (!gameStateManager.ReachedFlag)
            {
                if (playerSelection == PlayerSelection.Player)
                {
                    gameStateManager.Player.Jump();
                }
                if (playerSelection == PlayerSelection.Enemy && gameStateManager.Multiplayer && selectedEnemy != null && !selectedEnemy.IsDead)
                {
                    selectedEnemy.Jump();
                }
            }
        }

        public void SwitchLeft()
        {
            if (gameStateManager.Multiplayer)
            {
                if (canChange)
                {
                    if (selectedEnemy != null)
                    {
                        selectedEnemy.EnemyCollidable = true;
                    }
                    int index = playerProximityEnemies.IndexOf(selectedEnemy);
                    if (index != -1)
                    {
                        if (index == 0)
                        {
                            selectedEnemy = playerProximityEnemies[playerProximityEnemies.Count - 1];
                        }
                        else
                        {
                            selectedEnemy = playerProximityEnemies[index - 1];
                        }
                    }
                    else
                    {
                        if (playerProximityEnemies.Count > 0)
                        {
                            selectedEnemy = playerProximityEnemies[0];
                        }
                    }
                    canChange = false;
                    changeCooldownTime = Utils.Instance.EnemySelectCooldown;
                    SoundFactory.Instance.PlayEnemySelectSound();
                    selectedEnemy.EnemyCollidable = false;
                }
            }
        }

        public void SwitchRight()
        {
            if (gameStateManager.Multiplayer)
            {
                if (canChange)
                {
                    if (selectedEnemy != null)
                    {
                        selectedEnemy.EnemyCollidable = true;
                    }
                    int index = playerProximityEnemies.IndexOf(selectedEnemy);
                    if (index != -1)
                    {
                        if (index == playerProximityEnemies.Count - 1)
                        {
                            selectedEnemy = playerProximityEnemies[0];
                        }
                        else
                        {
                            selectedEnemy = playerProximityEnemies[index + 1];
                        }
                    }
                    else
                    {
                        if (playerProximityEnemies.Count > 0)
                        {
                            selectedEnemy = playerProximityEnemies[0];
                        }
                    }
                    canChange = false;
                    changeCooldownTime = Utils.Instance.EnemySelectCooldown;
                    SoundFactory.Instance.PlayEnemySelectSound();
                    selectedEnemy.EnemyCollidable = false;
                }
            }
        }

        public void Quit()
        {
            gameStateManager.ReturnToMenu();
        }

        public void HurryMode()
        {
            if (!hurryMode)
            {
                hurryMode = true;
                SongManager.Instance.PlayOverworldHurryMusic();
            }
        }

        private void WinState(GameTime gameTime)
        {
                gameStateManager.CanReset = false;
                gameStateManager.Player.Update(gameTime);
                if (gameStateManager.Time > 0)
                {
                    gameStateManager.Player.DrawPlayer = false;
                    gameStateManager.Time--;
                    gameStateManager.Score += Utils.Instance.WinScoreIncrement;;
                }
                else
                {
                    gameStateManager.ReturnToMenu();
                }
        }

        private void ReachedFlagState()
        {
                gameStateManager.CanReset = false;
                if (gameStateManager.Player.PlayerLoweredFlag)
                {
                    if (!winSequence) SongManager.Instance.PlayLevelCompleteMusic();
                    winSequence = true;
                    gameStateManager.Player.MoveRight();
                    gameStateManager.Player.PlayerPhysics.WalkToCastle();
                }

                else
                {
                    gameStateManager.Player.PlayerPhysics.ClimbDownFlag();
                }
        }

        private void UpdateProximityEnemies(GameTime gameTime)
        {
            if (gameStateManager.Multiplayer)
            {
                playerProximityEnemies = new List<IEnemy>();
                IList<IEnemy> currentWorldList;
                if (gameStateManager.InPrimaryWorld)
                {
                    currentWorldList = gameStateManager.PrimaryWorld.Enemies;
                }
                else
                {
                    currentWorldList = gameStateManager.HiddenWorld.Enemies;
                }
                foreach (IEnemy enemy in currentWorldList)
                {
                    if (enemy is Goomba || enemy is Koopa)
                    {
                        if (!enemy.IsDead)
                        {
                            Rectangle bounds = SuperMarioBros.Instance.Window.ClientBounds;
                            bounds = new Rectangle(gameStateManager.Player.Rectangle.Location.X - bounds.Width / 2, 0, bounds.Width, bounds.Height);
                            if (bounds.Contains(enemy.Rectangle.Center))
                            {
                                playerProximityEnemies.Add(enemy);
                            }
                        }
                    }
                }
                changeCooldownTime -= gameTime.ElapsedGameTime.TotalSeconds;
                if (changeCooldownTime < 0f)
                {
                    canChange = true;
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            if (selectedEnemy != null && !selectedEnemy.IsDead)
            {
                selectedEnemy.EnemyCollidable = false;
            }
            UpdateProximityEnemies(gameTime);
            coin.Update(gameTime);
            score = gameStateManager.Score.ToString();
            while (score.Length < Utils.Instance.ScoreHUDLength)
            {
                score = 0 + score;
            }
            coins = gameStateManager.Coins.ToString();
            while (coins.Length < Utils.Instance.CoinsHUDLength)
            {
                coins = 0 + coins;
            }
            coins = Utils.Instance.CoinsX + coins;
            currentTime = ((int)gameStateManager.Time).ToString();

            if (gameStateManager.Win)
            {
                WinState(gameTime);
            }
            else if (gameStateManager.ReachedFlag)
            {
                ReachedFlagState();
            }
            else
            {
                gameStateManager.Time -= gameTime.ElapsedGameTime.TotalSeconds;
                if (gameStateManager.Time < Utils.Instance.HurryTime)
                {
                    HurryMode();
                }
                if (gameStateManager.Time <= 0d && !gameStateManager.Player.IsDead)
                {
                    gameStateManager.Player.PlayerDeath();
                    gameStateManager.Player.PlayerPhysics.DeathSequence();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(spriteFont, Utils.Instance.Mario, marioLocation, Color.White);
            spriteBatch.DrawString(spriteFont, score, scoreLocation, Color.White);
            coin.Draw(spriteBatch, coinLocation);
            spriteBatch.DrawString(spriteFont, coins, coinsLocation, Color.White);
            spriteBatch.DrawString(spriteFont, Utils.Instance.World, worldLocation, Color.White);
            spriteBatch.DrawString(spriteFont, currentWorld, currentWorldLocation, Color.White);
            spriteBatch.DrawString(spriteFont, Utils.Instance.Time, timeLocation, Color.White);
            spriteBatch.DrawString(spriteFont, currentTime, currentTimeLocation, Color.White);
            if (gameStateManager.Paused)
            {
                spriteBatch.DrawString(spriteFont, Utils.Instance.Paused, pausedLocation, Color.White);
            }
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, MyCamera.GameWorldTransform());
            if (selectedEnemy != null && !selectedEnemy.IsDead)
            {
                selectedEnemyMarker.Draw(spriteBatch, selectedEnemy.Rectangle.Location.ToVector2() - new Vector2((selectedEnemy.Rectangle.Width - selectedEnemyMarker.Width) / 2, selectedEnemyMarker.Height + 8));
            }
            spriteBatch.End();
        }
    }
}
