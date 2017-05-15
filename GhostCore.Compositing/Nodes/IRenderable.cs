using GhostCore.Compositing.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostCore.Compositing.Nodes
{
    public interface IRenderable
    {
        void Render(Scanline scanline, int frameNumber);
    }
}
