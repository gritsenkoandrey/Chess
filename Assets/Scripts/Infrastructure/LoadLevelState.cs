using Behaviours;
using Data;
using Factory;
using GameBoardBase;
using UnityEngine;

namespace Infrastructure
{
    public sealed class LoadLevelState : ILoadState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;

        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain, IAssetData assetData)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;

            _gameFactory = new GameFactory(assetData);
            _uiFactory = new UIFactory(assetData);
        }

        public void Enter(string sceneName)
        {
            Debug.Log("Enter LoadLevelState");

            _curtain.Show();
            
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            Debug.Log("Exit LoadLevelState");
            
            _curtain.Hide();
        }

        private void OnLoaded()
        {
            GameBoard gameBoard = _gameFactory.CreateGameBoard();
            
            gameBoard.Construct(_gameFactory);
            
            GameCamera gameCamera = _gameFactory.CreateGameCamera();
            
            _uiFactory.CreateUIMediator().Construct(gameBoard, gameCamera);
            
            _stateMachine.Enter<GameLoopState>();
        }
    }
}