using Cosmos.System.Graphics;
using Cosmos.System;
using System;
using CosmosKernel1.Apps;
using System.Drawing;
using Console = System.Console;
using os;

namespace CosmosKernel1.Graphics
{
    public static class GUI
    {
        public static int ScreenSizeX = 1920, ScreenSizeY = 1080;
        public static SVGAIICanvas MainCanvas;
        public static Bitmap wall, cur;
        private static bool cursorVisible = true; // Flaga do kontrolowania widoczności kursora
        public static Colors colors = new Colors();

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

                // Wyświetlanie wszystkiego na ekranie
                MainCanvas.Display();
            }
            catch (Exception ex)
            {
                Kernel.LogError($"Błąd w GUI.Update: {ex.Message}");
                Kernel.runGui = false; // Wyłącz GUI i przejdź do trybu konsolowego
            }
        }

        public static void StartGui()
        {
            try
            {
                // Inicjalizacja głównego ekranu
                MainCanvas = new SVGAIICanvas(new Mode(ScreenSizeX, ScreenSizeY, ColorDepth.ColorDepth32));

                // Ustawienie rozmiarów ekranu i pozycji kursora na środku
                MouseManager.ScreenWidth = (uint)ScreenSizeX;
                MouseManager.ScreenHeight = (uint)ScreenSizeY;
                MouseManager.X = (uint)ScreenSizeX / 2;
                MouseManager.Y = (uint)ScreenSizeY / 2;

                ProcessManager.Start(new MessageBox { windowData = new WindowData { WinPos = new Rectangle(100, 100, 350, 200) } });
                ProcessManager.Start(new TaskBar { windowData = new WindowData { WinPos = new Rectangle(0, 0, ScreenSizeX, 50) } });
            }
            catch (Exception ex)
            {
                WriteMessage.writeError($"Błąd w GUI.StartGui: {ex.Message}");
                Kernel.runGui = false; // Wyłącz GUI i przejdź do trybu konsolowego
            }
        }

        // Możesz dodać metody do kontrolowania widoczności kursora
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