using FooBarHappyHour.Blocks;
using FooBarHappyHour.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FooBarHappyHour.Utility
{
    public class MultiArray
    {
        public IList<IBlock> OneBlockLevel { get; private set; }
        public MultiArray(int Height)
        {
            OneBlockLevel = new List<IBlock>(Height);
            for (int i = 0; i < Height; i++)
            {
                OneBlockLevel.Add(null);
            }
        }
    }
}
