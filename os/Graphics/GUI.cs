using Cosmos.System;
using Cosmos.System.Graphics;
using CosmosKernel1.Apps;
using os;
using os.Apps;
using os.Graphics;
using System;
using System.Drawing;

namespace CosmosKernel1.Graphics
{
    public static class GUI
    {
        public static int ScreenSizeX = 1920, ScreenSizeY = 1080;
        public static SVGAIICanvas MainCanvas;
        public static Bitmap wall, cur;
        private static bool cursorVisible = true; // Flaga do kontrolowania widoczności kursora
        public static Colors colors = new Colors();
        static Button terminal;

        public static void Update()
        {
            try
            {
                // Rysowanie tła
                MainCanvas.DrawImage(wall, 0, 0);
                ProcessManager.Update();

                // Jeśli kursor jest widoczny, rysujemy go na ekranie
                if (cursorVisible)
                {
                    MainCanvas.DrawImageAlpha(cur, (int)MouseManager.X, (int)MouseManager.Y);
                }
                MainCanvas.Display();
            }
            catch (Exception ex)
            {
                Kernel.LogError($"Błąd w GUI.Update: {ex.Message}");
                Kernel.runGui = false;
            }
        }

        public static void StartGui()
        {
            try
            {

                MainCanvas = new SVGAIICanvas(new Mode(ScreenSizeX, ScreenSizeY, ColorDepth.ColorDepth32));
                terminal = new Button("Terminal", 100, 50, 100, 100, Color.Black, Color.White, MainCanvas);
                terminal.Draw();

                MouseManager.ScreenWidth = (uint)ScreenSizeX;
                MouseManager.ScreenHeight = (uint)ScreenSizeY;
                MouseManager.X = (uint)ScreenSizeX / 2;
                MouseManager.Y = (uint)ScreenSizeY / 2;
                terminal.OnClick = () => ProcessManager.Start(new Terminal { windowData = new WindowData { WinPos = new Rectangle(100, 100, 800, 600) } });
                ProcessManager.Start(new TaskBar { windowData = new WindowData { WinPos = new Rectangle(0, 0, ScreenSizeX, 50) } });

            }
            catch (Exception ex)
            {
                WriteMessage.writeError($"Błąd w GUI.StartGui: {ex.Message}");
                Kernel.runGui = false;
            }
        }

        public static void ShowCursor()
        {
            cursorVisible = true;
        }

        public static void HideCursor()
        {
            cursorVisible = false;
        }
    }
}