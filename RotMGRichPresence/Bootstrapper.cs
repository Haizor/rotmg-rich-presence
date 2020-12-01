using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RotMGRichPresence {
    public class Bootstrapper : MonoBehaviour {
        public static bool ran = false;
        public Bootstrapper(IntPtr ptr) : base(ptr) { }

        [HarmonyPostfix]
        public static void Update() {
            if (!ran) {
                GameObject obj = new GameObject("DataGrabber");
                obj.AddComponent<DataGrabber>();
                obj.AddComponent<UIManager>();
                DontDestroyOnLoad(obj);
                ran = true;
            }
        }
    }
}
