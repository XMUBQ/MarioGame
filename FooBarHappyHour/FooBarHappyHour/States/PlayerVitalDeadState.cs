using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Players;
using FooBarHappyHour.Utility;
using Microsoft.Xna.Framework;

namespace FooBarHappyHour.States
{
    class PlayerVitalDeadState : IPlayerVitalState
    {
        public string StateName { get => Utils.Instance.PlayerDead; }
        private Player player;

        public PlayerVitalDeadState(Player player)
        {
            this.player = player;
            player.FindSpriteByVitalState(StateName);
        }

        public void Alive()
        {
            player.VitalState = new PlayerVitalAliveState(player);
        }

        public void Dead()
        {
            // Does not respond to dead.
        }

        public void Update(GameTime gameTime)
        {
            // Nothing to update.
        }
    }
}
