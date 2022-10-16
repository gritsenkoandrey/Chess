namespace Enums
{
    [System.Serializable]
    public enum State : byte
    {
        None = byte.MinValue,
        Drag = 1,
        Drop = 2,
        Promotion = 3,
    }
}