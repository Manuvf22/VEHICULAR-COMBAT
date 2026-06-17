using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [Header("Crosshair Settings")]
    public float size = 20f;
    public float thickness = 2f;
    public float gap = 5f;
    public Color color = Color.white;

    void Start()
    {
        Cursor.visible = false;
    }

    void OnGUI()
    {
        Vector2 mouse = new Vector2(Event.current.mousePosition.x, Event.current.mousePosition.y);

        GUI.color = color;

        // Línea horizontal
        GUI.DrawTexture(new Rect(mouse.x - size - gap, mouse.y - thickness / 2, size, thickness), Texture2D.whiteTexture);
        GUI.DrawTexture(new Rect(mouse.x + gap, mouse.y - thickness / 2, size, thickness), Texture2D.whiteTexture);

        // Línea vertical
        GUI.DrawTexture(new Rect(mouse.x - thickness / 2, mouse.y - size - gap, thickness, size), Texture2D.whiteTexture);
        GUI.DrawTexture(new Rect(mouse.x - thickness / 2, mouse.y + gap, thickness, size), Texture2D.whiteTexture);

        GUI.color = Color.white; // resetear
    }
}