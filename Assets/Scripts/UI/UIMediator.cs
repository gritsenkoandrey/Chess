using ChessRules;
using OnlineChess.Scripts.Behaviours;
using OnlineChess.Scripts.BoardProgressData;
using OnlineChess.Scripts.Cameras;
using OnlineChess.Scripts.Extensions;
using OnlineChess.Scripts.GameBoards;
using OnlineChess.Scripts.Infrastructure.Services;
using OnlineChess.Scripts.Services.Factories;
using OnlineChess.Scripts.Services.PersistentProgress;
using OnlineChess.Scripts.Services.SaveLoad;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OnlineChess.Scripts.UI
{
    public sealed class UIMediator : BaseObject, IProgressReader, IProgressWriter
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _restartButton;

        [SerializeField] private Image _crown;
        
        [SerializeField] private TextMeshProUGUI _textUp;
        [SerializeField] private TextMeshProUGUI _textDown;

        private IGameBoard _gameBoard;
        private IGameCamera _gameCamera;
        private ISaveLoadService _saveLoadService;
        
        private int _turn;

        private const float Speed = 0.75f;

        private void Awake()
        {
            _gameBoard = AllServices.Container.Single<IGameFactory>().GameBoard;
            _gameCamera = AllServices.Container.Single<IGameFactory>().GameCamera;
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
        }

        private void OnEnable()
        {
            _startButton.onClick.AddListener(StartGame);
            _restartButton.onClick.AddListener(RestartGame);
            
            _gameBoard.UpdateChess += UpdateChess;
            _gameBoard.ChangeTurn += ChangeTurn;
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(StartGame);
            _restartButton.onClick.RemoveListener(RestartGame);
            
            _gameBoard.UpdateChess -= UpdateChess;
            _gameBoard.ChangeTurn -= ChangeTurn;
        }

        private void StartGame()
        {
            _gameCamera.StartGame();

            _gameBoard.StartGame();

            StartCoroutine(_crown.ImageFadeAndScale(Speed));

            StartCoroutine(_startButton.ButtonScaleZero(Speed));
        }

        private void RestartGame()
        {
            _gameCamera.StartGame();
            
            _gameBoard.RestartGame();

            StartCoroutine(_restartButton.ButtonScaleZero(Speed));
        }

        private void EndGame()
        {
            _gameCamera.EndGame();
                
            StartCoroutine(_restartButton.ButtonScaleOne(Speed));
        }

        private void UpdateChess(Chess chess)
        {
            string[] move = chess.Fen.Split();
            
            if (chess.IsCheck && !chess.IsCheckmate)
            {
                _textDown.text = "Check";
            }
            else if (chess.IsCheck && chess.IsCheckmate)
            {
                _textDown.text = "Checkmate";
                
                _textUp.text = move[1] == "b" ? "White Win" : "Black Win";
                
                EndGame();

                _turn = 1;
            }
            else if (chess.IsStalemate)
            {
                _textDown.text = "Stalemate";

                _textUp.text = "Draw";
                
                EndGame();

                _turn = 1;
            }
            else
            {
                _textUp.text = move[1] == "b" ? "Black Turn" : "White Turn";
                
                _textDown.text = $"Move Number {_turn}";
            }

            if (move[1] == "b" && !chess.IsStalemate && !(chess.IsCheck && chess.IsCheckmate))
            {
                _gameBoard.OpponentMove();
            }
            
            _saveLoadService.SaveProgress();
        }

        private void ChangeTurn() => _turn++;

        public void Read(BoardProgress progress) => _turn = progress.Turn;
        public void Write(BoardProgress progress) => progress.Turn = _turn;
    }
}