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
using Last.Management;

namespace FFPR_DebugLogger
{
    public sealed class ModComponent : MonoBehaviour
    {
        public static ModComponent Instance { get; private set; }
        public static ManualLogSource MainLog { get; set; }
        public static ManualLogSource Log { get; private set; }
        public static DiskLogListener DiskLog { get; set; }
        public static UnityEngine.SceneManagement.Scene CurrentScene { get; set; }
        public static DebugLogController LogController { get; set; }
        private Boolean _isDisabled;
        public ModComponent(IntPtr ptr) : base(ptr)
        {
        }
        public void MoveScenes()
        {
            UnityEngine.SceneManagement.SceneManager.MoveGameObjectToScene(gameObject, UnityEngine.SceneManagement.SceneManager.GetActiveScene());

        }
        public void Awake()
        {
            Instance = this;
            MainLog = BepInEx.Logging.Logger.CreateLogSource("FFPR_DebugLogger");
            Log = BepInEx.Logging.Logger.CreateLogSource("Debug Log");
            DiskLog = new DiskLogListener(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/FFPR_Debug.log", appendLog: false);
            Log.LogEvent += DiskLog.LogEvent;
            LogController = EntryPoint.LogController;
            GameObject singleton = gameObject;
            LogController.copyButton = singleton.AddComponent<UnityEngine.UI.Button>();
            LogController.scrollRect = singleton.AddComponent<UnityEngine.UI.ScrollRect>();
            LogController.text = singleton.AddComponent<UnityEngine.UI.Text>();
            MoveScenes();
            //LogController.Initialize();// initialize function no longer works it seems, can add everything it needs 
            try
            {
                Instance = this;
                MainLog.LogMessage($"[{nameof(ModComponent)}].{nameof(Awake)}: Processed successfully.");
            }
            catch (Exception ex)
            {
                _isDisabled = true;
                MainLog.LogError($"[{nameof(ModComponent)}].{nameof(Awake)}(): {ex}");
                throw;
            }

        }
        public void Update()
        {
            try
            {
                if(gameObject.scene != null)
                {
                    if(gameObject.scene.name != UnityEngine.SceneManagement.SceneManager.GetActiveScene().name)
                    {
                        MoveScenes();
                    }
                }
                else
                {
                    MoveScenes();
                }
                if (_isDisabled)
                {
                    return;
                }
                BattlePlugManager bpm = BattlePlugManager.Instance();
                if(bpm != null)
                {
                    bpm.debugLogController = LogController;
                }
                _isDisabled = true;
            }
            catch (Exception ex)
            {
                _isDisabled = true;
                MainLog.LogError($"[{nameof(ModComponent)}].{nameof(Update)}(): {ex}");
                throw;
            }

        }
    }
}
