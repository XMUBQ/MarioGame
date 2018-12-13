using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Players;
using FooBarHappyHour.Utility;
using Microsoft.Xna.Framework;

namespace FooBarHappyHour.States
{
    class PlayerVitalAliveState : IPlayerVitalState
    {
        public string StateName { get => Utils.Instance.PlayerAlive; }
        private Player player;

        public PlayerVitalAliveState(Player player)
        {
            this.player = player;
            player.FindSpriteByVitalState(StateName);
        }

        public void Alive()
        {
            // Does not respond to alive.
        }

        public void Dead()
        {
            player.VitalState = new PlayerVitalDeadState(player);
        }

        public void Update(GameTime gameTime)
        {
            // Nothing to update.
        }
    }
}
