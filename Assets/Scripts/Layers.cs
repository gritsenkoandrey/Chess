using UnityEngine;

public sealed class Layers
{
    public static int Figure => LayerMask.GetMask(nameof(Figure));
    public static int Board => LayerMask.GetMask(nameof(Board));
}