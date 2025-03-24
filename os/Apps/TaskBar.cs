using Cosmos.System;
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
        private Menu menu;

        public override void Run()
        {
            try
            {
                Window.DrawTop(this);
                int x = windowData.WinPos.X, y = windowData.WinPos.Y;
                int sizeX = windowData.WinPos.Width, sizeY = windowData.WinPos.Height;
                GUI.MainCanvas.DrawFilledRectangle(new Cosmos.System.Graphics.Pen(GUI.colors.mainColor), x, y, sizeX, sizeY);

                // Inicjalizacja przycisku Start
                start = new Button("Start", 100, 50, 0, 0, Color.Black, Color.White, GUI.MainCanvas);
                start.OnClick = OnStartClick;
                start.Draw();

                // Menu nie jest tworzone od razu – inicjalizujemy je dopiero po kliknięciu "Start"
                menu = new Menu();

                HandleMouse();
            }
            catch (Exception ex)
            {
                WriteMessage.writeError($"Błąd w TaskBar.Run: {ex.Message}");
                Kernel.PrintDebug($"Błąd w TaskBar.Run: {ex.Message}");
                Kernel.runGui = false; // Wyłącz GUI i przejdź do trybu konsolowego
            }
        }

        private void OnStartClick()
        {
            ProcessManager.Start(menu);
            menu.Show();
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

                // Obsługa myszy dla przycisku Start
                start.HandleMouse();

                // Obsługa myszy dla menu
                menu.HandleMouse();
            }
            catch (Exception ex)
            {
                Kernel.PrintDebug(ex.Message);
            }
        }
    }
}