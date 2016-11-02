using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarsRover
{
    static class NumberUtility
    {
        public static bool IsPositiveInteger(string value)
        {
            int intValue;
            if(Int32.TryParse(value, out intValue)){
                if (intValue >= 0)
                {
                    return true;
                }
            }
            return false;
        }

        public static int Mod(int x, int modulus)
        {
            return ((x % modulus) + modulus) % modulus;
        }
    }
}
