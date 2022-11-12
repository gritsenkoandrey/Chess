using System.Collections;
using ChessRules;
using GameBoards;
using Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Behaviours
{
    public sealed class UIMediator : BaseObject
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _restartButton;
        
        [SerializeField] private TextMeshProUGUI _textUp;
        [SerializeField] private TextMeshProUGUI _textDown;

        private IGameBoard _gameBoard;
        private ICamera _camera;
        
        private int _turn;
        
        private bool _isBlackTurn;

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
            StartCoroutine(StartButtonScaleZero());
        }

        private void RestartGame()
        {
            StartCoroutine(RestartButtonScaleZero());
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
                
                StartCoroutine(RestartButtonScaleOne());

                _turn = 0;
            }
            else if (chess.IsStalemate)
            {
                _textDown.text = "Stalemate";
            }
            else
            {
                string[] move = chess.Fen.Split();
                
                bool isBlackTurn = move[1] != "b";
                
                _textUp.text = isBlackTurn ? "Black Turn" : "White Turn";

                if (_isBlackTurn != isBlackTurn)
                {
                    _isBlackTurn = isBlackTurn;
                    
                    _turn++;
                }
                
                _textDown.text = $"Move Number {_turn}";
            }
        }

        private IEnumerator StartButtonScaleZero()
        {
            _camera.StartGame();
            
            _gameBoard.StartGame();

            _startButton.transform.localScale = Vector3.one;
            
            _startButton.interactable = false;

            float scale = 1f;
            float time = 0.01f;
            float speed = 2f;
            
            while (scale > 0f)
            {
                yield return new WaitForSeconds(time);

                scale -= Time.deltaTime * speed;
                
                _startButton.transform.localScale = Vector3.one * Ease(scale);
            }
            
            _startButton.interactable = true;
            
            _startButton.gameObject.SetActive(false);
        }
        private IEnumerator RestartButtonScaleZero()
        {
            _camera.StartGame();
            
            _gameBoard.RestartGame();

            _startButton.transform.localScale = Vector3.one;

            _startButton.interactable = false;

            float scale = 1f;
            float time = 0.01f;
            float speed = 2f;

            while (scale > 0f)
            {
                yield return new WaitForSeconds(time);

                scale -= Time.deltaTime * speed;
                
                _restartButton.transform.localScale = Vector3.one * Ease(scale);
            }

            _startButton.interactable = true;

            _restartButton.gameObject.SetActive(false);
        }
        private IEnumerator RestartButtonScaleOne()
        {
            _camera.EndGame();
                
            _restartButton.gameObject.SetActive(true);
            
            _restartButton.transform.localScale = Vector3.zero;

            _restartButton.interactable = false;
            
            float scale = 0f;
            float time = 0.01f;
            float speed = 2f;

            while (scale < 1f)
            {
                yield return new WaitForSeconds(time);

                scale += Time.deltaTime * speed;
                
                _restartButton.transform.localScale = Vector3.one * Ease(scale);
            }
            
            _restartButton.interactable = true;
        }

        private static float Ease(float number)
        {
            float c1 = 0.75f;
            float c3 = c1 + 1f;

            return 1 + c3 * Mathf.Pow(number - 1, 3) + c1 * Mathf.Pow(number - 1, 2);
        }
    }
}