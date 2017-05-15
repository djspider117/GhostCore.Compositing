using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GhostCore.Compositing.Rendering;

namespace GhostCore.Compositing.Nodes
{
    public abstract class AbstractNode : IInputNode
    {
        public bool IsAggregated { get; set; }
        public bool IsBlackOutside { get; set; }
        public bool IsCollapsed { get; set; }
        public bool IsEnabled { get; set; }

        public IOutputNode MainInput { get; set; }

        public string Name { get; set; }

        public abstract void Render(Scanline scanline, int frameNumber);
    }
}
