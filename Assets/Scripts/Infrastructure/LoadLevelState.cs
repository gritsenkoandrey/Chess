using Interfaces;
using UnityEngine;
using Utils;

namespace Infrastructure
{
    public sealed class LoadLevelState : ILoadState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
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
            IData data = CustomResources.LoadData<IData>();

            IGameBoard gameBoard = data.SpawnItem.GetGameBoard();
            IGameCamera gameCamera = data.SpawnItem.GetGameCamera();
            
            data.SpawnItem.GetUIMediator().Construct(gameBoard, gameCamera);
            
            _stateMachine.Enter<GameLoopState>();
        }
    }
}