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
            if (Render.ShowGhostESP == true)
            {
                ghostESP();
            }
            if (Render.ShowPlayerESP == true)
            {
                playerESP();
            }
            if (Render.ShowCursedItem == true)
            {
                itemESP();
            }
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
            if (Render.MakeApperence == true){Appear();}
            if (Render.DoRandomAction == true) {RandomAction();}
            if (Render.DoAttack == true) {DoAttack();}
            if (Render.DoFastAttack == true) {DoFastAttack();}
            if (Render.DoHunt == true) {DoHunt();}
            if (Render.CapturePlayer == true) {DoCapture();}
            if (Render.MakeNoise == true) {MakeNoise();}
        }

        public static void Settings()
        {
            Render.ShowESPSettings();
            if (Render.ShowPlayerActions == true)
            {
                Render.ShowActions();
            }
            if (Render.ShowGhostActions == true)
            {
                Render.ShowGhostActionsSettings();
            }
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
                string rank = ghost.Data.Rank.ToString();
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
                    rank,
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
                        Render.DrawGhostBox(footpos.x - (width / 2), (float)Screen.height - footpos.y - height, width, height, color, 2f, entity, Math.Round(Vector3.Distance(player.transform.position, ghost.transform.position)));
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
        public static Vector3 bHead, bNeck;
        public static Vector3 bChest, bShoulderR;
        public static Vector3 bElbowR, bWristR;
        public static Vector3 bShoulderL, bElbowL;
        public static Vector3 bWristL, bSpine2;
        public static Vector3 bHipR, bKneeR;
        public static Vector3 bAnkleR, bHipL;
        public static Vector3 bKneeL, bAnkleL;
        public static Vector3 bSpine1, bHips;
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
                                if (!entity.IsLocalPlayer)
                                {
                                    if (entity.Male.GetComponentInChildren<SkinnedMeshRenderer>() != null && IsOnScreen(entity.transform))
                                    {
                                        Transform[] boneEnt = entity.Male.GetComponentInChildren<SkinnedMeshRenderer>().bones;
                                        int checker = 0;
                        
                                        foreach (Transform b in boneEnt)
                                        {

                                            if (b.name.Contains("Head_"))
                                            {
                                                bHead = Camera.main.WorldToScreenPoint(b.transform.position);
                                                checker++;
                                            }
                                            if (b.name.Contains("Neck_"))
                                            {
                                                bNeck = Camera.main.WorldToScreenPoint(b.transform.position);
                                                checker++;
                                            }
                                            if (b.name.Contains("Chest_"))
                                            {
                                                bChest = Camera.main.WorldToScreenPoint(b.transform.position);
                                                checker++;
                                            }
                                            if (b.name.Contains("Spine2_"))
                                            {
                                                bSpine2 = Camera.main.WorldToScreenPoint(b.transform.position);
                                                checker++;
                                            }
                                            if (b.name.Contains("Spine1_"))
                                            {
                                                bSpine1 = Camera.main.WorldToScreenPoint(b.transform.position);
                                                checker++;
                                            }
                                            if (b.name.Contains("Root_"))
                                            {
                                                bHips = Camera.main.WorldToScreenPoint(b.transform.position);
                                                checker++;
                                            }
                                            if (b.name.Contains("Hip_L"))
                                            {
                                                bHipL = Camera.main.WorldToScreenPoint(b.transform.position);
                                                checker++;
                                            }
                                            if (b.name.Contains("Hip_R"))
                                            {
                                                bHipR = Camera.main.WorldToScreenPoint(b.transform.position);
                                                checker++;
                                            }
                                            if (b.name.Contains("Knee_L"))
                                            {
                                                bKneeL = Camera.main.WorldToScreenPoint(b.transform.position);
                                                checker++;
                                            }
                                            if (b.name.Contains("Knee_R"))
                                            {
                                                bKneeR = Camera.main.WorldToScreenPoint(b.transform.position);
                                                checker++;
                                            }
                                            if (b.name.Contains("Ankle_L"))
                                            {
                                                bAnkleL = Camera.main.WorldToScreenPoint(b.transform.position);
                                                checker++;
                                            }
                                            if (b.name.Contains("Ankle_R"))
                                            {
                                                bAnkleR = Camera.main.WorldToScreenPoint(b.transform.position);
                                                checker++;
                                            }
                                            if (b.name.Contains("Shoulder_R"))
                                            {
                                                bShoulderR = Camera.main.WorldToScreenPoint(b.transform.position);
                                                checker++;
                                            }
                                            if (b.name.Contains("Shoulder_L"))
                                            {
                                                bShoulderL = Camera.main.WorldToScreenPoint(b.transform.position);
                                                checker++;
                                            }
                                            if (b.name.Contains("Elbow_R"))
                                            {
                                                bElbowR = Camera.main.WorldToScreenPoint(b.transform.position);
                                                checker++;
                                            }
                                            if (b.name.Contains("Elbow_L"))
                                            {
                                                bElbowL = Camera.main.WorldToScreenPoint(b.transform.position);
                                                checker++;
                                            }
                                            if (b.name.Contains("Wrist_L"))
                                            {
                                                bWristL = Camera.main.WorldToScreenPoint(b.transform.position);
                                                checker++;
                                            }
                                            if (b.name.Contains("Wrist_R"))
                                            {
                                                bWristR = Camera.main.WorldToScreenPoint(b.transform.position);
                                                checker++;
                                            }
                                        }
                        
                                        if (checker >= 18)
                                        {
                                            Render.DrawString(bHead, nickname, Color.yellow);
                                            Render.DrawBoneLine(bHead, bNeck, Color.green);
                                            Render.DrawBoneLine(bNeck, bChest, Color.green);
                                            Render.DrawBoneLine(bChest, bSpine2,Color.green);
                                            Render.DrawBoneLine(bSpine2, bSpine1, Color.green);
                                            Render.DrawBoneLine(bSpine1, bHips, Color.green);

                                            Render.DrawBoneLine(bNeck, bShoulderR, Color.green);
                                            Render.DrawBoneLine(bShoulderR, bElbowR,Color.green);
                                            Render.DrawBoneLine(bElbowR, bWristR, Color.green);

                                            Render.DrawBoneLine(bNeck, bShoulderL, Color.green);
                                            Render.DrawBoneLine(bShoulderL, bElbowL, Color.green);
                                            Render.DrawBoneLine(bElbowL, bWristL, Color.green);

                                            Render.DrawBoneLine(bHips, bHipR, Color.green);
                                            Render.DrawBoneLine(bHipR, bKneeR, Color.green);
                                            Render.DrawBoneLine(bKneeR, bAnkleR, Color.green);

                                            Render.DrawBoneLine(bHips, bHipL, Color.green);
                                            Render.DrawBoneLine(bHipL, bKneeL, Color.green);
                                            Render.DrawBoneLine(bKneeL, bAnkleL, Color.green);
                                        }
                                    }
                                }
                             }
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
            if (Render.UnlockAllAchievements == true)
            {
                foreach (Donteco.AchievementType a in Enum.GetValues(typeof(Donteco.AchievementType)))
                {
                    Donteco.AchievementsManager.IncrementAchievementValue(a);
                }
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

        public static GhostAI ghost;
        public static void Appear()
        {
            ghost = UnityEngine.GameObject.FindObjectOfType<GhostAI>();
            if (ghost != null) { ghost.Data.Visibility.SetVisibleForTime(3f); }
        }
        public static void RandomAction()
        {
            ghost = UnityEngine.GameObject.FindObjectOfType<GhostAI>();
            if (ghost != null) { ghost.Actions.Random(); }
        }

        public static void DoAttack()
        {
            ghost = UnityEngine.GameObject.FindObjectOfType<GhostAI>();
            if (ghost != null)
            {
                ghost.ResetCooldowns();
                ghost.DoCooldownAttack();
            }
        }

        public static void DoFastAttack()
        {
            ghost = UnityEngine.GameObject.FindObjectOfType<GhostAI>();
            if (ghost != null)
            {
                ghost.ResetCooldowns();
                ghost.DoCooldownFastAttack();
            }
        }

        public static void DoHunt()
        {
            ghost = UnityEngine.GameObject.FindObjectOfType<GhostAI>();
            if (ghost != null)
            {
                ghost.ResetCooldowns();
                ghost.DoCooldownHunt();
            }
        }

        public static void DoDamage()
        {
            ghost = UnityEngine.GameObject.FindObjectOfType<GhostAI>();
            if (ghost != null)
            {
                ghost.ResetCooldowns();
                ghost.DoDamage();
            }
        }

        public static void DoCapture()
        {
            ghost = UnityEngine.GameObject.FindObjectOfType<GhostAI>();
            if (ghost != null)
            {
                ghost.ResetCooldowns();
                ghost.DoCooldownCapture();
            }
        }
        public static void Teleport(Vector3 destination)
        {
            ghost = UnityEngine.GameObject.FindObjectOfType<GhostAI>();
            if (ghost != null)
            {
                ghost.Movement.Teleport(destination);
            }
        }

        public static void MakeNoise()
        {
            ghost = UnityEngine.GameObject.FindObjectOfType<GhostAI>();
            ghost.Audio.PlayInteractBreath();
            ghost.Audio.PlayWarningRunAttack();
        }
    }
}
