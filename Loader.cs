using UnityEngine;

namespace Ghost_Watchers_Internal
{
    public class Loader
    {
        public static void init()
        {
            Loader.Load = new UnityEngine.GameObject();
            Loader.Load.AddComponent<Hacks>();
            UnityEngine.Object.DontDestroyOnLoad(Loader.Load);
        }

        public static void unload()
        {
            _Unload();
        }

        private static void _Unload()
        {
            GameObject.Destroy(Load);
        }

        private static GameObject Load;
    }
}
