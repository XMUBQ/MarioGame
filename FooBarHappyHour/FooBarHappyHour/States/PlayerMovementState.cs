using FooBarHappyHour.Factories;
using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Physics;
using FooBarHappyHour.Players;
using FooBarHappyHour.Utility;
using Microsoft.Xna.Framework;
using System;

namespace FooBarHappyHour.States
{
    public class PlayerMovementState : IPlayerMovementState
    {
        public Vector2 Location { get => PlayerPhysics.Location; set => PlayerPhysics.Location = value; }
        public Vector2 Velocity { get => PlayerPhysics.Velocity; set => PlayerPhysics.Velocity = value; }
        public PlayerPhysics PlayerPhysics { get; private set; }
        public bool InitalJumpAvailable { get; set; }
        public bool AirJumpAvailable { get; set; }
        public bool TeleportAnimationComplete { get => (!teleportUpAnimation && !teleportDownAnimation); }
        private Player player;
        private double remainingJumpTime;
        private bool teleportUpAnimation;
        private bool teleportDownAnimation;
        private float teleportLocation;

        public PlayerMovementState(Player player)
        {
            PlayerPhysics = new PlayerPhysics(new Vector2(), false, true);
            this.player = player;
            InitalJumpAvailable = true;
            AirJumpAvailable = false;
            remainingJumpTime = Utils.Instance.PlayerJumpTime;
            teleportUpAnimation = false;
            teleportDownAnimation = false;
        }

        public void Idle()
        {
            if (InitalJumpAvailable == false)
            {
                AirJumpAvailable = false;
            }
            PlayerPhysics.Idle();
        }

        public void MoveLeft()
        {
            if (player.IsAlive && !teleportUpAnimation)
            {
                PlayerPhysics.MoveLeft();
            }
        }

        public void MoveRight()
        {
            if (player.IsAlive && !teleportUpAnimation)
            {
                PlayerPhysics.MoveRight();
            }
        }

        public void Jump()
        {
            if (player.IsAlive && !teleportUpAnimation)
            {
                if (InitalJumpAvailable)
                {
                    PlayerPhysics.Jump();
                    PlayerPhysics.Fly();
                    InitalJumpAvailable = false;
                    AirJumpAvailable = true;
                    if (player.IsSmall)
                    {
                        SoundFactory.Instance.PlaySmallJumpSound();
                    }
                    else
                    {
                        SoundFactory.Instance.PlayBigJumpSound();
                    }
                }
                if (AirJumpAvailable)
                {
                    PlayerPhysics.Fly(); 
                }
            }
        }

        public void ResetJump()
        {
            InitalJumpAvailable = true;
            AirJumpAvailable = false;
            remainingJumpTime = Utils.Instance.PlayerJumpTime;
        }

        public void Run()
        {
            if (player.IsAlive && !teleportUpAnimation)
            {
                PlayerPhysics.Run();
            }
        }

        public void Bounce()
        {
            if (player.IsAlive && !teleportUpAnimation)
            {
                PlayerPhysics.Bounce();
            }
        }

        private void CheckMove()
        {
            if (Location.X < 0)
            {
                Location = new Vector2(0, Location.Y);
            }
            if (SuperMarioBros.Instance.GameStateManager.InPrimaryWorld)
            {
                if (Location.X > SuperMarioBros.Instance.GameStateManager.PrimaryWorld.Width * Utils.Instance.CommonObjectSize - player.Rectangle.Width)
                {
                    Location = new Vector2(SuperMarioBros.Instance.GameStateManager.PrimaryWorld.Width * Utils.Instance.CommonObjectSize - player.Rectangle.Width, Location.Y);
                }
            }
            else
            {
                if (Location.X > SuperMarioBros.Instance.GameStateManager.HiddenWorld.Width * Utils.Instance.CommonObjectSize - player.Rectangle.Width)
                {
                    Location = new Vector2(SuperMarioBros.Instance.GameStateManager.HiddenWorld.Width * Utils.Instance.CommonObjectSize - player.Rectangle.Width, Location.Y);
                }
            }
            
        }

        private void CheckDeath()
        {
            if (Location.Y > Utils.Instance.ViewHeight && !player.IsDead)
            {
                player.PlayerPhysics.Locked = true;
                player.PlayerDeath();
            }
        }

        public void PlayTeleportUpAnimation()
        {
            SuperMarioBros.Instance.GameStateManager.PrimaryWorld.WorldFrozen = true;
            SuperMarioBros.Instance.GameStateManager.HiddenWorld.WorldFrozen = true;
            teleportUpAnimation = true;
            teleportLocation = Location.Y - player.Rectangle.Height;
            PlayerPhysics.ResetMotion();
            player.Collidable = false;
        }

        public void PlayTeleportDownAnimation()
        {
            SuperMarioBros.Instance.GameStateManager.PrimaryWorld.WorldFrozen = true;
            SuperMarioBros.Instance.GameStateManager.HiddenWorld.WorldFrozen = true;
            teleportDownAnimation = true;
            teleportLocation = Location.Y + player.Rectangle.Height;
            PlayerPhysics.ResetMotion();
            player.Collidable = false;
        }

        public void UpdateTeleportAnimation(GameTime gameTime)
        {
            if (teleportUpAnimation && Location.Y > teleportLocation)
            {
                player.Collidable = false;
                Location = new Vector2(Location.X, Location.Y - Utils.Instance.TeleportSpeed * (float)gameTime.TotalGameTime.TotalSeconds);
            }
            else if (teleportDownAnimation && Location.Y < teleportLocation)
            {
                player.Collidable = false;
                Location = new Vector2(Location.X, Location.Y + Utils.Instance.TeleportSpeed * (float)gameTime.TotalGameTime.TotalSeconds);
            }
            else
            {
                teleportUpAnimation = false;
                teleportDownAnimation = false;
                SuperMarioBros.Instance.GameStateManager.PrimaryWorld.WorldFrozen = false;
                SuperMarioBros.Instance.GameStateManager.HiddenWorld.WorldFrozen = false;
            }
        }

        public void CollisionVertical(float verticalDelta)
        {
            PlayerPhysics.CollisionVertical(verticalDelta);
        }

        public void CollisionHorizontal(float horizontalDelta)
        {
            PlayerPhysics.CollisionHorizontal(horizontalDelta);
        }

        public void Update(GameTime gameTime)
        {
            if (teleportUpAnimation || teleportDownAnimation)
            {
                UpdateTeleportAnimation(gameTime);
            }
            else
            {
                PlayerPhysics.Update(gameTime);
                PlayerPhysics.Refresh();
                CheckMove();
                CheckDeath();
                if (AirJumpAvailable)
                {
                    remainingJumpTime -= gameTime.ElapsedGameTime.TotalSeconds;
                    if (remainingJumpTime < 0)
                    {
                        AirJumpAvailable = false;
                    }
                }
                
            }
        }
    }
}
