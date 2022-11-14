using System;
using System.Collections.Generic;
using ChessRules;
using Enums;
using Interfaces;
using UnityEngine;

namespace GameBoards
{
    public partial class GameBoard : MonoBehaviour, IGameBoard
    {
        private readonly Dictionary<string, ICell> _cells = new (64);
        private readonly Dictionary<string, IFigure> _figures = new (64);
        private readonly Dictionary<FigureType, IFigure> _promotions = new(8);

        private Chess _chess;
        
        public Action<Chess> UpdateChess { get; set; }
        public Action ChangeTurn { get; set; }

        private void Awake()
        {
            Initialize();
            DragAndDrop();
        }

        private void Update()
        {
            Execute();
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
    }
}