using Cosmos.System;
using Cosmos.System.Graphics.Fonts;
using CosmosKernel1.Graphics;
using os;
using os.Graphics;
using System;
using System.Drawing;
using Console = System.Console;

namespace CosmosKernel1.Apps
{
    internal class TaskBar : Process
    {
        private Button start;
        private Button shutdownButton;
        private Button restartButton;

        public TaskBar()
        {
            // Inicjalizacja przycisku Start
            start = new Button("Start", 100, 50, 0, 0, Color.Black, Color.White, GUI.MainCanvas);
            start.OnClick = OnStartClick;

            // Inicjalizacja przycisku Shutdown
            shutdownButton = new Button("Shutdown", 100, 50, 100, 0, Color.Red, Color.White, GUI.MainCanvas);
            shutdownButton.OnClick = Shutdown;

            // Inicjalizacja przycisku Restart
            restartButton = new Button("Restart", 100, 50, 200, 0, Color.Orange, Color.White, GUI.MainCanvas);
            restartButton.OnClick = Restart;
        }
        private void DrawDateTime()
        {
            // Pobranie aktualnej daty i godziny
            string date = DateTime.Now.ToString("dd/MM/yy");
            string time = DateTime.Now.ToString("HH:mm:ss");

            int textWidth = date.Length * 8; 
            int x = windowData.WinPos.Width - textWidth - 10; 
            int y = 10; 
            GUI.MainCanvas.DrawString(date, PCScreenFont.Default, new Cosmos.System.Graphics.Pen(Color.White), x, y);
            GUI.MainCanvas.DrawString(time, PCScreenFont.Default, new Cosmos.System.Graphics.Pen(Color.White), x, y + 20);
        }


        public override void Run()
        {
            try
            {
                Window.DrawTop(this);
                int x = windowData.WinPos.X, y = windowData.WinPos.Y;
                int sizeX = windowData.WinPos.Width, sizeY = windowData.WinPos.Height;
                GUI.MainCanvas.DrawFilledRectangle(new Cosmos.System.Graphics.Pen(GUI.colors.mainColor), x, y, sizeX, sizeY);

                // Rysowanie przycisków
                start.Draw();
                shutdownButton.Draw();
                restartButton.Draw();
                HandleMouse();
                DrawDateTime();
            }
            catch (Exception ex)
            {
                WriteMessage.writeError($"Błąd w TaskBar.Run: {ex.Message}");
                Kernel.PrintDebug($"Błąd w TaskBar.Run: {ex.Message}");
                Kernel.runGui = false;
            }
        }

        private void OnStartClick()
        {
        }

        private void Shutdown()
        {
            Console.WriteLine("System is shutting down...");
           
            Kernel.PrintDebug("System is shutting down...");
            Power.Shutdown();
        }

        private void Restart()
        {
            Kernel.runGui = false;
            Console.WriteLine("System is restarting...");
            Kernel.PrintDebug("System is restarting...");
            Power.Reboot();
        }

        private void HandleMouse()
        {
            try
            {
                if (MouseManager.ScreenWidth == 0 || MouseManager.ScreenHeight == 0)
                {
                    WriteMessage.writeError("Mysz nie działa poprawnie, pomijam obsługę.");
                    return;
                }
                start.HandleMouse();
                shutdownButton.HandleMouse();
                restartButton.HandleMouse();
            }
            catch (Exception ex)
            {
                Kernel.PrintDebug(ex.Message);
            }
        }
    }
}
