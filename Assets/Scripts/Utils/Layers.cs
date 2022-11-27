using UnityEngine;

namespace Utils
{
    public static class Layers
    {
        private const string DEAFAULT = "Default";
        private const string FIGURE = "Figure";
        private const string BOARD = "Board";
        private const string PROMOTIONS = "Promotions";

        public static int Default => LayerMask.NameToLayer(DEAFAULT);
        public static int Figure => LayerMask.NameToLayer(FIGURE);
        public static int Board => LayerMask.NameToLayer(BOARD);
        public static int Promotions => LayerMask.NameToLayer(PROMOTIONS);
    }
}