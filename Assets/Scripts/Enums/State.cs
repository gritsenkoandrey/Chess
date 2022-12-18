namespace OnlineChess.Scripts.Enums
{
    [System.Serializable]
    public enum State : byte
    {
        Player    = byte.MinValue,
        Drag      = 1,
        Drop      = 2,
        Promotion = 3,
        Opponent  = 4,
    }
}