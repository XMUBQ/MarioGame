using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Players;
using FooBarHappyHour.Utility;
using Microsoft.Xna.Framework;

namespace FooBarHappyHour.States
{
    class PlayerAnimationUpState : IPlayerAnimationState
    {
        public bool IsFacingRight { get => direction == Direction.Right; }
        public bool IsFacingLeft { get => direction == Direction.Left; }
        public string StateName { get => Utils.Instance.PlayerUp; }
        public string DirectionName { get => direction.ToString(); }
        private readonly Direction direction;
        private Player player;

        public PlayerAnimationUpState(Player player, Direction direction)
        {
            this.player = player;
            this.direction = direction;
            player.FindSpriteByAnimationState(StateName, DirectionName);
        }

        public void Idle()
        {
            if (player.MovementState.InitalJumpAvailable)
            {
                player.AnimationState = new PlayerAnimationIdleState(player, direction);
            }
        }

        public void Left()
        {
            if (direction == Direction.Right)
            {
                player.AnimationState = new PlayerAnimationUpState(player, Direction.Left);
            }
        }

        public void Right()
        {
            if (direction == Direction.Left)
            {
                player.AnimationState = new PlayerAnimationUpState(player, Direction.Right);
            }
        }

        public void Up()
        {
            // Does not respond to up.
        }

        public void Down()
        {
            // Does not respond to down.
        }

        public void Update(GameTime gameTime)
        {
            // Nothing to update.
        }
    }
}
