using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Utils.Colour
{
    public class Colour 
    {
        ///<summary>Red colour value.</summary>
        public byte R;
        ///<summary>Green colour value.</summary>
        public byte G;
        ///<summary>Blue colour value.</summary>
        public byte B;
        ///<summary>Alpha value.</summary>
        public byte A;
        public Colour(byte _r, byte _g, byte _b, byte _a)
        {
            new Colour(_r, _g, _b, _a);
        }
    }
}
