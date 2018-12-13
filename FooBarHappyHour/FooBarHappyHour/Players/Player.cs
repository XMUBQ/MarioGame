using FooBarHappyHour.Audio;
using FooBarHappyHour.Camera;
using FooBarHappyHour.Factories;
using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Physics;
using FooBarHappyHour.States;
using FooBarHappyHour.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FooBarHappyHour.Players
{
    public class Player : IPlayer
    {
        public Rectangle Rectangle { get => new Rectangle((int)MovementState.Location.X, (int)MovementState.Location.Y, sprite.Width, sprite.Height); }
        public IPhysics Physics { get => MovementState.PlayerPhysics; }
        public PlayerPhysics PlayerPhysics { get => MovementState.PlayerPhysics; }
        public bool Collidable { get; set; }
        public bool RemovalFlag { get; set; }
        public bool PlayerLoweredFlag { get; set; }
        public bool Frozen { get; set; }
        public bool IsAlive { get => VitalState is PlayerVitalAliveState; }
        public bool IsDead { get => VitalState is PlayerVitalDeadState; }
        public bool IsSmall { get => SizeState is PlayerSizeSmallState; }
        public bool IsBig { get => SizeState is PlayerSizeBigState; }
        public bool IsNormal { get => PowerUpState is PlayerPowerUpNormalState; }
        public bool IsSuper { get => PowerUpState is PlayerPowerUpSuperState; }
        public bool IsFire { get => PowerUpState is PlayerPowerUpFireState; }
        public bool IsInvincible { get => PowerUpState is PlayerPowerUpInvincibleState; }
        public bool IsRunning { get => AnimationState is PlayerAnimationRunState; }
        public bool IsIdling { get => AnimationState is PlayerAnimationIdleState; }
        public bool IsCrouching { get => AnimationState is PlayerAnimationDownState; }
        public bool IsJumping { get => AnimationState is PlayerAnimationUpState; }
        public bool IsDown { get; private set; }
        public bool IsFacingLeft { get => AnimationState.IsFacingLeft; }
        public bool IsFacingRight { get => AnimationState.IsFacingRight; }
        public IPlayerMovementState MovementState { get; set; }
        public IPlayerVitalState VitalState { get; set; }
        public IPlayerSizeState SizeState { get; set; }
        public IPlayerPowerUpState PowerUpState { get; set; }
        public IPlayerAnimationState AnimationState { get; set; }
        public IPlayerAbilityState AbilitiesState { get; set; }
        public int StompComboCounter { get; set; }
        private ISprite sprite;
        private double remainingInvincibilityTime;
        private double deathTimer;
        private bool canTakeDamage;
        public bool DrawPlayer { get; set; }

        public Player()
        {
            Collidable = true;
            RemovalFlag = false;
            Frozen = false;
            PlayerLoweredFlag = false;
            MovementState = new PlayerMovementState(this);
            VitalState = new PlayerVitalAliveState(this);
            SizeState = new PlayerSizeSmallState(this);
            PowerUpState = new PlayerPowerUpNormalState(this);
            AnimationState = new PlayerAnimationIdleState(this, Direction.Right);
            AbilitiesState = new PlayerAbilityRunState(this);
            remainingInvincibilityTime = Utils.Instance.InvincibilityTime;
            deathTimer = Utils.Instance.DeathTime;
            canTakeDamage = true;
            IsDown = false;
            DrawPlayer = true;
        }

        public void Idle()
        {
            if (!Frozen && IsAlive)
            {
                MovementState.Idle();
                AnimationState.Idle();
                IsDown = false;
            } 
        }

        public void MoveLeft()
        {
            if (!Frozen && IsAlive)
            {
                AnimationState.Left();
                if (!(IsBig && IsCrouching))
                {
                    MovementState.MoveLeft();
                }
            }
        }

        public void MoveRight()
        {
            if (!Frozen && IsAlive)
            {
                AnimationState.Right();
                if (!(IsBig && IsCrouching))
                {
                    MovementState.MoveRight();
                }
            }
        }

        public void Jump()
        {
            if (!Frozen && IsAlive)
            {
                MovementState.Jump();
                AnimationState.Up();
            }
        }

        public void Crouch()
        {
            if (!Frozen && IsAlive)
            {
                if (IsBig)
                {
                    AnimationState.Down();
                }
                IsDown = true;
            }
        }

        public void UseAbility()
        {
            if (!Frozen && IsAlive)
            {
                AbilitiesState.UseAbility();
            }
        }

        public void UseGreenMushroom()
        {
            if (!Frozen && IsAlive)
            {
                SuperMarioBros.Instance.GameStateManager.Lives++;
            }
        }

        public void UsePowerUp()
        {
            if (!Frozen && IsAlive)
            {
                if (!(AbilitiesState is PlayerAbilityShootFireState)) Frozen = true;
                SizeState.Big();

                if (IsNormal)
                {
                    PowerUpState.Super();
                    AbilitiesState.Run();
                }
                else if (IsSuper)
                {
                    PowerUpState.Firey();
                    AbilitiesState.ShootFire();
                }
                else if (IsInvincible)
                {
                    if (IsSmall)
                    {
                        PowerUpState.Super();
                    }
                    else
                    {
                        if (AbilitiesState is PlayerAbilityRunState)
                        {
                            PowerUpState.Firey();
                            AbilitiesState.ShootFire();
                        }
                    }

                }
            }
        }

        public void UseSuperStar()
        {
            if (!Frozen && IsAlive)
            {
                if (IsNormal)
                {
                    SizeState.Small();
                    PowerUpState.Invincible();
                }
                else if (IsSuper || IsFire)
                {
                    SizeState.Big();
                    PowerUpState.Invincible();
                }
                else if (IsInvincible)
                {
                    PowerUpState.Invincible();
                }
            }
        }

        public void TakeDamage()
        {
            if (!Frozen && canTakeDamage)
            {
                if (IsNormal)
                {
                    PlayerDeath();
                    PlayerPhysics.DeathSequence();
                }
                else if (IsSuper || IsFire)
                {
                    PlayerLosePowerup();
                }
                else if (IsInvincible)
                {
                    // Will not take damage here.
                }
            }
        }

        public void PlayerDeath()
        {
            Collidable = false;
            VitalState.Dead();
            SuperMarioBros.Instance.GameStateManager.PrimaryWorld.WorldFrozen = true;
            SongManager.Instance.PlayPlayerDeadMusic();
        }

        private void PlayerLosePowerup()
        {
            AbilitiesState.Run();
            SizeState.Small();
            PowerUpState.Normal();
            remainingInvincibilityTime = Utils.Instance.InvincibilityTime;
            canTakeDamage = false;
            sprite.SetTransparent(true);
            SoundFactory.Instance.PlayPlayerShrinkSound();
        }

        public void ResetStompCombo()
        {
            StompComboCounter = 0;
        }

        public void FindSpriteByVitalState(string vital)
        {
            if (vital == Utils.Instance.PlayerAlive)
            {
                if (SizeState != null && PowerUpState != null && AnimationState != null)
                {
                    sprite = PlayerSpriteFactory.Instance.FindSprite(vital, SizeState.StateName, PowerUpState.StateName, AnimationState.StateName, AnimationState.DirectionName);
                }
            }
            else
            {
                sprite = PlayerSpriteFactory.Instance.FindSprite(vital, String.Empty, String.Empty,String.Empty,String.Empty);
            }
            
        }

        public void FindSpriteBySizeState(string size)
        {
            
            if (VitalState != null && PowerUpState != null && AnimationState != null)
            {
                sprite = PlayerSpriteFactory.Instance.FindSprite(VitalState.StateName, size, PowerUpState.StateName, AnimationState.StateName, AnimationState.DirectionName);
            }
            else
            {
                Console.WriteLine((VitalState != null).ToString());
            }
        }

        public void FindSpriteByPowerUpState(string powerUp)
        {
            if (VitalState != null && SizeState != null && AnimationState != null)
            {
                sprite = PlayerSpriteFactory.Instance.FindSprite(VitalState.StateName, SizeState.StateName, powerUp, AnimationState.StateName, AnimationState.DirectionName);
            }
        }

        public void FindSpriteByAnimationState(string animation, string direction)
        {
            if (VitalState != null && SizeState != null && PowerUpState != null)
            {
                sprite = PlayerSpriteFactory.Instance.FindSprite(VitalState.StateName, SizeState.StateName, PowerUpState.StateName, animation, direction);
            }
        }

        public void FindTransition(string powerUp, string direction)
        {
            sprite = PlayerSpriteFactory.Instance.FindTransition(powerUp, direction);
        }

        public void Update(GameTime gameTime)
        {
            if (!canTakeDamage)
            {
                sprite.SetTransparent(true);
                remainingInvincibilityTime -= gameTime.ElapsedGameTime.TotalSeconds;
                if (remainingInvincibilityTime < 0)
                {
                    canTakeDamage = true;
                    sprite.SetTransparent(false);
                }
            }

            if (IsDead)
            {
                deathTimer -= gameTime.ElapsedGameTime.TotalSeconds;
                if (deathTimer < 0)
                {
                    SuperMarioBros.Instance.GameStateManager.PlayerDeath();
                }
            }

            if (Frozen)
            {
                PowerUpState.Update(gameTime);
            }
            else
            {
                MovementState.Update(gameTime);
                VitalState.Update(gameTime);
                SizeState.Update(gameTime);
                PowerUpState.Update(gameTime);
                AnimationState.Update(gameTime);
                AbilitiesState.Update(gameTime);
            }
            sprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, MyCamera.GameWorldTransform());
            if (DrawPlayer) sprite.Draw(spriteBatch, MovementState.Location);
            spriteBatch.End();
        }
    }
}
