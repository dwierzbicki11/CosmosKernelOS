using CosmosKernel1.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosKernel1.Apps
{
    public class MessageBox:Process
    {
        public override void Run()
        {
            Window.DrawTop(this);
            int x= windowData.WinPos.X, y= windowData.WinPos.Y;
            int sizeX= windowData.WinPos.Width, sizeY= windowData.WinPos.Height;
            GUI.MainCanvas.DrawFilledRectangle(new Cosmos.System.Graphics.Pen(GUI.colors.mainColor),x,y+Window.topSize,sizeX,sizeY-Window.topSize);
        }
    }
}
