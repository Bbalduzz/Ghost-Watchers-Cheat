using Donteco;

namespace Ghost_Watchers_Internal.objects
{
    class gwObjects
    {
        public static List<GhostAI> ghost;
        public static List<PlayerSetup> network_player;
        public static PlayerSetup localplayer;
        public static PlayerWallet wallet;
        public static LobbyManager lobbyManager;
        public static Lobby lobby;
        public static List<LobbyPlayer> lobbyPlayers;
        public static FlashlightController flashlight;
        public static List<OpenableObjectController> doors;
        public static List<LightSwitcherController> lights;
        public static List<WaterFaucetController> faucets;
        public static List<Footprint> footprints;
        public static List<Tool> items;
        public static List<ShopItem> shopItems;
        public static GhostInteractableItemsManager ghostItems;
        public static List<CandleEnvironmentController> candles;
        public static AreaNode areaNode;

        //CrossController, FireSaltController, EMPController, 

        public static void gwUpdater()
        {
            areaNode = UnityEngine.GameObject.FindObjectOfType<AreaNode>();
            ghost = UnityEngine.GameObject.FindObjectsOfType<GhostAI>().ToList();
            network_player = UnityEngine.GameObject.FindObjectsOfType<PlayerSetup>().ToList();
            flashlight = UnityEngine.GameObject.FindObjectOfType<FlashlightController>();
            wallet = UnityEngine.GameObject.FindObjectOfType<PlayerWallet>();
            doors = UnityEngine.GameObject.FindObjectsOfType<OpenableObjectController>().ToList();
            lights = UnityEngine.GameObject.FindObjectsOfType<LightSwitcherController>().ToList();
            faucets = UnityEngine.GameObject.FindObjectsOfType<WaterFaucetController>().ToList();
            footprints = UnityEngine.GameObject.FindObjectsOfType<Footprint>().ToList();
            items = UnityEngine.GameObject.FindObjectsOfType<Tool>().ToList();
            shopItems = UnityEngine.GameObject.FindObjectsOfType<ShopItem>().ToList();
            candles = UnityEngine.GameObject.FindObjectsOfType<CandleEnvironmentController>().ToList();

            ghostItems = GhostInteractableItemsManager.Instance;
            lobbyManager = LobbyManager.Instance;
            lobby = LobbyManager.Current;
            lobbyPlayers = lobby.Players.Values.ToList();

            foreach (PlayerSetup p in network_player)
            {
                if (p.IsLocalPlayer)
                {
                    localplayer = p;
                }
            }
        }

        public static void gwNull()
        {
            ghost = null;
            network_player = null;
            flashlight = null;
            wallet = null;
            doors = null;
            items = null;


            lobbyManager = LobbyManager.Instance;
            lobby = LobbyManager.Current;
            lobbyPlayers = lobby.Players.Values.ToList();

            foreach (PlayerSetup p in network_player)
            {
                if (p.IsLocalPlayer)
                {
                    localplayer = p;
                }
            }
        }
    }
}
