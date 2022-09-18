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
    public delegate void PickFigure (Vector3 from);

    private readonly DropFigure _dropFigure;
    private readonly PickFigure _pickFigure;

    public DragAndDrop(DropFigure dropFigure, PickFigure pickFigure)
    {
        _state = State.None;
        
        _item = null;

        _dropFigure = dropFigure;
        _pickFigure = pickFigure;
        
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

            _pickFigure(_fromPosition);
        }
    }

    private Transform GetFigure()
    {
        if (Physics.Raycast(GetRay(), out RaycastHit hit, 100f, 1 << Layers.Figure))
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

    private void Drag() => _item.MoveFigure(GetBoardPosition());
    private void Drop()
    {
        _toPosition = _item.position;

        _dropFigure(_fromPosition, _toPosition);
        
        _state = State.None;

        _item = null;
    }
}