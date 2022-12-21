using Cinemachine;
using OnlineChess.Behaviours;
using UnityEngine;

namespace OnlineChess.Cameras
{
    public sealed class GameCamera : BaseObject, IGameCamera
    {
        [SerializeField] private CinemachineVirtualCamera _cameraBefore;
        [SerializeField] private CinemachineVirtualCamera _cameraAfter;

        public void StartGame() => _cameraBefore.Priority = 0;
        public void EndGame() => _cameraBefore.Priority = 100;
    }
}