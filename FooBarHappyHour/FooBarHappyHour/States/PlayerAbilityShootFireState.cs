using FooBarHappyHour.Factories;
using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Misc;
using FooBarHappyHour.Players;
using FooBarHappyHour.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FooBarHappyHour.States
{
    class PlayerAbilityShootFireState : IPlayerAbilityState
    {
        private Player player;
        private double remainingCooldownTime;
        private bool canShoot;
        private bool keyPressed;
        private bool keyReleased;

        public PlayerAbilityShootFireState(Player player)
        {
            this.player = player;
            remainingCooldownTime = Utils.Instance.PlayerCoolDownTime;
            canShoot = true;
            keyPressed = false;
            keyReleased = true;
        }

        public void UseAbility()
        {
            player.MovementState.Run();
            if (canShoot && keyReleased)
            {
                if (SuperMarioBros.Instance.GameStateManager.InPrimaryWorld)
                {
                    SuperMarioBros.Instance.GameStateManager.PrimaryWorld.Fireballs.Add(new Fireball(player.MovementState.Location, player.IsFacingRight));
                }
                else
                {
                    SuperMarioBros.Instance.GameStateManager.HiddenWorld.Fireballs.Add(new Fireball(player.MovementState.Location, player.IsFacingRight));
                }
                
                canShoot = false;
                remainingCooldownTime = Utils.Instance.PlayerCoolDownTime;
                SoundFactory.Instance.PlayFireballSound();
            }
            keyReleased = false;
            keyPressed = true;
        }

        public void Run()
        {
            player.AbilitiesState = new PlayerAbilityRunState(player);
        }

        public void ShootFire()
        {
            // Does not respond to ShootFire.
        }

        public void Update(GameTime gameTime)
        {
            remainingCooldownTime -= gameTime.ElapsedGameTime.TotalSeconds;
            if (remainingCooldownTime < 0f)
            {
                canShoot = true;
            }
            if (!keyPressed)
            {
                keyReleased = true;
            }
            keyPressed = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Nothing to draw.
        }
    }
}
