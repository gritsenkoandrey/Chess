using OnlineChess.Scripts.BoardProgressData;
using OnlineChess.Scripts.Infrastructure.Services;

namespace OnlineChess.Scripts.Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        public void SaveProgress();
        public BoardProgress LoadProgress();
    }
}