using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InfiniteManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IScrollHandler
{
    
    [SerializeField] private ProductionMenuScroll scrollContent;

    [SerializeField] private float outOfBoundsThreshold;

    [Range(0, 3)]
    [SerializeField] private float renderSpacing;
    
    private ScrollRect _scrollRect;
    
    private Vector2 _lastDragPosition;

    private bool _dragDirection;
    private void Start()
    {
        _scrollRect = GetComponent<ScrollRect>();
        _scrollRect.vertical = scrollContent.isVertical;
        _scrollRect.horizontal = !scrollContent.isVertical;
        _scrollRect.movementType = ScrollRect.MovementType.Unrestricted;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _lastDragPosition = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _dragDirection = eventData.position.y > _lastDragPosition.y;

        _lastDragPosition = eventData.position;
    }

    public void OnScroll(PointerEventData eventData)
    {
        _dragDirection = eventData.scrollDelta.y < 0;
    }

    public void OnViewScroll()
    {
        int currItemIndex = _dragDirection ? _scrollRect.content.childCount - 1 : 0;
        var currItem = _scrollRect.content.GetChild(currItemIndex);
        
        if (!ReachedThreshold(currItem))
        {
            return;
        }
        
        int endItemIndex = _dragDirection ? 0 : _scrollRect.content.childCount - 1;
        Transform endItem = _scrollRect.content.GetChild(endItemIndex);
        Vector2 newPos = endItem.position;

        if (_dragDirection)
        {
            newPos.y = endItem.position.y - scrollContent.childHeight * renderSpacing + scrollContent.itemSpacing;
        }
        else
        {
            newPos.y = endItem.position.y + scrollContent.childHeight * renderSpacing - scrollContent.itemSpacing;
        }

        currItem.position = newPos;
        currItem.SetSiblingIndex(endItemIndex);
        
    }
    
    private bool ReachedThreshold(Transform item)
    {
        float posYThreshold = transform.position.y + scrollContent.height * 0.5f + outOfBoundsThreshold;
        float negYThreshold = transform.position.y - scrollContent.height * 0.5f - outOfBoundsThreshold;
        return _dragDirection ? item.position.y - scrollContent.childWidth * 0.5f > posYThreshold :
            item.position.y + scrollContent.childWidth * 0.5f < negYThreshold;

    }
}
