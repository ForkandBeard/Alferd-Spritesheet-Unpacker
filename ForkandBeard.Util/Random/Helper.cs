using System;
using System.Collections.Generic;
using System.Text;

namespace ForkandBeard.Util.Random
{    
    public class Helper
    {
        private static System.Random random = new System.Random();

        public static Single GetNextSingle(Single min, Single max)
        {
            return (Single)(min + (random.NextDouble() * (max - min)));
        }            
    }
}
