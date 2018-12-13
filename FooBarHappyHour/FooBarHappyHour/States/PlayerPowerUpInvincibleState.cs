using FooBarHappyHour.Audio;
using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Players;
using FooBarHappyHour.Utility;
using Microsoft.Xna.Framework;

namespace FooBarHappyHour.States
{
    class PlayerPowerUpInvincibleState : IPlayerPowerUpState
    {
        public string StateName { get => Utils.Instance.PlayerInvincible; }
        private Player player;
        private double remainingInvincibleTime;
        private double remainingTransitionTime;
        private string lastState;
        private bool transitioning;

        public PlayerPowerUpInvincibleState(Player player)
        {
            this.player = player;
            lastState = player.PowerUpState.StateName;
            remainingInvincibleTime = Utils.Instance.InvincibleTime;
            remainingTransitionTime = Utils.Instance.PlayerTransitionTime;
            player.FindSpriteByPowerUpState(StateName);
            transitioning = false;
        }

        public void Normal()
        {
            lastState = Utils.Instance.PlayerNormal;
        }

        public void Super()
        {
            lastState = Utils.Instance.PlayerSuper;
            transitioning = true;
            player.FindTransition(Utils.Instance.PlayerSuper, player.AnimationState.DirectionName);
        }

        public void Firey()
        {
            lastState = Utils.Instance.PlayerFire;
            transitioning = true;
            player.FindTransition(Utils.Instance.PlayerFire, player.AnimationState.DirectionName);
        }

        public void Invincible()
        {
            remainingInvincibleTime = Utils.Instance.InvincibleTime;
        }

        public void Update(GameTime gameTime)
        {
            if (transitioning)
            {
                SuperMarioBros.Instance.GameStateManager.PrimaryWorld.WorldFrozen = true;
                player.Frozen = true;

                remainingTransitionTime -= gameTime.ElapsedGameTime.TotalSeconds;
                if (remainingTransitionTime < 0)
                {
                    transitioning = false;
                    SuperMarioBros.Instance.GameStateManager.PrimaryWorld.WorldFrozen = false;
                    player.Frozen = false;
                    remainingTransitionTime = Utils.Instance.PlayerTransitionTime;
                }
            }
            else
            {
                remainingInvincibleTime -= gameTime.ElapsedGameTime.TotalSeconds;
                if (remainingInvincibleTime < 0)
                {
                    if (lastState == Utils.Instance.PlayerNormal)
                    {
                        player.PowerUpState = new PlayerPowerUpNormalState(player);
                    }
                    else if (lastState == Utils.Instance.PlayerSuper)
                    {
                        player.PowerUpState = new PlayerPowerUpSuperState(player);
                    }
                    else if (lastState == Utils.Instance.PlayerFire)
                    {
                        player.PowerUpState = new PlayerPowerUpFireState(player);
                    }
                    SongManager.Instance.ReturnToMainTheme();
                }
            }
        }
    }
}
