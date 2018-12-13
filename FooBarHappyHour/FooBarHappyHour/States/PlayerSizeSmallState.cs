using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Players;
using FooBarHappyHour.Utility;
using Microsoft.Xna.Framework;

namespace FooBarHappyHour.States
{
    class PlayerSizeSmallState : IPlayerSizeState
    {
        public string StateName { get => Utils.Instance.PlayerSmall; }
        private Player player;

        public PlayerSizeSmallState(Player player)
        {
            this.player = player;
            player.FindSpriteBySizeState(StateName);
        }

        public void Small()
        {
            // Does not respond to small.
        }

        public void Big()
        {
            player.SizeState = new PlayerSizeBigState(player);
        }

        public void Update(GameTime gameTime)
        {
            // Nothing to update.
        }
    }
}
