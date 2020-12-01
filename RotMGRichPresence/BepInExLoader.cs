using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using DecaGames.RotMG.DebugTools;
using DecaGames.RotMG.Extensions;
using DecaGames.RotMG.Managers;
using DecaGames.RotMG.Managers.Game;
using DecaGames.RotMG.Managers.Net;
using DecaGames.RotMG.UI.Panels;
using HarmonyLib;
using UnhollowerRuntimeLib;
using UnityEngine;
using UnityEngine.UI;

namespace RotMGRichPresence {
    [BepInPlugin(GUID, NAME, VERSION)]
    public class BepInExLoader : BepInEx.IL2CPP.BasePlugin {
        public const string
            GUID = "net.haizor.rotmg-rich-presence",
            NAME = "RotMG Rich Presence",
            VERSION = "0.5";

        public static ManualLogSource log;
        public static ConfigEntry<bool> configShowServer;
        public BepInExLoader() : base() {
            log = Log;
        }
        
        public override void Load() {
            configShowServer = Config.Bind("General", "Show Server", false, "Show the server you are in?");

            ClassInjector.RegisterTypeInIl2Cpp<Bootstrapper>();
            ClassInjector.RegisterTypeInIl2Cpp<DataGrabber>();
            ClassInjector.RegisterTypeInIl2Cpp<UIManager>();

            GameObject obj = new GameObject("Bootstrapper");
            obj.AddComponent<Bootstrapper>();
            Object.DontDestroyOnLoad(obj);

            Harmony harmony = new Harmony(GUID);

            var originalAwake = AccessTools.Method(typeof(CanvasScaler), "Update");
            var postAwake = AccessTools.Method(typeof(Bootstrapper), "Update");
            harmony.Patch(originalAwake, postfix: new HarmonyMethod(postAwake));

            RpcManager.Init();
        }
    }
}
