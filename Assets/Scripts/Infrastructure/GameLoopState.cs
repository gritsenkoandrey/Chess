using UnityEngine;

namespace Infrastructure
{
    public sealed class GameLoopState : IState
    {
        public GameLoopState(GameStateMachine gameStateMachine)
        {
            
        }

        public void Enter()
        {
            Debug.Log("Enter GameLoopState");
        }

        public void Exit()
        {
            Debug.Log("Exit GameLoopState");
        }
    }
}