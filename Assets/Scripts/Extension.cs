using UnityEngine;

public static class Extension
{
    public static void MoveFigure(this Transform current, Vector3 next)
    {
        current.position = new Vector3(Mathf.RoundToInt(next.x), current.position.y, Mathf.RoundToInt(next.z));
    }

    public static Vector3Int ConvertToVector3Int(this Vector3 vector)
    {
        return new Vector3Int((int)vector.x, (int)vector.y, (int)vector.z);
    }
}