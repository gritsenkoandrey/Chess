using OnlineChess.Infrastructure.Services;
using OnlineChess.Services.Data;
using OnlineChess.Services.Factories;
using OnlineChess.Services.PersistentProgress;
using OnlineChess.Services.SaveLoad;
using OnlineChess.Utils;
using UnityEngine;

namespace OnlineChess.Infrastructure.States
{
    public sealed class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;
        
        private const string Initial = "Initial";

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            
            RegisterServices();
        }
        
        public void Enter()
        {
            Debug.Log("Enter BootstrapState");

            _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
        }

        public void Exit()
        {
            Debug.Log("Exit BootstrapState");
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadProgressState>();
        }

        private void RegisterServices()
        {
            _services.RegisterSingle(CustomResources.Load<IAssetData>());
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssetData>()));
            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(_services.Single<IPersistentProgressService>(), _services.Single<IGameFactory>()));
        }
    }
}