using UnityEngine;
using System.Collections;

public static class VectorExtensions {

    public static Vector2 rotate(this Vector2 v2, float angle)
    {
        float ca = Mathf.Cos(angle * Mathf.Deg2Rad);
        float sa = Mathf.Sin(angle * Mathf.Deg2Rad);
        float x = ca * v2.x - sa * v2.y;
        float y = ca * v2.y + sa * v2.x;

        v2.x = x;
        v2.y = y;

        return v2;
    }

    public static Vector2 xy(this Vector3 v3)
    {
        return new Vector2(v3.x, v3.y);
    }

}
