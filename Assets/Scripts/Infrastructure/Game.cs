using Data;

namespace Infrastructure
{
    public sealed class Game
    {
        public readonly GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain, IAssetData assetData)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain, assetData);
        }
    }
}