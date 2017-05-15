using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GhostCore.Compositing.Rendering;

namespace GhostCore.Compositing.Nodes
{
    public class CheckerboardNode : AbstractNode
    {
        private bool _isOnWhite;

        public int CheckerWidth;
        public int CheckerHeight;

        public CheckerboardNode()
        {
            CheckerWidth = 40;
            CheckerHeight = 30;
        }

        public unsafe override void Render(Scanline scanline, int frameNumber)
        {
            byte* pData = scanline.ScanlineData;
            int si = scanline.Info.StartIndex;
            int stride = scanline.Info.Stride;
            byte bDepth = scanline.Info.BitDepth;
            int y = scanline.Y;

            for (int x = 0; x < scanline.Info.Width; x++)
            {
                var i = (x / CheckerWidth);
                var j = (y / CheckerWidth);
                _isOnWhite = (i + j) % 2 == 0;

                if (_isOnWhite)
                {
                    pData[si + stride * y + bDepth * x + 0] = 200;
                    pData[si + stride * y + bDepth * x + 1] = 200;
                    pData[si + stride * y + bDepth * x + 2] = 200;

                    if (bDepth > 3)
                    {
                        pData[si + stride * y + bDepth * x + 3] = 255;
                    }
                }
                else
                {
                    pData[si + stride * y + bDepth * x + 0] = 44;
                    pData[si + stride * y + bDepth * x + 1] = 44;
                    pData[si + stride * y + bDepth * x + 2] = 44;

                    if (bDepth > 3)
                    {
                        pData[si + stride * y + bDepth * x + 3] = 255;
                    }
                }
            }
        }
    }
}
