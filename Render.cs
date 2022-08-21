using UnityEngine;
using UnityEngine.UI;

namespace GhostWatchersESP
{
	public class Render : MonoBehaviour
	{
		public static bool ShowPlayerActions = true;
		public static bool AddMoneyAndEXP = false;
		public static bool UnlockAllAchievements = false;
		public static bool BecomeHost = false;
		public static bool ShowCursedItem = true;
		public static bool ShowGhostESP = true;
		public static bool ShowPlayerESP = false;
		public static bool ShowMoreInfos = false;
		public static bool ShowGhostActions = true;
		public static bool MakeApperence = false;
		public static bool DoRandomAction = false;
	    public static bool DoAttack = false;
		public static bool DoFastAttack = false;
		public static bool DoHunt = false;
		public static bool DoDamage = false;
	    public static bool CapturePlayer = false;
		public static bool MakeNoise = false;
		public static bool DoTeleport = false;
		public static bool ShowGhostInfos = true;

		public static GUIStyle StringStyle { get; set; } = new GUIStyle(GUI.skin.label);

		public static Color Color
		{
			get { return GUI.color; }
			set { GUI.color = value; }
		}

		public static void DrawString(Vector2 position, string label, Color color,  bool centered = true)
		{
			var content = new GUIContent(label);
			var size = StringStyle.CalcSize(content);
			var upperLeft = centered ? position - size / 2f : position;
			GUI.Label(new Rect(upperLeft, size), content);
		}
		public static bool ShowGhostLine = true; // ghost line bool
		public static Texture2D lineTex;
		public static void DrawLine(Vector2 pointA, Vector2 pointB, Color color, float width)
		{
			Matrix4x4 matrix = GUI.matrix;
			if (!lineTex)
				lineTex = new Texture2D(1, 1);

			Color color2 = GUI.color;
			GUI.color = color;
			float num = Vector3.Angle(pointB - pointA, Vector2.right);

			if (pointA.y > pointB.y)
				num = -num;

			GUIUtility.ScaleAroundPivot(new Vector2((pointB - pointA).magnitude, width), new Vector2(pointA.x, pointA.y + 0.5f));
			GUIUtility.RotateAroundPivot(num, pointA);
			GUI.DrawTexture(new Rect(pointA.x, pointA.y, 1f, 1f), lineTex);
			GUI.matrix = matrix;
			GUI.color = color2;
		}
		public static void ShowActions()
        {
			int x = Screen.width - 210;
			int y = Screen.height - 110;
			GUI.Box(new Rect (x,y,200,130), "Actions");
			if (GUI.Button(new Rect(x+10,y+30,150,20), "Give Money and EXP"))
            AddMoneyAndEXP = !AddMoneyAndEXP;
			if (GUI.Button(new Rect(x+10,y+60,150,20), "Unlock Achivements"))
            UnlockAllAchievements = !UnlockAllAchievements;
			if (GUI.Button(new Rect(x+10,y+90,150,20), "Become the Host"))
            BecomeHost = !BecomeHost;
        }

		public static void ShowGhostActionsSettings()
        {
			int x = Screen.width - 210;
			int y = 280;
			GUI.Box(new Rect (x,y,200,250), "Ghost Actions");
			if (GUI.Button(new Rect(x+10,y+30,150,20), "Appear"))
            MakeApperence = !MakeApperence; // to add 
			if (GUI.Button(new Rect(x+10,y+60,150,20), "Do Random Action"))
            DoRandomAction = !DoRandomAction; //to add
			if (GUI.Button(new Rect(x+10,y+90,150,20), "Attack"))
            DoAttack = !DoAttack; // to add
			if (GUI.Button(new Rect(x+10,y+120,150,20), "Fast Attack"))
            DoFastAttack = !DoFastAttack; // to add
			if (GUI.Button(new Rect(x+10,y+150,150,20), "Hunt"))
            DoHunt = !DoHunt; // to add
			if (GUI.Button(new Rect(x+10,y+180,150,20), "Do Damage"))
            DoDamage = !DoDamage; // to add
			if (GUI.Button(new Rect(x+10,y+210,150,20), "Capture a player"))
            CapturePlayer = !CapturePlayer; // to add
			if (GUI.Button(new Rect(x+10,y+240,150,20), "Make a noise"))
            MakeNoise = !MakeNoise; // to add
        }

		public static void ShowESPSettings()
        {
			int x = Screen.width - 210;
			GUI.Box(new Rect (x,10,200,250), "Settings");
			if (GUI.Button(new Rect(x+10,40,150,20), "Ghost Infos"))
            ShowGhostInfos = !ShowGhostInfos;
			if (GUI.Button(new Rect(x+10,70,150,20), "Show Players ESP"))
            ShowPlayerESP = !ShowPlayerESP;
			if (GUI.Button(new Rect(x+10,100,150,20), "Show Ghost ESP"))
            ShowGhostESP = !ShowGhostESP;
			if (GUI.Button(new Rect(x+10,130,150,20), "Show Cursed Item"))
            ShowCursedItem = !ShowCursedItem;
			if (GUI.Button(new Rect(x+10,160,150,20), "Show Ghost SnapLine"))
            ShowGhostLine = !ShowGhostLine;
			if (GUI.Button(new Rect(x+10,190,150,20), "Open Local Teaks"))
            ShowPlayerActions = !ShowPlayerActions;
			if (GUI.Button(new Rect(x+10,220,150,20), "Open Ghost Actions"))
            ShowGhostActions = !ShowGhostActions;
        }


		public static void DrawGhostBox(float x, float y, float w, float h, Color color, float thickness, string entity, double distance)
		{
			DrawLine(new Vector2(x, y), new Vector2(x + w, y), color, thickness);
			DrawLine(new Vector2(x, y), new Vector2(x, y + h), color, thickness);
			DrawLine(new Vector2(x + w, y), new Vector2(x + w, y + h), color, thickness);
			DrawLine(new Vector2(x, y + h), new Vector2(x + w, y + h), color, thickness);
			GUI.Label(new Rect(x,y,w,h), $"{entity} - [{distance}]");
		}

		public static void DrawPlayerBox(float x, float y, float w, float h, Color color, float thickness, string entity, double distance)
		{
			if(ShowPlayerESP == true)
            {
				DrawLine(new Vector2(x, y), new Vector2(x + w, y), color, thickness);
				DrawLine(new Vector2(x, y), new Vector2(x, y + h), color, thickness);
				DrawLine(new Vector2(x + w, y), new Vector2(x + w, y + h), color, thickness);
				DrawLine(new Vector2(x, y + h), new Vector2(x + w, y + h), color, thickness);
				GUI.Label(new Rect(x,y,w,h), $"{entity} - [{distance}]");
            }
			
		}

		public static void GhostInfos(string type, string age, string mood, float temp, string rank, bool cancapture, bool canattack, bool canrageattack, bool canhunt, bool cancrattack, bool isfullweak, int emp_value)
        {
			if (ShowGhostInfos == true)
            {
				GUI.Box(new Rect (10,10,200,150), "Ghost Infos");
				string gtype = string.Format("Ghost Type: {0}", type);
				GUI.Label(new Rect(20,40,200,20), new GUIContent(gtype));
				string gage = string.Format("Ghost Age: {0}", age);
				GUI.Label(new Rect(20,70,200,20), new GUIContent(gage));
				string gmood = string.Format("Ghost Mood: {0}", mood);
				GUI.Label(new Rect(20,100,200,20), new GUIContent(gmood));

				if (GUI.Button(new Rect(20,130,100,20), "Show More"))
				ShowMoreInfos = !ShowMoreInfos;
				string huntdistance = "";
				if (ShowMoreInfos == true)
				{
					GUI.Box(new Rect (10,160,200,330), "Additional Infos");
					string temps = string.Format("Temperature: {0}", temp);
					GUI.Label(new Rect(20,190,200,20), new GUIContent(temps));
					string emp = string.Format("Emp Level: {0}", emp_value);
					GUI.Label(new Rect(20,220,200,20), new GUIContent(emp));
					string r = string.Format("Ghost Rank: {0}", rank);
					GUI.Label(new Rect(20,250,200,20), new GUIContent(r));
					if (r.Contains("1"))
					{
						huntdistance = "10";
					} else if (r.Contains("2"))
					{
						huntdistance = "15";
					}
					else
					{
						huntdistance = "Unknown";
					}
					string hd = string.Format("Hunt Distance: {0}", huntdistance);
					GUI.Label(new Rect(20,280,200,20), new GUIContent(hd));
					string cc = string.Format("Can Capture Now: {0}", cancapture);
					GUI.Label(new Rect(20,310,200,20), new GUIContent(cc));
					string ca = string.Format("Can Attack Now: {0}", canattack);
					GUI.Label(new Rect(20,340,200,20), new GUIContent(ca));
					string cra = string.Format("Can Rage Attack Now: {0}", canrageattack);
					GUI.Label(new Rect(20,370,200,20), new GUIContent(cra));
					string ch = string.Format("Can Hunt Now: {0}", canhunt);
					GUI.Label(new Rect(20,400,200,20), new GUIContent(ch));
					string ccra = string.Format("Can Critical Hit: {0}", cancrattack);
					GUI.Label(new Rect(20,430,200,20), new GUIContent(ccra));
					string fw = string.Format("Full Weakness: {0}", isfullweak);
					GUI.Label(new Rect(20,460,200,20), new GUIContent(fw));
				}
            }
        }

		public static void DrawBoneLine(Vector3 w2s_objectStart, Vector3 w2s_objectFinish, Color color)
        {
            if (w2s_objectStart != null && w2s_objectFinish != null)
            {
                Render.DrawLine(new Vector2(w2s_objectStart.x, (float)Screen.height - w2s_objectStart.y), new Vector2(w2s_objectFinish.x, (float)Screen.height - w2s_objectFinish.y), color, 1f);
            }
        }
    }
}

