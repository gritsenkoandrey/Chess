﻿using OnlineChess.Scripts.BoardProgressData;
using OnlineChess.Scripts.Services.Factories;
using OnlineChess.Scripts.Services.PersistentProgress;
using UnityEngine;

namespace OnlineChess.Scripts.Services.SaveLoad
{
    public sealed class SaveLoadService : ISaveLoadService
    {
        private readonly IPersistentProgressService _progressService;
        private readonly IGameFactory _gameFactory;
        
        private const string Key = nameof(BoardProgress);

        public SaveLoadService(IPersistentProgressService progressService, IGameFactory gameFactory)
        {
            _progressService = progressService;
            _gameFactory = gameFactory;
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