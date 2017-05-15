using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GhostCore.Compositing.Rendering;

namespace GhostCore.Compositing.Nodes.Merge
{
    public class MergeNode : AbstractNode, IMultiInputNode
    {
        public IList<IOutputNode> SecondaryInputs { get; set; }

        public override void Render(Scanline scanline, int frameNumber)
        {
            
        }
    }
}
