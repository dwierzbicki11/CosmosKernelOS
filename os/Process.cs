using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosKernel1
{
    public class Process
    {
        public string name;
        public WindowData windowData = new WindowData();
        public virtual void Run() { }
        public virtual void Start() { }
    }
    public class WindowData {
        public Rectangle WinPos = new Rectangle { X = 100, Y = 100, Height = 100, Width = 100 };
        public bool MoveAble;
    }
}

