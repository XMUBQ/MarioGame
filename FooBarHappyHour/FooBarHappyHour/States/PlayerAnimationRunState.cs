using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Players;
using FooBarHappyHour.Utility;
using Microsoft.Xna.Framework;

namespace FooBarHappyHour.States
{ 
    class PlayerAnimationRunState : IPlayerAnimationState
    {
        public bool IsFacingRight { get => direction == Direction.Right; }
        public bool IsFacingLeft { get => direction == Direction.Left; }
        public string StateName { get => Utils.Instance.PlayerRun; }
        public string DirectionName { get => direction.ToString(); }
        private readonly Direction direction;
        private Player player;

        public PlayerAnimationRunState(Player player, Direction direction)
        {
            this.player = player;
            this.direction = direction;
            player.FindSpriteByAnimationState(StateName, DirectionName);
        }

        public void Idle()
        {
            player.AnimationState = new PlayerAnimationIdleState(player, direction);
        }

        public void Left()
        {
            if (direction == Direction.Right)
            {
                player.AnimationState = new PlayerAnimationIdleState(player, Direction.Left);
            }
        }

        public void Right()
        {
            if (direction == Direction.Left)
            {
                player.AnimationState = new PlayerAnimationIdleState(player, Direction.Right);
            }
        }

        public void Up()
        {
            player.AnimationState = new PlayerAnimationUpState(player, direction);
        }

        public void Down()
        {
            player.AnimationState = new PlayerAnimationDownState(player, direction);
        }

        public void Update(GameTime gameTime)
        {
            // Nothing to update.
        }
    }
}
