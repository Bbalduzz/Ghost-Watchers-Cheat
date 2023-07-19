using UnityEngine;
using ESP = Ghost_Watchers_Internal.src.ESP;
using gwObjs = Ghost_Watchers_Internal.objects.gwObjects;
using menu = Ghost_Watchers_Internal.objects.preferences;

namespace Ghost_Watchers_Internal
{
    internal class Hacks : MonoBehaviour
    {

        public static float Timer = 3f;
        public static Camera MainCamera = null;

        public void Start() { }

        public void Update()
        {
            gwObjs.gwUpdater();
        }
        public void OnGUI()
        {
            UI.displayUI();
            if (menu.show_ghost_esp) { ESP.ghostESP(); }
            if (menu.show_players_esp) { ESP.playerESP(); }
            if (menu.show_cursedItem) { ESP.itemESP(); }

        }
    }
}
