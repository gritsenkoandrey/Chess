using OnlineChess.Scripts.BoardProgressData;
using OnlineChess.Scripts.Factory;
using OnlineChess.Scripts.Services.PersistentProgress;
using UnityEngine;

namespace OnlineChess.Scripts.Services.SaveLoad
{
    public sealed class SaveLoadService : ISaveLoadService
    {
        private readonly IPersistentProgressService _progressService;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;
        
        private const string Key = nameof(BoardProgress);

        public SaveLoadService(IPersistentProgressService progressService, IGameFactory gameFactory, IUIFactory uiFactory)
        {
            _progressService = progressService;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
        }
        
        public void SaveProgress()
        {            
            foreach (IProgressWriter progressWriter in _gameFactory.ProgressWriters)
            {
                progressWriter.Write(_progressService.BoardProgress);
            }
            
            PlayerPrefs.SetString(Key, _progressService.BoardProgress.ToSerialize());
        }

        public BoardProgress LoadProgress()
        {
            return PlayerPrefs.GetString(Key)?.ToDeserialize<BoardProgress>();
        }
    }
}