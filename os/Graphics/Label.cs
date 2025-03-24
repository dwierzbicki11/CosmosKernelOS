using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace os.Graphics
{
    public class Label
    {
        string? Text { get; set; }
        int Width { get; set; }
        int Heigth { get; set; }
        int X { get; set; }
        int Y { get; set; }
        Color Background { get; set; }
        Color Foregound { get; set; }
        public Label(string text,int width,int heigth,int x,int y,Color back,Color fore)
        {
            Text = text;
            Width = width;
            Heigth = heigth;
            X = x;
            Y = y;
            if (back == null) Background = Color.White;
            else Background = back;
            if (fore == null) Foregound = Color.Black;
            else Foregound = fore;
        }
        public void Draw(Canvas canvas) {
            canvas.DrawFilledRectangle(new Pen(Background),new Cosmos.System.Graphics.Point(X,Y),Width,Heigth);
            //canvas.DrawString(PCScreenFont.Default,new Pen(Foregound),new Cosmos.System.Graphics.Point(X,Y));
        }
    }
}
