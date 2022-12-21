using OnlineChess.BoardProgressData;
using OnlineChess.Infrastructure.Services;

namespace OnlineChess.Services.PersistentProgress
{
    public interface IPersistentProgressService : IService
    {
        public BoardProgress BoardProgress { get; set; }
    }
}