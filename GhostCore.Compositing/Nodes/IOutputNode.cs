using GhostCore.Compositing.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostCore.Compositing.Nodes
{
    public interface IOutputNode : IRenderable
    {
        bool IsAggregated { get; set; }
        bool IsBlackOutside { get; set; }
        bool IsCollapsed { get; set; }
        bool IsEnabled { get; set; }
        string Name { get; set; }
    }
}
