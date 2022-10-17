using System;
using System.Collections.Generic;
using Enums;
using Extensions;
using Interfaces;
using UnityEngine;

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
        _state = State.None;
        
        _item = null;
    
        _dropFigure = dropFigure;
        _pickFigure = pickFigure;
        _promotionFigure = promotionFigure;
        
        _actions = new Dictionary<State, Action>
        {
            { State.None, ActionNone },
            { State.Drag, ActionDrag },
            { State.Promotion, ActionPromotion },
            { State.Drop, ActionDrop },
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
            
                item.localScale = Vector3.one * 1.5f;
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
        _state = State.None;
        
        _toPosition = _item.position;

        _dropFigure(_fromPosition, _toPosition);

        _item.localScale = Vector3.one;

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

                _state = State.None;
            }
        }
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