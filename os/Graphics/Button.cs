using Cosmos.System;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using System;
using System.Drawing;
using Console = System.Console;

namespace os.Graphics
{
    public class Button
    {
        public string Text { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Color Background { get; set; }
        public Color Foreground { get; set; }
        public Canvas Canvas { get; set; }

        // Delegat do przechowywania akcji, które są wywoływane po kliknięciu
        public Action OnClick { get; set; }

        private const int CharWidth = 8;  // Szerokość pojedynczego znaku w pikselach (monospaced)

        // Konstruktor
        public Button(string text, int width, int height, int x, int y, Color background, Color foreground, Canvas canvas)
        {
            Text = text;
            Width = width;
            Height = height;
            X = x;
            Y = y;
            Background = background;
            Foreground = foreground;
            Canvas = canvas;
        }

        // Metoda rysująca przycisk na ekranie
        public void Draw()
        {
            try
            {
                // Rysowanie tła przycisku
                Canvas.DrawFilledRectangle(new Pen(Background), X, Y, Width, Height);
                // Rysowanie obramowania
                Canvas.DrawRectangle(new Pen(Color.Black), X, Y, Width, Height);

                // Obliczenie pozycji tekstu na środku przycisku
                int textWidth = Text.Length * CharWidth;  // Szerokość tekstu
                int textX = X + (Width - textWidth) / 2;
                int textY = Y + (Height - 8) / 2;  // Wysokość czcionki na 8 pikseli

                // Rysowanie tekstu na przycisku
                Canvas.DrawString(Text, PCScreenFont.Default, new Pen(Foreground), textX, textY);
            }
            catch (Exception ex)
            {
                // Obsługa błędów podczas rysowania przycisku
                CosmosKernel1.Kernel.LogError($"Błąd podczas rysowania przycisku: {ex.Message}");
                WriteMessage.writeError($"Błąd podczas rysowania przycisku: {ex.Message}");
            }
        }

        // Obsługuje kliknięcia myszy
        public void HandleMouse()
        {
            try
            {
                int mouseX = (int)Math.Clamp(MouseManager.X, 0, MouseManager.ScreenWidth - 1);
                int mouseY = (int)Math.Clamp(MouseManager.Y, 0, MouseManager.ScreenHeight - 1);

                // Sprawdzenie, czy mysz znajduje się na przycisku i czy zostało kliknięte
                if (IsMouseOverButton(mouseX, mouseY) && MouseManager.MouseState == MouseState.Left)
                {
                    // Jeśli kliknięto przycisk, wywołaj metodę OnClick (delegat)
                    OnClick?.Invoke(); // Sprawdza, czy istnieje przypisana metoda do OnClick i wywołuje ją
                }
            }
            catch (Exception ex)
            {
                // Obsługa błędów związanych z obsługą myszy
                CosmosKernel1.Kernel.LogError($"Błąd w obsłudze myszy dla przycisku '{Text}': {ex.Message}");
                WriteMessage.writeError($"Błąd w obsłudze myszy dla przycisku '{Text}': {ex.Message}");
                Kernel.PrintDebug($"Błąd w obsłudze myszy: {ex.Message}");
            }
        }

        // Sprawdzenie, czy mysz znajduje się na przycisku
        private bool IsMouseOverButton(int mouseX, int mouseY)
        {
            return mouseX >= X && mouseX < X + Width && mouseY >= Y && mouseY < Y + Height;
        }
    }
}
