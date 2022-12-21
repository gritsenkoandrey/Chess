using OnlineChess.BoardProgressData;
using OnlineChess.Infrastructure.Services;

namespace OnlineChess.Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        public void SaveProgress();
        public BoardProgress LoadProgress();
    }
}