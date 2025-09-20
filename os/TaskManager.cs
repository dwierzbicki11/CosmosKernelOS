using Cosmos.System.Graphics.Fonts;
using CosmosKernel1;
using CosmosKernel1.Graphics;
using System;
using System.Drawing;

namespace os
{
    class TaskManager : Process
    {
        public TaskManager()
        {
            this.name = "TaskManager";
        }

        public override void Run()
        {
            Window.DrawTop(this);
            GUI.MainCanvas.DrawRectangle(new Cosmos.System.Graphics.Pen(Color.Black), 200, 200, 500, 500);

            int x = 10;
            int y = 10;
            int lineHeight = 20;
            try
            {
                foreach (var process in ProcessManager.Processes)
                {
                    GUI.MainCanvas.DrawString(process.name, PCScreenFont.Default, new Cosmos.System.Graphics.Pen(Color.White), x, y);
                    y += lineHeight;
                }
            }
            catch (Exception ex) {
                Kernel.PrintDebug(ex.Message);
            }
        }
    }
}