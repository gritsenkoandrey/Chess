﻿using Cinemachine;
using OnlineChess.Scripts.Interfaces;
using UnityEngine;

namespace OnlineChess.Scripts.Behaviours
{
    public sealed class GameCamera : BaseObject, IGameCamera
    {
        [SerializeField] private CinemachineVirtualCamera _cameraBefore;
        [SerializeField] private CinemachineVirtualCamera _cameraAfter;

        public void StartGame() => _cameraBefore.Priority = 0;
        public void EndGame() => _cameraBefore.Priority = 100;
    }
}