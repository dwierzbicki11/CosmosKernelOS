using Cosmos.Core.Memory;
using Cosmos.System.Graphics;
using CosmosKernel1;
using CosmosKernel1.Graphics;
using System;
using System.Drawing;

namespace os.Graphics
{
    public class Menu : Process
    {
        public bool IsVisible { get; private set; }
        private Rectangle windowData;
        private Button shutdownButton;
        private Button restartButton;
        private bool isDrawn = false;

        public Menu()
        {
            IsVisible = false;
            windowData = new Rectangle(0, 50, 100, 100);

            shutdownButton = new Button("Shutdown", 100, 50, 0, 50, Color.Black, Color.White, GUI.MainCanvas);
            shutdownButton.OnClick = Shutdown;

            restartButton = new Button("Restart", 100, 50, 0, 100, Color.Black, Color.White, GUI.MainCanvas);
            restartButton.OnClick = Restart;
        }

        public override void Run()
        {
            if (IsVisible)
            {
                Draw();
            }
        }

        public void Show()
        {
            IsVisible = true;
            isDrawn = false;
        }

        public void Hide()
        {
            IsVisible = false;
        }

        private void Draw()
        {
            if (!isDrawn)
            {
                GUI.MainCanvas.DrawFilledRectangle(new Pen(Color.Gray), windowData.X, windowData.Y, windowData.Width, windowData.Height);
                shutdownButton.Draw();
                restartButton.Draw();
                isDrawn = true;
            }
        }

        public void HandleMouse()
        {
            if (IsVisible)
            {
                shutdownButton.HandleMouse();
                restartButton.HandleMouse();
            }
        }

        private void Shutdown()
        {
            Kernel.runGui = false;
            Console.WriteLine("System is shutting down...");
        }

        private void Restart()
        {
            Kernel.runGui = false;
            Console.WriteLine("System is restarting...");
        }
    }
}
