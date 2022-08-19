using System;

namespace GATVirtualBooth
{
    public static class FileSize
    {
        public static string ByteToMB(long downloadSize)
        {
            return (downloadSize / 1024f / 1024f).ToString("N2");
        }
    }
}