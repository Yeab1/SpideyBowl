using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeController : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Vector2 _startDragPosition;
    private bool _isDragging = false;

    // Minimum distance for a swipe to be recognized
    public float minSwipeDistance = 50f;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _startDragPosition = eventData.position;
        _isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_isDragging)
        {
            Vector2 currentDragPosition = eventData.position;
            Vector2 dragDirection = currentDragPosition - _startDragPosition;

            if (dragDirection.magnitude >= minSwipeDistance)
            {
                if (IsSwipeUp(dragDirection))
                {
                    OnSwipeUp();
                    _isDragging = false; // Stop further dragging detection for this swipe
                }
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _isDragging = false;
    }

    private bool IsSwipeUp(Vector2 direction)
    {
        direction.Normalize();
        return direction.y > 0 && Mathf.Abs(direction.y) > Mathf.Abs(direction.x);
    }

    private void OnSwipeUp()
    {
        GameControlls.jumpIfPossible();
    }
}
