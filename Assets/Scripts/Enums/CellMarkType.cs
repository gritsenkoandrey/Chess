namespace OnlineChess.Enums
{
    [System.Serializable]
    public enum CellMarkType : byte
    {
        None     = byte.MaxValue,
        Original = 0,
        From     = 1,
        To       = 2,
    }
}