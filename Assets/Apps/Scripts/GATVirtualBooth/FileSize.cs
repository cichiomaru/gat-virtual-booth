using System;

namespace GATVirtualBooth
{
    public static class FileSize
    {
        public static float ToMB(long downloadSize)
        {
            return downloadSize / 1024f / 1024f;
        }
    }
}