using OnlineChess.Infrastructure.Services;

namespace OnlineChess.Services.Data
{
    public interface IAssetData : IService
    {
        public GameAssetData GameAssetData { get; }
        public UIAssetData UIAssetData { get; }
    }
}