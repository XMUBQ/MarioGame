using System.Xml;
using Microsoft.Xna.Framework;
using System;
using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Blocks;
using FooBarHappyHour.Misc;
using FooBarHappyHour.Enemies;
using FooBarHappyHour.Items;
using FooBarHappyHour.Scenery;
using System.Collections.Generic;
using FooBarHappyHour.Utility;
using FooBarHappyHour.Teleporters;
using FooBarHappyHour.Score;

namespace FooBarHappyHour.Factories
{
    public static class WorldFactory
    {
        private static Dictionary<string, Action> ObjectDictionary(IWorld world, string type, float xPosition, float yPosition) => new Dictionary<string, Action>
        {
            { "Player", ()=> CreatePlayer(world, xPosition, yPosition) },
            { "Block", ()=> CreateBlock(world, type, xPosition, yPosition) },
            { "Enemy", ()=> CreateEnemy(world, type, xPosition, yPosition) },
            { "Item", ()=> CreateItem(world, type, xPosition, yPosition) },
            { "Scenery", ()=> CreateScenery(world, type, xPosition, yPosition) },
            { "Misc", ()=> CreateMisc(world, type, xPosition, yPosition) },
            { "Teleporter", ()=> CreateTeleporter(world, type, xPosition, yPosition) }
        };

        private static Dictionary<string, Action> BlocksDictionary(IWorld world, float xPosition, float yPosition) => new Dictionary<string, Action>
        {
            { "BeveledBlock", () => CreateBeveledBlock(world, xPosition, yPosition)},
            { "BrickBlock", () => CreateBrickBlock(world, xPosition, yPosition)},
            { "GroundBlock", () => CreateGroundBlock(world, xPosition, yPosition)},
            { "HiddenBlock", () => CreateHiddenBlock(world, xPosition, yPosition)},
            { "QuestionBlock", () => CreateQuestionBlock(world, xPosition, yPosition)},
            { "UsedBlock", () => CreateUsedBlock(world, xPosition, yPosition)},
            { Utils.Instance.SmallPipe, () => CreateSmallPipe(world, xPosition, yPosition)},
            { Utils.Instance.MediumPipe, () => CreateMediumPipe(world, xPosition, yPosition)},
            { Utils.Instance.BigPipe, () => CreateBigPipe(world, xPosition, yPosition)},
        };

        private static Dictionary<string, Action> EnemyDictionary(IWorld world, float xPosition, float yPosition) => new Dictionary<string, Action>
        {
            { "Goomba",()=>CreateGoomba(world,xPosition,yPosition)},
            { "Koopa",()=>CreateKoopa(world,xPosition,yPosition)},
            { "Piranha",()=>CreatePiranha(world,xPosition,yPosition) }
        };

        private static Dictionary<string, Action> MiscDictionary(IWorld world, float xPosition, float yPosition) => new Dictionary<string, Action>
        {
            { "FlagPole", ()=>CreateFlagPole(world,xPosition,yPosition)},
            { "Castle",()=>CreateCastle(world,xPosition,yPosition)},
            { "EnemySpawner",()=>CreateEnemySpawner(world,xPosition,yPosition)}
        };

        private static Dictionary<string, Action> TeleporterDictionary(IWorld world, float xPosition, float yPosition) => new Dictionary<string, Action>
        {
            { "InTeleporter", ()=>CreateInTeleporter(world,xPosition,yPosition)},
            { "OutTeleporter",()=>CreateOutTeleporter(world,xPosition,yPosition)}
        };

        public static IWorld CreatePrimaryWorld(int worldID)
        {
            XmlReader reader = XmlReader.Create("Content/Worlds/" + worldID.ToString() + "Primary.xml");
            reader.ReadToFollowing("Objects");
            reader.Read();
            reader.Read();
            IWorld world = new World.World(int.Parse(reader.GetAttribute("Width")), int.Parse(reader.GetAttribute("Major")), int.Parse(reader.GetAttribute("Minor")));
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    string name = reader.Name;
                    string type = reader.GetAttribute("Type");
                    float xPosition = float.Parse(reader.GetAttribute("X"));
                    float yPosition = float.Parse(reader.GetAttribute("Y"));
                    CreateObjects(world, name, type, xPosition, yPosition);
                }
            }
            return world;
        }

        public static IWorld CreateHiddenWorld(int worldID)
        {
            XmlReader reader = XmlReader.Create("Content/Worlds/" + worldID.ToString() + "Hidden.xml");
            reader.ReadToFollowing("Objects");
            reader.Read();
            reader.Read();
            IWorld world = new World.World(int.Parse(reader.GetAttribute("Width")), int.Parse(reader.GetAttribute("Major")), int.Parse(reader.GetAttribute("Minor")));
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    string name = reader.Name;
                    string type = reader.GetAttribute("Type");
                    float xPosition = float.Parse(reader.GetAttribute("X"));
                    float yPosition = float.Parse(reader.GetAttribute("Y"));
                    CreateObjects(world, name, type, xPosition, yPosition);
                }
            }
            return world;
        }

        private static void CreateObjects(IWorld world, string name, string type, float xPosition, float yPosition)
        {
            var addObject = ObjectDictionary(world, type, xPosition, yPosition);
            addObject[name].Invoke();
        }
        
        private static void CreateBlock(IWorld world, string type, float xPosition, float yPosition)
        {
            var addBlock = BlocksDictionary(world, xPosition, yPosition);
            addBlock[type].Invoke();
        }

        private static void CreateEnemy(IWorld world, string type, float xPosition, float yPosition)
        {
            var addEnemy = EnemyDictionary(world, xPosition, yPosition);
            addEnemy[type].Invoke();
        }

        private static void CreateMisc(IWorld world, string type, float xPosition, float yPosition)
        {
            var addMisc = MiscDictionary(world, xPosition, yPosition);
            addMisc[type].Invoke();
        }

        private static void CreateTeleporter(IWorld world, string type, float xPosition, float yPosition)
        {
            var addTeleporter = TeleporterDictionary(world, xPosition, yPosition);
            addTeleporter[type].Invoke();
        }

        private static void CreateItem(IWorld world, string type, float xPosition, float yPosition)
        {
            IBlock block = new HiddenBlock(new Vector2(0, 0));  // Get size of an arbitrary block to be size of local collision detection scope
            int blocksWidth = block.Rectangle.Width; ;
            int blocksHeight = block.Rectangle.Height;
            int blockIndexX = (int)Math.Floor(xPosition / blocksWidth);
            int blockIndexY = (int)Math.Floor(yPosition / blocksHeight);
            block = world.Blocks[blockIndexX].OneBlockLevel[blockIndexY];
            if (type.Equals("StaticCoin"))
            {
                world.Items.Add(new Coin(new Vector2(xPosition, yPosition), true, new CollectDelegate(ScoreManager.Instance.CollectCoin)));
            }
            else if (block is BrickBlock)
            {
                BrickBlock brickBlock = block as BrickBlock;
                brickBlock.AddItem(type);
            }
            else if (block is QuestionBlock)
            {
                QuestionBlock questionBlock = block as QuestionBlock;
                questionBlock.AddItem(type);
            }
            else if (block is HiddenBlock)
            {
                HiddenBlock hiddenBlock = block as HiddenBlock;
                hiddenBlock.AddItem(type);
            }
        }

        private static void CreatePlayer(IWorld world, float xPosition, float yPosition) => world.PlayerSpawn = new Vector2(xPosition, yPosition);

        private static void CreateScenery(IWorld world, string type, float xPosition, float yPosition) => world.Scenery.Add(new SceneryObject(new Vector2(xPosition, yPosition), type));
       
        private static void CreateFlagPole(IWorld world, float xPosition, float yPosition) => world.Flagpoles.Add(new Flagpole(new Vector2(xPosition, yPosition)));

        private static void CreateCastle(IWorld world, float xPosition, float yPosition) => world.Castles.Add(new Castle(new Vector2(xPosition, yPosition)));

        private static void CreateEnemySpawner(IWorld world, float xPosition, float yPosition) => world.EnemySpawners.Add(new EnemySpawner(new Vector2(xPosition, yPosition)));

        private static void CreateInTeleporter(IWorld world, float xPosition, float yPosition) => world.InTeleporter = new Teleporter(new Vector2(xPosition, yPosition), false);

        private static void CreateOutTeleporter(IWorld world, float xPosition, float yPosition) => world.OutTeleporter = new Teleporter(new Vector2(xPosition, yPosition), true);
        
        private static void CreateBeveledBlock(IWorld world, float xPosition, float yPosition) => world.Blocks[(int)xPosition / Utils.Instance.CommonObjectSize].OneBlockLevel[(int)yPosition / Utils.Instance.CommonObjectSize] = new BeveledBlock(new Vector2(xPosition, yPosition));

        private static void CreateBrickBlock(IWorld world, float xPosition, float yPosition) => world.Blocks[(int)xPosition / Utils.Instance.CommonObjectSize].OneBlockLevel[(int)yPosition / Utils.Instance.CommonObjectSize] = new BrickBlock(new Vector2(xPosition, yPosition));

        private static void CreateGroundBlock(IWorld world, float xPosition, float yPosition) => world.Blocks[(int)xPosition / Utils.Instance.CommonObjectSize].OneBlockLevel[(int)yPosition / Utils.Instance.CommonObjectSize] = new GroundBlock(new Vector2(xPosition, yPosition));

        private static void CreateHiddenBlock(IWorld world, float xPosition, float yPosition) => world.Blocks[(int)xPosition / Utils.Instance.CommonObjectSize].OneBlockLevel[(int)yPosition / Utils.Instance.CommonObjectSize] = new HiddenBlock(new Vector2(xPosition, yPosition));

        private static void CreateQuestionBlock(IWorld world, float xPosition, float yPosition) => world.Blocks[(int)xPosition / Utils.Instance.CommonObjectSize].OneBlockLevel[(int)yPosition / Utils.Instance.CommonObjectSize] = new QuestionBlock(new Vector2(xPosition, yPosition));

        private static void CreateUsedBlock(IWorld world, float xPosition, float yPosition) => world.Blocks[(int)xPosition / Utils.Instance.CommonObjectSize].OneBlockLevel[(int)yPosition / Utils.Instance.CommonObjectSize] = new UsedBlock(new Vector2(xPosition, yPosition));

        private static void CreateSmallPipe(IWorld world, float xPosition, float yPosition) => world.Blocks[(int)xPosition / Utils.Instance.CommonObjectSize].OneBlockLevel[(int)yPosition / Utils.Instance.CommonObjectSize] = new Pipe(new Vector2(xPosition, yPosition), Utils.Instance.SmallPipe);

        private static void CreateMediumPipe(IWorld world, float xPosition, float yPosition) => world.Blocks[(int)xPosition / Utils.Instance.CommonObjectSize].OneBlockLevel[(int)yPosition / Utils.Instance.CommonObjectSize] = new Pipe(new Vector2(xPosition, yPosition), Utils.Instance.MediumPipe);

        private static void CreateBigPipe(IWorld world, float xPosition, float yPosition) => world.Blocks[(int)xPosition / Utils.Instance.CommonObjectSize].OneBlockLevel[(int)yPosition / Utils.Instance.CommonObjectSize] = new Pipe(new Vector2(xPosition, yPosition), Utils.Instance.BigPipe);

        private static void CreateGoomba(IWorld world, float xPosition, float yPosition) => world.Enemies.Add(new Goomba(new Vector2(xPosition, yPosition)));

        private static void CreateKoopa(IWorld world, float xPosition, float yPosition) => world.Enemies.Add(new Koopa(new Vector2(xPosition, yPosition)));

        private static void CreatePiranha(IWorld world, float xPosition, float yPosition) => world.Enemies.Add(new PiranhaPlant(new Vector2(xPosition, yPosition)));
    }
}