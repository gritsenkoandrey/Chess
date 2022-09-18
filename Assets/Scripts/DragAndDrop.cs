using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class DragAndDrop
{
    private State _state;
    private Transform _item;
    private Vector3 _position;
    private Vector3 _fromPosition;
    private Vector3 _toPosition;
    
    private readonly Camera _camera;
    private readonly Dictionary<State, Action> _actions;
    
    public delegate void DropFigure (Vector3 from, Vector3 to);

    private readonly DropFigure _dFigure;

    public DragAndDrop(DropFigure dropFigure)
    {
        _state = State.None;
        
        _item = null;

        _dFigure = dropFigure;
        
        _actions = new Dictionary<State, Action>
        {
            { State.None, ActionNone },
            { State.Drag, ActionDrag },
        };

        _camera = Camera.main;
    }

    public void Action() => _actions[_state].Invoke();

    private void ActionDrag()
    {
        Drag();

        if (IsMouseButtonUp())
        {
            Drop();
        }
    }

    private void ActionNone()
    {
        if (IsMouseButtonDown())
        {
            PickUp();
        }
    }

    private bool IsMouseButtonDown() => Input.GetMouseButtonDown(0);
    private bool IsMouseButtonUp() => Input.GetMouseButtonUp(0);
    private Ray GetRay() => _camera.ScreenPointToRay(Input.mousePosition);

    private void PickUp()
    {
        Transform item = GetFigure();

        if (item)
        {
            _state = State.Drag;

            _item = item;

            _fromPosition = item.position;
        }
    }

    private Transform GetFigure()
    {
        if (Physics.Raycast(GetRay(), out RaycastHit hit, 100f, Layers.Figure))
        {
            return hit.collider.transform;
        }

        return null;
    }

    private Vector3 GetBoardPosition()
    {
        if (Physics.Raycast(GetRay(), out RaycastHit hit, 100f, Layers.Board))
        {
            _position = hit.point;
        }

        return _position;
    }

    private void Drag() => _item.MoveFigure(GetBoardPosition());
    private void Drop()
    {
        _toPosition = _item.position;

        _dFigure(_fromPosition, _toPosition);
        
        _state = State.None;

        _item = null;
    }
}