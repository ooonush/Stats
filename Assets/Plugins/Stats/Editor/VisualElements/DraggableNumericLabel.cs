/*using UnityEngine;
using UnityEngine.UIElements;

public class DraggableNumericLabel<T> : Label 
{
    private readonly TextValueField<T> _initialValue;
    
    private bool _isDragging;
    private Vector2 _mouseStartPosition;

    public DraggableNumericLabel (string text, TextValueField<T> initialValue) : base(text)
    {
        _initialValue = initialValue;

        RegisterCallback<MouseDownEvent>(OnMouseDown);
        RegisterCallback<MouseMoveEvent>(OnMouseMove);
        RegisterCallback<MouseUpEvent>(OnMouseUp);
    }

    private void OnMouseDown(MouseDownEvent evt)
    {
        if (evt.button == (int)MouseButton.LeftMouse)
        {
            _isDragging = true;
            _mouseStartPosition = evt.mousePosition;
            evt.StopPropagation();
        }
    }

    private void OnMouseMove(MouseMoveEvent evt)
    {
        if (_isDragging)
        {
            float delta = evt.mousePosition.x - _mouseStartPosition.x;
            _initialValue.value = _initialValue.value + delta * 0.01f;
            evt.StopPropagation();
        }
    }

    private void OnMouseUp(MouseUpEvent evt)
    {
        if (evt.button == (int)MouseButton.LeftMouse && _isDragging)
        {
            _isDragging = false;
            evt.StopPropagation();
        }
    }
}*/