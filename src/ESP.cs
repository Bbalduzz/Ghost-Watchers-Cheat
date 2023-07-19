using UnityEngine;

using menu = Ghost_Watchers_Internal.objects.preferences;

namespace Ghost_Watchers_Internal.src
{
    internal class ESP
    {
        public static void ghostESP()
        {
            foreach (Donteco.GhostAI ghost in UnityEngine.GameObject.FindObjectsOfType(typeof(Donteco.GhostAI)) as Donteco.GhostAI[])
            {
                Vector3 pivotPos = ghost.transform.position; //Pivot point NOT at the feet, at the center
                Vector3 playerFootPos; playerFootPos.x = pivotPos.x; playerFootPos.z = pivotPos.z; playerFootPos.y = pivotPos.y - 2f; //At the feet
                Vector3 playerHeadPos; playerHeadPos.x = pivotPos.x; playerHeadPos.z = pivotPos.z; playerHeadPos.y = pivotPos.y + 2f; //At the head

                Vector3 w2s_footpos = Camera.main.WorldToScreenPoint(playerFootPos);
                Vector3 w2s_headpos = Camera.main.WorldToScreenPoint(playerHeadPos);

                if (w2s_footpos.z > 0f)
                {
                    Vector3 footpos = w2s_footpos;
                    Vector3 headpos = w2s_headpos;
                    float height = headpos.y - footpos.y;
                    float widthOffset = 2f;
                    float width = height / widthOffset;
                    string entity = ghost.Data.name.Replace("(Clone)", "");
                    Color color = Color.red;
                    Renderer.Draw2DCornerBox(footpos.x - (width / 2), (float)Screen.height - footpos.y - (height / 2), width / 2, height / 2, color, 2f, entity);
                    if (menu.show_ghost_snapline)
                    {
                        Renderer.DrawLine(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), new Vector2(footpos.x, (float)Screen.height - footpos.y), color, 1f);
                    }

                }
            }
        }

        public static void playerESP()
        {
            foreach (Donteco.PlayerAnimation player in UnityEngine.GameObject.FindObjectsOfType(typeof(Donteco.PlayerAnimation)) as Donteco.PlayerAnimation[])
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
                    Vector3 footpos = w2s_footpos;
                    Vector3 headpos = w2s_headpos;
                    float height = headpos.y - footpos.y;
                    float widthOffset = 2f;
                    float width = height / widthOffset;
                    Color color = Color.green;
                    Renderer.Draw2DBox(footpos.x - (width / 2), (float)Screen.height - footpos.y - (height / 2), width / 2, height / 2, color, 2f, "Player");
                    if (menu.show_players_snapline)
                    {
                        Renderer.DrawLine(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), new Vector2(footpos.x, (float)Screen.height - footpos.y), color, 1f);
                    }
                }
            }
        }

        private static bool IsOnScreen(Transform ent_transform)
        {
            Vector3 w2s_check = Camera.main.WorldToScreenPoint(ent_transform.position);
            if (w2s_check.z > 0.1f && w2s_check.x > 0f && w2s_check.x < (float)Screen.width && w2s_check.y > 0 && w2s_check.y < (float)Screen.height) { return true; }
            return false;
        }

        public static void itemESP()
        {
            Color color = Color.magenta;
            foreach (Donteco.ChildrenToyController entity in UnityEngine.GameObject.FindObjectsOfType(typeof(Donteco.ChildrenToyController)) as Donteco.ChildrenToyController[])
            {
                if (entity != null)
                {
                    if (IsOnScreen(entity.transform))
                    {
                        Vector3 w2s_obj = Camera.main.WorldToScreenPoint(entity.transform.position);
                        Renderer.DrawString(new Vector2(w2s_obj.x, (float)Screen.height - w2s_obj.y), $"Cursed", color);
                    }
                }
            }

        }
    }
}
