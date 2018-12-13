using FooBarHappyHour.Audio;
using FooBarHappyHour.Collision;
using FooBarHappyHour.Factories;
using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Players;
using FooBarHappyHour.Utility;
using FooBarHappyHour.CheatCodes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FooBarHappyHour.MetaStates
{
    public enum PlayerSelection { Player, Enemy }
    public class GameStateManager
    {
        public IMetaState State { get; set; }
        public bool InPrimaryWorld { get; private set; }
        public IWorld PrimaryWorld { get; private set; }
        public IWorld HiddenWorld { get; private set; }
        public IPlayer Player { get; set; }
        public bool Multiplayer { get; set; }
        public int Lives { get; set; }
        public int Score { get; set; }
        public int Coins { get; set; }
        public double Time { get; set; }
        public bool Win { get; set; }
        public bool ReachedFlag { get; set; }
        public bool Paused { get; set; }
        public CollisionDetection PrimaryWorldCollisionDetection { get; private set; }
        public CollisionDetection HiddenWorldCollisionDetection { get; private set; }
        public bool CanReset { get; set; }
        private int worldID;
        private double remainingPauseTime;
        private bool canPause;
        private double remainingResetTime;
        private bool inTransition;
        private bool outTransition;
        private CheatCodesManager Manager;

        public GameStateManager()
        {
            State = new MenuState(this);
            Manager = new CheatCodesManager();
            HardResetWorlds();
            remainingPauseTime = 0;
            remainingResetTime = 0;
        }

        public void Idle()
        {
            if (!Paused && State is WorldState)
            {
                Player.Idle();
            }
        }

        public void Up(PlayerSelection playerSelection)
        {
            if (!Paused)
            {
                State.Up(playerSelection);
            }
        }

        public void Down(PlayerSelection playerSelection)
        {
            if (!Paused)
            {
                State.Down(playerSelection);
            }
        }

        public void Left(PlayerSelection playerSelection)
        {
            if (!Paused)
            {
                State.Left(playerSelection);
            }
        }

        public void Right(PlayerSelection playerSelection)
        {
            if (!Paused)
            {
                State.Right(playerSelection);
            }
        }

        public void Ability(PlayerSelection playerSelection)
        {
            if (!Paused)
            {
                State.Ability(playerSelection);
            }
        }

        public void Jump(PlayerSelection playerSelection)
        {
            if (!Paused)
            {
                State.Jump(playerSelection);
            }
        }

        public void SwitchLeft()
        {
            if (!Paused)
            {
                State.SwitchLeft();
            }
        }

        public void SwitchRight()
        {
            if (!Paused)
            {
                State.SwitchRight();
            }
        }

        public void Reset()
        {
            if (CanReset && State is WorldState)
            {
                State = new WorldState(this);
                CanReset = false;
                Time = Utils.Instance.InitialTimeLeft;
                Player = new Player();
                PrimaryWorld = WorldFactory.CreatePrimaryWorld(worldID);
                PrimaryWorldCollisionDetection = new CollisionDetection(PrimaryWorld);
                HiddenWorld = WorldFactory.CreateHiddenWorld(worldID);
                HiddenWorldCollisionDetection = new CollisionDetection(HiddenWorld);
                if (InPrimaryWorld)
                {
                    Player.MovementState.Location = PrimaryWorld.PlayerSpawn;
                }
                else
                {
                    Player.MovementState.Location = HiddenWorld.PlayerSpawn;
                }
                remainingResetTime = Utils.Instance.PauseTime;
                SongManager.Instance.PlayOverworldMusic();
            }
        }

        public void Quit()
        {
            State.Quit();
        }

        public static void ExitGame()
        {
            SuperMarioBros.Instance.Exit();
        }

        public void Pause()
        {
            if (!(State is WorldState && canPause)) return;

            canPause = false;
            Paused = !Paused;
            remainingPauseTime = Utils.Instance.PauseTime;

            if (Paused)
            {
                SongManager.PauseMusic();
            }
            else
            {
                SongManager.ResumeMusic();
            }
            SoundFactory.Instance.PlayPauseGameSound();
        }

        public void ReturnToMenu()
        {
            HardResetWorlds();
            State = new MenuState(this);
        }

        public void SwitchWorld()
        {
            inTransition = true;
            Player.MovementState.PlayTeleportDownAnimation();
            SoundFactory.Instance.PlayWarpPipeSound();
        }

        public void CreatePrimaryWorld()
        {
            PrimaryWorld = WorldFactory.CreatePrimaryWorld(worldID);
            PrimaryWorldCollisionDetection = new CollisionDetection(PrimaryWorld);
        }

        public void CreateHiddenWorld()
        {
            HiddenWorld = WorldFactory.CreateHiddenWorld(worldID);
            HiddenWorldCollisionDetection = new CollisionDetection(HiddenWorld);
        }

        public void CreateNextWorld()
        {
            worldID++;
            State = new IntroState(this);
        }

        public void RecreateWorld()
        {
            State = new IntroState(this);
        }

        public void PrimaryWorldReady()
        {
            Player.MovementState.Location = PrimaryWorld.PlayerSpawn;
            InPrimaryWorld = true;
        }

        public void HiddenWorldReady()
        {
            Player.MovementState.Location = HiddenWorld.PlayerSpawn;
            InPrimaryWorld = false;
        }

        public void PrimaryWorldTeleport()
        {
            outTransition = true;
            Player.MovementState.Location = PrimaryWorld.InTeleporter.Physics.Location;
            InPrimaryWorld = true;
            Player.MovementState.PlayTeleportUpAnimation();
        }

        public void HiddenWorldTeleport()
        {
            Player.MovementState.Location = HiddenWorld.InTeleporter.Physics.Location;
            InPrimaryWorld = false;
        }

        public void HardResetWorlds()
        {
            Lives = Utils.Instance.InitialPlayerLives;
            Score = 0;
            Coins = 0;
            Time = Utils.Instance.InitialTimeLeft;
            worldID = Utils.Instance.StartingWorldID;
            PrimaryWorld = null;
            PrimaryWorldCollisionDetection = null;
            HiddenWorld = null;
            HiddenWorldCollisionDetection = null;
            Player = null;
            Win = false;
            ReachedFlag = false;

            canPause = true;
            Paused = false;
            CanReset = true;
            inTransition = false;
            outTransition = false;
        }

        public void SoftResetWorlds()
        {
            if (State is WorldState && Lives > 0)
            {
                Time = Utils.Instance.InitialTimeLeft;
                Player = new Player();
                if (InPrimaryWorld)
                {
                    PrimaryWorld.WorldFrozen = false;
                    Player.MovementState.Location = PrimaryWorld.PlayerSpawn;
                }
                else
                {
                    HiddenWorld.WorldFrozen = false;
                    Player.MovementState.Location = HiddenWorld.PlayerSpawn;
                }
                State = new IntroState(this);
            }
            else
            {
                State = new GameOverState(this);
            }
        }

        public void PlayerDeath()
        {
            Lives--;
            SoftResetWorlds();
        }

        private void PauseCooldown(GameTime gameTime)
        {
            if (remainingPauseTime > 0)
            {
                remainingPauseTime -= gameTime.ElapsedGameTime.TotalSeconds;
                if (remainingPauseTime <= 0)
                {
                    canPause = true;
                }
            }
        }

        private void ResetCooldown(GameTime gameTime)
        {
            if (remainingResetTime > 0)
            {
                remainingResetTime -= gameTime.ElapsedGameTime.TotalSeconds;
                if (remainingResetTime <= 0)
                {
                    CanReset = true;
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            if (SuperMarioBros.Instance.GameStateManager.Coins == Utils.Instance.CoinsToLives) SuperMarioBros.Instance.GameStateManager.Lives++;
            State.Update(gameTime);

            PauseCooldown(gameTime);
            ResetCooldown(gameTime);

            if (State is WorldState && !Paused)
            {
                Manager.Update(gameTime);
                if (Player.IsAlive)
                {
                    if (Player.MovementState.TeleportAnimationComplete)
                    {
                        Player.Collidable = true;
                    }
                }
                else
                {
                    Player.Collidable = false;
                }
                if (inTransition && Player.MovementState.TeleportAnimationComplete)
                {
                    inTransition = false;
                    State = new WorldTransitionState(this);
                }
                if (outTransition && Player.MovementState.TeleportAnimationComplete)
                {
                    outTransition = false;
                }

                if (InPrimaryWorld)
                {
                    PrimaryWorld.Update(gameTime);
                    Player.Update(gameTime);
                    PrimaryWorldCollisionDetection.CheckAllCollisions();
                }
                else
                {
                    HiddenWorld.Update(gameTime);
                    Player.Update(gameTime);
                    HiddenWorldCollisionDetection.CheckAllCollisions();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            if (State is WorldState)
            {
                if (inTransition || outTransition)
                {
                    Player.Draw(spriteBatch);
                    if (InPrimaryWorld)
                    {
                        PrimaryWorld.Draw(spriteBatch, graphicsDevice);
                    }
                    else
                    {
                        HiddenWorld.Draw(spriteBatch, graphicsDevice);
                    }
                }
                else
                {
                    if (InPrimaryWorld)
                    {
                        PrimaryWorld.Draw(spriteBatch, graphicsDevice);
                    }
                    else
                    {
                        HiddenWorld.Draw(spriteBatch, graphicsDevice);
                    }
                    Player.Draw(spriteBatch);
                }
            }
            State.Draw(spriteBatch, graphicsDevice);
        }
    }
}
