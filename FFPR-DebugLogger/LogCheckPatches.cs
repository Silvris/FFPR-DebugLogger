using HarmonyLib;
using Last.Battle;
using Last.Battle.Damage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFPR_DebugLogger
{
    [HarmonyPatch(typeof(BattleAiLogController),nameof(BattleAiLogController.UpdateLog))]
    class BattleAiLogController_UpdateLog : Il2CppSystem.Object
    {
        public BattleAiLogController_UpdateLog(IntPtr ptr) : base(ptr)
        {
        }
        public static void Prefix(BattleAiLogController __instance)
        {
            ModComponent.MainLog.LogInfo("BattleAiLogController.UpdateLog");
            if (__instance.debugLogController == null)
            {
                __instance.debugLogController = ModComponent.LogController;
            }
        }
    }
    [HarmonyPatch(typeof(MonsterSelectController),nameof(MonsterSelectController.OutputLog))]
    class MonsterSelectController_OutputLog : Il2CppSystem.Object
    {
        public MonsterSelectController_OutputLog(IntPtr ptr) : base(ptr)
        {
        }
        public static void Prefix(MonsterSelectController __instance)
        {
            ModComponent.MainLog.LogInfo("MonsterSelectController.OutputLog");
            if (__instance.debugLogController == null)
            {
                __instance.debugLogController = ModComponent.LogController;
            }
        }
    }
    [HarmonyPatch(typeof(PlayerDataController), nameof(PlayerDataController.OutputLog))]
    class PlayerDataController_OutputLog : Il2CppSystem.Object
    {
        public PlayerDataController_OutputLog(IntPtr ptr) : base(ptr)
        {
        }
        public static void Prefix(PlayerDataController __instance)
        {
            ModComponent.MainLog.LogInfo("PlayerDataController.OutputLog");
            if (__instance.debugLogController == null)
            {
                __instance.debugLogController = ModComponent.LogController;
            }
        }
    }
    [HarmonyPatch(typeof(ResultController), nameof(ResultController.DamageValue))]
    class ResultController_DamageValue : Il2CppSystem.Object
    {
        public ResultController_DamageValue(IntPtr ptr) : base(ptr)
        {
        }
        public static void Prefix(ResultController __instance)
        {
            ModComponent.MainLog.LogInfo("ResultController.DamageValue");
            if (__instance.viewResultInputs != null)
            {
                if(__instance.viewResultInputs.debugLogController == null)
                {
                    __instance.viewResultInputs.debugLogController = ModComponent.LogController;
                }
                
            }
        }
    }
    [HarmonyPatch(typeof(ResultController), nameof(ResultController.ViewCondition))]
    class ResultController_ViewCondition : Il2CppSystem.Object
    {
        public ResultController_ViewCondition(IntPtr ptr) : base(ptr)
        {
        }
        public static void Prefix(ResultController __instance)
        {
            ModComponent.MainLog.LogInfo("ResultController.ViewCondition");
            if (__instance.viewResultInputs != null)
            {
                if (__instance.viewResultInputs.debugLogController == null)
                {
                    __instance.viewResultInputs.debugLogController = ModComponent.LogController;
                }

            }
        }
    }
    [HarmonyPatch(typeof(ResultController), nameof(ResultController.ViewHitType))]
    class ResultController_ViewHitType : Il2CppSystem.Object
    {
        public ResultController_ViewHitType(IntPtr ptr) : base(ptr)
        {
        }
        public static void Prefix(ResultController __instance)
        {
            ModComponent.MainLog.LogInfo("ResultController.ViewHitType");
            if (__instance.viewResultInputs != null)
            {
                if (__instance.viewResultInputs.debugLogController == null)
                {
                    __instance.viewResultInputs.debugLogController = ModComponent.LogController;
                }

            }
        }
    }
}
