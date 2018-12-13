using FooBarHappyHour.Blocks;
using FooBarHappyHour.Misc;
using FooBarHappyHour.Score;
using FooBarHappyHour.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace FooBarHappyHour.Interfaces
{
    public interface IWorld
    {
        int Width { get; }
        int Height { get; }
        int MajorWorldID { get; }
        int MinorWorldID { get; }
        bool WorldFrozen { get; set; }
        Vector2 PlayerSpawn { get; set; }

        IList<MultiArray> Blocks { get;}

        IList<IEnemy> Enemies { get; }
        IList<IItem> Items { get; }
        IList<IScenery> Scenery { get; }
        IList<Fireball> Fireballs { get; }
        IList<Flagpole> Flagpoles { get; }
        IList<Castle> Castles { get; }
        IList<EnemySpawner> EnemySpawners { get; }
        IList<ScoreObject> Scores { get; }
      //  IList<IGameObject> CurrentUpdateList { get; }
      //  IList<IGameObject> AllUpdateable { get;  }
        ITeleporter InTeleporter { get; set; }
        ITeleporter OutTeleporter { get; set; }
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice);
    }
}
