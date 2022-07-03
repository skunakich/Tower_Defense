using UnityEngine;
using UnityEngine.Serialization;

public class Tower : MonoBehaviour
{
    public Renderer mainRenderer;
    public Vector2Int size = Vector2Int.one;
    public static bool Isbuild;

    public void SetTransparent(bool available)
    {
        if (available)
        {
            mainRenderer.material.color = Color.green;
        }
        else
        {
            mainRenderer.material.color = Color.red;
        }
    }

    public void SetNormal()
    {
        mainRenderer.material.color = Color.white;
        Isbuild = true;
    }

    private void OnDrawGizmos()
    {
        for (var x = 0; x < size.x; x++)
        {
            for (var y = 0; y < size.y; y++)
            {
                Gizmos.color = (x + y) % 2 == 0 ? new Color(0.88f, 0f, 1f, 0.3f) : new Color(1f, 0.68f, 0f, 0.3f);

                Gizmos.DrawCube(transform.position + new Vector3(x, 0, y), new Vector3(1, .1f, 1));
            }
        }
    }
}