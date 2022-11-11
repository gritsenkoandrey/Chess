using ChessRules;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Behaviours
{
    public sealed class UIMediator : MonoBehaviour
    {
        [SerializeField] private CameraChanger _cameraChanger;
        [SerializeField] private GameBoard _gameBoard;
        
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _restartButton;
        
        [SerializeField] private TextMeshProUGUI _textUp;
        [SerializeField] private TextMeshProUGUI _textDown;

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
            _cameraChanger.StartGame();
            _startButton.gameObject.SetActive(false);

            _gameBoard.StartGame();
        }

        private void RestartGame()
        {
            _cameraChanger.StartGame();
            _restartButton.gameObject.SetActive(false);

            _gameBoard.RestartGame();
        }

        private void UpdateChess(Chess chess)
        {
            if (chess.IsCheck && !chess.IsCheckmate)
            {
                _textDown.text = "Check";
            }
            else if (chess.IsCheck && chess.IsCheckmate)
            {
                _textDown.text = "Checkmate";
                
                string[] move = chess.Fen.Split();

                _textUp.text = move[1] == "b" ? "White Win" : "Black Win";
                
                _cameraChanger.EndGame();
                
                _restartButton.gameObject.SetActive(true);
            }
            else if (chess.IsStalemate)
            {
                _textDown.text = "Stalemate";
            }
            else
            {
                string[] move = chess.Fen.Split();
                
                _textUp.text = move[1] == "b" ? "Black Turn" : "White Turn";
                _textDown.text = "";
            }
        }
    }
}