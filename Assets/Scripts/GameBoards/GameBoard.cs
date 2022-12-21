using System;
using System.Collections.Generic;
using ChessRules;
using OnlineChess.Scripts.Behaviours;
using OnlineChess.Scripts.BoardProgressData;
using OnlineChess.Scripts.Cells;
using OnlineChess.Scripts.DragDrop;
using OnlineChess.Scripts.Enums;
using OnlineChess.Scripts.Figures;
using OnlineChess.Scripts.Infrastructure.Services;
using OnlineChess.Scripts.Services.Factories;
using OnlineChess.Scripts.Services.PersistentProgress;
using UnityEngine;

namespace OnlineChess.Scripts.GameBoards
{
    public partial class GameBoard : BaseObject, IGameBoard, IProgressWriter, IProgressReader
    {
        [SerializeField] private Transform _cellsRoot;
        [SerializeField] private Transform _figuresRoot;

        private readonly Dictionary<string, ICell> _cells = new (64);
        private readonly Dictionary<string, IFigure> _figures = new (64);
        private readonly Dictionary<FigureType, IFigure> _promotions = new(8);

        public Action<Chess> UpdateChess { get; set; }
        public Action ChangeTurn { get; set; }

        private Chess _chess;
        
        private IGameFactory _gameFactory;
        private IDragAndDrop _dragAndDrop;

        private string _onTransformationMove = "";
        private string _fen;

        private void Awake()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();
            
            _dragAndDrop = new DragAndDrop(DropFigure, PickFigure, PromotionFigure);
        }

        private void Update()
        {
            _dragAndDrop.Action();
        }

        public void StartGame()
        {
            _chess = new Chess(_fen);
            
            UpdateChess.Invoke(_chess);

            InitGameBoard();
            UpdateFigures();
            MarkCellsFrom();
            InstantiatePromotions();
        }

        public void RestartGame()
        {
            _chess = new Chess();
            
            UpdateChess.Invoke(_chess);
            
            UpdateFigures();
            MarkCellsFrom();
        }
        
        public void OpponentMove()
        {
            StartCoroutine(OpponentMoveCoroutine());
        }
        
        public void Write(BoardProgress progress) => progress.Fen = _chess.Fen;
        public void Read(BoardProgress progress) => _fen = progress.Fen;
    }
}