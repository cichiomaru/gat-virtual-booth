using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GATVirtualBooth
{
    public static class Logger
    {
        public static void Log(string msg)
        {
            Debug.Log($"{DateTime.Now.TimeOfDay} : {msg}");
        }
    }
}
