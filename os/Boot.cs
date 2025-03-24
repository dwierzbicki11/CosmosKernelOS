using Cosmos.System.Graphics;
using CosmosKernel1.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosKernel1
{
    public static class Boot
    {
        public static void onBoot() {
            Kernel.runGui = true;
            GUI.wall = new Bitmap(res.Files.wallpapers);
            GUI.cur = new Bitmap(res.Files.cursor);
            GUI.StartGui();
        }
    }
}
