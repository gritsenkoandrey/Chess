using OnlineChess.Scripts.Infrastructure.Services;

namespace OnlineChess.Scripts.Data
{
    public interface IAssetData : IService
    {
        public GameAssetData GameAssetData { get; }
        public UIAssetData UIAssetData { get; }
    }
}