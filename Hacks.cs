using UnityEngine;

namespace GhostWatchersESP
{
    class Hacks : MonoBehaviour
    {

        public void OnGUI()
        {
            // Donteco.GhostAI = Ghost watchers Ghost class
            foreach (Donteco.GhostAI player in FindObjectsOfType(typeof(Donteco.GhostAI)) as Donteco.GhostAI[])
            {
                //In-Game Position
                Vector3 pivotPos = player.transform.position; //Pivot point NOT at the feet, at the center
                Vector3 playerFootPos; playerFootPos.x = pivotPos.x; playerFootPos.z = pivotPos.z; playerFootPos.y = pivotPos.y - 2f; //At the feet
                Vector3 playerHeadPos; playerHeadPos.x = pivotPos.x; playerHeadPos.z = pivotPos.z; playerHeadPos.y = pivotPos.y + 2f; //At the head

                //Screen Position
                Vector3 w2s_footpos = Camera.main.WorldToScreenPoint(playerFootPos);
                Vector3 w2s_headpos = Camera.main.WorldToScreenPoint(playerHeadPos);

                string ghosttype = GetGhostName(player.Data.name);
                string ghostmood = player.Data.MoodProperties.ToString();
                string ghostage = player.Data.AgeProperties.ToString();
                Render.GhostInfos(ghosttype, ghostage, ghostmood);

                if (w2s_footpos.z > 0f)
                {
                    DrawGhostBoxESP(w2s_footpos, w2s_headpos, Color.white);
                }
            }

            // Donteco.IngamePlayerInfoView = Ghost watchers Player class
            foreach (Donteco.PlayerAnimation player in FindObjectsOfType(typeof(Donteco.PlayerAnimation)) as Donteco.PlayerAnimation[])
            {
                //In-Game Position
                Vector3 pivotPos = player.transform.position; //Pivot point NOT at the feet, at the center
                Vector3 playerFootPos; playerFootPos.x = pivotPos.x; playerFootPos.z = pivotPos.z; playerFootPos.y = pivotPos.y - 2f; //At the feet
                Vector3 playerHeadPos; playerHeadPos.x = pivotPos.x; playerHeadPos.z = pivotPos.z; playerHeadPos.y = pivotPos.y + 2f; //At the head

                //Screen Position
                Vector3 w2s_footpos = Camera.main.WorldToScreenPoint(playerFootPos);
                Vector3 w2s_headpos = Camera.main.WorldToScreenPoint(playerHeadPos);

                if (w2s_footpos.z > 0f)
                {
                    DrawPlayerBoxESP(w2s_footpos, w2s_headpos, Color.blue);
                }
            }
        }

        public string GetGhostName(string ghosttype)
        {
            if (ghosttype.Contains("Poltergeist")):
                ghosttype = "Poltergeist";
            else if (ghosttype.Contains("GallowsGhost"))
                ghosttype = "GallowsGhost";
            else if (ghosttype.Contains("Baby"))
                ghosttype = "Baby";
            else if (ghosttype.Contains("Twins"))
                ghosttype = "Twins";
            else if (ghosttype.Contains("Drowned"))
                ghosttype = "Drowned";
            else if (ghosttype.Contains("Doppelganger"))
                ghosttype = "Doppelganger";
            else if (ghosttype.Contains("Darkness"))
                ghosttype = "Darkness";
            else if (ghosttype.Contains("Demon"))
                ghosttype = "Demon";
            else if (ghosttype.Contains("SupremeDemon"))
                ghosttype = "SupremeDemon";
            else if (ghosttype.ToString().Contains("KarlssonOnTheRoof"))
                ghosttype = "KarlssonOnTheRoof";
            else
                ghosttype = "Unknown";
     
            return ghosttype;
        }

        public void DrawGhostBoxESP(Vector3 footpos, Vector3 headpos, Color color) //Rendering the GHOST ESP
        {
            float height = headpos.y - footpos.y;
            float widthOffset = 2f;
            float width = height / widthOffset;
            string entity = "Ghost";

            //ESP BOX on the ghost
            Render.DrawGhostBox(footpos.x - (width / 2), (float)Screen.height - footpos.y - height, width, height, color, 2f, entity);
            //Snapline from the center of our screen
            Render.DrawLine(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), new Vector2(footpos.x, (float)Screen.height - footpos.y), color, 1f);
        }

        public void DrawPlayerBoxESP(Vector3 footpos, Vector3 headpos, Color color) //Rendering the PLAYER ESP
        {
            float height = headpos.y - footpos.y;
            float widthOffset = 2f;
            float width = height / widthOffset;
            string entity = "Player";

            //ESP BOX on the player
            Render.DrawPlayerBox(footpos.x - (width / 2), (float)Screen.height - footpos.y - height, width, height, color, 1f, entity);
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
