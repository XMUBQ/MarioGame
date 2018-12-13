using FooBarHappyHour.Audio;
using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Players;
using FooBarHappyHour.Utility;
using Microsoft.Xna.Framework;

namespace FooBarHappyHour.States
{
    class PlayerPowerUpSuperState : IPlayerPowerUpState
    {
        public string StateName { get => Utils.Instance.PlayerSuper; }
        private Player player;
        private double remainingTransitionTime;
        private bool normalTransition;
        private bool fireTransition;

        public PlayerPowerUpSuperState(Player player)
        {
            this.player = player;
            remainingTransitionTime = Utils.Instance.TransitionTime;
            normalTransition = false;
            fireTransition = false;
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
            fireTransition = true;
            player.FindTransition(Utils.Instance.PlayerFire, player.AnimationState.DirectionName);
        }

        public void Invincible()
        {
            player.PowerUpState = new PlayerPowerUpInvincibleState(player);
            SongManager.Instance.PlayStarPowerMusic();
        }

        public void Update(GameTime gameTime)
        {
            if (normalTransition || fireTransition)
            {
                SuperMarioBros.Instance.GameStateManager.PrimaryWorld.WorldFrozen = true;
                player.Frozen = true;
                remainingTransitionTime -= gameTime.ElapsedGameTime.TotalSeconds;
                if (remainingTransitionTime < 0)
                {
                    SuperMarioBros.Instance.GameStateManager.PrimaryWorld.WorldFrozen = false;
                    player.Frozen = false;
                    if (normalTransition)
                    {
                        player.PowerUpState = new PlayerPowerUpNormalState(player);
                    }
                    if (fireTransition)
                    {
                        player.PowerUpState = new PlayerPowerUpFireState(player);
                    }
                }
            }
        }
    }
}
