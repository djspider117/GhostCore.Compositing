using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostCore.Compositing.Rendering
{
    public unsafe class Scanline
    {
        public RenderOptions RenderOptions;
        public byte* ScanlineData;
        public int Y;
        public BitmapInfo Info;
    }

    public class BitmapInfo
    {
        public int StartIndex;
        public int Stride;
        public byte BitDepth;
        public int Width;
        public int Height;
    }
}
