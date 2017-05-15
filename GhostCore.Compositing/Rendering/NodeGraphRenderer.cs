using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GhostCore.Compositing.Nodes;
using System.Collections.Concurrent;

namespace GhostCore.Compositing.Rendering
{
    public unsafe class NodeGraphRenderer
    {
        public NodeGraph NodeGraph { get; set; }
        public RenderOptions RenderOptions { get; set; }
        public BitmapInfo FrameInfo { get; set; }
        public bool IsSingleThreaded { get; set; }

        public NodeGraphRenderer(NodeGraph graph, BitmapInfo target)
        {
            IsSingleThreaded = false;
            NodeGraph = graph;
            FrameInfo = target;
        }

        public void RenderFrame(byte* surface, int frameNumber, int stardIndex)
        {
            if (IsSingleThreaded)
            {
                InternalRender_SingleThread(surface, frameNumber, stardIndex);
            }
            else
            {
                InternalRender_MultiThread(surface, frameNumber, stardIndex);
            }
        }

        private void InternalRender_MultiThread(byte* surface, int frameNumber, int stardIndex)
        {
            var optimalTaskCount = Environment.ProcessorCount;
            var root = NodeGraph.RenderRoot;

            BlockingCollection<Scanline> scans = new BlockingCollection<Scanline>();
            for (int i = 0; i < optimalTaskCount; i++)
            {
                scans.TryAdd(MakeScanline(surface, stardIndex));
            }

            ParallelOptions opts = new ParallelOptions();
            opts.MaxDegreeOfParallelism = optimalTaskCount;

            Parallel.For(0, FrameInfo.Height, opts, (y) =>
            {
                var scan = scans.Take();
                scan.Y = y;
                root.Render(scan, frameNumber);
                scans.Add(scan);
            });
        }

        private void InternalRender_SingleThread(byte* surface, int frameNumber, int stardIndex)
        {
            var root = NodeGraph.RenderRoot;
            Scanline scan = MakeScanline(surface, stardIndex);

            for (int y = 0; y < FrameInfo.Height; y++)
            {
                scan.Y = y;
                root.Render(scan, frameNumber);
            }
        }

        private Scanline MakeScanline(byte* surface, int stardIndex)
        {
            Scanline scan = new Scanline();
            scan.Info = FrameInfo;
            scan.RenderOptions = RenderOptions;
            scan.ScanlineData = surface;
            scan.Info.StartIndex = stardIndex;
            return scan;
        }
    }
}
