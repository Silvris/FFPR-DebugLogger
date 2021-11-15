using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFPR_DebugLogger
{
    [HarmonyPatch(typeof(DebugLogController),nameof(DebugLogController.Log))]
    class DebugLogController_Log : Il2CppSystem.Object
    {
        public DebugLogController_Log(IntPtr ptr) : base(ptr)
        {
        }
        public static bool Prefix(String text)
        {
            //ModComponent.Log.LogInfo(text);
            ModComponent.DiskLog.LogWriter.WriteLine(text);
            return false;

        }
    }
}
