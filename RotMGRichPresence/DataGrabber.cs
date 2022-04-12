using DecaGames.RotMG.Managers;
using DecaGames.RotMG.Managers.Game;
using DecaGames.RotMG.Managers.GUI;
using DecaGames.RotMG.Managers.Tooltip;
using DecaGames.RotMG.Managers.UI;
using DecaGames.RotMG.UI.Controllers;
using DecaGames.RotMG.UI.GUI;
using DecaGames.RotMG.UI.Managers;
using DecaGames.RotMG.UI.Panels;
using DecaGames.RotMG.UI.Slots;
using DecaGames.RotMG.UI.Slots.Character;
using HarmonyLib;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Input = BepInEx.IL2CPP.UnityEngine.Input;
using KeyCode = BepInEx.IL2CPP.UnityEngine.KeyCode;

namespace RotMGRichPresence {
    public class DataGrabber : MonoBehaviour {
        public static bool pressed = false;
        public static string location = "None!";
        public static string portalName = "Unknown";
        public static string realmName = "Unknown";
        public static string server = "Unknown";
        public static string accountName = null;
        public static string className = null;

        private static float elapsed = 0;
        private static float updateRate = 1;

        private static CharacterInfo characterInfo;
        private static ApplicationManager appManager;
        private static GuiManager guiManager;

        private static string[] realmPortalNames = new string[] {
            "Pirate",
            "Deathmage",
            "Spectre",
            "Titan",
            "Gorgon",
            "Kraken",
            "Satyr",
            "Drake",
            "Chimera",
            "Dragon",
            "Wrym",
            "Hydra",
            "Leviathan",
            "Minotaur",
            "Mummy",
            "Reaper",
            "Phoenix",
            "Giant",
            "Unicorn",
            "Harpy",
            "Gargoyle",
            "Snake",
            "Cube",
            "Goblin",
            "Hobbit",
            "Skeleton",
            "Scorpion",
            "Bat",
            "Ghost",
            "Slime",
            "Lich",
            "Orc",
            "Imp",
            "Spider",
            "Demon",
            "Blob",
            "Golem",
            "Sprite",
            "Flayer",
            "Ogre",
            "Djinn",
            "Cyclops",
            "Beholder",
            "Medusa"
        };

        public DataGrabber(IntPtr ptr) : base(ptr) {}

        public void Update() {
            if (elapsed >= updateRate) {
                if (guiManager == null) {
                    guiManager = FindObjectOfType<GuiManager>();
                }

                if (guiManager != null) {
                    if (guiManager.interactionPanel != null && guiManager.interactionPanel.headText != null) {
                        portalName = guiManager.interactionPanel.headText.text;
                        string matched = Regex.Replace(portalName, @"[\(1234567890+\/\) ]", "");
                        if (Array.Exists(realmPortalNames, (name) => name == matched)) {
                            realmName = matched;
                        }
                    }
                } 

                if (characterInfo == null) {
                    characterInfo = FindObjectOfType<CharacterInfo>();
                }

                if (characterInfo != null) {
                    accountName = characterInfo.accountName.GetParsedText();
                    location = characterInfo.DPFDOFNIJHM.GMJGBNCMJDG;
                    
                    if (location == "Realm of the Mad God") {
                        location = realmName;
                    }
                    try {
                        className = characterInfo.characterIconLoader.CDPFBLIIEMA.FHEEIFDDCME.ANELECFAPHF;
                    }
                    catch { }
                }

                if (appManager == null) {
                    appManager = FindObjectOfType<ApplicationManager>();
                }

                if (appManager != null) {
                    var serverManager = appManager.CHMPDHDJEOA;

                    if (serverManager != null)
                    {
                        server = serverManager.JLLDPBAANCK().ANELECFAPHF;
                    }
                }

                RpcManager.UpdateStatus();
                elapsed = 0;
            }
            elapsed += Time.deltaTime;
            
        }

        void OnApplicationQuit() {
            RpcManager.Close();
        }
    }
}
