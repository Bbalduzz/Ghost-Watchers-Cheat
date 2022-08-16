using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using Donteco;

namespace GhostWatchersESP
{
    class Hacks : MonoBehaviour
    {

        public void OnGUI()
        {
            ghostESP();
            playerESP();
            itemESP();
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
                    Color color = Color.white;
                    foreach (Donteco.PlayerAnimation player in FindObjectsOfType(typeof(Donteco.PlayerAnimation)) as Donteco.PlayerAnimation[])
                    {
                        Render.DrawGhostBox(footpos.x - (width / 2), (float)Screen.height - footpos.y - height, width, height, color, 2f, entity, Math.Round(Vector3.Distance(player.transform.position, ghost.transform.position)));
                    }      
                    Render.DrawLine(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), new Vector2(footpos.x, (float)Screen.height - footpos.y), color, 1f);
                }
            }
        }

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

                string player_nickname = player.name;
                
                if (w2s_footpos.z > 0f)
                {
                    // players esp
                    Vector3 footpos = w2s_footpos;
                    Vector3 headpos = w2s_headpos;
                    float height = headpos.y - footpos.y;
                    float widthOffset = 2f;
                    float width = height / widthOffset;
                    string entity = player_nickname;
                    Color color = Color.blue;
                    foreach (Donteco.GhostAI ghost in FindObjectsOfType(typeof(Donteco.GhostAI)) as Donteco.GhostAI[])
                    {
                        Render.DrawPlayerBox(footpos.x - (width / 2), (float)Screen.height - footpos.y - height, width, height, color, 1f, entity, Math.Round(Vector3.Distance(player.transform.position, ghost.transform.position)));
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
