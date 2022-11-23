using UnityEngine;

namespace Infrastructure
{
    public sealed class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;

        private readonly SceneLoader _sceneLoader;
        
        private const string Initial = "Initial";
        private const string Main = "Main";

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
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
            _stateMachine.Enter<LoadLevelState, string>(Main);
        }
    }
}