using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class ShopScroll : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{

    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private VerticalLayoutGroup _verticalLayoutGroup;
    [SerializeField] private float _lerpSpeed = 3;
    [SerializeField] private float _stopVelocityY = 200;
    [SerializeField] private float _minItemSize = 200;
    [SerializeField] private float _maxItemSize = 500;

    private bool _isInitialized;
    private bool _isDragging;
    private float _correctivePositionY;
    private RectTransform _content;
    private List<RectTransform> _items = new List<RectTransform>();

    private void Awake()
    {
        _content = _scrollRect.content;

        float center = -_scrollRect.viewport.transform.localPosition.y;
        _correctivePositionY = center - _maxItemSize;
        _verticalLayoutGroup.padding = new RectOffset((int)center, (int)center, _verticalLayoutGroup.padding.top, _verticalLayoutGroup.padding.bottom);
    }

    private void Update()
    {
        if (_isInitialized == false)
        { 
            return; 
        }

        int nearestIndex = 0;
        float nearestDistance = float.MaxValue;
        float center = _scrollRect.transform.position.y;

        for (int i = 0; i < _items.Count; i++)
        {
            float itemDistance = Mathf.Abs(center - _items[i].position.y);

            if (itemDistance < nearestDistance)
            {
                nearestDistance = itemDistance;
                nearestIndex = i;
            }

            float size = Mathf.Lerp(_maxItemSize, _minItemSize, itemDistance / center);
            _items[i].sizeDelta = CalculateSize(_items[i].sizeDelta, size);
        }

        if (_isDragging == false)
        {
            if (Mathf.Abs(_scrollRect.velocity.y) < _stopVelocityY)
            {
                ScrollTo(nearestIndex);
            }
        }
    }

    public void Initialize(List<ItemView> items)
    {
        if (_isInitialized)
            throw new InvalidOperationException("Already initialized");

        items.ForEach(item => _items.Add((RectTransform)item.transform));
        _isInitialized = true;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _isDragging = true;
        _scrollRect.inertia = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _isDragging = false;
    }

    private void ScrollTo(int index)
    {
        _scrollRect.inertia = false;

        RectTransform item = _items[index];
        float contentTargetPositionY = -1 * Mathf.Clamp(item.anchoredPosition.y - item.sizeDelta.y - _correctivePositionY, 0, _content.sizeDelta.y);
        Vector2 nextContentPosition = new Vector2(contentTargetPositionY, _content.anchoredPosition.y);

        _content.anchoredPosition = Vector2.Lerp(_content.anchoredPosition, nextContentPosition, _lerpSpeed * Time.deltaTime);
    }

    private Vector2 CalculateSize(Vector2 from, float to)
    {
        return Vector2.Lerp(from, Vector2.one * to, 0.5f);
    }
}
