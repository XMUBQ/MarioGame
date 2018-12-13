using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using FooBarHappyHour.Interfaces;
using FooBarHappyHour.Sprites;
using System.Collections.Generic;
using System;
using FooBarHappyHour.Utility;

namespace FooBarHappyHour.Factories
{
    public class PlayerSpriteFactory
    {
        private IDictionary<string, Texture2D> staticSprites;
        private IDictionary<string, Tuple<Texture2D, int>> dynamicSprites;
        private const string directory = "MarioSprites/";
        private const string MarioAliveBigFireDownLeft = "MarioAliveBigFireDownLeft";
        private const string MarioAliveBigFireDownRight = "MarioAliveBigFireDownRight";
        private const string MarioAliveBigFireIdleLeft = "MarioAliveBigFireIdleLeft";
        private const string MarioAliveBigFireIdleRight = "MarioAliveBigFireIdleRight";
        private const string MarioAliveBigFireRunLeft = "MarioAliveBigFireRunLeft";
        private const string MarioAliveBigFireRunRight = "MarioAliveBigFireRunRight";
        private const string MarioAliveBigFireUpLeft = "MarioAliveBigFireUpLeft";
        private const string MarioAliveBigFireUpRight = "MarioAliveBigFireUpRight";
        private const string MarioAliveBigInvincibleDownLeft = "MarioAliveBigInvincibleDownLeft";
        private const string MarioAliveBigInvincibleDownRight = "MarioAliveBigInvincibleDownRight";
        private const string MarioAliveBigInvincibleIdleLeft = "MarioAliveBigInvincibleIdleLeft";
        private const string MarioAliveBigInvincibleIdleRight = "MarioAliveBigInvincibleIdleRight";
        private const string MarioAliveBigInvincibleRunLeft = "MarioAliveBigInvincibleRunLeft";
        private const string MarioAliveBigInvincibleRunRight = "MarioAliveBigInvincibleRunRight";
        private const string MarioAliveBigInvincibleUpLeft = "MarioAliveBigInvincibleUpLeft";
        private const string MarioAliveBigInvincibleUpRight = "MarioAliveBigInvincibleUpRight";
        private const string MarioAliveBigSuperDownLeft = "MarioAliveBigSuperDownLeft";
        private const string MarioAliveBigSuperDownRight = "MarioAliveBigSuperDownRight";
        private const string MarioAliveBigSuperIdleLeft = "MarioAliveBigSuperIdleLeft";
        private const string MarioAliveBigSuperIdleRight = "MarioAliveBigSuperIdleRight";
        private const string MarioAliveBigSuperRunLeft = "MarioAliveBigSuperRunLeft";
        private const string MarioAliveBigSuperRunRight = "MarioAliveBigSuperRunRight";
        private const string MarioAliveBigSuperUpLeft = "MarioAliveBigSuperUpLeft";
        private const string MarioAliveBigSuperUpRight = "MarioAliveBigSuperUpRight";
        private const string MarioAliveSmallInvincibleIdleLeft = "MarioAliveSmallInvincibleIdleLeft";
        private const string MarioAliveSmallInvincibleIdleRight = "MarioAliveSmallInvincibleIdleRight";
        private const string MarioAliveSmallInvincibleRunLeft = "MarioAliveSmallInvincibleRunLeft";
        private const string MarioAliveSmallInvincibleRunRight = "MarioAliveSmallInvincibleRunRight";
        private const string MarioAliveSmallInvincibleUpLeft = "MarioAliveSmallInvincibleUpLeft";
        private const string MarioAliveSmallInvincibleUpRight = "MarioAliveSmallInvincibleUpRight";
        private const string MarioAliveSmallNormalIdleLeft = "MarioAliveSmallNormalIdleLeft";
        private const string MarioAliveSmallNormalIdleRight = "MarioAliveSmallNormalIdleRight";
        private const string MarioAliveSmallNormalRunLeft = "MarioAliveSmallNormalRunLeft";
        private const string MarioAliveSmallNormalRunRight = "MarioAliveSmallNormalRunRight";
        private const string MarioAliveSmallNormalUpLeft = "MarioAliveSmallNormalUpLeft";
        private const string MarioAliveSmallNormalUpRight = "MarioAliveSmallNormalUpRight";
        private const string MarioDead = "MarioDead";
        private const string MarioNormalLeft = "MarioNormalLeft";
        private const string MarioNormalRight = "MarioNormalRight";
        private const string MarioSuperLeft = "MarioSuperLeft";
        private const string MarioSuperRight = "MarioSuperRight";
        private const string MarioFireLeft = "MarioFireLeft";
        private const string MarioFireRight = "MarioFireRight";
        private static readonly PlayerSpriteFactory instance = new PlayerSpriteFactory();
        public static PlayerSpriteFactory Instance { get => instance; }

        private PlayerSpriteFactory()
        {
            staticSprites = new Dictionary<string, Texture2D>();
            dynamicSprites = new Dictionary<string, Tuple<Texture2D, int>>();
        }

        public void LoadAllTextures(ContentManager content)
        {
            staticSprites.Add(MarioAliveBigFireDownLeft, content.Load<Texture2D>(directory + MarioAliveBigFireDownLeft));
            staticSprites.Add(MarioAliveBigFireDownRight, content.Load<Texture2D>(directory + MarioAliveBigFireDownRight));
            staticSprites.Add(MarioAliveBigFireIdleLeft, content.Load<Texture2D>(directory + MarioAliveBigFireIdleLeft));
            staticSprites.Add(MarioAliveBigFireIdleRight, content.Load<Texture2D>(directory + MarioAliveBigFireIdleRight));
            dynamicSprites.Add(MarioAliveBigFireRunLeft, new Tuple<Texture2D, int>(content.Load<Texture2D>(directory + MarioAliveBigFireRunLeft), 4));
            dynamicSprites.Add(MarioAliveBigFireRunRight, new Tuple<Texture2D, int>(content.Load<Texture2D>(directory + MarioAliveBigFireRunRight), 4));
            staticSprites.Add(MarioAliveBigFireUpLeft, content.Load<Texture2D>(directory + MarioAliveBigFireUpLeft));
            staticSprites.Add(MarioAliveBigFireUpRight, content.Load<Texture2D>(directory + MarioAliveBigFireUpRight));
            dynamicSprites.Add(MarioAliveBigInvincibleDownLeft, new Tuple<Texture2D, int>(content.Load<Texture2D>(directory + MarioAliveBigInvincibleDownLeft), 4));
            dynamicSprites.Add(MarioAliveBigInvincibleDownRight, new Tuple<Texture2D, int>(content.Load<Texture2D>(directory + MarioAliveBigInvincibleDownRight), 4));
            dynamicSprites.Add(MarioAliveBigInvincibleIdleLeft, new Tuple<Texture2D, int>(content.Load<Texture2D>(directory + MarioAliveBigInvincibleIdleLeft), 4));
            dynamicSprites.Add(MarioAliveBigInvincibleIdleRight, new Tuple<Texture2D, int>(content.Load<Texture2D>(directory + MarioAliveBigInvincibleIdleRight), 4));
            dynamicSprites.Add(MarioAliveBigInvincibleRunLeft, new Tuple<Texture2D, int>(content.Load<Texture2D>(directory + MarioAliveBigInvincibleRunLeft), 4));
            dynamicSprites.Add(MarioAliveBigInvincibleRunRight, new Tuple<Texture2D, int>(content.Load<Texture2D>(directory + MarioAliveBigInvincibleRunRight), 4));
            dynamicSprites.Add(MarioAliveBigInvincibleUpLeft, new Tuple<Texture2D, int>(content.Load<Texture2D>(directory + MarioAliveBigInvincibleUpLeft), 4));
            dynamicSprites.Add(MarioAliveBigInvincibleUpRight, new Tuple<Texture2D, int>(content.Load<Texture2D>(directory + MarioAliveBigInvincibleUpRight), 4));
            staticSprites.Add(MarioAliveBigSuperDownLeft, content.Load<Texture2D>(directory + MarioAliveBigSuperDownLeft));
            staticSprites.Add(MarioAliveBigSuperDownRight, content.Load<Texture2D>(directory + MarioAliveBigSuperDownRight));
            staticSprites.Add(MarioAliveBigSuperIdleLeft, content.Load<Texture2D>(directory + MarioAliveBigSuperIdleLeft));
            staticSprites.Add(MarioAliveBigSuperIdleRight, content.Load<Texture2D>(directory + MarioAliveBigSuperIdleRight));
            dynamicSprites.Add(MarioAliveBigSuperRunLeft, new Tuple<Texture2D, int>(content.Load<Texture2D>(directory + MarioAliveBigSuperRunLeft), 4));
            dynamicSprites.Add(MarioAliveBigSuperRunRight, new Tuple<Texture2D, int>(content.Load<Texture2D>(directory + MarioAliveBigSuperRunRight), 4));
            staticSprites.Add(MarioAliveBigSuperUpLeft, content.Load<Texture2D>(directory + MarioAliveBigSuperUpLeft));
            staticSprites.Add(MarioAliveBigSuperUpRight, content.Load<Texture2D>(directory + MarioAliveBigSuperUpRight));
            dynamicSprites.Add(MarioAliveSmallInvincibleIdleLeft, new Tuple<Texture2D, int>(content.Load<Texture2D>(directory + MarioAliveSmallInvincibleIdleLeft), 4));
            dynamicSprites.Add(MarioAliveSmallInvincibleIdleRight, new Tuple<Texture2D, int>(content.Load<Texture2D>(directory + MarioAliveSmallInvincibleIdleRight), 4));
            dynamicSprites.Add(MarioAliveSmallInvincibleRunLeft, new Tuple<Texture2D, int>(content.Load<Texture2D>(directory + MarioAliveSmallInvincibleRunLeft), 4));
            dynamicSprites.Add(MarioAliveSmallInvincibleRunRight, new Tuple<Texture2D, int>(content.Load<Texture2D>(directory + MarioAliveSmallInvincibleRunRight), 4));
            dynamicSprites.Add(MarioAliveSmallInvincibleUpLeft, new Tuple<Texture2D, int>(content.Load<Texture2D>(directory + MarioAliveSmallInvincibleUpLeft), 4));
            dynamicSprites.Add(MarioAliveSmallInvincibleUpRight, new Tuple<Texture2D, int>(content.Load<Texture2D>(directory + MarioAliveSmallInvincibleUpRight), 4));
            staticSprites.Add(MarioAliveSmallNormalIdleLeft, content.Load<Texture2D>(directory + MarioAliveSmallNormalIdleLeft));
            staticSprites.Add(MarioAliveSmallNormalIdleRight, content.Load<Texture2D>(directory + MarioAliveSmallNormalIdleRight));
            dynamicSprites.Add(MarioAliveSmallNormalRunLeft, new Tuple<Texture2D, int>(content.Load<Texture2D>(directory + MarioAliveSmallNormalRunLeft), 4));
            dynamicSprites.Add(MarioAliveSmallNormalRunRight, new Tuple<Texture2D, int>(content.Load<Texture2D>(directory + MarioAliveSmallNormalRunRight), 4));
            staticSprites.Add(MarioAliveSmallNormalUpLeft, content.Load<Texture2D>(directory + MarioAliveSmallNormalUpLeft));
            staticSprites.Add(MarioAliveSmallNormalUpRight, content.Load<Texture2D>(directory + MarioAliveSmallNormalUpRight));
            staticSprites.Add(MarioDead, content.Load<Texture2D>(directory + MarioDead));
            dynamicSprites.Add(MarioNormalLeft, new Tuple<Texture2D, int>(content.Load<Texture2D>(directory + MarioNormalLeft), 10));
            dynamicSprites.Add(MarioNormalRight, new Tuple<Texture2D, int>(content.Load<Texture2D>(directory + MarioNormalRight), 10));
            dynamicSprites.Add(MarioSuperLeft, new Tuple<Texture2D, int>(content.Load<Texture2D>(directory + MarioSuperLeft), 2));
            dynamicSprites.Add(MarioSuperRight, new Tuple<Texture2D, int>(content.Load<Texture2D>(directory + MarioSuperRight), 2));
            dynamicSprites.Add(MarioFireLeft, new Tuple<Texture2D, int>(content.Load<Texture2D>(directory + MarioAliveBigInvincibleIdleLeft), 4));
            dynamicSprites.Add(MarioFireRight, new Tuple<Texture2D, int>(content.Load<Texture2D>(directory + MarioAliveBigInvincibleIdleRight), 4));
        }

        public ISprite FindSprite(string vital, string size, string powerUp, string animation, string direction)
        {
            ISprite sprite = null;
            string texture = Utils.Instance.PlayerMario + vital + size + powerUp + animation + direction;
            if (staticSprites.ContainsKey(texture))
            {
                sprite = new StaticSprite(staticSprites[texture]);
            }
            if (dynamicSprites.ContainsKey(texture))
            {
                sprite = new DynamicSprite(dynamicSprites[texture].Item1, dynamicSprites[texture].Item2);
            }

            return sprite;
        }

        public ISprite FindTransition(string powerUp, string direction)
        {
            string texture = Utils.Instance.PlayerMario + powerUp + direction;
            return new DynamicSprite(dynamicSprites[texture].Item1, dynamicSprites[texture].Item2,0.1, false);
        }
    }
}
