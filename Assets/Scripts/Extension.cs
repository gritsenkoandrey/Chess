using UnityEngine;

public static class Extension
{
    public static void MoveFigure(this Transform current, Vector3 next)
    {
        current.position = new Vector3(Mathf.RoundToInt(next.x), current.position.y, Mathf.RoundToInt(next.z));
    }

    public static string VectorToCell(this Vector3 vector)
    {
        int x = (int)vector.x;
        int y = (int)vector.z;
        
        return (char)('a' + x) + (y + 1).ToString();
    }
}