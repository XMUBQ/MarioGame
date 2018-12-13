using FooBarHappyHour.Physics;

namespace FooBarHappyHour.Interfaces
{
    public interface IPlayer : IGameObject
    {
        PlayerPhysics PlayerPhysics { get; }
        bool IsAlive { get; }
        bool IsDead { get; }
        bool IsSmall { get; }
        bool IsBig { get; }
        bool IsNormal { get; }
        bool IsSuper { get; }
        bool IsFire { get; }
        bool IsInvincible { get; }
        bool IsRunning { get; }
        bool IsIdling { get; }
        bool IsCrouching { get; }
        bool IsJumping { get; }
        bool IsFacingLeft { get; }
        bool IsFacingRight { get; }
        bool IsDown { get; }
        bool DrawPlayer { get; set; }
        bool Frozen { get; set; }
        bool PlayerLoweredFlag { get; set; }
        IPlayerMovementState MovementState { get; set; }
        IPlayerVitalState VitalState { get; set; }
        IPlayerSizeState SizeState { get; set; }
        IPlayerPowerUpState PowerUpState { get; set; }
        IPlayerAnimationState AnimationState { get; set; }
        IPlayerAbilityState AbilitiesState { get; set; }
        int StompComboCounter { get; set; }
        void Idle();
        void MoveLeft();
        void MoveRight();
        void Jump();
        void Crouch();
        void UseAbility();
        void UseGreenMushroom();
        void UsePowerUp();
        void UseSuperStar();
        void TakeDamage();
        void PlayerDeath();
        void ResetStompCombo();
    }
}
