namespace Infrastructure
{
    public interface ILoadState<in TLoad> : IExitState
    {
        public void Enter(TLoad load);
    }
}