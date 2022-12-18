using OnlineChess.Scripts.BoardProgressData;
using OnlineChess.Scripts.Infrastructure.Services;

namespace OnlineChess.Scripts.Services.PersistentProgress
{
    public interface IPersistentProgressService : IService
    {
        public BoardProgress BoardProgress { get; set; }
    }
}