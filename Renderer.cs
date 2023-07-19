using UnityEngine;

namespace Ghost_Watchers_Internal
{
    internal class Renderer
    {
        public static GUIStyle StringStyle { get; set; } = new GUIStyle(GUI.skin.label);
        public static Color Color
        {
            get { return GUI.color; }
            set { GUI.color = value; }
        }
        public static void DrawString(Vector2 position, string label, Color color, bool centered = true)
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

        public static void DrawBoneLine(Vector3 w2s_objectStart, Vector3 w2s_objectFinish, Color color)
        {
            Renderer.DrawLine(new Vector2(w2s_objectStart.x, (float)Screen.height - w2s_objectStart.y), new Vector2(w2s_objectFinish.x, (float)Screen.height - w2s_objectFinish.y), color, 1f);
        }

        public static void Draw2DBox(float x, float y, float w, float h, Color color, float thickness, string entity)
        {
            DrawLine(new Vector2(x, y), new Vector2(x + w, y), color, thickness);
            DrawLine(new Vector2(x, y), new Vector2(x, y + h), color, thickness);
            DrawLine(new Vector2(x + w, y), new Vector2(x + w, y + h), color, thickness);
            DrawLine(new Vector2(x, y + h), new Vector2(x + w, y + h), color, thickness);
            GUI.Label(new Rect(x, y, w, h), $"{entity}");
        }

        public static void Draw2DCornerBox(float x, float y, float w, float h, Color color, float thickness, string entity)
        {
            float cornerSize = Mathf.Min(w, h) * 0.25f;

            // top-left corner
            DrawLine(new Vector2(x, y), new Vector2(x + cornerSize, y), color, thickness);
            DrawLine(new Vector2(x, y), new Vector2(x, y + cornerSize), color, thickness);
            // top-right corner
            DrawLine(new Vector2(x + w - cornerSize, y), new Vector2(x + w, y), color, thickness);
            DrawLine(new Vector2(x + w, y), new Vector2(x + w, y + cornerSize), color, thickness);
            // bottom-left corner
            DrawLine(new Vector2(x, y + h - cornerSize), new Vector2(x, y + h), color, thickness);
            DrawLine(new Vector2(x, y + h), new Vector2(x + cornerSize, y + h), color, thickness);
            // bottom-right corner
            DrawLine(new Vector2(x + w - cornerSize, y + h), new Vector2(x + w, y + h), color, thickness);
            DrawLine(new Vector2(x + w, y + h - cornerSize), new Vector2(x + w, y + h), color, thickness);

            // Calculate the position for entity label
            float labelX = x + (w / 2f);
            float labelY = y + h + 2f;

            // Set the alignment for the entity label
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.alignment = TextAnchor.LowerCenter;

            Vector2 labelSize = style.CalcSize(new GUIContent($"{entity}"));
            float labelWidth = labelSize.x;
            float labelHeight = labelSize.y;

            GUI.Label(new Rect(labelX - (labelWidth / 2f), labelY, labelWidth, labelHeight), $"{entity}", style);
        }
    }
}
