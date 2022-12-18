using OnlineChess.Scripts.Factory;
using OnlineChess.Scripts.Services.PersistentProgress;
using OnlineChess.Scripts.Services.SaveLoad;
using UnityEngine;

namespace OnlineChess.Scripts.Infrastructure.States
{
    public sealed class LoadLevelState : ILoadState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;

        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadLevelState(
            GameStateMachine stateMachine, 
            SceneLoader sceneLoader, 
            LoadingCurtain curtain, 
            IGameFactory gameFactory, 
            IUIFactory uiFactory,
            IPersistentProgressService progressService,
            ISaveLoadService saveLoadService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
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
            
            _uiFactory.CreateUIMediator();

            foreach (IProgressReader progressReader in _gameFactory.ProgressReaders)
            {
                progressReader.Read(_progressService.BoardProgress);
            }
            
            _stateMachine.Enter<GameLoopState>();
        }
    }
}