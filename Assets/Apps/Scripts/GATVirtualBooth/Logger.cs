using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GATVirtualBooth
{
    public static class Logger
    {
        public static void Log(string msg)
        {
            Debug.Log($"{DateTime.Now.TimeOfDay} : {msg}");
        }
        public static void Log(string msg, Object context)
        {
            Debug.Log($"{DateTime.Now.TimeOfDay} : {msg}", context);
        }
    }
}
