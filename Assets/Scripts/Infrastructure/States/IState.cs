namespace OnlineChess.Scripts.Infrastructure.States
{
    public interface IState : IExitState
    {
        public void Enter();
    }
}