using System.Collections;
using ChessRules;
using GameBoards;
using Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Behaviours
{
    using Color = UnityEngine.Color;

    public sealed class UIMediator : BaseObject
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _restartButton;

        [SerializeField] private Image _crown;
        
        [SerializeField] private TextMeshProUGUI _textUp;
        [SerializeField] private TextMeshProUGUI _textDown;

        private IGameBoard _gameBoard;
        private ICamera _camera;
        
        private int _turn = 1;
        
        private bool _isBlackTurn;

        private const float Speed = 1f;

        private void Awake()
        {
            Application.targetFrameRate = 60;

            _gameBoard = FindObjectOfType<GameBoard>();
            _camera = FindObjectOfType<CameraChanger>();
        }

        private void OnEnable()
        {
            _startButton.onClick.AddListener(StartGame);
            _restartButton.onClick.AddListener(RestartGame);
            
            _gameBoard.UpdateChess += UpdateChess;
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(StartGame);
            _restartButton.onClick.RemoveListener(RestartGame);
            
            _gameBoard.UpdateChess -= UpdateChess;
        }

        private void StartGame()
        {
            _camera.StartGame();

            _gameBoard.StartGame();

            StartCoroutine(ImageFadeAndScale(_crown));

            StartCoroutine(ButtonScaleZero(_startButton));
        }

        private void RestartGame()
        {
            _camera.StartGame();
            
            _gameBoard.RestartGame();

            StartCoroutine(ButtonScaleZero(_restartButton));
        }

        private void EndGame()
        {
            _camera.EndGame();
                
            StartCoroutine(ButtonScaleOne(_restartButton));
        }

        private void UpdateChess(Chess chess)
        {
            if (chess.IsCheck && !chess.IsCheckmate)
            {
                _textDown.text = "Check";
                _turn++;
            }
            else if (chess.IsCheck && chess.IsCheckmate)
            {
                _textDown.text = "Checkmate";
                
                string[] move = chess.Fen.Split();

                _textUp.text = move[1] == "b" ? "White Win" : "Black Win";
                
                EndGame();

                _turn = 1;
            }
            else if (chess.IsStalemate)
            {
                _textDown.text = "Stalemate";
            }
            else
            {
                string[] move = chess.Fen.Split();
                
                bool isBlackTurn = move[1] == "b";
                
                _textUp.text = isBlackTurn ? "Black Turn" : "White Turn";
                
                _textDown.text = $"Move Number {_turn}";

                if (_isBlackTurn != isBlackTurn)
                {
                    _isBlackTurn = isBlackTurn;
                    
                    _turn++;
                }
            }
        }

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
    }
}