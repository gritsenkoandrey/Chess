using UnityEngine;

namespace Utils
{
    public static class BezierCurves
    {
        public static Vector3 Quadratic(Vector3 from, Vector3 center, Vector3 to, float t)
        {
            return (1 - t) * (1 - t) * from + 2 * (1 - t) * t * center + t * t * to;
        }
    }
}