using OnlineChess.Scripts.Infrastructure.Services;
using OnlineChess.Scripts.Infrastructure.States;

namespace OnlineChess.Scripts.Infrastructure
{
    public sealed class Game
    {
        public readonly GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain, AllServices.Container);
        }
    }
}