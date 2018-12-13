using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Players;
using FooBarHappyHour.Utility;
using Microsoft.Xna.Framework;

namespace FooBarHappyHour.States
{
    class PlayerSizeBigState : IPlayerSizeState
    {
        public string StateName { get => Utils.Instance.PlayerBig; }
        private Player player;

        public PlayerSizeBigState(Player player)
        {
            this.player = player;
            player.FindSpriteBySizeState(StateName);
        }

        public void Small()
        {
            player.SizeState = new PlayerSizeSmallState(player);
        }

        public void Big()
        {
            // Does not respond to big.
        }

        public void Update(GameTime gameTime)
        {
            // Nothing to update.
        }
    }
}
