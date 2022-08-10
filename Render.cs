using UnityEngine;

namespace GhostWatchersESP
{
	public class Render : MonoBehaviour
	{
		public static GUIStyle StringStyle { get; set; } = new GUIStyle(GUI.skin.label);

		public static Color Color
		{
			get { return GUI.color; }
			set { GUI.color = value; }
		}

		public static void DrawString(Vector2 position, string label, bool centered = true)
		{
			var content = new GUIContent(label);
			var size = StringStyle.CalcSize(content);
			var upperLeft = centered ? position - size / 2f : position;
			GUI.Label(new Rect(upperLeft, size), content);
		}

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

		public static void DrawGhostBox(float x, float y, float w, float h, Color color, float thickness, string entity)
		{
			Vector2 P1 = new Vector2(x, y);
			Vector2 P2 = new Vector2((x+(w/2))-90, y);
			Vector2 P3 = new Vector2((x+(w/2))+90, y);
			Vector2 P4 = new Vector2(x+w, y);
			Vector2 P5 = new Vector2(x, (y+(h/2))-150);
			Vector2 P6 = new Vector2(x, (y+(h/2))+150);
			Vector2 P7 = new Vector2(x, y+h);
			Vector2 P8 = new Vector2((x+(w/2))-90, y+h);
			Vector2 P9 = new Vector2((x+(w/2))+90, y+h);
			Vector2 P10 = new Vector2(x+w, y+h);
			Vector2 P11 = new Vector2(x+w, (y+(h/2))+150); 
			Vector2 P12 = new Vector2(x+w, (y+(h/2))-150);
            DrawLine(P1, P2, color, thickness);
			DrawLine(P1, P5, color, thickness);
			DrawLine(P3, P4, color, thickness);
			DrawLine(P4, P12, color, thickness);
			DrawLine(P6, P7, color, thickness);
			DrawLine(P7, P8, color, thickness);
			DrawLine(P9, P10, color, thickness);
			DrawLine(P10, P11, color, thickness);
			GUI.Label(new Rect(x,y,w,h), new GUIContent(entity));
		}

		public static void DrawPlayerBox(float x, float y, float w, float h, Color color, float thickness, string entity)
		{
			DrawLine(new Vector2(x, y), new Vector2(x + w, y), color, thickness);
			DrawLine(new Vector2(x, y), new Vector2(x, y + h), color, thickness);
			DrawLine(new Vector2(x + w, y), new Vector2(x + w, y + h), color, thickness);
			DrawLine(new Vector2(x, y + h), new Vector2(x + w, y + h), color, thickness);
			GUI.Label(new Rect(x,y,w,h), new GUIContent(entity));
		}

		public static void GhostInfos(string type, string age, string mood)
        {
			GUI.Box(new Rect (10,10,200,120), "Ghost Infos");
			string gtype = string.Format("Ghost Type: {0}", type);
			GUI.Label(new Rect(20,40,200,20), new GUIContent(gtype));
			string gage = string.Format("Ghost Age: {0}", age);
			GUI.Label(new Rect(20,70,200,20), new GUIContent(gage));
			string gmood = string.Format("Ghost Mood: {0}", mood);
			GUI.Label(new Rect(20,100,200,20), new GUIContent(gmood));
        }
	}
}
