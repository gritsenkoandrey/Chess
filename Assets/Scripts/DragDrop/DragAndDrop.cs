using System;
using System.Collections.Generic;
using OnlineChess.Scripts.Enums;
using OnlineChess.Scripts.Extensions;
using OnlineChess.Scripts.Utils;
using UnityEngine;

namespace OnlineChess.Scripts.DragDrop
{
    public sealed class DragAndDrop : IDragAndDrop
    {
        private State _state;
    
        private Transform _item;
    
        private Vector3 _position;
        private Vector3 _fromPosition;
        private Vector3 _toPosition;
    
        private readonly Camera _camera;
    
        private readonly Dictionary<State, Action> _actions;
    
        public delegate void DropFigure (Vector3 from, Vector3 to);
        public delegate void PickFigure (Vector3 from);
        public delegate void PromotionFigure(Vector3 from);
    
        private readonly DropFigure _dropFigure;
        private readonly PickFigure _pickFigure;
        private readonly PromotionFigure _promotionFigure;
    
        public DragAndDrop(DropFigure dropFigure, PickFigure pickFigure, PromotionFigure promotionFigure)
        {
            _state = State.Player;
        
            _item = null;
    
            _dropFigure = dropFigure;
            _pickFigure = pickFigure;
            _promotionFigure = promotionFigure;
        
            _actions = new Dictionary<State, Action>
            {
                { State.Player, ActionNone },
                { State.Drag, ActionDrag },
                { State.Promotion, ActionPromotion },
                { State.Drop, ActionDrop },
                { State.Opponent, ActionOpponent },
            };
    
            _camera = Camera.main;
        }

        public void Action() => _actions[_state].Invoke();
        public void ChangeState(State state) => _state = state;

        private void ActionNone()
        {
            if (IsMouseButtonDown())
            {
                Transform item = GetFigure();

                if (item)
                {
                    _state = State.Drag;

                    _item = item;

                    _fromPosition = item.position;

                    _pickFigure(_fromPosition);
                }
            }
        }

        private void ActionDrag()
        {
            _item.MoveFigure(GetBoardPosition());

            if (IsMouseButtonUp())
            {
                _state = State.Drop;
            }
        }

        private void ActionDrop()
        {
            _state = State.Player;
        
            _toPosition = _item.position;

            _dropFigure(_fromPosition, _toPosition);

            _item = null;
        }

        private void ActionPromotion()
        {
            if (IsMouseButtonDown())
            {
                Transform item = GetPromotionFigure();

                if (item)
                {
                    _item = item;

                    _fromPosition = item.position;

                    _promotionFigure(_fromPosition);

                    _state = State.Player;
                }
            }
        }

        private void ActionOpponent()
        {
            return;
        }

        private bool IsMouseButtonDown() => Input.GetMouseButtonDown(0);
        private bool IsMouseButtonUp() => Input.GetMouseButtonUp(0);
    
        private Ray GetRay() => _camera.ScreenPointToRay(Input.mousePosition);

        private Transform GetFigure()
        {
            if (Physics.Raycast(GetRay(), out RaycastHit hit, 100f, 1 << Layers.Figure))
            {
                return hit.collider.transform;
            }

            return null;
        }
        private Transform GetPromotionFigure()
        {
            if (Physics.Raycast(GetRay(), out RaycastHit hit, 100f, 1 << Layers.Promotions))
            {
                return hit.collider.transform;
            }

            return null;
        }
        private Vector3 GetBoardPosition()
        {
            if (Physics.Raycast(GetRay(), out RaycastHit hit, 100f, 1 << Layers.Board))
            {
                _position = hit.point;
            }

            return _position;
        }
    }
}