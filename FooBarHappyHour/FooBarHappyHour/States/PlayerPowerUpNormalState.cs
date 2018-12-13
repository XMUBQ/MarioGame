using FooBarHappyHour.Audio;
using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Players;
using FooBarHappyHour.Utility;
using Microsoft.Xna.Framework;

namespace FooBarHappyHour.States
{
    class PlayerPowerUpNormalState : IPlayerPowerUpState
    {
        public string StateName { get => Utils.Instance.PlayerNormal; }
        private Player player;
        private double remainingTransitionTime;
        private bool superTransition;

        public PlayerPowerUpNormalState(Player player)
        {
            this.player = player;
            remainingTransitionTime = Utils.Instance.TransitionTime;
            superTransition = false;
            player.FindSpriteByPowerUpState(StateName);
        }

        public void Normal()
        {
            // Does not respond to normal.
        }

        public void Super()
        {
            superTransition = true;
            player.PlayerPhysics.RaisingSprite();
            player.FindTransition(Utils.Instance.PlayerSuper, player.AnimationState.DirectionName);
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
            if (superTransition)
            {
                SuperMarioBros.Instance.GameStateManager.PrimaryWorld.WorldFrozen = true;
                player.Frozen = true;

                remainingTransitionTime -= gameTime.ElapsedGameTime.TotalSeconds;
                if (remainingTransitionTime < 0)
                {
                    superTransition = false;
                    SuperMarioBros.Instance.GameStateManager.PrimaryWorld.WorldFrozen = false;
                    player.Frozen = false;
                    player.PowerUpState = new PlayerPowerUpSuperState(player);
                }
            }
        }
    }
}
