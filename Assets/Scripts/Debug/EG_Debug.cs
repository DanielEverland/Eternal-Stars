using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EG_Debug {

    public static void DrawRect(Rect rect)
    {
        DrawRect(rect, Color.white, 0, true);
    }
    public static void DrawRect(Rect rect, Color color)
    {
        DrawRect(rect, color, 0, true);
    }
    public static void DrawRect(Rect rect, Color color, float duration)
    {
        DrawRect(rect, color, duration, true);
    }
	public static void DrawRect(Rect rect, Color color, float duration, bool depthTest)
    {
        //Top
        Vector3 topStart = new Vector3(rect.x, 0, rect.y);
        Vector3 topEnd = new Vector3(rect.x, 0, rect.y) + new Vector3(rect.width, 0);
        Debug.DrawLine(topStart, topEnd, color, duration, depthTest);

        //Bottom
        Vector3 bottomStart = new Vector3(rect.x, 0, rect.y + rect.height);
        Vector3 bottomEnd = new Vector3(rect.x + rect.width, 0, rect.y + rect.height);
        Debug.DrawLine(bottomStart, bottomEnd, color, duration, depthTest);

        //Left
        Vector3 leftStart = new Vector3(rect.x, 0, rect.y);
        Vector3 leftEnd = new Vector3(rect.x, 0, rect.y + rect.height);
        Debug.DrawLine(leftStart, leftEnd, color, duration, depthTest);

        //Right
        Vector3 rightStart = new Vector3(rect.x + rect.width, 0, rect.y);
        Vector3 rightEnd = new Vector3(rect.x + rect.width, 0, rect.y + rect.height);
        Debug.DrawLine(rightStart, rightEnd, color, duration, depthTest);
    }
}
