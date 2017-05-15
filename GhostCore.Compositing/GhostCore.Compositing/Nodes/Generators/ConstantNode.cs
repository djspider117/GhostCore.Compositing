using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GhostCore.Compositing.Rendering;

namespace GhostCore.Compositing.Nodes
{
    public unsafe class ConstantNode : AbstractNode
    {
        public ColorRGBA8 Color;

        public override unsafe void Render(Scanline scanline, int frameNumber)
        {
            byte* pData = scanline.ScanlineData;
            int si = scanline.Info.StartIndex;
            int stride = scanline.Info.Stride;
            byte bDepth = scanline.Info.BitDepth;
            int y = scanline.Y;

            for (int x = 0; x < scanline.Info.Width; x++)
            {
                pData[si + stride * y + bDepth * x + 0] = Color.B;
                pData[si + stride * y + bDepth * x + 1] = Color.G;
                pData[si + stride * y + bDepth * x + 2] = Color.R;

                if (bDepth > 3)
                {
                    pData[si + stride * y + bDepth * x + 3] = Color.A;
                }
            }

        }
    }
}
