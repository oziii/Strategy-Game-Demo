using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionMenuScroll : MonoBehaviour
{
    [Header("Menu Fields")] 
    
    private RectTransform _rectTransform;
    private RectTransform[] _rtChildren;

    public float itemSpacing;
    
    [SerializeField] private float horizontalMargin;
    [SerializeField] private float verticalMargin;
    
    
    [HideInInspector] public float width;
    [HideInInspector] public float height;

    [HideInInspector] public float childWidth;
    [HideInInspector] public float childHeight;

    public bool isVertical;
    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _rtChildren = new RectTransform[_rectTransform.childCount];

        for (int i = 0; i < _rectTransform.childCount; i++)
        {
            _rtChildren[i] = _rectTransform.GetChild(i) as RectTransform;
        }

        width = _rectTransform.rect.width - (2 * horizontalMargin);
        
        height = _rectTransform.rect.height - (2 * verticalMargin);
        
        childWidth = _rtChildren[0].rect.width;
        childHeight = _rtChildren[0].rect.height;
        
        InitializeMenu();
    }
    
    private void InitializeMenu()
    {
        float originY = 0 - (height * 0.5f);
        float posOffset = childHeight * 0.5f;
        for (int i = 0; i < _rtChildren.Length; i++)
        {
            Vector2 childPos = _rtChildren[i].localPosition;
            childPos.y = originY + posOffset + i * (childHeight + itemSpacing);
            _rtChildren[i].localPosition = childPos;
        }
    }
}
