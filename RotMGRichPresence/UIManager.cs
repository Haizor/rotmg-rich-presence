using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RotMGRichPresence {
    public class UIManager : MonoBehaviour {
        public bool display = false;
        public Vector2 pos = Vector2.zero;

        public UIManager(IntPtr ptr) : base(ptr) {}

        public void Update() {
            if (Input.GetKeyUp(KeyCode.F1)) {
                display = !display;
                pos = Input.mousePosition;
                pos.y = Screen.height - pos.y;
            }
        }

        public void OnGUI() {
            if (display) {
                GUI.backgroundColor = Color.black;

                var style = GUI.skin.box;
                style.normal.textColor = Color.white;

                GUIContent content = new GUIContent();
                GUI.Box(new Rect(pos.x, pos.y, 155, 35), content, style);
                GUI.Label(new Rect(pos.x + 5, pos.y + 5, 100, 25), "Show Server: ", style);
                if (GUI.Button(new Rect(pos.x + 110, pos.y + 5, 40, 25), BepInExLoader.configShowServer.Value ? "Yes" : "No")) {
                    BepInExLoader.configShowServer.Value = !BepInExLoader.configShowServer.Value;
                }
            }
        }
    }
}
