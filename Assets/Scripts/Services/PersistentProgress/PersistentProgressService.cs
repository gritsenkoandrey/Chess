using OnlineChess.BoardProgressData;

namespace OnlineChess.Services.PersistentProgress
{
    public sealed class PersistentProgressService : IPersistentProgressService
    {
        public BoardProgress BoardProgress { get; set; }
    }
}