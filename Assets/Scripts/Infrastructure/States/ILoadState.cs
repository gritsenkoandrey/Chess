﻿namespace OnlineChess.Infrastructure.States
{
    public interface ILoadState<in TLoad> : IExitState
    {
        public void Enter(TLoad load);
    }
}