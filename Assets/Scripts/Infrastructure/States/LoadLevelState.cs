using OnlineChess.Scripts.Services.Factories;
using OnlineChess.Scripts.Services.PersistentProgress;
using UnityEngine;

namespace OnlineChess.Scripts.Infrastructure.States
{
    public sealed class LoadLevelState : ILoadState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;

        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;

        public LoadLevelState(
            GameStateMachine stateMachine, 
            SceneLoader sceneLoader, 
            LoadingCurtain curtain, 
            IGameFactory gameFactory, 
            IPersistentProgressService progressService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
        }

        public void Enter(string sceneName)
        {
            Debug.Log("Enter LoadLevelState");

            _curtain.Show();
            
            _gameFactory.Cleanup();
            
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            Debug.Log("Exit LoadLevelState");
            
            _curtain.Hide();
        }

        private void OnLoaded()
        {
            _gameFactory.CreateGameBoard();
            _gameFactory.CreateGameCamera();
            _gameFactory.CreateUIMediator();

            ReadProgress();
            
            _stateMachine.Enter<GameLoopState>();
        }

        private void ReadProgress()
        {
            foreach (IProgressReader progressReader in _gameFactory.ProgressReaders)
            {
                progressReader.Read(_progressService.BoardProgress);
            }
        }
    }
}