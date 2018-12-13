using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FooBarHappyHour.States
{
    class PlayerAbilityRunState : IPlayerAbilityState
    {
        private Player player;

        public PlayerAbilityRunState(Player player)
        {
            this.player = player;
        }

        public void UseAbility()
        {
            player.MovementState.Run();
        }

        public void Run()
        {
            // Does not respond to Run.
        }

        public void ShootFire()
        {
            player.AbilitiesState = new PlayerAbilityShootFireState(player);
        }

        public void Update(GameTime gameTime)
        {
            // Nothing to update.
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Nothing to draw.
        }
    }
}
