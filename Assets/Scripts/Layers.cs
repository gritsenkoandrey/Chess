using UnityEngine;

public sealed class Layers
{
    private static readonly string FIGURE = "Figure";
    private static readonly string BOARD = "Board";
    private static readonly string TRANSFORMATIONS = "Transformations";
    private static readonly string DEAFAULT = "Default";
    
    public static int Figure => LayerMask.NameToLayer(FIGURE);
    public static int Default => LayerMask.NameToLayer(DEAFAULT);
    public static int Board => LayerMask.NameToLayer(BOARD);
    public static int Transformations => LayerMask.NameToLayer(TRANSFORMATIONS);
}