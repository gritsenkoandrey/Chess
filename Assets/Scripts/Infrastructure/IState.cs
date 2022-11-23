namespace Infrastructure
{
    public interface IState : IExitState
    {
        public void Enter();
    }
}