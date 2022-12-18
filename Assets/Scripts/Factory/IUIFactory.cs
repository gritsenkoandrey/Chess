using OnlineChess.Scripts.Behaviours;
using OnlineChess.Scripts.Infrastructure.Services;

namespace OnlineChess.Scripts.Factory
{
    public interface IUIFactory : IService
    {
        public UIMediator CreateUIMediator();
    }
}