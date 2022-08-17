using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using Donteco;
using MhNetworking;

namespace GhostWatchersESP
{
    class Hacks : MonoBehaviour
    {
        public void OnGUI()
        {
            Settings();
            ghostESP();
            playerESP();
            itemESP();
            if(Render.AddMoneyAndEXP == true)
            {
                GiveMoneyAndExp(10000, 1000);
            }
            if(Render.UnlockAllAchievements == true)
            {
                UnlockAllAchievements();
            }
            if(Render.BecomeHost == true)
            {
                SetMasterClient();
            }
        }

        public static void Settings()
        {
            Render.ShowESPSettings();
            Render.ShowActions();
        }

        public static void ghostESP()
        {
            foreach (Donteco.GhostAI ghost in FindObjectsOfType(typeof(Donteco.GhostAI)) as Donteco.GhostAI[])
            {
                Vector3 pivotPos = ghost.transform.position; //Pivot point NOT at the feet, at the center
                Vector3 playerFootPos; playerFootPos.x = pivotPos.x; playerFootPos.z = pivotPos.z; playerFootPos.y = pivotPos.y - 2f; //At the feet
                Vector3 playerHeadPos; playerHeadPos.x = pivotPos.x; playerHeadPos.z = pivotPos.z; playerHeadPos.y = pivotPos.y + 2f; //At the head

                //Screen Position
                Vector3 w2s_footpos = Camera.main.WorldToScreenPoint(playerFootPos);
                Vector3 w2s_headpos = Camera.main.WorldToScreenPoint(playerHeadPos);

                //Get ghost informations
                string ghosttype = ghost.Data.name.Replace("(Clone)", "");
                string ghostmood = ghost.Data.MoodProperties.ToString();
                string ghostage = ghost.Data.AgeProperties.ToString();
                float temp = ghost.Data.GetTemperatureValue();
                int emp = ghost.Data.GetEmpValue();
                float huntdistance = ghost.Data.DistanceForHunt;
                bool cancapture = ghost.CanStartCapture();
                bool canattack = ghost.CanStartAttack();
                bool canrageattack = ghost.CanRangeAttack();
                bool canhunt = ghost.CanHunt();
                bool cancrattack = ghost.CanCriticalAttack();
                bool isfullweak = ghost.IsFullWeakness.Value;
                Render.GhostInfos(
                    ghosttype,
                    ghostage,
                    ghostmood,
                    temp,
                    huntdistance,
                    cancapture,
                    canattack,
                    canrageattack,
                    canhunt,
                    cancrattack,
                    isfullweak,
                    emp
                );

                if (w2s_footpos.z > 0f)
                {
                    // ghost esp
                    Vector3 footpos = w2s_footpos;
                    Vector3 headpos = w2s_headpos;
                    float height = headpos.y - footpos.y;
                    float widthOffset = 2f;
                    float width = height / widthOffset;
                    string entity = ghosttype;
                    Color color = Color.red;
                    foreach (Donteco.PlayerAnimation player in FindObjectsOfType(typeof(Donteco.PlayerAnimation)) as Donteco.PlayerAnimation[])
                    {
                        if (Render.ShowGhostESP == true)
                        {
                            Render.DrawGhostBox(footpos.x - (width / 2), (float)Screen.height - footpos.y - height, width, height, color, 2f, entity, Math.Round(Vector3.Distance(player.transform.position, ghost.transform.position)));
                        }
                    }      
                    if (Render.ShowGhostLine == true)
                    {
                        Render.DrawLine(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), new Vector2(footpos.x, (float)Screen.height - footpos.y), color, 1f);
                    }
                }
            }
        }
        public static LobbyManager lobbyManager;
        public static List<LobbyPlayer> lobbyPlayers;
        public static Lobby lobby;
        public static PlayerSetup localplayer;
        public static List<PlayerSetup> network_player;
        public static void playerESP()
        {
            foreach (Donteco.PlayerAnimation player in FindObjectsOfType(typeof(Donteco.PlayerAnimation)) as Donteco.PlayerAnimation[])
            {
                //In-Game Position
                Vector3 pivotPos = player.transform.position; //Pivot point NOT at the feet, at the center
                Vector3 playerFootPos; playerFootPos.x = pivotPos.x; playerFootPos.z = pivotPos.z; playerFootPos.y = pivotPos.y - 2f; //At the feet
                Vector3 playerHeadPos; playerHeadPos.x = pivotPos.x; playerHeadPos.z = pivotPos.z; playerHeadPos.y = pivotPos.y + 2f; //At the head

                //Screen Position
                Vector3 w2s_footpos = Camera.main.WorldToScreenPoint(playerFootPos);
                Vector3 w2s_headpos = Camera.main.WorldToScreenPoint(playerHeadPos);

                network_player = UnityEngine.GameObject.FindObjectsOfType<PlayerSetup>().ToList();
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
                string nickname = "";
                foreach (LobbyPlayer i in lobbyPlayers)
                {
                    foreach (PlayerSetup entity in network_player)
                    {
                         if (i.Id == entity.SteamId.ToString())
                        {
                            if (IsOnScreen(entity.transform))
                            {
                                nickname = $"{i.Nickname} [{Math.Round(Vector3.Distance(entity.transform.position, Hacks.localplayer.transform.position), 0)}]";
                            }
                        }
                    }
                   
                }
                if (w2s_footpos.z > 0f)
                {
                    // players esp
                    Vector3 footpos = w2s_footpos;
                    Vector3 headpos = w2s_headpos;
                    float height = headpos.y - footpos.y;
                    float widthOffset = 2f;
                    float width = height / widthOffset;
                    string entity = nickname;
                    Color color = Color.blue;
                    foreach (Donteco.GhostAI ghost in FindObjectsOfType(typeof(Donteco.GhostAI)) as Donteco.GhostAI[])
                    {
                        if (Render.ShowPlayerESP == true)
                        {
                            Render.DrawPlayerBox(footpos.x - (width / 2), (float)Screen.height - footpos.y - height, width, height, color, 1f, entity, Math.Round(Vector3.Distance(player.transform.position, ghost.transform.position)));
                        }
                    }
                }
            }
        }
        public static List<ChildrenToyController> item;
        private static void itemESP()
        {
           item = UnityEngine.GameObject.FindObjectsOfType<ChildrenToyController>().ToList();
           foreach (Donteco.ChildrenToyController entity in item)
            {
                if (entity != null)
                {
                    if (IsOnScreen(entity.transform))
                    {
                        Vector3 w2s_obj = Camera.main.WorldToScreenPoint(entity.transform.position);
                        Render.DrawString(new Vector2(w2s_obj.x, (float)Screen.height - w2s_obj.y), $"Cursed Item", Color.yellow);
                    }
                }
            }
        }

        private static bool IsOnScreen(Transform ent_transform)
        {
            Vector3 w2s_check = Camera.main.WorldToScreenPoint(ent_transform.position);   
            if (w2s_check.z > 0.1f && w2s_check.x > 0f && w2s_check.x < (float)Screen.width && w2s_check.y > 0 && w2s_check.y < (float)Screen.height) { return true;}
            return false;
        }

        public static PlayerWallet wallet;
        public static void GiveMoneyAndExp(int money, int exp)
        {
            wallet = UnityEngine.GameObject.FindObjectOfType<PlayerWallet>();
            if (wallet != null)
            {
                wallet.AddMoneyAndExp(money, exp);
            }
        }

        public static void UnlockAllAchievements()
        {
            foreach (Donteco.AchievementType a in Enum.GetValues(typeof(Donteco.AchievementType)))
            {
                Donteco.AchievementsManager.IncrementAchievementValue(a);
            }
        }

        public static void SetMasterClient()
        {
            GameObject localPlayer = Donteco.GameConsole.CommandUtils.FindLocalPlayer();
            CommonMsg.SetMasterClient setMasterClient = new CommonMsg.SetMasterClient();
            setMasterClient.ObjectId = -1;
            setMasterClient.NewMasterId = localPlayer.GetComponent<NetIdentity>().PlayerId;
            NetworkManager.Send((Message)setMasterClient);
        }

        public void Update()
        {
            // to hide the esp (press "J")
            if (Input.GetKeyDown(KeyCode.J))
            {
                Loader.Unload();
            }
        }
    }
}
