using Cosmos.System.Graphics;
using CosmosKernel1.Graphics;
using CosmosKernel1.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosKernel1
{
    public static class Window
    {
        public static int topSize = 30;
        public static void DrawTop(Process process) {
            CustomDrawing.DrawTopRoundedRectangle(process.windowData.WinPos.X, process.windowData.WinPos.Y, process.windowData.WinPos.Width,topSize,topSize/2,new Pen(GUI.colors.darkColor));
        }
    }
}
