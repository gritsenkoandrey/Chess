using UnityEngine;

namespace Extensions
{
    public static class Extension
    {
        public static void MoveFigure(this Transform current, Vector3 next)
        {
            current.position = new Vector3(Mathf.RoundToInt(next.x), 0.15f, Mathf.RoundToInt(next.z));
        }

        public static string VectorToCell(this Vector3 vector)
        {
            int x = (int)vector.x;
            int y = (int)vector.z;
        
            return (char)('a' + x) + (y + 1).ToString();
        }
        
        public static Vector3 AddY(this Vector3 vector, float add)
        {
            return new Vector3(vector.x, vector.y + add, vector.z);
        }
    }
}