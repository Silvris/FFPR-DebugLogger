using BepInEx.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Last.Battle;
using UnityEngine;
using Last.Battle.Damage;

namespace FFPR_DebugLogger
{
    public sealed class ModComponent : MonoBehaviour
    {
        public static ModComponent Instance { get; private set; }
        public static ManualLogSource Log { get; private set; }
        public static DiskLogListener DiskLog { get; set; }
        public static DebugLogController LogController { get; set; }
        private static Type[] debugLogList = { 
            typeof(BattleAiLogController), 
            typeof(BattlePlugManager), 
            typeof(BattleFieldConditionFunction), 
            typeof(MonsterSelectController),
            typeof(PlayerDataController),
            typeof(ResultController)
                };
        private Boolean _isDisabled;
        public ModComponent(IntPtr ptr) : base(ptr)
        {
        }
        public void Awake()
        {
            Instance = this;
            Log = BepInEx.Logging.Logger.CreateLogSource("FFPR_DebugLogger");
            DiskLog = new DiskLogListener(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/FFPR_Debug.log", appendLog: true);
            LogController = EntryPoint.LogController;
            GameObject singleton = gameObject;
            LogController.copyButton = singleton.AddComponent<UnityEngine.UI.Button>();
            LogController.scrollRect = singleton.AddComponent<UnityEngine.UI.ScrollRect>();
            LogController.text = singleton.AddComponent<UnityEngine.UI.Text>();
            LogController.Initialize();
            //LogController.hideFlags = HideFlags.HideAndDontSave;
            //DontDestroyOnLoad(LogController);
            try
            {
                Instance = this;
                Log.LogMessage($"[{nameof(ModComponent)}].{nameof(Awake)}: Processed successfully.");
            }
            catch (Exception ex)
            {
                _isDisabled = true;
                Log.LogError($"[{nameof(ModComponent)}].{nameof(Awake)}(): {ex}");
                throw;
            }

        }
        public void Update()
        {
            try
            {
                if (_isDisabled)
                {
                    return;
                }
                BattlePlugManager bpm = BattlePlugManager.Instance();
                if(bpm != null)
                {
                    bpm.debugLogController = LogController;
                    _isDisabled = true;
                }
                /*
                UnityEngine.Object[] obs = FindObjectsOfType<UnityEngine.Object>();
                foreach(UnityEngine.Object ob in obs)
                {
                    if (debugLogList.Contains(ob.GetType()))
                    {
                        ob.FieldSetter(ob.GetType().Name, "debugLogController", LogController); //baby's first system.reflection
                    }
                }*/
            }
            catch (Exception ex)
            {
                _isDisabled = true;
                Log.LogError($"[{nameof(ModComponent)}].{nameof(Update)}(): {ex}");
                throw;
            }

        }
    }
}
