using System.Collections;
using ChessRules;
using OnlineChess.Scripts.Behaviours;
using OnlineChess.Scripts.BoardProgressData;
using OnlineChess.Scripts.Cameras;
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
    using Color = UnityEngine.Color;

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
        
        private int _turn = 1;

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

            StartCoroutine(ImageFadeAndScale(_crown));

            StartCoroutine(ButtonScaleZero(_startButton));
        }

        private void RestartGame()
        {
            _gameCamera.StartGame();
            
            _gameBoard.RestartGame();

            StartCoroutine(ButtonScaleZero(_restartButton));
        }

        private void EndGame()
        {
            _gameCamera.EndGame();
                
            StartCoroutine(ButtonScaleOne(_restartButton));
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

        private static IEnumerator ButtonScaleZero(Button button)
        {
            button.transform.localScale = Vector3.one;
            
            button.interactable = false;

            float scale = 1f;
            
            while (scale > 0f)
            {
                yield return null;

                scale -= Time.deltaTime * Speed;

                button.transform.localScale = Vector3.one * EaseOutBack(scale);
            }
            
            button.interactable = true;
            
            button.gameObject.SetActive(false);
        }
        private static IEnumerator ButtonScaleOne(Button button)
        {
            button.gameObject.SetActive(true);

            button.transform.localScale = Vector3.zero;

            button.interactable = false;
            
            float scale = 0f;

            while (scale < 1f)
            {
                yield return null;

                scale += Time.deltaTime * Speed;

                button.transform.localScale = Vector3.one * EaseOutBack(scale);
            }
            
            button.interactable = true;
        }
        private static IEnumerator ImageFadeAndScale(Image image)
        {
            Color c = image.color;
            float scale = 1f;
            float fade = 1f;

            while (fade > 0f)
            {
                yield return null;
                
                scale += Time.deltaTime * Speed;
                fade -= Time.deltaTime * Speed;

                image.transform.localScale = Vector3.one * scale;

                Color color = new Color(c.r, c.g, c.b, fade);

                image.color = color;
            }
            
            image.gameObject.SetActive(false);
        }
        
        private static float EaseOutBack(float number)
        {
            float c1 = 0.70158f;
            float c3 = c1 + 1f;

            return 1f + c3 * Mathf.Pow(number - 1f, 3) + c1 * Mathf.Pow(number - 1f, 2);
        }

        public void Read(BoardProgress progress) => _turn = progress.Turn;
        public void Write(BoardProgress progress) => progress.Turn = _turn;
    }
}