﻿using OnlineChess.BoardProgressData;
using OnlineChess.Services.PersistentProgress;
using OnlineChess.Services.SaveLoad;
using UnityEngine;

namespace OnlineChess.Infrastructure.States
{
    public sealed class LoadProgressState : IState
    {
        private readonly GameStateMachine _stateMachine;
        
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        private const string Main = "Main";

        public LoadProgressState(
            GameStateMachine stateMachine, 
            IPersistentProgressService progressService, 
            ISaveLoadService saveLoadService)
        {
            _stateMachine = stateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }
        
        public void Enter()
        {
            Debug.Log("Enter LoadProgressState");

            LoadProgress();
            
            _stateMachine.Enter<LoadLevelState, string>(Main);
        }

        public void Exit()
        {
            Debug.Log("Exit LoadProgressState");
        }

        private void LoadProgress()
        {
            _progressService.BoardProgress = _saveLoadService.LoadProgress() ?? new BoardProgress();
        }
    }
}