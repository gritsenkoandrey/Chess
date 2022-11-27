using System;
using System.Collections;
using System.Collections.Generic;
using Behaviours;
using ChessRules;
using DragDrop;
using Enums;
using Factory;
using Interfaces;
using UnityEngine;

namespace GameBoardBase
{
    public partial class GameBoard : BaseObject, IGameBoard
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

        public void Construct(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
            
            _dragAndDrop = new DragAndDrop(DropFigure, PickFigure, PromotionFigure);
        }

        private void Update()
        {
            _dragAndDrop.Action();
        }

        public void StartGame()
        {
            _chess = new Chess();
            
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
            StartCoroutine((IEnumerator)OpponentMoveCoroutine());
        }
    }
}