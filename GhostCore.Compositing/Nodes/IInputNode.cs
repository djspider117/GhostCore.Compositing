using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostCore.Compositing.Nodes
{
    public interface IInputNode : IOutputNode
    {
        IOutputNode MainInput { get; set; }
    }

    public interface IMultiInputNode : IInputNode
    {
        IList<IOutputNode> SecondaryInputs { get; set; }
    }
}
