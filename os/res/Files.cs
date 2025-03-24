using IL2CPU.API.Attribs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosKernel1.res
{
    public static class Files
    {
        [ManifestResourceStream(ResourceName = "os.res.Wallpaper.wall.bmp")] public static byte[] wallpapers;
        [ManifestResourceStream(ResourceName = "os.res.cursor.cur.bmp")] public static byte[] cursor;
    }
}
