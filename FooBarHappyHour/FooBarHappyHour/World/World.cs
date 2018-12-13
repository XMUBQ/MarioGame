using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Misc;
using FooBarHappyHour.Score;
using FooBarHappyHour.Camera;
using FooBarHappyHour.Utility;
using System;

namespace FooBarHappyHour.World
{
    
    class World : IWorld
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int MajorWorldID { get; private set; }
        public int MinorWorldID { get; private set; }
        public bool WorldFrozen { get; set; }
        public Vector2 PlayerSpawn { get; set; }
        
        public IList<MultiArray> Blocks { get; set; }
        public IList<IEnemy> Enemies { get; set; }
        public IList<IItem> Items { get; set; }
        public IList<IScenery> Scenery { get; set; }
        public IList<Flagpole> Flagpoles { get; set; }
        public IList<Castle> Castles { get; set; }
        public IList<ScoreObject> Scores { get; set; }
        public IList<Fireball> Fireballs { get; set; }
        public IList<EnemySpawner> EnemySpawners { get; set; }
       // public IList<IGameObject> CurrentUpdateList { get; set; }
       // public IList<IGameObject> AllUpdateable { get; set; }
        public ITeleporter InTeleporter { get; set; }
        public ITeleporter OutTeleporter { get; set; }
        private IList<IEnemy> enemyRemovalList;
        private IList<IItem> itemRemovalList;
        private IList<Fireball> fireballRemovalList;
        private IList<ScoreObject> scoreRemovalList;

        public World(int width, int majorWorldID, int minorWorldID)
        {
            Width = width;
            Height = Utils.Instance.CommonObjectSize;
            MajorWorldID = majorWorldID;
            MinorWorldID = minorWorldID;
            WorldFrozen = false;
            Blocks = new List<MultiArray>(Width);
            for (int i = 0; i < Width; i++)
            {
                Blocks.Add(new MultiArray(Height));
            }
            Enemies = new List<IEnemy>();
            Items = new List<IItem>();
            Scenery = new List<IScenery>();
            Flagpoles = new List<Flagpole>();
            Castles = new List<Castle>();
            Scores = new List<ScoreObject>();
            Fireballs = new List<Fireball>();
            EnemySpawners = new List<EnemySpawner>();
            enemyRemovalList = new List<IEnemy>();
            itemRemovalList = new List<IItem>();
            fireballRemovalList = new List<Fireball>();
            scoreRemovalList  = new List<ScoreObject>();
        //    AllUpdateable = new List<IGameObject>();
         //   CurrentUpdateList = new List<IGameObject>();
        }

        public void Update(GameTime gameTime)
        {
            if (!WorldFrozen)
            {
                foreach (IScenery scenery in Scenery)
                {
                    scenery.Update(gameTime);
                }
                for (int i = 0; i < Width; i++)
                {
                    for (int j = 0; j < Height; j++)
                    {
                        IBlock block = Blocks[i].OneBlockLevel[j];
                        if (block != null)
                        {
                            block.Update(gameTime);
                        }
                    }
                }
                foreach (EnemySpawner enemySpawner in EnemySpawners)
                {
                    enemySpawner.Update(gameTime);
                }
                foreach (IEnemy enemy in Enemies)
                {
                    enemy.Update(gameTime);
                    if (enemy.RemovalFlag) enemyRemovalList.Add(enemy);
                }
                foreach (IItem item in Items)
                {
                    item.Update(gameTime);
                    if (item.RemovalFlag) itemRemovalList.Add(item);
                }
                foreach (Fireball fireball in Fireballs)
                {
                    fireball.Update(gameTime);
                    if (fireball.RemovalFlag) fireballRemovalList.Add(fireball);
                }
                foreach (ScoreObject score in Scores)
                {
                    score.Update();
                    if (score.RemovalFlag) scoreRemovalList.Add(score);
                }
                foreach (Flagpole flagpole in Flagpoles)
                {
                    flagpole.Update(gameTime);
                }
            }
            RemoveGameObjects();
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            graphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, MyCamera.GameWorldTransform());
            foreach (IScenery scenery in Scenery)
            {
                scenery.Draw(spriteBatch);
            }
            foreach (Flagpole flagpole in Flagpoles)
            {
                flagpole.Draw(spriteBatch);
            }
            foreach (Castle castle in Castles)
            {
                castle.Draw(spriteBatch);
            }
            foreach (IItem item in Items)
            {
                item.Draw(spriteBatch);
            }
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    IBlock block = Blocks[i].OneBlockLevel[j];
                    if (block != null)
                    {
                       block.Draw(spriteBatch);
                    }
                }
            }
            foreach (EnemySpawner enemySpawner in EnemySpawners)
            {
                enemySpawner.Draw(spriteBatch);
            }
            foreach (IEnemy enemy in Enemies)
            {
                enemy.Draw(spriteBatch);
            }
            foreach (Fireball fireball in Fireballs)
            {
                fireball.Draw(spriteBatch);
            }
            foreach (ScoreObject score in Scores)
            {
                score.Draw(spriteBatch);
            }
            spriteBatch.End();
        }

        public void RemoveGameObjects()
        {
            RemoveEnemies();
            RemoveItems();
            RemoveBlocks();
            RemoveFireballs();
            RemoveScores();
        }

        public void RemoveEnemies()
        {
            foreach (IEnemy enemy in enemyRemovalList)
            {
                Enemies.Remove(enemy);
            }
            enemyRemovalList.Clear();
        }

        public void RemoveItems()
        {
            foreach (IItem item in itemRemovalList)
            {
                Items.Remove(item);
            }
            itemRemovalList.Clear();
        }

        public void RemoveBlocks()
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    IBlock block = Blocks[i].OneBlockLevel[j];
                    if (block != null && block.RemovalFlag)
                    {
                       block = null;
                    }
                }
            }
        }

        public void RemoveFireballs()
        {
            foreach (Fireball fireball in fireballRemovalList)
            {
                Fireballs.Remove(fireball);
            }
            fireballRemovalList.Clear();
        }

        public void RemoveScores()
        {
            foreach (ScoreObject score in scoreRemovalList)
            {
                Scores.Remove(score);
            }
            scoreRemovalList.Clear();
        }
    }
}
