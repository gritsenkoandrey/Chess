using OnlineChess.Scripts.BoardProgressData;

namespace OnlineChess.Scripts.Services.PersistentProgress
{
    public sealed class PersistentProgressService : IPersistentProgressService
    {
        public BoardProgress BoardProgress { get; set; }
    }
}