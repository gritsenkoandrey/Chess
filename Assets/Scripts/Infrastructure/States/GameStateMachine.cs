﻿using System;
using System.Collections.Generic;
using OnlineChess.Scripts.Infrastructure.Services;
using OnlineChess.Scripts.Services.Factories;
using OnlineChess.Scripts.Services.PersistentProgress;
using OnlineChess.Scripts.Services.SaveLoad;

namespace OnlineChess.Scripts.Infrastructure.States
{
    public sealed class GameStateMachine
    {
        private readonly Dictionary<Type, IExitState> _states;
        
        private IExitState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain curtain, AllServices services)
        {
            _states = new Dictionary<Type, IExitState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
                [typeof(LoadProgressState)] = new LoadProgressState(this, services.Single<IPersistentProgressService>(), services.Single<ISaveLoadService>()),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, curtain,services.Single<IGameFactory>(), services.Single<IPersistentProgressService>()),
                [typeof(GameLoopState)] = new GameLoopState(this),
            };
        }
        
        public void Enter<TState>() where TState : class, IState
        {
            TState state = ChangeState<TState>();

            state.Enter();
        }

        public void Enter<TState, TLoad>(TLoad load) where TState : class, ILoadState<TLoad>
        {
            TState state = ChangeState<TState>();
            
            state.Enter(load);
        }

        private TState ChangeState<TState>() where TState : class, IExitState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();

            _activeState = state;
            
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}