using FooBarHappyHour.Factories;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FooBarHappyHour.Audio
{
    public class SongManager
    {
        private Song currentBGM;
        private Song currentTheme;
        private TimeSpan currentThemeTime;

        private static readonly SongManager instance = new SongManager();
        public static SongManager Instance { get => instance; }

        private SongManager()
        {
        }

        public void PlayOverworldMusic()
        {
            currentBGM = SoundFactory.Instance.GetOverworldMusic;
            currentTheme = currentBGM;
            MediaPlayer.Play(currentBGM);
            MediaPlayer.IsRepeating = true;
        }

        public void PlayOverworldHurryMusic()
        {
            currentBGM = SoundFactory.Instance.GetOverworldHurryMusic;
            MediaPlayer.Play(currentBGM);
            currentTheme = currentBGM;
            MediaPlayer.IsRepeating = false;
        }

        public void PlayLevelCompleteMusic()
        {
            currentBGM = SoundFactory.Instance.GetLevelCompleteMusic;
            MediaPlayer.Play(currentBGM);
            MediaPlayer.IsRepeating = false;
        }

        public void PlayPlayerDeadMusic()
        {
            currentBGM = SoundFactory.Instance.GetPlayerDeadMusic;
            MediaPlayer.Play(currentBGM);
            MediaPlayer.IsRepeating = false;
        }

        public void PlayGameOverMusic()
        {
            currentBGM = SoundFactory.Instance.GetGameOverMusic;
            MediaPlayer.Play(currentBGM);
            MediaPlayer.IsRepeating = false;
        }

        public void PlayStarPowerMusic()
        {
            currentThemeTime = MediaPlayer.PlayPosition;
            currentBGM = SoundFactory.Instance.GetStarPowerMusic;
            MediaPlayer.Play(currentBGM);
            MediaPlayer.IsRepeating = true;
        }

        public void ReturnToMainTheme()
        {
            currentBGM = currentTheme;
            MediaPlayer.Play(currentBGM, currentThemeTime);
        }

        public void PlayMenuSong()
        {
            currentTheme = SoundFactory.Instance.GetMenuMusic;
            currentBGM = currentTheme;
            MediaPlayer.Play(currentBGM);
            MediaPlayer.IsRepeating = true;
        }

        public static void StopMusic()
        {
            MediaPlayer.Stop();
        }

        public static void PauseMusic()
        {
            MediaPlayer.Pause();
        }

        public static void ResumeMusic()
        {
            MediaPlayer.Resume();
        }
    }
}
