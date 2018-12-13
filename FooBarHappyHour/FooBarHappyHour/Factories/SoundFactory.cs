using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FooBarHappyHour.Factories
{
    public class SoundFactory
    {
        private Song overworld;
        private Song overworldHurry;
        private Song levelComplete;
        private Song playerDead;
        private Song gameOver;
        private Song starPower;
        private Song menuTheme;

        private SoundEffect extraLife;
        private SoundEffect breakBlock;
        private SoundEffect bumpBlock;
        private SoundEffect collectCoin;
        private SoundEffect fireball;
        private SoundEffect reachFlagpole;
        private SoundEffect playerSmallJump;
        private SoundEffect playerBigJump;
        private SoundEffect kickEnemy;
        private SoundEffect warpPipe;
        private SoundEffect spawnPowerUp;
        private SoundEffect gainPowerUp;
        private SoundEffect stompEnemy;
        private SoundEffect playerShrink;
        private SoundEffect pauseGame;
        private SoundEffect selectEnemy;
        private SoundEffect enemyJump;

        private static readonly SoundFactory instance = new SoundFactory();
        public static SoundFactory Instance { get => instance; }

        private SoundFactory()
        {

        }

        public void LoadAllSFX(ContentManager content)
        {
            overworld = content.Load<Song>("BGM/smb_main_theme");
            overworldHurry = content.Load<Song>("BGM/smb_hurry");
            levelComplete = content.Load<Song>("BGM/smb_level_complete");
            playerDead = content.Load<Song>("BGM/smb_dead");
            gameOver = content.Load<Song>("BGM/smb_game_over");
            starPower = content.Load<Song>("BGM/smb_starman");
            menuTheme = content.Load<Song>("BGM/smb_menu");

            extraLife = content.Load<SoundEffect>("SFX/smb_1-up");
            breakBlock = content.Load<SoundEffect>("SFX/smb_breakblock");
            bumpBlock = content.Load<SoundEffect>("SFX/smb_bump");
            collectCoin = content.Load<SoundEffect>("SFX/smb_coin");
            fireball = content.Load<SoundEffect>("SFX/smb_fireball");
            reachFlagpole = content.Load<SoundEffect>("SFX/smb_flagpole");
            playerSmallJump = content.Load<SoundEffect>("SFX/smb_jump-small");
            playerBigJump = content.Load<SoundEffect>("SFX/smb_jump-super");
            kickEnemy = content.Load<SoundEffect>("SFX/smb_kick");
            warpPipe = content.Load<SoundEffect>("SFX/smb_pipe");
            gainPowerUp = content.Load<SoundEffect>("SFX/smb_powerup");
            spawnPowerUp = content.Load<SoundEffect>("SFX/smb_powerup_appears");
            stompEnemy = content.Load<SoundEffect>("SFX/smb_stomp");
            playerShrink = content.Load<SoundEffect>("SFX/smb_shrink");
            pauseGame = content.Load<SoundEffect>("SFX/smb_pause");
            selectEnemy = content.Load<SoundEffect>("SFX/pokemon_select");
            enemyJump = content.Load<SoundEffect>("SFX/pokemon_hop");
        }

        public void PlayExtraLifeSound()
        {
            extraLife.Play();
        }

        public void PlayBreakBlockSound()
        {
            breakBlock.Play();
        }

        public void PlayBumpBlockSound()
        {
            bumpBlock.Play();
        }

        public void PlayCollectCoinSound()
        {
            collectCoin.Play();
        }

        public void PlayFireballSound()
        {
            fireball.Play();
        }

        public void PlayFlagpoleSound()
        {
            reachFlagpole.Play();
        }

        public void PlaySmallJumpSound()
        {
            playerSmallJump.Play();
        }

        public void PlayBigJumpSound()
        {
            playerBigJump.Play();
        }

        public void PlayKickEnemySound()
        {
            kickEnemy.Play();
        }

        public void PlayWarpPipeSound()
        {
            warpPipe.Play();
        }

        public void PlayGainPowerUpSound()
        {
            gainPowerUp.Play();
        }

        public void PlaySpawnPowerUpSound()
        {
            spawnPowerUp.Play();
        }

        public void PlayStompEnemySound()
        {
            stompEnemy.Play();
        }

        public void PlayPlayerShrinkSound()
        {
            playerShrink.Play();
        }

        public void PlayPauseGameSound()
        {
            pauseGame.Play();
        }

        public void PlayEnemySelectSound()
        {
            selectEnemy.Play();
        }

        public void PlayEnemyJumpSound()
        {
            enemyJump.Play();
        }

        public Song GetOverworldMusic { get => overworld; }

        public Song GetOverworldHurryMusic { get => overworldHurry; }

        public Song GetLevelCompleteMusic { get => levelComplete; }

        public Song GetPlayerDeadMusic { get => playerDead; }

        public Song GetGameOverMusic { get => gameOver; }

        public Song GetStarPowerMusic {get=> starPower; }

        public Song GetMenuMusic { get => menuTheme; }
    }
}
