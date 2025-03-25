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
        private int lastHeapCollect;

        public Menu()
        {
            IsVisible = false;
            windowData = new Rectangle(0, 50, 100, 100); // Przykładowe wymiary i pozycja menu

            // Inicjalizacja przycisków
            shutdownButton = new Button("Shutdown", 100, 50, 0, 50, Color.Black, Color.White, GUI.MainCanvas);
            shutdownButton.OnClick = Shutdown;

            restartButton = new Button("Restart", 100, 50, 0, 100, Color.Black, Color.White, GUI.MainCanvas);
            restartButton.OnClick = Restart;
        }

        public override void Run()
        {
            // Optymalizacja zbierania śmieci – nie wywołuj za często
            if (lastHeapCollect >= 300) // Zwiększ limit przed Garbage Collection
            {
                lastHeapCollect = 0;
                Heap.Collect();
            }
            else { 
                lastHeapCollect++;
                
            }
            Kernel.PrintDebug($"Ilość śmieci w Menu.cs: {lastHeapCollect}");
            if (IsVisible)
            {
                Draw();
            }
        }

        public void Show()
        {
            IsVisible = true;
            ProcessManager.Start(this);
        }

        public void Hide()
        {
            IsVisible = false;
        }
           

        private void Draw()
        {
            // Rysowanie menu
            GUI.MainCanvas.DrawFilledRectangle(new Pen(Color.Gray), windowData.X, windowData.Y, windowData.Width, windowData.Height);
            shutdownButton.Draw();
            restartButton.Draw();
        }

        public void HandleMouse()
        {
            if (IsVisible)
            {
                // Obsługuje kliknięcia w przyciski
                shutdownButton.HandleMouse();
                restartButton.HandleMouse();
            }
        }

        private void Shutdown()
        {
            Kernel.runGui = false;
            Console.WriteLine("System is shutting down...");
            // Dodaj kod do zamykania systemu
        }

        private void Restart()
        {
            Kernel.runGui = false;
            Console.WriteLine("System is restarting...");
            // Dodaj kod do ponownego uruchamiania systemu
        }
    }
}
