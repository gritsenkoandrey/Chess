﻿namespace OnlineChess.Infrastructure.States
{
    public interface IState : IExitState
    {
        public void Enter();
    }
}