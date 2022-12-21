using UnityEngine;

namespace OnlineChess.BoardProgressData
{
    public static class BoardProgressExtension
    {
        public static T ToDeserialize<T>(this string json) => JsonUtility.FromJson<T>(json);

        public static string ToSerialize(this object obj) => JsonUtility.ToJson(obj);
    }
}