using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostCore.Compositing
{
    public struct ColorRGBA8
    {
        public byte R;
        public byte G;
        public byte B;
        public byte A;

        public static ColorRGBA8 Create(byte r, byte g, byte b, byte a)
        {
            return new ColorRGBA8() { A = a, B = b, G = g, R = r };
        }
    }
}
