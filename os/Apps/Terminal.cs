using CosmosKernel1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cosmos.System.Graphics;
using System.Drawing;
using CosmosKernel1.Graphics;
using Cosmos.System.Graphics.Fonts;

namespace os.Apps
{
    class Terminal : Process
    {
        private string inputBuffer = string.Empty;
        private List<string> outputBuffer = new List<string>();
        private int cursorX = 0;
        private int cursorY = 0;
        private const int maxLines = 25;

        public Terminal()
        {
            name = "Terminal";
            windowData = new WindowData { WinPos = new Rectangle(0, 50, 800, 600) };
        }

        public override void Run()
        {
            Draw();
            HandleInput();
        }

        private void Draw()
        {
            // Rysowanie tła terminala
            GUI.MainCanvas.DrawFilledRectangle(new Pen(Color.Black), windowData.WinPos.X, windowData.WinPos.Y, windowData.WinPos.Width, windowData.WinPos.Height);

            // Rysowanie wyjścia terminala
            int lineY = windowData.WinPos.Y + 10;
            foreach (var line in outputBuffer)
            {
                GUI.MainCanvas.DrawString(line, PCScreenFont.Default, new Pen(Color.White), windowData.WinPos.X + 10, lineY);
                lineY += 20;
            }

            // Rysowanie bufora wejścia
            GUI.MainCanvas.DrawString(inputBuffer,PCScreenFont.Default, new Pen(Color.White), windowData.WinPos.X + 10, lineY);

            // Wyświetlanie wszystkiego na ekranie
            GUI.MainCanvas.Display();
        }

        private void HandleInput()
        {
            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Enter)
            {
                ExecuteCommand(inputBuffer);
                inputBuffer = string.Empty;
            }
            else if (key.Key == ConsoleKey.Backspace)
            {
                if (inputBuffer.Length > 0)
                {
                    inputBuffer = inputBuffer.Substring(0, inputBuffer.Length - 1);
                }
            }
            else
            {
                inputBuffer += key.KeyChar;
            }
        }

        private void ExecuteCommand(string command)
        {
            outputBuffer.Add($"> {command}");
            Command.Run(command);
            if (outputBuffer.Count > maxLines)
            {
                outputBuffer.RemoveAt(0);
            }
        }
    }
}
