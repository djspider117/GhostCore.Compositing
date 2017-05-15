using GhostCore.Compositing.Nodes;
using GhostCore.Compositing.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GhostCore.Compositing.UWP.TestApp
{
    [ComImport]
    [Guid("5B0D3235-4DBA-4D44-865E-8F1D0E4FD04D")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    unsafe interface IMemoryBufferByteAccess
    {
        void GetBuffer(out byte* buffer, out uint capacity);
    }
    public sealed partial class MainPage : Page
    {
        private SoftwareBitmap _softBit;
        private SoftwareBitmapSource _src;

        private NodeGraphRenderer _renderer;
        private NodeGraph _graph;

        public MainPage()
        {
            _graph = new NodeGraph();

            CheckerboardNode constant = new CheckerboardNode();
            //constant.Color = ColorRGBA8.Create(255, 0, 255, 255);

            _graph.RenderRoot = constant;

            _renderer = new NodeGraphRenderer(_graph, new BitmapInfo() { BitDepth = 4, Height = 480, Width = 480 });

            InitializeComponent();

            _softBit = new SoftwareBitmap(BitmapPixelFormat.Bgra8, 640, 480);
            _src = new SoftwareBitmapSource();
            imgSrc.Source = _src;
        }

        private unsafe void Proc()
        {
            using (BitmapBuffer buffer = _softBit.LockBuffer(BitmapBufferAccessMode.Write))
            {
                using (var reference = buffer.CreateReference())
                {
                    byte* surface;
                    uint capacity;
                    ((IMemoryBufferByteAccess)reference).GetBuffer(out surface, out capacity);
                    BitmapPlaneDescription bufferLayout = buffer.GetPlaneDescription(0);

                    _renderer.FrameInfo.Stride = bufferLayout.Stride;

                    _renderer.RenderFrame(surface, 0, bufferLayout.StartIndex);
                }
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            _renderer.IsSingleThreaded = true;
            Proc();

            await _src.SetBitmapAsync(SoftwareBitmap.Convert(_softBit, BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied));
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _renderer.IsSingleThreaded = false;
            Proc();

            await _src.SetBitmapAsync(SoftwareBitmap.Convert(_softBit, BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied));
        }
    }
}
