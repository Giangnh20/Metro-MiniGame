using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseInputArea : MonoBehaviour
{

    public Action<float> OnMouseWheelScroll;
    public Action OnMouseUpdate;

    private bool isMouseOver = false;
    private Vector3 dragOrigin;
    private RectTransform _rect;
    public RectTransform RectTransform => _rect;
    private bool IsPointerOverMe
    {
        get
        {
            Vector2 localMousePosition = _rect.InverseTransformPoint(Input.mousePosition);
            if (_rect.rect.Contains(localMousePosition))
            {
                return true;
            }
            return false;
        }
    }
    
    private void Awake()
    {
        _rect = this.GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (!IsPointerOverMe)
            return;
        
        OnMouseUpdate?.Invoke();

        // Scroll wheel
        var scrollValue = Input.mouseScrollDelta.y;
        if (Math.Abs(scrollValue) > 0.01f)
        {
            OnMouseWheelScroll?.Invoke(scrollValue);
        }
    }

}
