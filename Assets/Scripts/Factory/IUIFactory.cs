using Behaviours;

namespace Factory
{
    public interface IUIFactory
    {
        public UIMediator CreateUIMediator();
    }
}