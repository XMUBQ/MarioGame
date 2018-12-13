using FooBarHappyHour.Factories;
using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Physics;
using FooBarHappyHour.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FooBarHappyHour.Misc
{
    public class EnemySpawner : IMisc
    {
        public Rectangle Rectangle => new Rectangle((int)SceneryPhysics.Location.X + sprite.Width / 2, (int)SceneryPhysics.Location.Y, sprite.Width, sprite.Height);
        public IPhysics Physics { get => SceneryPhysics; }
        public SceneryPhysics SceneryPhysics { get; private set; }
        public bool Collidable { get; set; }
        public bool RemovalFlag { get; set; }

        private ISprite sprite;
        private double spawnTimer;

        public EnemySpawner(Vector2 location)
        {
            Collidable = false;
            RemovalFlag = false;
            sprite = MiscSpriteFactory.Instance.CreateEnemySpawnerSprite();
            SceneryPhysics = new SceneryPhysics(location);
            spawnTimer = Utils.Instance.EnemySpawnInterval;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, Physics.Location);
        }

        public void Update(GameTime gameTime)
        {
            spawnTimer -= gameTime.ElapsedGameTime.TotalSeconds;
            if (spawnTimer <= 0)
            {
                SpawnEnemy();
                spawnTimer = Utils.Instance.EnemySpawnInterval;
            }
            sprite.Update(gameTime);
        }

        /** Spawns goombas 75% of the time, and koopas 25% of the time. 
         * Currently hardcoded values, to be refactored. **/
        private void SpawnEnemy()
        {
            Random rng = new Random();
            float val = (float)rng.NextDouble();
            if (val < Utils.Instance.KoopaSpawnRate)
            {
                SuperMarioBros.Instance.GameStateManager.PrimaryWorld.Enemies.Add(new Enemies.Koopa(Physics.Location));
            }
            else
            {
                SuperMarioBros.Instance.GameStateManager.PrimaryWorld.Enemies.Add(new Enemies.Goomba(Physics.Location));
            }
        }
    }
}
