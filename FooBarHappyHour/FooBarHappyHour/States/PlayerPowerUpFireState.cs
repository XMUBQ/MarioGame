using FooBarHappyHour.Audio;
using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Players;
using FooBarHappyHour.Utility;
using Microsoft.Xna.Framework;

namespace FooBarHappyHour.States
{
    class PlayerPowerUpFireState : IPlayerPowerUpState
    {
        public string StateName { get => Utils.Instance.PlayerFire; }
        private Player player;
        private double remainingTransitionTime;
        private bool normalTransition;

        public PlayerPowerUpFireState(Player player)
        {
            this.player = player;
            remainingTransitionTime = Utils.Instance.PlayerTransitionTime;
            normalTransition = false;
            player.FindSpriteByPowerUpState(StateName);
        }

        public void Normal()
        {
            normalTransition = true;
            player.FindTransition(Utils.Instance.PlayerNormal, player.AnimationState.DirectionName);
        }

        public void Super()
        {
            // Does not respond to super.
        }

        public void Firey()
        {
            // Does not respond to fire.
        }

        public void Invincible()
        {
            player.PowerUpState = new PlayerPowerUpInvincibleState(player);
            SongManager.Instance.PlayStarPowerMusic();
        }

        public void Update(GameTime gameTime)
        {
            if (normalTransition)
            {
                SuperMarioBros.Instance.GameStateManager.PrimaryWorld.WorldFrozen = true;
                player.Frozen = true;
                remainingTransitionTime -= gameTime.ElapsedGameTime.TotalSeconds;
                if (remainingTransitionTime < 0)
                {
                    SuperMarioBros.Instance.GameStateManager.PrimaryWorld.WorldFrozen = false;
                    player.Frozen = false;
                    player.PowerUpState = new PlayerPowerUpNormalState(player);
                }
            }
        }
    }
}
