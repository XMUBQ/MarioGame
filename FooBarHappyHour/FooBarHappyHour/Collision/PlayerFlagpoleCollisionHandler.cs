using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Score;
using FooBarHappyHour.Misc;
using FooBarHappyHour.Utility;
using Microsoft.Xna.Framework;
using System;
using static FooBarHappyHour.Collision.CollisionDetection;
using FooBarHappyHour.Audio;
using FooBarHappyHour.Factories;

namespace FooBarHappyHour.Collision
{
    public static class PlayerFlagpoleCollisionHandler
    {
        public static void HandleCollision(IPlayer player, Flagpole flagpole)
        {
            Vector2 playerLocation = player.MovementState.Location;
            Vector2 flagpoleLocation = flagpole.SceneryPhysics.Location;
            if (!flagpole.FlagReached)
            {
                flagpole.FlagReached = true;
                flagpole.LowerFlag = true;
                Vector2 playerNewLocation = new Vector2(flagpoleLocation.X, playerLocation.Y);                
                SuperMarioBros.Instance.GameStateManager.Player.MovementState.Location = playerNewLocation;

                float playerRelativeJumpHeight = flagpoleLocation.Y / playerLocation.Y;
                int jumpScore = Utils.Instance.DefaultFlagpoleScore;

                if (playerRelativeJumpHeight > Utils.Instance.playerFlagRelativeLocationHigh) jumpScore = Utils.Instance.FullFlagpoleScore;
                else if ((playerRelativeJumpHeight <= Utils.Instance.playerFlagRelativeLocationHigh) && (playerRelativeJumpHeight > Utils.Instance.playerFlagRelativeLocationMid)) jumpScore = Utils.Instance.HighFlagpoleScore;
                else if ((playerRelativeJumpHeight <= Utils.Instance.playerFlagRelativeLocationMid) && (playerRelativeJumpHeight > Utils.Instance.playerFlagRelativeLocationLow)) jumpScore = Utils.Instance.MidFlagpoleScore;
                else if ((playerRelativeJumpHeight <= Utils.Instance.playerFlagRelativeLocationLow) && (playerRelativeJumpHeight > Utils.Instance.playerFlagRelativeLocation)) jumpScore = Utils.Instance.LowFlagpoleScore;

                SuperMarioBros.Instance.GameStateManager.PrimaryWorld.Scores.Add(new ScoreObject(jumpScore, playerLocation, true));
                SuperMarioBros.Instance.GameStateManager.ReachedFlag = true;
                SuperMarioBros.Instance.GameStateManager.Player.PlayerPhysics.ClimbDownFlag();
                SongManager.StopMusic();
                SoundFactory.Instance.PlayFlagpoleSound();
            }
            if (flagpole.FlagLowered)
            {
                SuperMarioBros.Instance.GameStateManager.Player.PlayerLoweredFlag = true;
                flagpole.FlagLowered = false;
            }
        }
    }
}
