using Data;
using UnityEngine;
using Utils;

namespace Infrastructure
{
    public sealed class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingCurtain _curtain;
        
        private Game _game;
        
        private void Awake()
        {
            IAssetData assetData = CustomResources.Load<IAssetData>();

            _game = new Game(this, _curtain, assetData);
            
            _game.StateMachine.Enter<BootstrapState>();
            
            Application.targetFrameRate = 60;

            DontDestroyOnLoad(this);
        }
    }
}