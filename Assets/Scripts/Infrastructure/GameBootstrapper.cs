﻿using UnityEngine;

namespace Infrastructure
{
    public sealed class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingCurtain _curtain;
        
        private Game _game;
        
        private void Awake()
        {
            _game = new Game(this, _curtain);
            
            _game.StateMachine.Enter<BootstrapState>();
            
            Application.targetFrameRate = 60;

            DontDestroyOnLoad(this);
        }
    }
}