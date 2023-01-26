using ChessRules;
using OnlineChess.Behaviours;
using OnlineChess.BoardProgressData;
using OnlineChess.Cameras;
using OnlineChess.Extensions;
using OnlineChess.GameBoards;
using OnlineChess.Infrastructure.Services;
using OnlineChess.Services.PersistentProgress;
using OnlineChess.Services.SaveLoad;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OnlineChess.UI
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

        public void Construct(IGameBoard gameBoard, IGameCamera gameCamera)
        {
            _gameBoard = gameBoard;
            _gameCamera = gameCamera;
            
            _gameBoard.UpdateChess += UpdateChess;
            _gameBoard.ChangeTurn += ChangeTurn;
            
            _startButton.onClick.AddListener(StartGame);
            _restartButton.onClick.AddListener(RestartGame);
        }

        private void Awake()
        {
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
        }

        private void OnDestroy()
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